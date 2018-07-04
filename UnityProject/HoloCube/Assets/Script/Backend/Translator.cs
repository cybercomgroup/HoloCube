using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class Translator : MonoBehaviour {
    private StringBuilder sb;
    private Dictionary<string, List<Color>> dict;

	// Use this for initialization
	void Start () {
        sb = new StringBuilder();
        Debug.Log("Reached translator");
        Test();
	}

    public void Test()
    {
        dict = new Dictionary<string, List<Color>>();
        dict["white"] = new List<Color> { Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green, Color.green };
        dict["orange"] = new List<Color> { Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white };
        dict["yellow"] = new List<Color> { Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue, Color.blue };
        dict["red"] = new List<Color> { Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white, Color.white };
        dict["green"] = new List<Color> { Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red, Color.red };
        dict["blue"] = new List<Color> { Color.yellow, Color.yellow, Color.yellow, Color.yellow, Color.yellow, Color.yellow, Color.yellow, Color.yellow, Color.yellow };

        Debug.Log(TranslateFromDictToString(dict));
    }
	

    public string TranslateFromDictToString(Dictionary<string, List<Color>> dict)
    {
        sb = new StringBuilder();
        foreach (string s in dict.Keys)
        {
            for(int i = 0; i < 9; i++)
            {
                sb.Append(ColorCharFromColorClass(dict[s][i]));
            }
        }
        return sb.ToString();
    }

    private char ColorCharFromColorClass(Color c)
    {

        if (c.Equals(Color.white)) return 'F';
        if (c.Equals(Color.green)) return 'U';
        if (c.Equals(Color.yellow)) return 'B';
        if (c.Equals(Color.red)) return 'L';
        if (c.Equals(Color.blue)) return 'D';

        else return 'R';

        }
    


	// Update is called once per frame
	void Update () {
		
	}
}
