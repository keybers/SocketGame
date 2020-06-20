using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRequest : MonoBehaviour
{
    protected RequestCode requestCode;
    protected ActionCode actionCode;
    protected GameFace face;

    public ActionCode GetActionCode
    {
        get
        {
            return actionCode;
        }
    }

    public virtual void Awake()
    {
        face = GameFace.Face;
    }

    public virtual void Start()
    {
        face.AddRequest(this);
        Debug.Log("添加" + actionCode.ToString());
    }

    public virtual void OnDestroy()
    {
        face.RemoveRequest(actionCode);
    }

    /// <summary>
    /// 接收信息都是不同的
    /// </summary>
    /// <param name="pack"></param>
    public virtual void OnResponse(MainPack pack)
    {

    }
    
    /// <summary>
    /// 发送是统一的
    /// </summary>
    /// <param name="pack"></param>
    public virtual void SendResquest(MainPack pack)
    {
        face.Send(pack);
    }
}
