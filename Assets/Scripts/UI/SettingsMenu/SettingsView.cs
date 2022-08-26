using DG.Tweening;
using Graf.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsView : UIShowableHidable
{
    [SerializeField]
    private Button BackButton;

    [SerializeField]
    private Slider soundSlider;

    [SerializeField]
    private Slider musicSlider;

    [SerializeField]
    private TextMeshProUGUI soundValueText;

    [SerializeField]
    private TextMeshProUGUI musicValueText;

    [SerializeField]
    private Button VibrationSwitch;

    [SerializeField]
    private Sprite VibrationOn;

    [SerializeField]
    private Sprite VibrationOff;


    public UnityEvent OnBackButton;
    public UnityEvent<float> OnSoundSlider;
    public UnityEvent<float> OnMusicSlider;

    private FloatHashed floatHashedSound = new FloatHashed("Sound");
    private FloatHashed floatHashedMusic = new FloatHashed("Music");
    private FloatHashed floatHashedVibration = new FloatHashed("Vibration");
 

    private bool isVibrationOn = false;
    public void Init()
    {
        SendValueOnStart(floatHashedSound.Value, floatHashedMusic.Value, floatHashedVibration.Value);
        InitActions();
     }

    private void InitActions()
    {
        VibrationSwitch.onClick.AddListener(() => VibrationSwitched(isVibrationOn));
        soundSlider.onValueChanged.AddListener((float value) => OnSoundSlider?.Invoke(value));
        musicSlider.onValueChanged.AddListener((float value) => OnMusicSlider?.Invoke(value));

        BackButton.onClick.AddListener(() => OnBackButton?.Invoke());
    }

    private void VibrationSwitched(bool value)
    {
        if (value)
        {
            floatHashedVibration.Value = 1;
            VibrationSwitch.image.sprite = VibrationOn;
        }
        else
        {
            floatHashedVibration.Value = 0;
            VibrationSwitch.image.sprite = VibrationOff;
        }
        isVibrationOn = !value;
    }
    public void SoundValueChanged(float value)
    {
        float percentValue = (float)System.Math.Round(value, 2) * 100;
        soundValueText.text = $"{percentValue}%";
        floatHashedSound.Value = percentValue;
    }
    public void MusicValueChanged(float value)
    {
        float percentValue = (float)System.Math.Round(value, 2) * 100;
        musicValueText.text = $"{percentValue}%";
        floatHashedMusic.Value = percentValue;
    }

    private void SendValueOnStart(float soundValue, float musicValue, float vibrationValue)
    {
        soundValueText.text = $"{soundValue}%";
        musicValueText.text = $"{musicValue}%";
        soundSlider.value = soundValue / 100;
        musicSlider.value = musicValue / 100;

        if (vibrationValue == 1)
            VibrationSwitched(true);
        else
            VibrationSwitched(false);
    }

    public void PlayAnimationUI()
    {
        transform.DOLocalMoveX(0, 1).SetEase(Ease.OutBounce);
    }
    public void ExitAnimationUI()
    {
        transform.DOLocalMoveX(1200, 1).SetEase(Ease.OutBounce);
    }
}