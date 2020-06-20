using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestManager : BaseManager
{
    public RequestManager(GameFace gameFace) : base(gameFace) { }

    private Dictionary<ActionCode, BaseRequest> requestDict = new Dictionary<ActionCode, BaseRequest>();

    public void AddRequest(BaseRequest baseRequest)
    {
        requestDict.Add(baseRequest.GetActionCode, baseRequest);
    }

    public void RemoveRequest(ActionCode actionCode)
    {
        requestDict.Remove(actionCode);
    }

    public void HandleResponse(MainPack pack)
    {
        if(requestDict.TryGetValue(pack.Actioncode,out BaseRequest request))
        {
            request.OnResponse(pack);
            Debug.Log("manager处理");
        }
        else
        {
            Debug.LogWarning("不能找到对应的处理");
        }
    }
}
