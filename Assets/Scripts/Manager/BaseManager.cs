using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager
{
    protected GameFace gameFace;

    public BaseManager(GameFace gameFace)
    {
        this.gameFace = gameFace;
    }

    public virtual void OnInit()
    {

    }

    public virtual void OnDestroy()
    {

    }
}
