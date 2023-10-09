using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using System;

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D confiner2D;
    public CinemachineImpulseSource impuseSource;
    public VoidEventSO cameraShakeEvent;

    private void Awake() {
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void OnEnable() {
        cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
    }

    private void OnDisable() {
        cameraShakeEvent.OnEventRaised -= OnCameraShakeEvent;
    }

    private void Start() {
        GetNewCameraBound();
    }

    private void OnCameraShakeEvent() {
        impuseSource.GenerateImpulse();
    }

    private void GetNewCameraBound() {
        var obj = GameObject.FindGameObjectWithTag("Bounds");
        if (obj != null) {
            confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
            confiner2D.InvalidateCache();
        }
    }
    
}
