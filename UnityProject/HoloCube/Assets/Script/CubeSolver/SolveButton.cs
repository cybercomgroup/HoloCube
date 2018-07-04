using System;
using System.Text;
using UnityEngine;

public class SolveButton : MonoBehaviour {
    StringBuilder sb = new StringBuilder(54);
    private static UnityEngine.Color up, down, front, back, right, left;
    public static string[] commands;

    private void OnMouseDown()
    {
        up = GameObject.Find("U5").GetComponent<Renderer>().material.color; // green
        right = GameObject.Find("R5").GetComponent<Renderer>().material.color; // white
        front = GameObject.Find("F5").GetComponent<Renderer>().material.color; // red
        down = GameObject.Find("D5").GetComponent<Renderer>().material.color; // blue
        left = GameObject.Find("L5").GetComponent<Renderer>().material.color; // yellow
        back = GameObject.Find("B5").GetComponent<Renderer>().material.color; //orange

        string[] faces = { "U", "R", "F", "D", "L", "B" };
        
        for (int i = 0; i < 6; i++)
            for (int j = 1; j < 10; j++)
            {
                if (GameObject.Find(faces[i] + j).GetComponent<Renderer>().material.color == up)
                    sb.Append('U');   
                else if (GameObject.Find(faces[i] + j).GetComponent<Renderer>().material.color == right)
                    sb.Append('R');
                else if (GameObject.Find(faces[i] + j).GetComponent<Renderer>().material.color == front)
                    sb.Append('F');
                else if (GameObject.Find(faces[i] + j).GetComponent<Renderer>().material.color == down)
                    sb.Append('D');
                else if (GameObject.Find(faces[i] + j).GetComponent<Renderer>().material.color == left)
                    sb.Append('L');
                else if (GameObject.Find(faces[i] + j).GetComponent<Renderer>().material.color == back)
                    sb.Append('B');
            }

        string s = Search.solution(sb.ToString(), 24, 999999, false);
        Debug.Log(s);
        char[] delim = new char[] { ' ' };
        commands = s.Split(delim, StringSplitOptions.RemoveEmptyEntries);
    }
}