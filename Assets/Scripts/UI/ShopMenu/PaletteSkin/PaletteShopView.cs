using Graf.Properties;
using Graf.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaletteShopView : MonoBehaviour
{
    private List<PaletteShopItem> paletteShopItems = new List<PaletteShopItem>();
    private EventManager eventManager;
    private List<ShopItemData> shopItemsData;
    private ResourceLoaderService<GameObject> resourceLoaderService;

    private Sprite selectPaletteSprite;
    private Sprite selectedPaletteSprite;

    private FloatHashed floatHashed = new FloatHashed("Money");
    private Color selectedColor = new Color(132f / 255, 163f / 255, 158f / 255);
    private Color selectColor = Color.black;

    public void Init(EventManager eventManager, 
                     List<ShopItemData> shopItemData, 
                     ResourceLoaderService<GameObject> resourceLoaderService,
                     Sprite selectPaletteSprite,
                     Sprite selectedPaletteSprite
                     )
    {
        this.eventManager = eventManager;
        this.shopItemsData = shopItemData;
        this.resourceLoaderService = resourceLoaderService;
        this.selectPaletteSprite = selectPaletteSprite;
        this.selectedPaletteSprite = selectedPaletteSprite;

        InitListOfPaletteItem();
        BuyPalette(0);
        SelectPalette(PlayerPrefs.GetInt("CurrentPalette"));
    }

    private void InitListOfPaletteItem()
    {
        for (int i = 0; i < shopItemsData.Count; i++)
        {
            var paletteShopItem = GameObject.Instantiate(resourceLoaderService.Prefabs.PALETTE_SHOP_ITEM, transform).GetComponent<PaletteShopItem>();
            paletteShopItems.Add(paletteShopItem);
            paletteShopItems[i].Init(i, shopItemsData[i]);
            paletteShopItems[i].OnBoughtPalette.AddListener(BuyPalette);
            paletteShopItems[i].OnSelectedPalette.AddListener(SelectPalette);
        }
    }
    private void BuyPalette(int currentPalette)
    {
        if (floatHashed.Value >= paletteShopItems[currentPalette].ShopItemData.Price)
        {
            paletteShopItems[currentPalette].PriceText.text = "Select";
            paletteShopItems[currentPalette].BuyButtonImage.sprite = selectPaletteSprite;
            eventManager.SendMoneyChangeEvent(-paletteShopItems[currentPalette].ShopItemData.Price);
            PlayerPrefs.SetInt($"BuyItem_{currentPalette}", currentPalette);
        }
    }

    private void SelectPalette(int currentPalette)
    {
        ChoosePalette(currentPalette);
        CloseOthersPalette(currentPalette);
    }

    private void ChoosePalette(int currentPalette)
    {
        
        PlayerPrefs.SetInt("CurrentPalette", currentPalette);
        paletteShopItems[currentPalette].PriceText.text = "Selected";
        paletteShopItems[currentPalette].PriceText.color = selectedColor;
        paletteShopItems[currentPalette].BuyButtonImage.sprite = selectedPaletteSprite;
        paletteShopItems[currentPalette].BuyButton.interactable = false;
    }

    private void CloseOthersPalette(int currentPalette)
    {
        for (int i = 0; i < paletteShopItems.Count; i++)
        {
            if (i != currentPalette)
            {
                if (PlayerPrefs.HasKey($"BuyItem_{i}"))
                {
                    paletteShopItems[i].PriceText.text = "Select";
                    paletteShopItems[i].PriceText.color = selectColor;
                    paletteShopItems[i].BuyButtonImage.sprite = selectPaletteSprite;
                }
                else
                {
                    paletteShopItems[i].PriceText.text = paletteShopItems[i].ShopItemData.Price.ToString() + " <sprite name=\"Ellipse 53\">";
                }
                paletteShopItems[i].BuyButton.interactable = true;
            }
        }
    }
}
