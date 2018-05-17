using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

public class GDV_Core {

    TcpClient socket;
    NetworkStream stream;
    StreamReader reader;
    StreamWriter writer;
    bool errorOccured = false;

    private bool debugMode;

    string ip;
    int port;

    public static GDV_Core DEFAULT = new GDV_Core();

    public GDV_Core(string ip = "127.0.0.1", int port = 12345, bool debugMode = false, bool useDefaultConfig = true)
    {
        this.ip = ip;
        this.port = port;
        this.debugMode = debugMode;

        if(useDefaultConfig)
        {
            graphConfig(new GDV_Core.DefaultGraphConfig("X", "Y", "Graph", "r-"));
        }
    }

    private void connect()
    {
        try
        {
            socket = new TcpClient(ip, 12345);
            stream = socket.GetStream();
            reader = new StreamReader(stream);

            writer = new StreamWriter(stream);

            if(debugMode)
                Debug.Log("connected successfully");
        
        }
        catch (Exception e)
        {
            errorOccuredFunc(e);
        }
    }

    private void errorOccuredFunc(Exception e)
    {
        Debug.LogError("Error in network shutting down GDV: " + e);
        errorOccured = true;
    }

    private void disconnect()
    {
        socket.Close();
    }

    private string recvData()
    {
        if (errorOccured)
            return null;

        connect();

        string data = reader.ReadLine();
        if (data != null)
            //Debug.Log("Incoming data: " + data);

        disconnect();

        return data;
    }

    private void sendData(string write)
    {
        if (errorOccured)
            return;

        connect();

        if (debugMode)
            Debug.Log("writing: " + write);
        //byte[] bytes = Encoding.UTF8.GetBytes(write);
        writer.Write(write);
        writer.Flush();

        disconnect();
        
    }

    public void graphConfig(DefaultGraphConfig names)
    {
        if (debugMode)
            Debug.Log(JsonUtility.ToJson(names));
        sendData(JsonUtility.ToJson(names));
    }

    List<float> values = new List<float>();

    public void addValues(params float[] values)
    {
        this.values.AddRange(values);
    }

    public void send()
    {
        sendData(JsonUtility.ToJson(new Data(values.ToArray())));
        values = new List<float>();
    }

    public void addAndSend(params float[] values)
    {
        addValues(values);
        send();
    }

    [Serializable]
    public class Data
    {
        public float[] datas;

        public Data(float[] datas)
        {
            this.datas = datas;
        }
    }
    

    [Serializable]
    public class DefaultGraphConfig
    {
        public string graphType = "default";
        public string xLabel;
        public string yLabel;
        public string title;
        public string[] graphTypeString;

        public DefaultGraphConfig(string xLabel, string yLabel, string title, params string[] graphTypeString)
        {
            this.xLabel = xLabel;
            this.yLabel = yLabel;
            this.title = title;
            this.graphTypeString = graphTypeString;
        }


    }

}
