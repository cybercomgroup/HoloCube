using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationInstruction : MonoBehaviour
{

    private void OnMouseDown()
    {
        Debug.Log("u pressed me");
            char face = 'F';
            performX(face);
    }

    /**
        *  Each side is relevant to what you put as front. Does not care about predefined sides. 
        *  E.g if you turn 90* on L side it will assume it is front. 
        */
    public void performX(char face)
    {
        //DEFINE ADJACENT SIDES
        char left = ' ';
        char right = ' ';
        char front = ' ';
        char up = ' ';
        char down = ' ';

        if (face == 'F')
        {
            front = 'F';
            left = 'L';
            right = 'R';
            up = 'U';
            down = 'D';
        }


        //FRONT 
        UnityEngine.Color f1 = GameObject.Find(front + "1").GetComponent<Renderer>().material.color;
        UnityEngine.Color f2 = GameObject.Find(front + "2").GetComponent<Renderer>().material.color;
        UnityEngine.Color f3 = GameObject.Find(front + "3").GetComponent<Renderer>().material.color;
        UnityEngine.Color f4 = GameObject.Find(front + "4").GetComponent<Renderer>().material.color;
        UnityEngine.Color f5 = GameObject.Find(front + "5").GetComponent<Renderer>().material.color;
        UnityEngine.Color f6 = GameObject.Find(front + "6").GetComponent<Renderer>().material.color;
        UnityEngine.Color f7 = GameObject.Find(front + "7").GetComponent<Renderer>().material.color;
        UnityEngine.Color f8 = GameObject.Find(front + "8").GetComponent<Renderer>().material.color;
        UnityEngine.Color f9 = GameObject.Find(front + "9").GetComponent<Renderer>().material.color;
        //LEFT 
        UnityEngine.Color l3 = GameObject.Find(left + "3").GetComponent<Renderer>().material.color;
        UnityEngine.Color l6 = GameObject.Find(left + "6").GetComponent<Renderer>().material.color;
        UnityEngine.Color l9 = GameObject.Find(left + "9").GetComponent<Renderer>().material.color;

        //RIGHT 
        UnityEngine.Color r1 = GameObject.Find(right + "1").GetComponent<Renderer>().material.color;
        UnityEngine.Color r4 = GameObject.Find(right + "4").GetComponent<Renderer>().material.color;
        UnityEngine.Color r7 = GameObject.Find(right + "7").GetComponent<Renderer>().material.color;

        //UP 
        UnityEngine.Color u7 = GameObject.Find(up + "7").GetComponent<Renderer>().material.color;
        UnityEngine.Color u8 = GameObject.Find(up + "8").GetComponent<Renderer>().material.color;
        UnityEngine.Color u9 = GameObject.Find(up + "9").GetComponent<Renderer>().material.color;

        //DOWN 
        UnityEngine.Color d1 = GameObject.Find(down + "1").GetComponent<Renderer>().material.color;
        UnityEngine.Color d2 = GameObject.Find(down + "2").GetComponent<Renderer>().material.color;
        UnityEngine.Color d3 = GameObject.Find(down + "3").GetComponent<Renderer>().material.color;

        //PERFORM CHANGES - FRONT
        GameObject.Find(front + "3").GetComponent<Renderer>().material.color = f1;
        GameObject.Find(front + "6").GetComponent<Renderer>().material.color = f2;
        GameObject.Find(front + "9").GetComponent<Renderer>().material.color = f3;
        GameObject.Find(front + "2").GetComponent<Renderer>().material.color = f4;
        GameObject.Find(front + "8").GetComponent<Renderer>().material.color = f6;
        GameObject.Find(front + "1").GetComponent<Renderer>().material.color = f7;
        GameObject.Find(front + "4").GetComponent<Renderer>().material.color = f8;
        GameObject.Find(front + "7").GetComponent<Renderer>().material.color = f9;
        //UP
        GameObject.Find(up + "9").GetComponent<Renderer>().material.color = l3;
        GameObject.Find(up + "8").GetComponent<Renderer>().material.color = l6;
        GameObject.Find(up + "7").GetComponent<Renderer>().material.color = l9;
        //RIGHT
        GameObject.Find(right + "1").GetComponent<Renderer>().material.color = u7;
        GameObject.Find(right + "4").GetComponent<Renderer>().material.color = u8;
        GameObject.Find(right + "7").GetComponent<Renderer>().material.color = u9;
        //DOWN R1->D3,R4->D2,R7->D1
        GameObject.Find(down + "3").GetComponent<Renderer>().material.color = r1;
        GameObject.Find(down + "2").GetComponent<Renderer>().material.color = r4;
        GameObject.Find(down + "1").GetComponent<Renderer>().material.color = r7;
        //LEFT  D1->L3,D2->L6,D3->L9
        GameObject.Find(left + "3").GetComponent<Renderer>().material.color = d1;
        GameObject.Find(left + "6").GetComponent<Renderer>().material.color = d2;
        GameObject.Find(left + "9").GetComponent<Renderer>().material.color = d3;
        //BACK DOES NOT CHANGE
    }

    public void performX2()
    {

    }

    public void performXInverse()
    {

    }

}

