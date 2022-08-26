using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItems : MonoBehaviour 
{ 
  public void Init(PaletteConfig colorsConfig)
    {
        gameObject.GetComponent<ColorPainter>().Init(colorsConfig);
    }
}
