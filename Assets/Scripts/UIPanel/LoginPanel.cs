using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    public LogonRequest loginRequest;
    public InputField user, pass;
    public Button loginButton, switchButton;

    private void Start()
    {
        loginButton.onClick.AddListener(OnLoginClick);
        switchButton.onClick.AddListener(SwitchLogon);
    }

    private void OnLoginClick()
    {
        if (user.text == null || pass.text == null)
        {
            Debug.LogWarning("用户名或密码不能为空");
            return;
        }
        loginRequest.SendResquest(user.text, pass.text);
    }

    private void SwitchLogon()
    {
        uIManager.PushPanel(PanelType.LOGON);
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

    public void OnResponse(MainPack mainPack)
    {
        switch (mainPack.Returncode)
        {
            case ReturnCode.Succeed:
                uIManager.ShowMessage("登录成功");
                uIManager.PushPanel(PanelType.ROOMLIST);
                break;

            case ReturnCode.Fail:
                uIManager.ShowMessage("登录失败");
                break;

            default:
                Debug.Log("def");
                break;
        }
    }
}
