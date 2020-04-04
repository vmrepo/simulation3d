﻿using System;
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
public class PacketHeader
{
    public string packet;
    public string json_data;
}

[System.Serializable]
public class Packet
{
    public string packet = "";

    public Packet(string packet_)
    {
        packet = packet_;
    }
}

public class Server0
{
    static private Mutex stopmut = new Mutex();
    static private bool stopped = false;
    static private TcpListener Listener = null;

    static private void log(string s)
    {
        UnityEngine.MonoBehaviour.print(s);
    }

    static private PacketHeader receive_packet(Context context, bool blocking = true)
    {
        string buf = "";

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

        log(("control connected " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString()));

        Context context = new Context();

        context.client = client;
        context.status = Status.Connected;

        while (context.status != Status.End)
        {
            try
            {
                if (context.status == Status.Connected)
                {
                    send_packet(context, new Packet("ready"));
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
                        send_packet(context, new Packet("ready"));
                    }
                }
            }
            catch (Exception e)
            {
                log("control error: exception " + e.ToString());
            }
        }

        log(("control disconnected " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString()));
    }
}
