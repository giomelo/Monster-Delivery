using UnityEngine;

namespace _Script
{
    public class ColorShader : MonoBehaviour
    {
        private Material toonMaterial;
        [SerializeField]
        private Color shadowColor;

        private static readonly int ShadowColor = Shader.PropertyToID("Color_1BAB2A31");

        // Start is called before the first frame update
        private void Start()
        {
            toonMaterial = GetComponent<Renderer>().material;
            Material newMaterial = toonMaterial;
            newMaterial.SetColor(ShadowColor, shadowColor);
            GetComponent<Renderer>().material = newMaterial;
        }
    }
}
