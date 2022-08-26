using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapple : MonoBehaviour
{
    private Camera camera_;
    private Vector3 mousePos;
    private bool cheak;
    private DistanceJoint2D distanceJoin;
    private LineRenderer lineRenderer;
    private Vector3 tempPos;
    [SerializeField]
    LayerMask grappleMask;
    void Start()
    {
        camera_ = Camera.main;
        distanceJoin = GetComponent<DistanceJoint2D>();
        lineRenderer = GetComponent<LineRenderer>();
        distanceJoin.enabled = false;
        cheak = true;
        lineRenderer.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetMouse();
        RaycastHit2D hit2d = Physics2D.Raycast(camera_.transform.position, mousePos, Mathf.Infinity, grappleMask);

        if(Input.GetMouseButtonDown(0) && cheak && hit2d)
        {
            distanceJoin.enabled = true;
            distanceJoin.connectedAnchor = mousePos;
            lineRenderer.positionCount = 2;
            tempPos = mousePos;
            cheak = false;
        }
        else if(Input.GetMouseButtonDown(0))
        {
            distanceJoin.enabled = false;
            cheak = true;
            lineRenderer.positionCount = 0;
        }
        DrawLine();
    }

    private void DrawLine()
    {
        if (lineRenderer.positionCount <= 0) return;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, tempPos);
    }

    private void GetMouse()
    {
        mousePos = camera_.ScreenToWorldPoint(Input.mousePosition);
    }
}
