using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New ShopItemData", menuName = "Scriptable Object/ShopItem Data", order = 53)]
public class ShopItemData : ScriptableObject
{
    public PaletteConfig PaletteConfig;
    public int Price;
}
