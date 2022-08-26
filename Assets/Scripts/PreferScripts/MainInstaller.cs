using Graf.Utils;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
public class MainInstaller : MonoInstaller
{
    [SerializeField]
    private Transform ContainerUI;

    [SerializeField]
    private Sprite RopeSprite;

    [SerializeField]
    private Sprite SelectPaletteSprite;

    [SerializeField]
    private Sprite SelectedPaletteSprite;

    public override void InstallBindings()
    {
        Container.BindInstance(ContainerUI).WithId("UIViewContainer");
        Container.BindInstance(RopeSprite).WithId("RopeSprite");
        Container.BindInstance(SelectPaletteSprite).WithId("selectPaletteSprite");
        Container.BindInstance(SelectedPaletteSprite).WithId("selectedPaletteSprite");

        InitScripts();
    }

    private void InitScripts()
    {
        Container.Bind<MainMenuManager>().AsSingle().NonLazy();
        Container.Bind<CameraController>().AsSingle().NonLazy();
        Container.Bind<EventManager>().AsSingle();
        Container.Bind<ResourceLoaderService<GameObject>>().AsSingle();
        Container.Bind<MenuState>().AsSingle();
        Container.Bind<GameState>().AsSingle();
        Container.Bind<GameOverState>().AsSingle();
        Container.Bind<ShopState>().AsSingle();
        Container.Bind<SettingsState>().AsSingle();
        Container.Bind<PauseMenuState>().AsSingle();
        Container.Bind<ADSMenuState>().AsSingle();
        Container.Bind<WeightController>().AsSingle();
        Container.Bind<MapGenerator>().AsSingle();
        Container.Bind<PaletteManager>().AsSingle();
        Container.Bind<ScoreManager>().AsSingle();
        Container.Bind<RecordManager>().AsSingle();
        Container.Bind<MoneyManager>().AsSingle();
        Container.Bind<PaletteShopController>().AsSingle();
    }
}

