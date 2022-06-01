using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorShader : MonoBehaviour
{
    [SerializeField]
    private Material toonMaterial;
    [SerializeField]
    private Color shadowColor;

    private static readonly int ShadowColor = Shader.PropertyToID("_ShadowColor");

    // Start is called before the first frame update
    private void Start()
    {
        toonMaterial= GetComponent<Renderer>().material;
        toonMaterial.SetColor("_ShadowColor", shadowColor);
        this.GetComponent<Renderer>().material = toonMaterial;
    }
    
}
