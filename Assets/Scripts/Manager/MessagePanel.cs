using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    public Text text;

    public override void OnEnter()
    {
        base.OnEnter();
        text.CrossFadeAlpha(0, 0.1f, false);
    }

    public void ShowMessage(string message)
    {
        text.text = message;
        text.CrossFadeAlpha(1, 1f, false);
        Invoke("HideText",1);
    }

    private void HideText()
    {
        text.CrossFadeAlpha(0, 1f, false);
    }
}
