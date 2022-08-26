using Graf.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class SettingsState : UIState
{
    protected override IUIShowableHidable ShowableHidable { get; set; }

    private SettingsView settingsView;

    [Inject]
    public SettingsState(ResourceLoaderService<GameObject> resourceLoaderService,
                        [Inject(Id = "UIViewContainer")] Transform container)
    {
        settingsView = GameObject.Instantiate(resourceLoaderService.Prefabs.SETTINGS_VIEW, container).GetComponent<SettingsView>();
        settingsView.Init();
              
        ShowableHidable = settingsView;
        ShowableHidable.Instantiate();
    }

    protected override void Enter(params object[] parameters)
    {
        settingsView.OnSoundSlider.AddListener((float value) => settingsView.SoundValueChanged(value));
        //settingsView.OnSoundSlider.AddListener((float value) => { floatHashedSound.Value = percentValue; });
        settingsView.OnMusicSlider.AddListener((float value) => settingsView.MusicValueChanged(value));
        settingsView.OnBackButton.AddListener(() => MainMenuManager.menuManager.GoToScreenOfType<MenuState>());
        settingsView.PlayAnimationUI();
    }

    protected override void Exit()
    {
        settingsView.OnSoundSlider.RemoveListener((float value) => settingsView.SoundValueChanged(value));
        settingsView.OnMusicSlider.RemoveListener((float value) => settingsView.MusicValueChanged(value));
        settingsView.OnBackButton.RemoveListener(() => MainMenuManager.menuManager.GoToScreenOfType<MenuState>());
        settingsView.ExitAnimationUI();
    }
}
