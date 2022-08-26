using Graf.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class PaletteShopController
{
    private PaletteShopView paletteShopView;
    private ResourceLoaderService<GameObject> resourceLoaderService;
    private EventManager eventManager;
    private List<ShopItemData> shopItemData;
    private Sprite SelectPaletteSprite;
    private Sprite SelectedPaletteSprite;
    [Inject]
    public PaletteShopController(ResourceLoaderService<GameObject> resourceLoaderService,
                                EventManager eventManager,
                                List<ShopItemData> shopItemData,
                                [Inject(Id = "selectPaletteSprite")] Sprite SelectPaletteSprite,
                                [Inject(Id = "selectedPaletteSprite")] Sprite SelectedPaletteSprite
                                )
    {
        this.resourceLoaderService = resourceLoaderService;
        this.eventManager = eventManager;
        this.shopItemData = shopItemData;
        this.SelectPaletteSprite = SelectPaletteSprite;
        this.SelectedPaletteSprite = SelectedPaletteSprite;
    }

    public void Init(Transform container)
    {
        paletteShopView = GameObject.Instantiate(resourceLoaderService.Prefabs.PALETTE_SHOP_VIEW, container).GetComponent<PaletteShopView>();
        paletteShopView.Init(eventManager, shopItemData, resourceLoaderService, SelectPaletteSprite, SelectedPaletteSprite);
    }

}
