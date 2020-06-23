using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class UIManager : BaseManager
{
    public UIManager(GameFace gameFace) : base(gameFace) { }

    private Dictionary<PanelType, BasePanel> panelDict = new Dictionary<PanelType, BasePanel>();
    private Dictionary<PanelType, string> panelPath = new Dictionary<PanelType, string>();

    private Stack<BasePanel> stackPanels = new Stack<BasePanel>();

    private Transform canvasTransform;
    private MessagePanel messagePanel;
    public override void OnInit()
    {
        base.OnInit();
        InitPanel();
        canvasTransform = GameObject.Find("Canvas").transform;
        PushPanel(PanelType.START);
    }
    /// <summary>
    /// 把UI显示到界面上
    /// </summary>
    /// <param name="panelType"></param>
    public void PushPanel(PanelType panelType)
    {
        if (panelDict.TryGetValue(panelType, out BasePanel panel))
        {
            if (stackPanels.Count > 0)
            {
                //确认顶部UI
                BasePanel topPanel = stackPanels.Peek();
                //暂停顶部UI
                topPanel.OnPause();
                //因为已经存在字典里,所以运行选定UI
            }
            stackPanels.Push(panel);
            panel.OnEnter();
        }
        else
        {
            BasePanel _Panel = SpawnPanel(panelType);
            if (stackPanels.Count > 0)
            {
                BasePanel topPanel = stackPanels.Peek();
                topPanel.OnPause();
            }
            stackPanels.Push(_Panel);
            _Panel.OnEnter();
        }
    }
    /// <summary>
    /// 关闭当前UI
    /// </summary>
    public void PopPanel()
    {
        if (panelDict.Count == 0) return;
        //移除顶层UI
        BasePanel topPanel = stackPanels.Pop();
        topPanel.OnExit();

        BasePanel panel = stackPanels.Peek();
        panel.OnRecovery();
        //最后一定有startUI
    }

    /// <summary>
    /// 实例化对应的UI
    /// </summary>
    /// <param name="panelType"></param>
    private BasePanel SpawnPanel(PanelType panelType)
    {
        if(panelPath.TryGetValue(panelType, out string path))
        {
            GameObject panelObject = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            panelObject.transform.SetParent(canvasTransform, false);
            BasePanel panel = panelObject.GetComponent<BasePanel>();
            panel.uIManager = this;//
            panelDict.Add(panelType, panel);
            return panel;
        }
        else
        {
            Debug.LogError("生成Panel失败");
            return null;
        }
    }

    /// <summary>
    /// 初始化UI
    /// </summary>
    private void InitPanel()
    {
        string panelpath = "Panel/";
        string[] path = new string[] { "MessagePanel", "StartPanel", "LoginPanel", "LogonPanel" };
        panelPath.Add(PanelType.MESSAGE, panelpath + path[0]);
        panelPath.Add(PanelType.START, panelpath + path[1]);
        panelPath.Add(PanelType.LOGIN, panelpath + path[2]);
        panelPath.Add(PanelType.LOGON, panelpath + path[3]);
    }

    public void SetMessagePanel(MessagePanel message)
    {
        messagePanel = message;
    }

    public void ShowMessage(string message,bool synce = false)
    {
        messagePanel.ShowMessage(message,synce);
    }
}
