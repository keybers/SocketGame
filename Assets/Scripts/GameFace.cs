using SocketGameProtocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFace : MonoBehaviour
{
    private ClientManager clientManager;
    private RequestManager requestManager;
    private UIManager uIManager;

    private static GameFace face;

    public static GameFace Face
    {
        get
        {
            if (face == null)
            {
                face = GameObject.Find("GameFace").GetComponent<GameFace>();
            }
            return face;
        }
    }

    void Awake()
    {
        uIManager = new UIManager(this);
        clientManager = new ClientManager(this);
        requestManager = new RequestManager(this);

        uIManager.OnInit();
        clientManager.OnInit();
        requestManager.OnInit();
    }

    private void OnDestroy()
    {
        clientManager.OnDestroy();
        requestManager.OnDestroy();
        uIManager.OnDestroy();
    }

    public void Send(MainPack pack)
    {
        clientManager.send(pack);
    }

    public void HandleResponse(MainPack pack)
    {
        //处理
        requestManager.HandleResponse(pack);
        Debug.Log("GameFace处理");
    }

    public void AddRequest(BaseRequest baseRequest)
    {
        //Debug.LogError("add了一次");
        requestManager.AddRequest(baseRequest);
    }

    public void RemoveRequest(ActionCode actionCode)
    {
        requestManager.RemoveRequest(actionCode);
    }

    public void ShowMessage(string message,bool sync =false)
    {
        uIManager.ShowMessage(message,sync);
    }

}
