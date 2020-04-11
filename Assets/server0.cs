using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using System.Diagnostics;

public enum Status
{
    End,
    Connected,
    Working
}

public class Context
{
    public TcpClient client;
    public string buffer;
    public Status status;
    public Queue<Packet> packetdeque = new Queue<Packet>();
    public Dictionary<int, Packet> packetdict = new Dictionary<int, Packet>();
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
}

[System.Serializable]
public class PacketDelete : Packet
{
    public int id;
}

[System.Serializable]
public class PacketClear : Packet
{
}

[System.Serializable]
public class PacketSetpos : Packet
{
    public int id;
    public float a0;
    public float a1;
    public float a2;
}

[System.Serializable]
public class PacketSetcamera : Packet
{
    public float x0;
    public float y0;
    public float z0;
    public float x1;
    public float y1;
    public float z1;
}

[System.Serializable]
public class PacketCreateReady : Packet
{
    public int id;

    public PacketCreateReady(int id_)
    {
        packet = "ready";
        id = id_;
    }
}

[System.Serializable]
public class PacketDeleteReady : Packet
{
    public int ok;

    public PacketDeleteReady(int ok_)
    {
        packet = "ready";
        ok = ok_;
    }
}

[System.Serializable]
public class PacketClearReady : Packet
{
    public int ok;

    public PacketClearReady(int ok_)
    {
        packet = "ready";
        ok = ok_;
    }
}

[System.Serializable]
public class PacketSetcameraReady : Packet
{
    public int ok;

    public PacketSetcameraReady(int ok_)
    {
        packet = "ready";
        ok = ok_;
    }
}

public enum Calltype
{
    None,
    Create,
    Delete,
    Clear,
    Setpos,
    Setcamera
}

public class Calldata
{
    public Mutex mutex = new Mutex();
    public ManualResetEvent manualevent = new ManualResetEvent(false);
    public Calltype type = Calltype.None;
    public Packet inputpacket = null;
    public Packet outputpacket = null;
}

public class Server0
{
    static public int Port = 8888;
    static public string Logfile = "./simulation3dlog.txt";

    static private Calldata calldata = new Calldata();

    //доступ только из потока событий unity
    static private int maxid = 0;
    static private Dictionary<int, device> devices = new Dictionary<int, device>();

    static private Mutex stoppedmutex = new Mutex();
    static private bool stopped = false;
    static private TcpListener Listener = null;

    static private void async(Calltype type, Packet inputpacket)
    {
        calldata.mutex.WaitOne();
        calldata.type = type;
        calldata.inputpacket = inputpacket;
        calldata.outputpacket = null;
        calldata.mutex.ReleaseMutex();
    }

    static private Packet call(Calltype type, Packet inputpacket)
    {
        calldata.mutex.WaitOne();
        calldata.type = type;
        calldata.inputpacket = inputpacket;
        calldata.outputpacket = null;
        calldata.manualevent.Reset();
        calldata.mutex.ReleaseMutex();

        calldata.manualevent.WaitOne();

        calldata.mutex.WaitOne();
        calldata.type = Calltype.None;
        calldata.inputpacket = null;
        Packet outputpacket = calldata.outputpacket;
        calldata.mutex.ReleaseMutex();

        return outputpacket;
    }

    // вызывается из потока собыйти unity
    static private PacketCreateReady create(PacketHeader packet)
    {
        PacketCreate create = UnityEngine.JsonUtility.FromJson<PacketCreate>(packet.json_data);

        switch (create.type)
        {
            case "manipulator1":
                return new PacketCreateReady(create_manipulator1(packet));

            case "manipulator2":
                return new PacketCreateReady(create_manipulator2(packet));

            default:
                return new PacketCreateReady(0);
        }
    }

    // вызывается из потока собыйти unity
    static private PacketDeleteReady delete(PacketHeader packet)
    {
        PacketDelete delete = UnityEngine.JsonUtility.FromJson<PacketDelete>(packet.json_data);

        if (!devices.ContainsKey(delete.id))
            return new PacketDeleteReady(0);

        devices[delete.id].Remove();
        devices.Remove(delete.id);

        return new PacketDeleteReady(1);
    }

    // вызывается из потока собыйти unity
    static private PacketClearReady clear(PacketHeader packet)
    {
        PacketClear delete = UnityEngine.JsonUtility.FromJson<PacketClear>(packet.json_data);

        foreach (KeyValuePair<int, device> pair in devices) 
        {
            pair.Value.Remove();
        }

        devices.Clear();

        return new PacketClearReady(1);
    }

    // вызывается из потока собыйти unity
    static private void setpos(PacketSetpos setpos)
    {
        if (!devices.ContainsKey(setpos.id))
            return;

        switch (devices[setpos.id].GetType().ToString())
        {
            case "manipulator1":
                {
                    manipulator1 manipulator = (manipulator1)devices[setpos.id];
                    manipulator.SetPos0(setpos.a0);
                    manipulator.SetPos1(setpos.a1);
                    manipulator.SetPos2(setpos.a2);
                }
                break;

            case "manipulator2":
                {
                    manipulator2 manipulator = (manipulator2)devices[setpos.id];
                    manipulator.SetPos0(setpos.a0);
                    manipulator.SetPos1(setpos.a1);
                    manipulator.SetPos2(setpos.a2);
                }
                break;

            default:
                return;
        }

        return;
    }

    // вызывается из потока собыйти unity
    static private PacketSetcameraReady setcamera(PacketHeader packet)
    {
        PacketSetcamera setcamera = UnityEngine.JsonUtility.FromJson<PacketSetcamera>(packet.json_data);

        UnityEngine.GameObject obj = UnityEngine.GameObject.Find("Main Camera");
        camera bhv = obj.GetComponent<camera>();

        bhv.targetposition = new UnityEngine.Vector3(setcamera.x0, setcamera.y0, setcamera.z0);
        obj.transform.position = new UnityEngine.Vector3(setcamera.x1, setcamera.y1, setcamera.z1);

        bhv.Init();

        return new PacketSetcameraReady(1);
    }

    // в потоке клиента нельзя вызывать, только из потока событий unity
    static private int create_manipulator1(PacketHeader packet)
    {
        maxid++;
        device dev = new manipulator1();
        ((manipulator1)dev).config = UnityEngine.JsonUtility.FromJson<configmanipulator1>(packet.json_data);
        dev.Place();
        devices[maxid] = dev;
        return maxid;
    }

    // в потоке клиента нельзя вызывать, только из потока событий unity
    static private int create_manipulator2(PacketHeader packet)
    {
        maxid++;
        device dev = new manipulator2();
        ((manipulator2)dev).config = UnityEngine.JsonUtility.FromJson<configmanipulator2>(packet.json_data);
        dev.Place();
        devices[maxid] = dev;
        return maxid;
    }

    static public void Log(string text)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(Logfile, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(DateTime.Now.ToString() + " " + text);
            }
        }
        catch (Exception e)
        {
            UnityEngine.MonoBehaviour.print("error write log to " + Logfile + " " + e.ToString());
        }
    }

    static private Packet receive_packet(Context context, bool blocking = true)
    {
        var start = Process.GetCurrentProcess().TotalProcessorTime;

        try
        {
            byte[] bytes = new byte[1024];

            if (context.buffer.Length == 0)
            {
                if (blocking || context.client.GetStream().DataAvailable)
                {
                    while (true)
                    {
                        int r = context.client.GetStream().Read(bytes, 0, bytes.Length);

                        if (r > 0)
                        {
                            context.buffer += Encoding.ASCII.GetString(bytes, 0, r);
                        }

                        if (!context.client.GetStream().DataAvailable)
                        {
                            break;
                        }
                    }
                }
            }

            while (true)
            {
                int pos = 0;

                string packet = "";

                if (context.buffer.Length > 0 && context.buffer.Substring(pos, 4) == "json")
                {
                    int pos1 = context.buffer.IndexOf(":", pos);

                    if (pos1 != -1)
                    {
                        int size = Int32.Parse(context.buffer.Substring(pos + 4, pos1 - pos - 4));
                        pos1 += 1;
                        pos = pos1 + size;

                        while (pos > context.buffer.Length)
                        {
                            if (!context.client.GetStream().DataAvailable)
                            {
                                break;
                            }

                            int r = context.client.GetStream().Read(bytes, 0, bytes.Length);

                            if (r > 0)
                            {
                                context.buffer += Encoding.ASCII.GetString(bytes, 0, r);
                            }
                        }

                        packet = context.buffer.Substring(pos1, size);
                        context.buffer = context.buffer.Substring(pos);
                    }
                }

                if (packet.Length == 0)
                {
                    break;
                }

                PacketHeader header = UnityEngine.JsonUtility.FromJson<PacketHeader>(packet);
                header.json_data = packet;

                if (header.packet == "setpos")
                {
                    PacketSetpos setpos = UnityEngine.JsonUtility.FromJson<PacketSetpos>(header.json_data);
                    context.packetdict[setpos.id] = setpos;
                }
                else
                {
                    context.packetdeque.Enqueue(header);
                }

                var check = Process.GetCurrentProcess().TotalProcessorTime;

                if ((check - start).Milliseconds >= 20)
                {
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Log(e.ToString());
        }

        if (context.packetdeque.Count == 0)
        {
            if (context.packetdict.Count == 0)
            {
                return null;
            }

            Random rand = new Random();
            int r = rand.Next(context.packetdict.Count);
            int i = 0;
            foreach (var item in context.packetdict)
            {
                if (i == r)
                {
                    context.packetdict.Remove(item.Key);
                    return item.Value;
                }
                i++;
            }
        }

        Log("received " + ((PacketHeader)context.packetdeque.Peek()).json_data);

        return context.packetdeque.Dequeue();
    }

    static private bool send_packet(Context context, Packet packet)
    {
        string json_data = UnityEngine.JsonUtility.ToJson(packet);

        Log("sent " + json_data);

        json_data = "json" + json_data.Length.ToString() + ":" + json_data;

        context.client.GetStream().Write(Encoding.ASCII.GetBytes(json_data), 0, json_data.Length);

        return true;
    }

    static private void setstopped(bool stopped_)
    {
        stoppedmutex.WaitOne();
        stopped = stopped_;
        stoppedmutex.ReleaseMutex();
    }

    static private bool isstopped()
    {
        stoppedmutex.WaitOne();
        bool stopped_ = stopped;
        stoppedmutex.ReleaseMutex();
        return stopped_;
    }

    static public void Start()
    {
        Thread Thread = new Thread(new ParameterizedThreadStart(ServerThread));
        Thread.Start(Port);
    }

    static public void Update()
    {
        calldata.mutex.WaitOne();

        switch (calldata.type)
        {
            case Calltype.Create:
                calldata.outputpacket = create((PacketHeader)calldata.inputpacket);
                calldata.manualevent.Set();
                break;

            case Calltype.Delete:
                calldata.outputpacket = delete((PacketHeader)calldata.inputpacket);
                calldata.manualevent.Set();
                break;

            case Calltype.Clear:
                calldata.outputpacket = clear((PacketHeader)calldata.inputpacket);
                calldata.manualevent.Set();
                break;

            case Calltype.Setpos:
                setpos((PacketSetpos)calldata.inputpacket);//async
                break;

            case Calltype.Setcamera:
                calldata.outputpacket = setcamera((PacketHeader)calldata.inputpacket);
                calldata.manualevent.Set();
                break;

            default:
                calldata.manualevent.Set();
                break;
        }

        calldata.mutex.ReleaseMutex();
    }

    static public void Stop()
    {
        setstopped(true);
        Listener.Stop();
    }

    static private void ServerThread(Object StateInfo)
    {
        int port = (int)StateInfo;

        try
        {
            Listener = new TcpListener(IPAddress.Any, port);
            Listener.Start();
        }
        catch
        {
            Log("error start server, port " + Port.ToString());
            return;
        }

        Log("control server started, port " + Port.ToString());

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

        Log("control server stopped");
    }

    static private void ClientThread(Object StateInfo)
    {
        TcpClient client = (TcpClient)StateInfo;

        string ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

        Log("control client connected " + ip);

        Context context = new Context();

        context.client = client;
        context.status = Status.Connected;
        context.buffer = "";

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
                    Packet packet = receive_packet(context);

                    if (packet == null)
                    {
                        break;
                    }
                    else if (packet.packet == "create")
                    {
                        send_packet(context, call(Calltype.Create, packet));
                        continue;

                    }
                    else if (packet.packet == "delete")
                    {
                        send_packet(context, call(Calltype.Delete, packet));
                        continue;

                    }
                    else if (packet.packet == "clear")
                    {
                        send_packet(context, call(Calltype.Clear, packet));
                        continue;

                    }
                    else if (packet.packet == "setpos")
                    {
                        async(Calltype.Setpos, packet);
                        continue;
                    }
                    else if (packet.packet == "setcamera")
                    {
                        send_packet(context, call(Calltype.Setcamera, packet));
                        continue;
                    }
                    else if (packet.packet == "end")
                    {
                        context.status = Status.End;
                        continue;
                    }
                    else
                    {
                        Log("unexpected packet");
                    }
                    {
                        send_packet(context, new PacketReady());
                    }
                }
            }
            catch (Exception e)
            {
                Log("control client error: exception " + e.ToString());
                break;
            }
        }

        Log("control client disconnected " + ip);
    }
}