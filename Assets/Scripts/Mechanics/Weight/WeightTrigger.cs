using DG.Tweening;
using Graf.Properties;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
public class WeightTrigger : MonoBehaviour
{
    private UnityEvent<Transform> onConnectStart = new UnityEvent<Transform>();
    private UnityEvent<Transform> onConnectEnd = new UnityEvent<Transform>();
    private EventManager eventManager;

    private int scoreSum = 0;
    private float money = 0;

    public Vector3 startPoint;

    public void Init(UnityEvent<Transform> onConnectStart,
                    UnityEvent<Transform> onConnectEnd,
                    EventManager eventManager
                   )
    {
        this.onConnectStart = onConnectStart;
        this.onConnectEnd = onConnectEnd;
        this.eventManager = eventManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MainStay"))
            onConnectStart?.Invoke(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainStay"))
        {
            onConnectEnd?.Invoke(collision.transform);
            scoreSum++;
            money++;
            CalculateAngle(collision.transform);
            collision.enabled = false;
            eventManager.SendHookToMainStayEvent(collision.transform.position);
            eventManager.SendScoreChangeEvent(scoreSum);
            eventManager.SendMoneyChangeEvent(money);
            
        }
    }

    private void CalculateAngle(Transform collision)
    {
        var direction = collision.transform.position - startPoint;
        direction.Normalize();
        float angle = Vector2.Angle(direction, collision.GetComponent<MainStayView>().IsRight ? -transform.right : transform.right) - 90;
        if (angle > 0)
        {
            collision.GetComponent<MainStayView>().StartRotate(false);
        }
        else
        {
            collision.GetComponent<MainStayView>().StartRotate();
        }
    }

    public void OnMainButtonClickHandler()
    {
        startPoint = transform.position;
    }
}
