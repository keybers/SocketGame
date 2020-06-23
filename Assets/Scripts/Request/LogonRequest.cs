using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogonRequest : BaseRequest
{
    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Logon;
        base.Awake();
    }

    public override void OnResponse(MainPack pack)
    {
        switch (pack.Returncode)
        {
            case ReturnCode.Succeed:
                face.ShowMessage("注册成功", true);
                break;

            case ReturnCode.Fail:
                face.ShowMessage("注册失败", true);
                break;

            default:Debug.LogError("def");
                break;
        }
    }

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
