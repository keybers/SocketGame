using SocketGameProtocol;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ClientManager : BaseManager
{
    private Socket socket;
    private Message message;
    private MainPack pack;
    /// <summary>
    /// 这样就可以调用和重写gameface里的方法
    /// </summary>
    /// <param name="gameFace"></param>
    public ClientManager(GameFace gameFace) : base(gameFace)
    {

    }
    /// <summary>
    /// 重写意味这可以调用basemanager里的东西
    /// </summary>
    public override void OnInit()
    {
        message = new Message();
        base.OnInit();
        InitSocket();

    }
    /// <summary>
    /// 初始化
    /// </summary>
    private void InitSocket()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            socket.Connect("127.0.0.1", 6666);
            //连接成功
            StartReceive();
            gameFace.ShowMessage("连接成功");
        }
        catch(Exception e)
        {
            //连接失败
            Debug.LogWarning(e);
            gameFace.ShowMessage("连接失败！");
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        message = null;
        CloseSocket();

    }

    /// <summary>
    /// 关闭socket
    /// </summary>
    private void CloseSocket()
    {
        if (socket.Connected)
        {
            socket.Close();
        }
    }

    private void StartReceive()
    {
        socket.BeginReceive(message.Buffer,message.StartIndex,message.Remsize,SocketFlags.None, ReceiveCallBack, null);
    }

    private void ReceiveCallBack(IAsyncResult asyncResult)
    {
        try
        {
            if (socket == null || socket.Connected == false) return;
            int len = socket.EndReceive(asyncResult);
            if(len == 0)
            {
                CloseSocket();
                return;
            }
            //消息传输是异步的
            message.ReadBuffer(len, HandleResponse);
            StartReceive();

        }
        //只能在主线程运行，不能在多线程运行
        catch(Exception e)
        {
            Debug.LogWarning(e);
        }
    }

    private void HandleResponse(MainPack pack)
    {
        gameFace.HandleResponse(pack);
        Debug.Log("Client处理");
    }

    public void send(MainPack pack)
    {
        socket.Send(Message.PackData(pack));
    }

}
