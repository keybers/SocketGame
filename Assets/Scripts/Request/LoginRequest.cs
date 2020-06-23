using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginRequest : BaseRequest
{
    public LoginPanel loginPanel;

    private MainPack mainPack = null;

    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Logon;
        base.Awake();
    }

    private void Update()
    {
        if(mainPack != null)
        {
            loginPanel.OnResponse(mainPack);
            mainPack = null;
        }
    }

    public override void OnResponse(MainPack pack)
    {
        this.mainPack = pack;
    }

    public void SendResquest(string user, string pass)
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
