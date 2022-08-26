using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseMenuView : MonoBehaviour
{
    [SerializeField]
    private Button ResumeButton;
    
    [SerializeField]
    private Button MenuButton;


    public UnityEvent OnResumeButton;
    public UnityEvent OnMenuButton;

    private bool isPaused;

    public void Init()
    {
        ResumeButton.onClick.AddListener(() => OnResumeButton?.Invoke());
        MenuButton.onClick.AddListener(() => OnMenuButton?.Invoke());
    }


    public void ActiveObject()
    {
        gameObject.SetActive(true);
        OnApplicationPause(false);
    }

    public void InActiveObject()
    {
        gameObject.SetActive(false);
        OnApplicationPause(true);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }
}
