using DG.Tweening;
using Graf.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainStayView : MonoBehaviour
{
    [SerializeField]
    private Collider2D colider;

    [SerializeField]
    private ColorPainter colorPainer;

    private EventManager eventManager;
    public bool IsRight { get; private set;}

    public void Init(PaletteConfig palleteConfig, EventManager eventManager, bool isMain = false)
    {
        if (isMain)
        {
            colider.enabled = false;
            StartRotate();
        }
        else
        {
            colider.enabled = true;
        }
        colorPainer.Init(palleteConfig);
        this.eventManager = eventManager;
    }


    public void StartRotate(bool right = true)
    {
        //Invoke("ReturnElement", 10f);

        IsRight = right;
        transform.DORotate(new Vector3(0, 0, IsRight ? 360 * 10 : 360 * -10), 20, RotateMode.FastBeyond360)
            /*.OnComplete()*/;
    }

    public void ReturnElement()
    {
        eventManager.SendRecordChangeEvent();
        GetComponent<PoolObject>().Return();
    }
}
