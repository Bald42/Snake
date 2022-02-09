using static Enums;
using UnityEngine;
using System;

public class ActionController
{
    public Action<DirectionMove> OnChangeDirectionMoveEvent = null;
    public Action OnStartHoldMoveEvent = null;
    public Action OnStopHoldMoveEvent = null;

    private float currentTimeHold = 0f;
    private float maxTimeHold = 0.5f;

    private bool isHold = false;

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

    protected void CheckStartHold()
    {
        currentTimeHold += Time.deltaTime;
        if (!isHold && currentTimeHold >= maxTimeHold)
        {
            isHold = true;
            OnStartHoldMoveEvent?.Invoke();
        }
    }

    protected void CheckStopHold()
    {
        currentTimeHold = 0f;
        if (isHold)
        {
            isHold = false;
            OnStopHoldMoveEvent?.Invoke();
        }
    }
}