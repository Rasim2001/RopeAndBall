using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCircle : MonoBehaviour
{
    private CircleCollider2D colider;

    
    private void Start()
    {
        colider = GetComponent<CircleCollider2D>();
        PlayerCircle.OnChangeRadius.AddListener(ChangeRadiusCollider);
    }


    private void ChangeRadiusCollider()
    {
        colider.enabled = false;
        Debug.Log("123");
    }
}
