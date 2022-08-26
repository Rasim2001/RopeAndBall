using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarView : MonoBehaviour
{
    [SerializeField]
    private ColorPainter colorPainter;

    public void Init(PaletteConfig paletteConfig)
    {
        colorPainter.Init(paletteConfig);
    }
}
