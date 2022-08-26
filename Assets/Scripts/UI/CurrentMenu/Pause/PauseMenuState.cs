using Graf.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
public class PauseMenuState
{
    private PauseMenuView pauseMenuView;
    private ResourceLoaderService<GameObject> resourceLoaderService;

    public void Init(ResourceLoaderService<GameObject> resourceLoaderService, Transform container)
    {
        pauseMenuView = GameObject.Instantiate(resourceLoaderService.Prefabs.CURRENT_MENU_VIEW.GetComponent<PauseMenuView>(), container);
        pauseMenuView.Init();

        InitActions();
    }

    private void InitActions()
    {
        pauseMenuView.OnResumeButton.AddListener(pauseMenuView.InActiveObject);
        pauseMenuView.OnMenuButton.AddListener(pauseMenuView.InActiveObject);
        pauseMenuView.OnMenuButton.AddListener(() => MainMenuManager.menuManager.GoToScreenOfType<MenuState>());
    }

    public void OpenPauseMenu()
    {
        pauseMenuView.ActiveObject();
    }

}
