using UnityEngine;
using HoloToolkit.Unity.InputModule;

/*
 * Responsible for performing the moves on the virtual cube
 */

public class RotationInstruction : MonoBehaviour,  IInputClickHandler
{

    public void OnInputClicked(InputClickedEventData eventData)
    { 
        foreach (string com in SolveButton.commands)
        {
            if (com[0] == 'U' && com.Length == 1)
                U90();
            else if (com[0] == 'U' && com[1] == '2')
                U180();
            else if (com[0] == 'U' && com.Length == 2)
                U90Inv(); 

            else if (com[0] == 'R' && com.Length == 1)
                R90();
            else if (com[0] == 'R' && com[1] == '2')
                R180();
            else if (com[0] == 'R' && com.Length == 2)
                R90Inv();

            else if (com[0] == 'F' && com.Length == 1)
                F90();
            else if (com[0] == 'F' && com[1] == '2')
                F180();
            else if (com[0] == 'F' && com.Length == 2)
                F90Inv();

            else if (com[0] == 'D' && com.Length == 1)
                D90(); 
            else if (com[0] == 'D' && com[1] == '2')
                D180();
            else if (com[0] == 'D' && com.Length == 2)
                D90Inv();

            else if (com[0] == 'L' && com.Length == 1)
                L90();
            else if (com[0] == 'L' && com[1] == '2')
                L180();
            else if (com[0] == 'L' && com.Length == 2)
                L90Inv(); 

            else if (com[0] == 'B' && com.Length == 1)
                B90();
            else if (com[0] == 'B' && com[1] == '2')
                B180();
            else if (com[0] == 'B' && com.Length == 2)
                B90Inv(); 
            
        }
    }

    public void F90()
    { 
        UnityEngine.Color f1 = GameObject.Find("F1").GetComponent<Renderer>().material.color;
        UnityEngine.Color f2 = GameObject.Find("F2").GetComponent<Renderer>().material.color;
        UnityEngine.Color f3 = GameObject.Find("F3").GetComponent<Renderer>().material.color;
        UnityEngine.Color f4 = GameObject.Find("F4").GetComponent<Renderer>().material.color;
        UnityEngine.Color f5 = GameObject.Find("F5").GetComponent<Renderer>().material.color;
        UnityEngine.Color f6 = GameObject.Find("F6").GetComponent<Renderer>().material.color;
        UnityEngine.Color f7 = GameObject.Find("F7").GetComponent<Renderer>().material.color;
        UnityEngine.Color f8 = GameObject.Find("F8").GetComponent<Renderer>().material.color;
        UnityEngine.Color f9 = GameObject.Find("F9").GetComponent<Renderer>().material.color;

        UnityEngine.Color l3 = GameObject.Find("L3").GetComponent<Renderer>().material.color;
        UnityEngine.Color l6 = GameObject.Find("L6").GetComponent<Renderer>().material.color;
        UnityEngine.Color l9 = GameObject.Find("L9").GetComponent<Renderer>().material.color;

        UnityEngine.Color r1 = GameObject.Find("R1").GetComponent<Renderer>().material.color;
        UnityEngine.Color r4 = GameObject.Find("R4").GetComponent<Renderer>().material.color;
        UnityEngine.Color r7 = GameObject.Find("R7").GetComponent<Renderer>().material.color;

        UnityEngine.Color u7 = GameObject.Find("U7").GetComponent<Renderer>().material.color;
        UnityEngine.Color u8 = GameObject.Find("U8").GetComponent<Renderer>().material.color;
        UnityEngine.Color u9 = GameObject.Find("U9").GetComponent<Renderer>().material.color;

        UnityEngine.Color d1 = GameObject.Find("D1").GetComponent<Renderer>().material.color;
        UnityEngine.Color d2 = GameObject.Find("D2").GetComponent<Renderer>().material.color;
        UnityEngine.Color d3 = GameObject.Find("D3").GetComponent<Renderer>().material.color;

        GameObject.Find("F3").GetComponent<Renderer>().material.color = f1;
        GameObject.Find("F6").GetComponent<Renderer>().material.color = f2;
        GameObject.Find("F9").GetComponent<Renderer>().material.color = f3;
        GameObject.Find("F2").GetComponent<Renderer>().material.color = f4;
        GameObject.Find("F8").GetComponent<Renderer>().material.color = f6;
        GameObject.Find("F1").GetComponent<Renderer>().material.color = f7;
        GameObject.Find("F4").GetComponent<Renderer>().material.color = f8;
        GameObject.Find("F7").GetComponent<Renderer>().material.color = f9;

        GameObject.Find("U9").GetComponent<Renderer>().material.color = l3;
        GameObject.Find("U8").GetComponent<Renderer>().material.color = l6;
        GameObject.Find("U7").GetComponent<Renderer>().material.color = l9;

        GameObject.Find("R1").GetComponent<Renderer>().material.color = u7;
        GameObject.Find("R4").GetComponent<Renderer>().material.color = u8;
        GameObject.Find("R7").GetComponent<Renderer>().material.color = u9;

        GameObject.Find("D3").GetComponent<Renderer>().material.color = r1;
        GameObject.Find("D2").GetComponent<Renderer>().material.color = r4;
        GameObject.Find("D1").GetComponent<Renderer>().material.color = r7;

        GameObject.Find("L3").GetComponent<Renderer>().material.color = d1;
        GameObject.Find("L6").GetComponent<Renderer>().material.color = d2;
        GameObject.Find("L9").GetComponent<Renderer>().material.color = d3;
    }

    public void F180()
    {
        F90();
        F90();
    }

    public void F90Inv() 
    {
        F90();
        F90();
        F90();
    }

    public void U90()
    {
        UnityEngine.Color u1 = GameObject.Find("U1").GetComponent<Renderer>().material.color;
        UnityEngine.Color u2 = GameObject.Find("U2").GetComponent<Renderer>().material.color;
        UnityEngine.Color u3 = GameObject.Find("U3").GetComponent<Renderer>().material.color;
        UnityEngine.Color u4 = GameObject.Find("U4").GetComponent<Renderer>().material.color;
        UnityEngine.Color u5 = GameObject.Find("U5").GetComponent<Renderer>().material.color;
        UnityEngine.Color u6 = GameObject.Find("U6").GetComponent<Renderer>().material.color;
        UnityEngine.Color u7 = GameObject.Find("U7").GetComponent<Renderer>().material.color;
        UnityEngine.Color u8 = GameObject.Find("U8").GetComponent<Renderer>().material.color;
        UnityEngine.Color u9 = GameObject.Find("U9").GetComponent<Renderer>().material.color;

        GameObject.Find("U3").GetComponent<Renderer>().material.color = u1;
        GameObject.Find("U6").GetComponent<Renderer>().material.color = u2;
        GameObject.Find("U9").GetComponent<Renderer>().material.color = u3;
        GameObject.Find("U2").GetComponent<Renderer>().material.color = u4;
        GameObject.Find("U8").GetComponent<Renderer>().material.color = u6;
        GameObject.Find("U1").GetComponent<Renderer>().material.color = u7;
        GameObject.Find("U4").GetComponent<Renderer>().material.color = u8;
        GameObject.Find("U7").GetComponent<Renderer>().material.color = u9;

        UnityEngine.Color l1 = GameObject.Find("L1").GetComponent<Renderer>().material.color;
        UnityEngine.Color l2 = GameObject.Find("L2").GetComponent<Renderer>().material.color;
        UnityEngine.Color l3 = GameObject.Find("L3").GetComponent<Renderer>().material.color;

        UnityEngine.Color r1 = GameObject.Find("R1").GetComponent<Renderer>().material.color;
        UnityEngine.Color r2 = GameObject.Find("R2").GetComponent<Renderer>().material.color;
        UnityEngine.Color r3 = GameObject.Find("R3").GetComponent<Renderer>().material.color;

        UnityEngine.Color b3 = GameObject.Find("B3").GetComponent<Renderer>().material.color;
        UnityEngine.Color b2 = GameObject.Find("B2").GetComponent<Renderer>().material.color;
        UnityEngine.Color b1 = GameObject.Find("B1").GetComponent<Renderer>().material.color;

        UnityEngine.Color f1 = GameObject.Find("F1").GetComponent<Renderer>().material.color;
        UnityEngine.Color f2 = GameObject.Find("F2").GetComponent<Renderer>().material.color;
        UnityEngine.Color f3 = GameObject.Find("F3").GetComponent<Renderer>().material.color;

        GameObject.Find("B1").GetComponent<Renderer>().material.color = l1;
        GameObject.Find("B2").GetComponent<Renderer>().material.color = l2;
        GameObject.Find("B3").GetComponent<Renderer>().material.color = l3;

        GameObject.Find("R1").GetComponent<Renderer>().material.color = b1;
        GameObject.Find("R2").GetComponent<Renderer>().material.color = b2;
        GameObject.Find("R3").GetComponent<Renderer>().material.color = b3;

        GameObject.Find("F1").GetComponent<Renderer>().material.color = r1;
        GameObject.Find("F2").GetComponent<Renderer>().material.color = r2;
        GameObject.Find("F3").GetComponent<Renderer>().material.color = r3;

        GameObject.Find("L3").GetComponent<Renderer>().material.color = f3;
        GameObject.Find("L2").GetComponent<Renderer>().material.color = f2;
        GameObject.Find("L1").GetComponent<Renderer>().material.color = f1;
    }

    public void U180()
    {
        U90();
        U90();
    }

    public void U90Inv()
    {
        U90();
        U90();
        U90();
    }

    public void D90()
    {
        UnityEngine.Color d1 = GameObject.Find("D1").GetComponent<Renderer>().material.color;
        UnityEngine.Color d2 = GameObject.Find("D2").GetComponent<Renderer>().material.color;
        UnityEngine.Color d3 = GameObject.Find("D3").GetComponent<Renderer>().material.color;
        UnityEngine.Color d4 = GameObject.Find("D4").GetComponent<Renderer>().material.color;
        UnityEngine.Color d5 = GameObject.Find("D5").GetComponent<Renderer>().material.color;
        UnityEngine.Color d6 = GameObject.Find("D6").GetComponent<Renderer>().material.color;
        UnityEngine.Color d7 = GameObject.Find("D7").GetComponent<Renderer>().material.color;
        UnityEngine.Color d8 = GameObject.Find("D8").GetComponent<Renderer>().material.color;
        UnityEngine.Color d9 = GameObject.Find("D9").GetComponent<Renderer>().material.color;

        GameObject.Find("D3").GetComponent<Renderer>().material.color = d1;
        GameObject.Find("D6").GetComponent<Renderer>().material.color = d2;
        GameObject.Find("D9").GetComponent<Renderer>().material.color = d3;
        GameObject.Find("D2").GetComponent<Renderer>().material.color = d4;
        GameObject.Find("D8").GetComponent<Renderer>().material.color = d6;
        GameObject.Find("D1").GetComponent<Renderer>().material.color = d7;
        GameObject.Find("D4").GetComponent<Renderer>().material.color = d8;
        GameObject.Find("D7").GetComponent<Renderer>().material.color = d9;

        UnityEngine.Color l7 = GameObject.Find("L7").GetComponent<Renderer>().material.color;
        UnityEngine.Color l8 = GameObject.Find("L8").GetComponent<Renderer>().material.color;
        UnityEngine.Color l9 = GameObject.Find("L9").GetComponent<Renderer>().material.color;

        UnityEngine.Color r7 = GameObject.Find("R7").GetComponent<Renderer>().material.color;
        UnityEngine.Color r8 = GameObject.Find("R8").GetComponent<Renderer>().material.color;
        UnityEngine.Color r9 = GameObject.Find("R9").GetComponent<Renderer>().material.color;
 
        UnityEngine.Color f7 = GameObject.Find("F7").GetComponent<Renderer>().material.color;
        UnityEngine.Color f8 = GameObject.Find("F8").GetComponent<Renderer>().material.color;
        UnityEngine.Color f9 = GameObject.Find("F9").GetComponent<Renderer>().material.color;
 
        UnityEngine.Color b7 = GameObject.Find("B7").GetComponent<Renderer>().material.color;
        UnityEngine.Color b8 = GameObject.Find("B8").GetComponent<Renderer>().material.color;
        UnityEngine.Color b9 = GameObject.Find("B9").GetComponent<Renderer>().material.color;

        GameObject.Find("F7").GetComponent<Renderer>().material.color = l7;
        GameObject.Find("F8").GetComponent<Renderer>().material.color = l8;
        GameObject.Find("F9").GetComponent<Renderer>().material.color = l9;

        GameObject.Find("R7").GetComponent<Renderer>().material.color = f7;
        GameObject.Find("R8").GetComponent<Renderer>().material.color = f8;
        GameObject.Find("R9").GetComponent<Renderer>().material.color = f9;

        GameObject.Find("B7").GetComponent<Renderer>().material.color = r7;
        GameObject.Find("B8").GetComponent<Renderer>().material.color = r8;
        GameObject.Find("B9").GetComponent<Renderer>().material.color = r9;
 
        GameObject.Find("L7").GetComponent<Renderer>().material.color = b7;
        GameObject.Find("L8").GetComponent<Renderer>().material.color = b8;
        GameObject.Find("L9").GetComponent<Renderer>().material.color = b9;
    }

    public void D180()
    {
        D90();
        D90();
    }

    public void D90Inv()
    {
        D90();
        D90();
        D90();
    }

    void R90()
    {
        UnityEngine.Color r1 = GameObject.Find("R1").GetComponent<Renderer>().material.color;
        UnityEngine.Color r2 = GameObject.Find("R2").GetComponent<Renderer>().material.color;
        UnityEngine.Color r3 = GameObject.Find("R3").GetComponent<Renderer>().material.color;
        UnityEngine.Color r4 = GameObject.Find("R4").GetComponent<Renderer>().material.color;
        UnityEngine.Color r5 = GameObject.Find("R5").GetComponent<Renderer>().material.color;
        UnityEngine.Color r6 = GameObject.Find("R6").GetComponent<Renderer>().material.color;
        UnityEngine.Color r7 = GameObject.Find("R7").GetComponent<Renderer>().material.color;
        UnityEngine.Color r8 = GameObject.Find("R8").GetComponent<Renderer>().material.color;
        UnityEngine.Color r9 = GameObject.Find("R9").GetComponent<Renderer>().material.color;

        GameObject.Find("R3").GetComponent<Renderer>().material.color = r1;
        GameObject.Find("R6").GetComponent<Renderer>().material.color = r2;
        GameObject.Find("R9").GetComponent<Renderer>().material.color = r3;
        GameObject.Find("R2").GetComponent<Renderer>().material.color = r4;
        GameObject.Find("R8").GetComponent<Renderer>().material.color = r6;
        GameObject.Find("R1").GetComponent<Renderer>().material.color = r7;
        GameObject.Find("R4").GetComponent<Renderer>().material.color = r8;
        GameObject.Find("R7").GetComponent<Renderer>().material.color = r9;

        UnityEngine.Color f3 = GameObject.Find("F3").GetComponent<Renderer>().material.color;
        UnityEngine.Color f6 = GameObject.Find("F6").GetComponent<Renderer>().material.color;
        UnityEngine.Color f9 = GameObject.Find("F9").GetComponent<Renderer>().material.color;

        UnityEngine.Color u3 = GameObject.Find("U3").GetComponent<Renderer>().material.color;
        UnityEngine.Color u6 = GameObject.Find("U6").GetComponent<Renderer>().material.color;
        UnityEngine.Color u9 = GameObject.Find("U9").GetComponent<Renderer>().material.color;

        UnityEngine.Color b1 = GameObject.Find("B1").GetComponent<Renderer>().material.color;
        UnityEngine.Color b4 = GameObject.Find("B4").GetComponent<Renderer>().material.color;
        UnityEngine.Color b7 = GameObject.Find("B7").GetComponent<Renderer>().material.color;

        UnityEngine.Color d3 = GameObject.Find("D3").GetComponent<Renderer>().material.color;
        UnityEngine.Color d6 = GameObject.Find("D6").GetComponent<Renderer>().material.color;
        UnityEngine.Color d9 = GameObject.Find("D9").GetComponent<Renderer>().material.color;

        GameObject.Find("F3").GetComponent<Renderer>().material.color = d3;
        GameObject.Find("F6").GetComponent<Renderer>().material.color = d6;
        GameObject.Find("F9").GetComponent<Renderer>().material.color = d9;

        GameObject.Find("U3").GetComponent<Renderer>().material.color = f3;
        GameObject.Find("U6").GetComponent<Renderer>().material.color = f6;
        GameObject.Find("U9").GetComponent<Renderer>().material.color = f9;

        GameObject.Find("B1").GetComponent<Renderer>().material.color = u9;
        GameObject.Find("B4").GetComponent<Renderer>().material.color = u6;
        GameObject.Find("B7").GetComponent<Renderer>().material.color = u3;

        GameObject.Find("D9").GetComponent<Renderer>().material.color = b1;
        GameObject.Find("D6").GetComponent<Renderer>().material.color = b4;
        GameObject.Find("D3").GetComponent<Renderer>().material.color = b7;
    }

    void R180()
    {
        R90();
        R90();
    }

    void R90Inv()
    {
        R90();
        R90();
        R90();
    }

    void L90()
    { 
        UnityEngine.Color l1 = GameObject.Find("L1").GetComponent<Renderer>().material.color;
        UnityEngine.Color l2 = GameObject.Find("L2").GetComponent<Renderer>().material.color;
        UnityEngine.Color l3 = GameObject.Find("L3").GetComponent<Renderer>().material.color;
        UnityEngine.Color l4 = GameObject.Find("L4").GetComponent<Renderer>().material.color;
        UnityEngine.Color l5 = GameObject.Find("L5").GetComponent<Renderer>().material.color;
        UnityEngine.Color l6 = GameObject.Find("L6").GetComponent<Renderer>().material.color;
        UnityEngine.Color l7 = GameObject.Find("L7").GetComponent<Renderer>().material.color;
        UnityEngine.Color l8 = GameObject.Find("L8").GetComponent<Renderer>().material.color;
        UnityEngine.Color l9 = GameObject.Find("L9").GetComponent<Renderer>().material.color;

        GameObject.Find("L3").GetComponent<Renderer>().material.color = l1;
        GameObject.Find("L6").GetComponent<Renderer>().material.color = l2;
        GameObject.Find("L9").GetComponent<Renderer>().material.color = l3;
        GameObject.Find("L2").GetComponent<Renderer>().material.color = l4;
        GameObject.Find("L8").GetComponent<Renderer>().material.color = l6;
        GameObject.Find("L1").GetComponent<Renderer>().material.color = l7;
        GameObject.Find("L4").GetComponent<Renderer>().material.color = l8;
        GameObject.Find("L7").GetComponent<Renderer>().material.color = l9;

        UnityEngine.Color f1 = GameObject.Find("F1").GetComponent<Renderer>().material.color;
        UnityEngine.Color f4 = GameObject.Find("F4").GetComponent<Renderer>().material.color;
        UnityEngine.Color f7 = GameObject.Find("F7").GetComponent<Renderer>().material.color;

        UnityEngine.Color d1 = GameObject.Find("D1").GetComponent<Renderer>().material.color;
        UnityEngine.Color d4 = GameObject.Find("D4").GetComponent<Renderer>().material.color;
        UnityEngine.Color d7 = GameObject.Find("D7").GetComponent<Renderer>().material.color;

        UnityEngine.Color b3 = GameObject.Find("B3").GetComponent<Renderer>().material.color;
        UnityEngine.Color b6 = GameObject.Find("B6").GetComponent<Renderer>().material.color;
        UnityEngine.Color b9 = GameObject.Find("B9").GetComponent<Renderer>().material.color;

        UnityEngine.Color u1 = GameObject.Find("U1").GetComponent<Renderer>().material.color;
        UnityEngine.Color u4 = GameObject.Find("U4").GetComponent<Renderer>().material.color;
        UnityEngine.Color u7 = GameObject.Find("U7").GetComponent<Renderer>().material.color;

        GameObject.Find("F1").GetComponent<Renderer>().material.color = u1;
        GameObject.Find("F4").GetComponent<Renderer>().material.color = u4;
        GameObject.Find("F7").GetComponent<Renderer>().material.color = u7;

        GameObject.Find("D1").GetComponent<Renderer>().material.color = f1;
        GameObject.Find("D4").GetComponent<Renderer>().material.color = f4;
        GameObject.Find("D7").GetComponent<Renderer>().material.color = f7;

        GameObject.Find("B3").GetComponent<Renderer>().material.color = d7;
        GameObject.Find("B6").GetComponent<Renderer>().material.color = d4;
        GameObject.Find("B9").GetComponent<Renderer>().material.color = d1;

        GameObject.Find("U7").GetComponent<Renderer>().material.color = b3;
        GameObject.Find("U4").GetComponent<Renderer>().material.color = b6;
        GameObject.Find("U1").GetComponent<Renderer>().material.color = b9;
    }

    void L180()
    {
        L90();
        L90();
    }

    void L90Inv()
    {
        L90();
        L90();
        L90();
    }

    void B90()
    { 
        UnityEngine.Color b1 = GameObject.Find("B1").GetComponent<Renderer>().material.color;
        UnityEngine.Color b2 = GameObject.Find("B2").GetComponent<Renderer>().material.color;
        UnityEngine.Color b3 = GameObject.Find("B3").GetComponent<Renderer>().material.color;
        UnityEngine.Color b4 = GameObject.Find("B4").GetComponent<Renderer>().material.color;
        UnityEngine.Color b5 = GameObject.Find("B5").GetComponent<Renderer>().material.color;
        UnityEngine.Color b6 = GameObject.Find("B6").GetComponent<Renderer>().material.color;
        UnityEngine.Color b7 = GameObject.Find("B7").GetComponent<Renderer>().material.color;
        UnityEngine.Color b8 = GameObject.Find("B8").GetComponent<Renderer>().material.color;
        UnityEngine.Color b9 = GameObject.Find("B9").GetComponent<Renderer>().material.color;

        GameObject.Find("B3").GetComponent<Renderer>().material.color = b1;
        GameObject.Find("B6").GetComponent<Renderer>().material.color = b2;
        GameObject.Find("B9").GetComponent<Renderer>().material.color = b3;
        GameObject.Find("B2").GetComponent<Renderer>().material.color = b4;
        GameObject.Find("B8").GetComponent<Renderer>().material.color = b6;
        GameObject.Find("B1").GetComponent<Renderer>().material.color = b7;
        GameObject.Find("B4").GetComponent<Renderer>().material.color = b8;
        GameObject.Find("B7").GetComponent<Renderer>().material.color = b9;

        UnityEngine.Color r3 = GameObject.Find("R3").GetComponent<Renderer>().material.color;
        UnityEngine.Color r6 = GameObject.Find("R6").GetComponent<Renderer>().material.color;
        UnityEngine.Color r9 = GameObject.Find("R9").GetComponent<Renderer>().material.color;

        UnityEngine.Color d7 = GameObject.Find("D7").GetComponent<Renderer>().material.color;
        UnityEngine.Color d8 = GameObject.Find("D8").GetComponent<Renderer>().material.color;
        UnityEngine.Color d9 = GameObject.Find("D9").GetComponent<Renderer>().material.color;

        UnityEngine.Color u1 = GameObject.Find("U1").GetComponent<Renderer>().material.color;
        UnityEngine.Color u2 = GameObject.Find("U2").GetComponent<Renderer>().material.color;
        UnityEngine.Color u3 = GameObject.Find("U3").GetComponent<Renderer>().material.color;

        UnityEngine.Color l1 = GameObject.Find("L1").GetComponent<Renderer>().material.color;
        UnityEngine.Color l4 = GameObject.Find("L4").GetComponent<Renderer>().material.color;
        UnityEngine.Color l7 = GameObject.Find("L7").GetComponent<Renderer>().material.color;

        GameObject.Find("R9").GetComponent<Renderer>().material.color = d7;
        GameObject.Find("R6").GetComponent<Renderer>().material.color = d8;
        GameObject.Find("R3").GetComponent<Renderer>().material.color = d9;

        GameObject.Find("D7").GetComponent<Renderer>().material.color = l1;
        GameObject.Find("D8").GetComponent<Renderer>().material.color = l4;
        GameObject.Find("D9").GetComponent<Renderer>().material.color = l7;

        GameObject.Find("L1").GetComponent<Renderer>().material.color = u3;
        GameObject.Find("L4").GetComponent<Renderer>().material.color = u2;
        GameObject.Find("L7").GetComponent<Renderer>().material.color = u1;

        GameObject.Find("U1").GetComponent<Renderer>().material.color = r3;
        GameObject.Find("U2").GetComponent<Renderer>().material.color = r6;
        GameObject.Find("U3").GetComponent<Renderer>().material.color = r9;

    }

    void B180()
    {
        B90();
        B90();
    }

    void B90Inv()
    {
        B90();
        B90();
        B90();
    }
}
