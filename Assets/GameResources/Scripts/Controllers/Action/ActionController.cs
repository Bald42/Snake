using static Enums;
using UnityEngine;
using System;

public class ActionController
{
    public Action<DirectionMove> OnChangeDirectionMoveEvent = null;

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