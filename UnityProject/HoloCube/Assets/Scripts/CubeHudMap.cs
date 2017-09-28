using System;
using Backend;
using UnityEngine;

public class CubeHudMap : MonoBehaviour
{

    [HideInInspector]
    public Cube Cube { get; set; }
    
    private Sprite mySprite;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        sr.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);

        transform.position = new Vector3(0.0f, 1.0f, -7.0f);
    }

    /// <summary>
    /// Calling this will redraw the cube map
    /// </summary>
    public void Draw()
    {
        var scale = 10;

        var tex = new Texture2D((3 * 3 * scale), (4 * 3 * scale));
        DrawCubeMap(tex,scale);
        tex.Apply();

        sr.sprite = CreateSprite(tex);
    }
    
    private Sprite CreateSprite(Texture2D tex)
    {
        var size = new Rect(0.0f, 0.0f, tex.width, tex.height);
        return Sprite.Create(tex, size, new Vector2(0.5f, 0.5f));
    }

    private void DrawCubeMap(Texture2D textureToDrawOn, int scale)
    {
        //doing this in a loop is harden than it looks...
        var front = DrawFaceTexture(Cube.Front, scale);
        var top = DrawFaceTexture(Cube.Top, scale);
        var left = DrawFaceTexture(Cube.Left, scale);
        var right = DrawFaceTexture(Cube.Right, scale);
        var bottom = DrawFaceTexture(Cube.Bottom, scale);
        var back = DrawFaceTexture(Cube.Back, scale);


        AddTextureToTexture(front, textureToDrawOn, 3 * scale, 3 * scale);
        AddTextureToTexture(top, textureToDrawOn, 3 * scale, 6 * scale);
        AddTextureToTexture(right, textureToDrawOn, 6 * scale, 3 * scale);
        AddTextureToTexture(left, textureToDrawOn, 0 * scale, 3 * scale);
        AddTextureToTexture(bottom, textureToDrawOn, 3 * scale, 0 * scale);
        AddTextureToTexture(back, textureToDrawOn, 3 * scale, 9 * scale);
    }

    private Texture2D DrawFaceTexture(Face face, int scale)
    {
        var tex = new Texture2D(3 * scale, 3 * scale);
        for (int y = 0; y < tex.height; y++)
        for (int x = 0; x < tex.width; x++)
        {
            var didPaintBlack = false;
            if (y == 0 || y == tex.height - 1)
            {
                tex.SetPixel(x, y, Color.black);
                didPaintBlack = true;
            }
            if (x == 0 || x == tex.width - 1)
            {
                tex.SetPixel(x, y, Color.black);
                didPaintBlack = true;
            }
            if (didPaintBlack)
                continue;

            tex.SetPixel(x, y, face.Colors[y / scale, x / scale]);
        }
        return tex;
    }

    private void AddTextureToTexture(Texture2D part, Texture2D master, int offsetX, int offsetY)
    {
        for (int y = 0; y < part.height; y++)
        for (int x = 0; x < part.width; x++)
        {
            master.SetPixel(offsetX + x, offsetY + y, part.GetPixel(x, y));
        }
    }

}