using Graf.Properties;
using Graf.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
public class ShopState : UIState
{
    protected override IUIShowableHidable ShowableHidable { get; set; }

    private ShopView shopView;
    private PaletteShopController paletteShopController;
    private MoneyManager moneyManager;

    private UnityAction OnBackClickButton;

    private PaletteManager paletteManager;

    [Inject]
    public ShopState(ResourceLoaderService<GameObject> resourceLoaderService,
                        [Inject(Id = "UIViewContainer")] Transform container,
                        PaletteShopController paletteShopController,
                        EventManager eventManager,
                        MoneyManager moneyManager,
                        PaletteManager paletteManager)
    {
        shopView = GameObject.Instantiate(resourceLoaderService.Prefabs.SHOP_VIEW, container).GetComponent<ShopView>();
        shopView.Init(eventManager);
        this.paletteManager = paletteManager;

        this.moneyManager = moneyManager;
        moneyManager.moneyTextShop = shopView.moneyText;

        this.paletteShopController = paletteShopController;
        paletteShopController.Init(shopView.transform);

        ShowableHidable = shopView;
        ShowableHidable.Instantiate();

        //PlayerPrefs.DeleteAll();

        InitActions();
    }
   
    private void InitActions()
    {
        OnBackClickButton = () => MainMenuManager.menuManager.GoToScreenOfType<MenuState>();
    }

    protected override void Enter(params object[] parameters)
    {
        shopView.PlayAnimationUI();
        shopView.OnBackClickButton += this.OnBackClickButton;
        moneyManager.PrintMoney();
    }

    protected override void Exit()
    {
        shopView.ExitAnimationUI();
        shopView.OnBackClickButton -= this.OnBackClickButton;
        paletteManager.CurrentColorsConfig = paletteManager.PalleteConfigs[PlayerPrefs.GetInt("CurrentPalette")]; //TO-DO чекнуть надо
    }
}
