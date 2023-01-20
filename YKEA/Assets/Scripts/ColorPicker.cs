using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    [SerializeField]
    private Renderer objRenderer;

    [SerializeField]
    private Slider hSlider;
    [SerializeField]
    private Slider sSlider;
    [SerializeField]
    private Slider vSlider;

    [SerializeField]
    public Material sGradient;
    [SerializeField]
    public Material vGradient;

    private float h, s, v;
    private Color hsvColor;
    private float prevH, prevS;

    private void Start()
    {
        Color.RGBToHSV(objRenderer.material.color, out h, out s, out v);

        sGradient.SetColor("_Color2", objRenderer.material.color);
        vGradient.SetColor("_Color2", objRenderer.material.color);

        hSlider.value = h;
        sSlider.value = s;
        vSlider.value = v;

        prevH = h;
        prevS = s;
    }

    private void Update()
    {
        h = hSlider.value;
        s = sSlider.value;
        v = vSlider.value;

        hsvColor = Color.HSVToRGB(h, s, v);

        if (prevH != h)
        {
            sGradient.SetColor("_Color2", Color.HSVToRGB(h, 1, 1));
            vGradient.SetColor("_Color2", Color.HSVToRGB(h, 1, 1));

            prevH = h;
        }

        if (prevS != s)
        {
            vGradient.SetColor("_Color2", Color.HSVToRGB(h, s, 1));

            prevS = s;
        }

        objRenderer.material.color = hsvColor;
    }


}
