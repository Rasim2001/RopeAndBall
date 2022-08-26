using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class GameView : UIShowableHidable
{
    [SerializeField]
    private Button PauseMenu;

    public UnityAction OnPauseMenuButton;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI moneyText;
    public void Init()
    {
        InitActions();
    }

    private void InitActions()
    {
        PauseMenu.onClick.AddListener(() => OnPauseMenuButton?.Invoke());
    }
   
}
