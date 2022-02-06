using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ActionController
{
    public void Init()
    {
        Subscribe();
    }

    #region Subscribe 

    private void Subscribe()
    {
        MainController.Instance.OnDestroyEvent += OnDestroyHandler;
        MainController.Instance.OnUpdateEvent += OnUpdateHandler;
    }

    private void Unsubscribe()
    {
        MainController.Instance.OnDestroyEvent -= OnDestroyHandler;
        MainController.Instance.OnUpdateEvent -= OnUpdateHandler;
    }

    private void OnDestroyHandler()
    {
        Unsubscribe();
    }

    private void OnUpdateHandler()
    {
        OnUpdate();
    }

    #endregion

    protected virtual void OnUpdate()
    {
        OnUpdate();
    }
}