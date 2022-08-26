using Graf.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
public class MenuState : UIState
{
    private MenuView menuView;

    private UnityAction OnButtonClickPlay;
    private UnityAction OnButtonClickSettings;
    private UnityAction OnButtonClickShop;

    protected override IUIShowableHidable ShowableHidable { get; set; }


    [Inject]
    public MenuState(ResourceLoaderService<GameObject> resourceLoaderService,
                    [Inject(Id = "UIViewContainer")] Transform container,
                    RecordManager recordManager
                    )
    {
        menuView = GameObject.Instantiate(resourceLoaderService.Prefabs.MAINMENU_VIEW, container).GetComponent<MenuView>();
        ShowableHidable = menuView;
        ShowableHidable.Instantiate();

        recordManager.InitMainMenu(menuView.recordText);
        InitActions();
    }


    private void InitActions()
    {
        OnButtonClickPlay = () => MainMenuManager.menuManager.GoToScreenOfType<GameState>();
        OnButtonClickSettings = () => MainMenuManager.menuManager.GoToScreenOfType<SettingsState>();
        OnButtonClickShop = () => MainMenuManager.menuManager.GoToScreenOfType<ShopState>();
    }


    protected override void Enter(params object[] parameters)
    {
        menuView.Init();
        menuView.OnButtonClickPlay += this.OnButtonClickPlay;
        menuView.OnButtonClickSettings += this.OnButtonClickSettings;
        menuView.OnButtonClickShop += this.OnButtonClickShop;
        menuView.PlayAnimationUI();
    }

    protected override void Exit()
    {
        menuView.ExitAnimationUI(); // TO-DO Сделать так, чтобы анимация закончилась
        menuView.OnButtonClickPlay -= this.OnButtonClickPlay;
        menuView.OnButtonClickSettings -= this.OnButtonClickSettings;
        menuView.OnButtonClickShop -= this.OnButtonClickShop;
    }
}
