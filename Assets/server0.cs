using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;

public enum Status
{
    End,
    Connected,
    Working
}

public class Context
{
    public TcpClient client;
    public Status status;
    public Queue<string> packetdeque = new Queue<string>();
}

[System.Serializable]
public class Packet
{
    public string packet = "";
}

[System.Serializable]
public class PacketReady : Packet
{
    public PacketReady()
    {
        packet = "ready";
    }
}

[System.Serializable]
public class PacketHeader : Packet
{
    public string json_data;
}

[System.Serializable]
public class PacketCreate : Packet
{
    public string type;
    public string json_data;
}

[System.Serializable]
public class PacketCreateResponse : Packet
{
    public int id;

    public PacketCreateResponse(int id_)
    {
        packet = "create";
        id = id_;
    }
}

public class Server0
{
    static private int maxid = 0;
    static private Dictionary<int, device> devices = new Dictionary<int, device>();
    static private Mutex stopmut = new Mutex();
    static private bool stopped = false;
    static private TcpListener Listener = null;

    static private int create(PacketHeader packet)
    {
        PacketCreate create = UnityEngine.JsonUtility.FromJson<PacketCreate>(packet.json_data);

        switch (create.type)
        {
            case "manipulator1":
                return create_manipulator1(packet);

            case "manipulator2":
                return create_manipulator2(packet);

            default:
                return 0;
        }
    }

    static private int create_manipulator1(PacketHeader packet)
    {
        maxid++;
        device dev = new manipulator1();
        ((manipulator1)dev).config = UnityEngine.JsonUtility.FromJson<configmanipulator1>(packet.json_data);
        dev.Place();
        devices[maxid] = dev;
        return maxid;
    }

    static private int create_manipulator2(PacketHeader packet)
    {
        maxid++;
        device dev = new manipulator2();
        ((manipulator2)dev).config = UnityEngine.JsonUtility.FromJson<configmanipulator2>(packet.json_data);
        dev.Place();
        devices[maxid] = dev;
        return maxid;
    }

    static private void log(string s)
    {
        UnityEngine.MonoBehaviour.print(s);
    }

    static private PacketHeader receive_packet(Context context, bool blocking = true)
    {
        log("receive packet entered");//?эта функция с ошибкой - не работает

        string buf = "";

        try
        {
            byte[] bytes = new byte[1024];

            if (blocking)
            {
                while (!context.client.GetStream().DataAvailable)
                {
                    Thread.Sleep(50);
                }
            }

            while (true)
            {
                int r = context.client.GetStream().Read(bytes, 0, bytes.Length);

                if (r > 0)
                {
                    buf += Encoding.ASCII.GetString(bytes, 0, r);
                }
                else
                {
                    break;
                }
            }

            int pos = 0;

            while (true)
            {
                string packet = "";

                if (buf.Substring(0, 4) == "json")
                {
                    int pos1 = buf.IndexOf(":", pos);

                    if (pos1 != -1)
                    {
                        int size = Int32.Parse(buf.Substring(pos + 4, pos1 - pos - 4));
                        pos1 += 1;
                        pos = pos1 + size;

                        while (pos > buf.Length)
                        {
                            int r = context.client.GetStream().Read(bytes, 0, bytes.Length);

                            if (r > 0)
                            {
                                buf += buf += Encoding.ASCII.GetString(bytes, 0, r);
                            }
                            else
                            {
                                break;
                            }
                        }

                        packet = buf.Substring(pos1, size);
                    }
                }

                if (packet.Length == 0)
                {
                    break;
                }

                context.packetdeque.Enqueue(packet);
            }
        }
        catch
        {
        }

        if (context.packetdeque.Count == 0)
        {
            return null;
        }

        string json_data = context.packetdeque.Dequeue();

        PacketHeader data = UnityEngine.JsonUtility.FromJson<PacketHeader>(json_data);

        log("received " + json_data);

        data.json_data = json_data;

        return data;
    }

    static private bool send_packet(Context context, Packet packet)
    {
        string json_data = UnityEngine.JsonUtility.ToJson(packet);
        json_data = "json" + json_data.Length.ToString() + ":" + json_data;

        context.client.GetStream().Write(Encoding.ASCII.GetBytes(json_data), 0, json_data.Length);

        log("sent " + json_data);

        return true;
    }

    static private void setstopped(bool stopped_)
    {
        stopmut.WaitOne();
        stopped = stopped_;
        stopmut.ReleaseMutex();
    }

    static private bool isstopped()
    {
        stopmut.WaitOne();
        bool stopped_ = stopped;
        stopmut.ReleaseMutex();
        return stopped_;
    }

    static public void Start(int port = 8887)
    {
        //string[] args = System.Environment.GetCommandLineArgs();

        Thread Thread = new Thread(new ParameterizedThreadStart(ServerThread));
        Thread.Start(port);
    }

    static public void Stop()
    {
        setstopped(true);
        Listener.Stop();
    }

    static private void ServerThread(Object StateInfo)
    {
        int port = (int)StateInfo;

        Listener = new TcpListener(IPAddress.Any, port);
        Listener.Start();

        log("control server started");

        setstopped(false);

        while (!isstopped())
        {
            try
            {
                TcpClient Client = Listener.AcceptTcpClient();
                Thread Thread = new Thread(new ParameterizedThreadStart(ClientThread));
                Thread.Start(Client);
            }
            catch
            {
            }
        }

        log("control server stopped");
    }

    static private void ClientThread(Object StateInfo)
    {
        TcpClient client = (TcpClient)StateInfo;

        string ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

        log("control connected " + ip);

        Context context = new Context();

        context.client = client;
        context.status = Status.Connected;

        while (context.status != Status.End)
        {
            try
            {
                if (context.status == Status.Connected)
                {
                    send_packet(context, new PacketReady());
                    context.status = Status.Working;
                }

                if (context.status == Status.Working)
                {
                    PacketHeader packet = receive_packet(context);

                    if (packet == null)
                    {
                        break;
                    }
                    else if (packet.packet == "create")
                    {
                        //send_packet(context, new PacketCreateResponse(create(packet)));
                        //send_packet(context, new PacketCreateResponse(0));
                        send_packet(context, new PacketReady());
                        continue;

                    }
                    else if (packet.packet == "setpos")
                    {

                    }
                    else if (packet.packet == "camera")
                    {

                    }
                    else if (packet.packet == "end")
                    {
                        context.status = Status.End;
                        continue;
                    }
                    else
                    {
                        log("unexpected packet");
                    }
                    {
                        send_packet(context, new PacketReady());
                    }
                }
            }
            catch (Exception e)
            {
                log("control error: exception " + e.ToString());
            }
        }

        log("control disconnected " + ip);
    }
}
