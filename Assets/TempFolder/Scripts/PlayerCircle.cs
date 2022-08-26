using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerCircle : MonoBehaviour
{
    public static UnityEvent OnChangeRadius = new UnityEvent();

    [SerializeField]
    private Button button;
    private DistanceJoint2D distanceJoin;
    private LineRenderer lineRenderer;
    private Rigidbody2D rb;

    private Vector3 tempPos;
    private Transform Target;

    public float force;
    public bool check;

    private void Start()
    {
        button.onClick.AddListener(Click);
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        distanceJoin = GetComponent<DistanceJoint2D>();
        distanceJoin.enabled = false;
    }

    

    private void Click()
    {
        OnChangeRadius?.Invoke();
        distanceJoin.enabled = false;
        lineRenderer.positionCount = 0;
        check = false;
        Target = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Target = collision.transform;
        distanceJoin.enabled = true;
        distanceJoin.connectedAnchor = collision.transform.position;
        lineRenderer.positionCount = 2;
        tempPos = collision.transform.position;
        check = true;
    }
        
 

    private void DrawLine()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, tempPos);
    }

    private void Update()
    {
        if (check)
        {
            DrawLine();
        }
        if(Target != null)
        {
            Vector2 dir = Target.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
       
    }
}
