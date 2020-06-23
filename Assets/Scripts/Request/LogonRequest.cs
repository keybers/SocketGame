﻿using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogonRequest : BaseRequest
{
    public LogonPanel logonPanel;

    private MainPack mainPack;

    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Logon;
        base.Awake();
    }
    /// <summary>
    /// 回到主线程
    /// </summary>
    private void Update()
    {
        if(mainPack != null)
        {
            logonPanel.OnResponse(mainPack);
            mainPack = null;
        }
    }

    public override void OnResponse(MainPack pack)
    {
        this.mainPack = pack;
    }

    //LoginPack
    public void SendResquest(string user,string pass)
    {
        MainPack pack = new MainPack();
        pack.Requestcode = requestCode;
        pack.Actioncode = actionCode;
        LoginPack loginPack = new LoginPack();
        loginPack.Username = user;
        loginPack.Password = pass;
        pack.Loginpack = loginPack;
        base.SendResquest(pack);
    }
}
