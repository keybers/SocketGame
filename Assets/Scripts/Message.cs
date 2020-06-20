using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using SocketGameProtocol;
using Google.Protobuf;

public class Message
{
    private byte[] buffer = new byte[1024];
    private int startIndex;

    public byte[] Buffer//消息整体
    {
        get
        {
            return buffer;
        }
    }

    public int StartIndex//消息包头
    {
        get
        {
            return startIndex;
        }
    }

    public int Remsize//消息体
    {
        get
        {
            return buffer.Length - startIndex;
        }
    }

    public void ReadBuffer(int len, Action<MainPack> HandleRequest)
    {
        startIndex += len;
        if (startIndex <= 4)//从包头长度以内开始，int类型占四个字节
        {
            return;
        }
        else
        {
            int count = BitConverter.ToInt32(buffer, 0);//包体长度
            while (true)
            {
                if (startIndex >= count + 4)//如果到了包体最后则打包,当前startIndex在最后
                {
                    //解析包体长度
                    MainPack pack = (MainPack)MainPack.Descriptor.Parser.ParseFrom(buffer, 4, count);
                    HandleRequest(pack);
                    Array.Copy(buffer, count + 4, buffer, 0, startIndex - count - 4);
                    startIndex -= (count + 4);
                }
                else
                {
                    break;
                }
            }
        }
    }

    public static byte[] PackData(MainPack pack)
    {
        byte[] data = pack.ToByteArray();//包体
        byte[] head = BitConverter.GetBytes(data.Length);//包头
        return head.Concat(data).ToArray();//返回包头后接包体
    }
}

