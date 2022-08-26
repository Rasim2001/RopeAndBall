using Graf.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PaletteManager
{
    public PaletteConfig CurrentColorsConfig { get; set; }

    public List<PaletteConfig> PalleteConfigs= new List<PaletteConfig>();

    [Inject]
    public PaletteManager(List<PaletteConfig> paletteColorsConfig)
    {
        PalleteConfigs = paletteColorsConfig;
        CurrentColorsConfig = PalleteConfigs[0];

        ChosePaletteConfig();
    }

    private void ChosePaletteConfig()
    {
       CurrentColorsConfig = PalleteConfigs[PlayerPrefs.GetInt("CurrentPalette")];
    }
    
}
