using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogonPanel : BasePanel
{
    public LogonRequest logonRequest;
    public InputField user, pass;
    public Button logonButton,switchButton;

    private void Start()
    {
        logonButton.onClick.AddListener(OnLogonClick);
        switchButton.onClick.AddListener(SwitchLogin);
    }

    private void SwitchLogin()
    {
        uIManager.PopPanel();
    }

    private void OnLogonClick()
    {
        if(user.text == null|| pass.text == null)
        {
            Debug.LogWarning("用户名或密码不能为空");
            return;
        }
        logonRequest.SendResquest(user.text,pass.text);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Enter();
    }

    public override void OnExit()
    {
        base.OnExit();
        Exit();
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnRecovery()
    {
        base.OnRecovery();
    }

    private void Enter()
    {
        gameObject.SetActive(true);
    }

    private void Exit()
    {
        gameObject.SetActive(false);
    }

    public void OnResponse(MainPack mainpack)
    {
        switch (mainpack.Returncode)
        {
            case ReturnCode.Succeed:
                uIManager.ShowMessage("注册成功");
                uIManager.PushPanel(PanelType.LOGIN);
                break;

            case ReturnCode.Fail:
                uIManager.ShowMessage("注册失败");
                break;

            default:
                Debug.LogError("def");
                break;
        }
    }
}
