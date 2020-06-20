using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    public UIManager uIManager;
    public UIManager setUIManager
    {
        set
        {
            setUIManager = value;
        }
    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnPause()
    {

    }

    public virtual void OnRecovery()
    {

    }

    public virtual void OnExit()
    {

    }
}
