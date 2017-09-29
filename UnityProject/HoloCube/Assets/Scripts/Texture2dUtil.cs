using UnityEngine;
using UnityEngine.Windows;

public static class Texture2DUtil
{
    public static Texture2D LoadPNG(string filePath)
    {
        if (!File.Exists(filePath)) return null;

        var fileData = File.ReadAllBytes(filePath);
        var tex = new Texture2D(2, 2);
        tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        return tex;
    }
    
    public static Texture2D GetFontPart(int nr)
    {
        var width = 3;
        var img = LoadPNG(Application.dataPath + "/Fonts/NumberFont.png");
        var texture = new Texture2D(width, 5);

        var start = nr * width;
        var lenght = (nr +1) * width;
        for (int x = start; x < lenght; x++)
        for (int y = 0; y < 5; y++)
        {
            var color = img.GetPixel(x, y);
            texture.SetPixel(start-x,y,color);
        }

        return texture;
    }
}