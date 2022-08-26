using Graf.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
public class ADSMenuState 
{
    private ResourceLoaderService<GameObject> resourceLoaderService;
    private ADSMenuView adsMenuView;
    private EventManager eventManager;

    public void Init(ResourceLoaderService<GameObject> resourceLoaderService, Transform container, EventManager eventManager)
    {
        adsMenuView = GameObject.Instantiate(resourceLoaderService.Prefabs.ADS_MENU_VIEW.GetComponent<ADSMenuView>(), container);
        adsMenuView.Init();

        this.eventManager = eventManager;
        InitActions();
    }

    private void InitActions()
    {
       adsMenuView.OnBackButton.AddListener(() => eventManager.SendGameOver());
       adsMenuView.OnFillImageADS.AddListener(() => eventManager.SendGameOver());
       adsMenuView.OnBackButton.AddListener(CloseADSMenu);
       adsMenuView.OnADSButton.AddListener(CloseADSMenu);
    }

    public void OpenADSMenu()
    {
        adsMenuView.ActiveObject();
        adsMenuView.StartAnim();
    }

    public void CloseADSMenu()
    {
        adsMenuView.StopAnim();
        adsMenuView.InActiveObject();
    }

}
