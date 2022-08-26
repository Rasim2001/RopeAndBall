using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PrefabsData", menuName = "Scriptable Object/Prefabs Data", order = 51)]
public class PrefabsData : ScriptableObject
{
    public GameView GAME_VIEW;
    public MenuView MAINMENU_VIEW;
    public WeightView WEIGHT_VIEW;
    public MainStayView MAINSTAY_VIEW;
    public SettingsView SETTINGS_VIEW;
    public ShopView SHOP_VIEW;
    public PaletteShopView PALETTE_SHOP_VIEW;
    public PaletteShopItem PALETTE_SHOP_ITEM;
    public PauseMenuView CURRENT_MENU_VIEW;
    public ADSMenuView ADS_MENU_VIEW;
    public GameOverView GAME_OVER_VIEW;

    public InputManager INPUT_MANAGER;
}
