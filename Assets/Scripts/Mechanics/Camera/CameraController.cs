using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
public class CameraController 
{
    private CameraView cameraMoveable;
    private EventManager eventManager;

    [Inject]
    public CameraController(EventManager eventManager)
    {
        this.eventManager = eventManager;
        cameraMoveable = Camera.main.GetComponent<CameraView>();
        initActions();
    }

    private void initActions()
    {
        eventManager.OnHookToMainStayEvent.AddListener(cameraMoveable.Move);
    }
}
