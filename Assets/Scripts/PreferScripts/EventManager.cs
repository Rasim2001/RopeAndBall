using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public UnityEvent OnMainButtonClickEvent = new UnityEvent();
    public UnityEvent<MainStayView> OnMainStayCreatedEvent = new UnityEvent<MainStayView>();
    public UnityEvent<Vector3> OnHookToMainStayEvent = new UnityEvent<Vector3>();
    public UnityEvent<int> OnScoreChangeEvent = new UnityEvent<int>();
    public UnityEvent OnRecordChangeEvent = new UnityEvent();
    public UnityEvent<float> OnMoneyChangeEvent = new UnityEvent<float>();
    public UnityEvent OnOpenADSMenu = new UnityEvent();
    public UnityEvent OnGameOver = new UnityEvent();

    public void SendGameOver()
    {
        OnGameOver?.Invoke();
    }
    public void SendOpenADSMenu()
    {
        OnOpenADSMenu?.Invoke();
    }
 
    public void SendMoneyChangeEvent(float money)
    {
        OnMoneyChangeEvent?.Invoke(money);
    }
    
    public void SendRecordChangeEvent()
    {
        OnRecordChangeEvent?.Invoke();
    }

    public void SendScoreChangeEvent(int score)
    {
        OnScoreChangeEvent?.Invoke(score);
    }


    public void SendHookToMainStayEvent(Vector3 target)
    {
        OnHookToMainStayEvent?.Invoke(target);
    }

    public void SendMainButtonClick()
    {
        OnMainButtonClickEvent?.Invoke();
    }

    public void SendMainStayCreated(MainStayView mainStayView)
    {
        OnMainStayCreatedEvent?.Invoke(mainStayView);
    }
}
