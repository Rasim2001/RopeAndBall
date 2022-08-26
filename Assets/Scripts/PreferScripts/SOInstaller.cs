using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
    

[CreateAssetMenu(fileName = "New ScriptableObjectInstaller", menuName = "Scriptable Object/ScriptableObject Installer", order = 53)]
public class SOInstaller : ScriptableObjectInstaller
{
    [SerializeField]
    private List<PaletteConfig> colorsConfig;

    [SerializeField]
    private List<ShopItemData> shopItemData;

    [SerializeField]
    private List<ListMapItems> prefabListMapItems;

    public override void InstallBindings()
    {

        Container.BindInstance(colorsConfig);
        Container.BindInstance(shopItemData);
        Container.BindInstance(prefabListMapItems);
    }
}
