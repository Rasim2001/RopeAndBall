using DG.Tweening;
using Graf.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class WeightController
{
    public UnityEvent<Transform> OnConnectStart = new UnityEvent<Transform>();
    public UnityEvent<Transform> OnConnectEnd = new UnityEvent<Transform>();

    private ResourceLoaderService<GameObject> resourceLoaderService;
    private Sprite ropeSprite;
    private EventManager eventManager;
    private WeightView weightView;
    private WeightTrigger weightTrigger;
    private WeightCollision weightCollision;
    private PaletteManager paletteManager;

    [Inject]
    public WeightController(ResourceLoaderService<GameObject> resourceLoaderService,
                            EventManager eventManager,
                            [Inject(Id = "RopeSprite")] Sprite ropeSprite,
                            PaletteManager paletteManager
                            )
    {
        this.eventManager = eventManager;
        this.resourceLoaderService = resourceLoaderService;
        this.ropeSprite = ropeSprite;
        this.paletteManager = paletteManager;
    }

    public void GameBegin()
    {
        weightView = GameObject.Instantiate(resourceLoaderService.Prefabs.WEIGHT_VIEW).GetComponent<WeightView>();
        weightTrigger = weightView.GetComponent<WeightTrigger>();
        weightCollision = weightView.GetComponent<WeightCollision>();
        weightView.Init(ropeSprite, paletteManager.CurrentColorsConfig, eventManager);
        weightTrigger.Init(OnConnectStart, OnConnectEnd, eventManager);
        weightCollision.Init(weightView);
        InitActions();
    }

    public void DestroyElement()
    {
        weightView.DestoyElement(); //TO-DO
    }

    private void InitActions()
    {
        eventManager.OnMainButtonClickEvent.AddListener(weightView.OnMainButtonClick);
        eventManager.OnMainButtonClickEvent.AddListener(weightTrigger.OnMainButtonClickHandler);
        eventManager.OnMainStayCreatedEvent.AddListener(SetPoint);
        OnConnectStart.AddListener(weightView.ConnectStart);
        OnConnectEnd.AddListener(weightView.ConnectEnd);
    }

    private void SetPoint(MainStayView mainStayView)
    {
        weightView.Point = mainStayView;
        weightView.transform.SetParent(mainStayView.transform);
    }
}
