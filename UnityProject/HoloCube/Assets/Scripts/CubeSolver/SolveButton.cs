using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SolveButton : MonoBehaviour {
    StringBuilder sb = new StringBuilder(54);
    private static UnityEngine.Color up, down, front, back, right, left;

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
                    sb.Append('U'); //sb[9 * i + j-1] = 'U';    
                else if (GameObject.Find(faces[i] + j).GetComponent<Renderer>().material.color == right)
                    sb.Append('R'); //sb[9 * i + j - 1] = 'R';
                else if (GameObject.Find(faces[i] + j).GetComponent<Renderer>().material.color == front)
                    sb.Append('F'); //sb[9 * i + j - 1] = 'F';
                else if (GameObject.Find(faces[i] + j).GetComponent<Renderer>().material.color == down)
                    sb.Append('D'); //sb[9 * i + j - 1] = 'D';
                else if (GameObject.Find(faces[i] + j).GetComponent<Renderer>().material.color == left)
                    sb.Append('L'); //sb[9 * i + j - 1] = 'L';
                else if (GameObject.Find(faces[i] + j).GetComponent<Renderer>().material.color == back)
                    sb.Append('B'); //sb[9 * i + j - 1] = 'B';
            }

        string s = Search.solution(sb.ToString(), 24, 999999, true);
        Debug.Log(s);
    }
}














/*
for (int i = 0; i < 54; i++)
    sb.Insert(i, 'B'); // Default Initialization

for (int i = 0; i < 6; i++) //Read the 54 Facelets
    for (int j = 0; j < 9; j++)
    {
        //if (gameObject -> "BLUE" == MIDDLE COLOR)
        if()
        sb[9 * i + j] = 'U';
        sb[9 * i + j] = 'R';
        sb[9 * i + j] = 'F';
        sb[9 * i + j] = 'D';
        sb[9 * i + j] = 'L';
        sb[9 * i + j] = 'B';
    }*/
