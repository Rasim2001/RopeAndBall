using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuView : UIShowableHidable
{
    [SerializeField]
    private Button buttonPlay;

    [SerializeField]
    private Button buttonSettings;

    [SerializeField]
    private Button buttonShop;

    [SerializeField]
    public TextMeshProUGUI recordText;

    public UnityAction OnButtonClickPlay;
    public UnityAction OnButtonClickSettings;
    public UnityAction OnButtonClickShop;


    public void Init()
    {
        buttonPlay.onClick.AddListener(() => OnButtonClickPlay?.Invoke());
        buttonSettings.onClick.AddListener(() => OnButtonClickSettings?.Invoke());
        buttonShop.onClick.AddListener(() => OnButtonClickShop?.Invoke());
    }

    public void PlayAnimationUI()
    {
        transform.DOLocalMoveY(0, 1).SetEase(Ease.OutBounce);
    }

    public void ExitAnimationUI()
    {
        transform.DOLocalMoveY(2600, 1).SetEase(Ease.OutBounce);
    }
}
