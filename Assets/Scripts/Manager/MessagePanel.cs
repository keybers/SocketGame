using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    public Text text;
    string message = null;

    public override void OnEnter()
    {
        base.OnEnter();
        text.CrossFadeAlpha(0, 0.1f, false);
        //uimanage才能管理初始化messagepanel
        uIManager.SetMessagePanel(this);
    }

    private void Update()
    {
        if(message != null)
        {
            ShowText(message);
            message = null;
        }
    }

    public void ShowMessage(string str,bool issync = false)
    {
        if (issync)
        {
            //本身就是异步传递信息显示
            message = str;
        }
        else
        {
            //主线程控制
            ShowText(str);
        }
    }

    private void ShowText(string message)
    {
        text.text = message;
        text.CrossFadeAlpha(1, 1f, false);
        Invoke("HideText", 1);
    }

    private void HideText()
    {
        text.CrossFadeAlpha(0, 1f, false);
    }
}
