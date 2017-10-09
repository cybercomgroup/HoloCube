using Backend;
using OpenCVForUnityExample.ColorDetection;
using UnityEngine;

public class ScanedFace : MonoBehaviour
{
    public Face Face;


    private static Shader _shader;

    // Use this for initialization
    void Start()
    {
        _shader = Shader.Find("Unlit/Color");
        Face = new Face();
    }

    public void Redraw()
    {
        for (int i = 0; i < Face.Colors.Count; i++)
        {
            SetColor("Pice" + i, Face.Colors[i], true);
        }

        SetColor("Middle", Face.MiddleColor, true);
        SetColor("TopColor", Face.TopColor, true);
    }
    
    public void Reset()
    {
        for (int i = 0; i < Face.Colors.Count; i++)
        {
            SetColor("Pice" + i, Face.Colors[i], false);
        }
        SetColor("Middle", Face.MiddleColor, true);
    }

    private void SetColor(string name, RubicColors rubicColor, bool shouldBeEnabled)
    {
        var col = ColorDetection.UnityColorFromEnum(rubicColor);
        var obj = transform.Find(name);
        var render = obj.GetComponent<Renderer>();

        render.enabled = shouldBeEnabled;

        render.material.shader = _shader;
        render.material.color = col;
    }
}