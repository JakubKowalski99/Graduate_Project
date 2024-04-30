using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class SignalCatcher : MonoBehaviour
{
    public Signal signal;
    public UnityEvent signalEvent;

    public void SignalCatch()
    {
        signalEvent.Invoke();
    }

    private void OnEnable()
    {
        signal.AddCathers(this);
    }

    private void OnDisable()
    {
        signal.RemoveCatcher(this);
    }
}