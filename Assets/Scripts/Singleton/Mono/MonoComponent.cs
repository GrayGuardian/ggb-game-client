using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MonoComponent : MonoBehaviour
{
    private event Action<string> _onListenEvent = null;
    public void AddListenEvent(Action<string> e)
    {
        _onListenEvent += e;
        Debug.Log("AddListenEvent");
    }
    public void DelListenEvent(Action<string> e)
    {
        _onListenEvent -= e;
        Debug.Log("DelListenEvent");
    }
    protected void Awake()
    {
        if (_onListenEvent != null) _onListenEvent("Awake");
    }
    protected void Start()
    {
        if (_onListenEvent != null) _onListenEvent("Start");
    }
    protected void OnEnable()
    {
        if (_onListenEvent != null) _onListenEvent("OnEnable");
    }
    protected void OnDisable()
    {
        if (_onListenEvent != null) _onListenEvent("OnDisable");
    }
    protected void OnDestroy()
    {
        if (_onListenEvent != null) _onListenEvent("OnDestroy");
    }
    protected void Update()
    {
        if (_onListenEvent != null) _onListenEvent("Update");
    }
    protected void FixedUpdate()
    {
        if (_onListenEvent != null) _onListenEvent("FixedUpdate");
    }
}
