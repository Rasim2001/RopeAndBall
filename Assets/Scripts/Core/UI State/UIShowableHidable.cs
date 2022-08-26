using System;
using UnityEngine;
public class UIShowableHidable : MonoBehaviour, IUIShowableHidable
{
    protected RectTransform rectTransform;

    protected virtual void Awake()
    {

    }

    public virtual void ShowUI()
    {
        gameObject.SetActive(true);
    }

    public virtual void HideUI()
    {
        gameObject.SetActive(false);
    }

    public virtual void Instantiate()
    {
        rectTransform = GetComponent<RectTransform>();
        gameObject.SetActive(false);

    }

    protected bool TrySendAction(Action action)
    {
        if (action == null) return false;
        action();
        return true;
    }
}
