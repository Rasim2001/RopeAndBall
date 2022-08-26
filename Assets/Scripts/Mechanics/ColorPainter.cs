using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPainter : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] colors1Sprites;

    [SerializeField]
    private SpriteRenderer[] colors2Sprites;

    [SerializeField]
    private SpriteRenderer[] colors3Sprites;

    [SerializeField]
    private SpriteRenderer[] colors4Sprites;

    [SerializeField]
    private SpriteRenderer[] colors5Sprites;

    [SerializeField]
    private SpriteRenderer[] colors6Sprites;

    [SerializeField]
    private SpriteRenderer[] colors7Sprites;

    [SerializeField]
    private SpriteRenderer[] colors8Sprites;

    [SerializeField]
    private SpriteRenderer[] colors9Sprites;


    
    public void Init(PaletteConfig colorsConfig)
    {
        foreach(var sprite in colors1Sprites)
        {
            sprite.color = colorsConfig.Color1;
        }
        foreach (var sprite in colors2Sprites)
        {
            sprite.color = colorsConfig.Color2;
        }
        foreach (var sprite in colors3Sprites)
        {
            sprite.color = colorsConfig.Color3;
        }
        foreach (var sprite in colors4Sprites)
        {
            sprite.color = colorsConfig.Color4;
        }
        foreach (var sprite in colors5Sprites)
        {
            sprite.color = colorsConfig.Color5;
        }
        foreach (var sprite in colors6Sprites)
        {
            sprite.color = colorsConfig.Color6;
        }
        foreach (var sprite in colors7Sprites)
        {
            sprite.color = colorsConfig.Color7;
        }
        foreach (var sprite in colors8Sprites)
        {
            sprite.color = colorsConfig.Color8;
        }
        foreach (var sprite in colors9Sprites)
        {
            sprite.color = colorsConfig.Color9;
        }
    }
}
