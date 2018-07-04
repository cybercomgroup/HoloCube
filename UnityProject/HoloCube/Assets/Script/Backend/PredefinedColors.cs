using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredefinedColors : MonoBehaviour {
    public Dictionary<string, List<Color>> list;
    public List<Color> w, o, y, r, g, b;
    private int i;
    public Color[] c1, c2, c3, c4, c5, c6;
    Color orange = new Color(1, 0.5f, 0);

    // Use this for initialization
    void Start () {
    }

    public List<Color> Get(string s)
    {
        switch (s)
            {
            case "white":
                i++;
                w = new List<Color>();
                Color[] c1 = { Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white };
                w.AddRange(c1);
                return w; 
            case "orange":
                i++;
                o = new List<Color>();
                Color[] c2 = { orange, orange, orange, orange, orange, orange, orange, orange, orange };
                o.AddRange(c2);
                return o;
            case "yellow":
                i++;
                y = new List<Color>();
                Color[] c3 = { Color.yellow, Color.yellow, Color.yellow, Color.yellow, Color.yellow, Color.yellow, Color.yellow, Color.yellow, Color.yellow, };
                o.AddRange(c3);
                return y;
            case "red":
                i++;
                r = new List<Color>();
                Color[] c4 = { Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red };
                r.AddRange(c4);
                return r;
            case "green":
                i++;
                g = new List<Color>();
                Color[] c5 = { Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, };
                g.AddRange(c5);
                return g;
            case "blue":
                i++;
                b = new List<Color>();
                Color[] c6 = { Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, };
                g.AddRange(c6);
                return g;
        }
        return null;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
