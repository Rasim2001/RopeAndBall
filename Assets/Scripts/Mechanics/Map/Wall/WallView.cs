using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallView : MonoBehaviour
{
    [SerializeField]
    private ColorPainter colorPainter;

    public float Angle;

    public void Init(PaletteConfig paletteConfig)
    {
        colorPainter.Init(paletteConfig);
    }
}
