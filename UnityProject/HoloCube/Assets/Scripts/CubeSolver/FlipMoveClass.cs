using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipMoveClass {
    // Move table for the flips of the edges
    // flip < 2048 in phase 1
    // flip = 0 in phase 2.

    //public static short[,] flipMove = new short[,] { { CoordCube.N_FLIP, CoordCube.N_MOVE } };
    //public static short[,] flipMove = new short[,] { { CoordCube.N_FLIP, CoordCube.N_MOVE } }; // 2048, 18
    public static short[,] flipMove = new short[CoordCube.N_FLIP, CoordCube.N_MOVE];
    static FlipMoveClass()
    {
        CubieCube a = new CubieCube();
        for (short i = 0; i < CoordCube.N_FLIP; i++)
        {
            a.setFlip(i);
            for (int j = 0; j < 6; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    a.edgeMultiply(CubieCube.moveCube[j]);
                    try
                    {
                        flipMove[i, (3 * j + k)] = a.getFlip(); // [2047, 17]
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e);
                        return;
                    }
                    
                }
                a.edgeMultiply(CubieCube.moveCube[j]);
                // a
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
