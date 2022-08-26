using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillView : MonoBehaviour
{
    [SerializeField]
    private ColorPainter colorPainter;

    public float Speed;
    public void Init(PaletteConfig paletteConfig)
    {
        colorPainter.Init(paletteConfig);
    }

    private void Update()
    {
        transform.Rotate(0, 0, Speed);
    }
}
