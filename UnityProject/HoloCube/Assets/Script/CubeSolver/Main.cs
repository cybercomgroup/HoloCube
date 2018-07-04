using UnityEngine;

public class Main : MonoBehaviour {
    private readonly static UnityEngine.Color[] COLORS = { UnityEngine.Color.white, UnityEngine.Color.red,
    UnityEngine.Color.blue, UnityEngine.Color.yellow, UnityEngine.Color.green, UnityEngine.Color.magenta};

    // Use this for initialization
    void Start () {
        GameObject[] facelets = GameObject.FindGameObjectsWithTag("Facelet");
        foreach(GameObject facelet in facelets)
        {
            facelet.GetComponent<Renderer>().enabled = true;
            if(facelet.name.Contains("U"))
            facelet.GetComponent<Renderer>().material.color = UnityEngine.Color.red;
            else if (facelet.name.Contains("R"))
                facelet.GetComponent<Renderer>().material.color = UnityEngine.Color.green;
            else if (facelet.name.Contains("F"))
                facelet.GetComponent<Renderer>().material.color = UnityEngine.Color.white;
            else if (facelet.name.Contains("D"))
                facelet.GetComponent<Renderer>().material.color = UnityEngine.Color.magenta;
            else if (facelet.name.Contains("L"))
                facelet.GetComponent<Renderer>().material.color = UnityEngine.Color.blue;
            else if (facelet.name.Contains("B"))
                facelet.GetComponent<Renderer>().material.color = UnityEngine.Color.yellow;

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
    }
}
