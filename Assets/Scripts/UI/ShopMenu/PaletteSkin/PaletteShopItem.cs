using Graf.Properties;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class PaletteShopItem : MonoBehaviour 
{
    public TextMeshProUGUI PriceText;
    public UnityEvent<int> OnSelectedPalette = new UnityEvent<int>();
    public UnityEvent<int> OnBoughtPalette = new UnityEvent<int>();
    public Button BuyButton;
    public ShopItemData ShopItemData;

    public Image BuyButtonImage;


    private int paletteIndex;

    public void Init(int paletteIndex, ShopItemData shopItemData)
    {
        this.paletteIndex = paletteIndex;
        this.ShopItemData = shopItemData;
    }

    public void ClickBuyItem()
    {
        if (!PlayerPrefs.HasKey($"BuyItem_{paletteIndex}"))
        {
            OnBoughtPalette?.Invoke(paletteIndex);
        }
        else
        {
            OnSelectedPalette?.Invoke(paletteIndex);
        }
    }

}
