using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Collections;

public class Main : MonoBehaviour {
    private readonly static UnityEngine.Color[] COLORS = { UnityEngine.Color.white, UnityEngine.Color.red,
    UnityEngine.Color.blue, UnityEngine.Color.yellow, UnityEngine.Color.green, UnityEngine.Color.magenta};

    // Use this for initialization
    void Start () {
        GameObject[] facelets = GameObject.FindGameObjectsWithTag("Facelet");
        foreach(GameObject facelet in facelets)
        {
            facelet.GetComponent<Renderer>().enabled = true;
            facelet.GetComponent<Renderer>().material.color = UnityEngine.Color.blue;
        }

        GameObject.Find("B").GetComponent<Renderer>().enabled = true;
        GameObject.Find("B").GetComponent<Renderer>().material.color = COLORS[2];
        GameObject.Find("G").GetComponent<Renderer>().enabled = true;
        GameObject.Find("G").GetComponent<Renderer>().material.color = COLORS[4];
        GameObject.Find("O").GetComponent<Renderer>().enabled = true;
        GameObject.Find("O").GetComponent<Renderer>().material.color = COLORS[5];
        GameObject.Find("R").GetComponent<Renderer>().enabled = true;
        GameObject.Find("R").GetComponent<Renderer>().material.color = COLORS[1];
        GameObject.Find("W").GetComponent<Renderer>().enabled = true;
        GameObject.Find("W").GetComponent<Renderer>().material.color = COLORS[0];
        GameObject.Find("Y").GetComponent<Renderer>().enabled = true;
        GameObject.Find("Y").GetComponent<Renderer>().material.color = COLORS[3];

        // U, R, F, D, L, B
        //string s = Search.solution("UUUUUUUUURRRRRRRRRFFFFFFFFFDDDDDDDDDLLLLLLLLLBBBBBBBBB", 24, 99999999, true);
        //string s = Search.solution("RRRUUDDDLBLURRDFBBRRUFFBBBLLLDUDUUDUDRBLLULLDFFFFBFRBF", 24, 99999999, true);
        //string s = Search.solution("LRRUUULLRULDURDURDFFBFFBFFBRRLDDDRLLURDULDULDBBFBBFBBF", 24, 999999, true);
        //string s = Search.solution("DUUBULDBFRBFRRULLLBRDFFFBLURDBFDFDRFRULBLUFDURRBLBDUDL", 24, 999999, true);
        
    }
}
