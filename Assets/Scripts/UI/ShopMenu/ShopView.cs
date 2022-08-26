using DG.Tweening;
using Graf.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopView : UIShowableHidable
{
    [SerializeField]
    private Button backButton;

    public TextMeshProUGUI moneyText;

    private EventManager eventManager;


    public UnityAction OnBackClickButton;

    public void Init(EventManager eventManager)
    {
        this.eventManager = eventManager;

        InitAction();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            MainMenuManager.menuManager.GoToScreenOfType<MenuState>();
        }
    }
    private void InitAction()
    {
        backButton.onClick.AddListener(() => OnBackClickButton.Invoke());
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
