using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Zenject;
public class InputManager : MonoBehaviour
{
    private EventManager eventManager;

    public void Init(EventManager eventManager)
    {
        this.eventManager = eventManager;
    }

    private void Update()
    {
        
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                eventManager.SendMainButtonClick();
            }
            
        }
     }

    public void InActiveElement()
    {
        this.gameObject.SetActive(false);
    }

    public void ActiveElement()
    {
        this.gameObject.SetActive(true);
    }

}
