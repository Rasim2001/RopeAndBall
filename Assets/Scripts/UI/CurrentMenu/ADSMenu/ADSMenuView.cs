using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ADSMenuView : MonoBehaviour
{
    [SerializeField]
    private Button ADSButton;

    [SerializeField]
    private Button BackButton;

    [SerializeField]
    private Image fillImageADS;

    public UnityEvent OnFillImageADS;
    public UnityEvent OnADSButton;
    public UnityEvent OnBackButton;

    private IEnumerator coroutine;
    public void Init()
    {
        coroutine = UpdateAnim();
        InitActions();
    }
    private void InitActions()
    {
        ADSButton.onClick.AddListener(() => OnADSButton?.Invoke());
        BackButton.onClick.AddListener(() => OnBackButton?.Invoke());
    }

    public void StartAnim()
    {
        if (gameObject.activeInHierarchy){ // TO-DO временно сделал
            fillImageADS.fillAmount = 1;
            StartCoroutine(coroutine);
            OnStartADSButton();
        }
       
    }

    public void StopAnim()
    {
        StopCoroutine(coroutine);
    }
    private IEnumerator UpdateAnim()
    {
        while (fillImageADS.fillAmount >= 0)
        {
            if(fillImageADS.fillAmount == 0)
            {
                OnFillImageADS?.Invoke();
                StopAnim();
            }
            fillImageADS.fillAmount -= 1.0f / 10 * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    private void OnStartADSButton()
    {
        ADSButton.transform.DOScale(new Vector3(1.07f, 1.07f, 1.07f), 0.5f).
                            OnComplete(CompleteADSButton).
                            SetEase(Ease.OutCubic, 2f);
    }

    private void CompleteADSButton()
    {
        ADSButton.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f).
                            OnComplete(OnStartADSButton).
                            SetEase(Ease.OutCubic, 2f);
    }

    public void ActiveObject()
    {
        gameObject.SetActive(true);
    }

    public void InActiveObject()
    {
        gameObject.SetActive(false);
    }
}
