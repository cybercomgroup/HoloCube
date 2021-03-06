﻿using System;

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//Cube on the facelet level
public class FaceCube
{
    public Color[] f = new Color[54];

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Map the corner positions to facelet positions. cornerFacelet[URF.ordinal()][0] e.g. gives the position of the
    // facelet in the URF corner position, which defines the orientation.<br>
    // cornerFacelet[URF.ordinal()][1] and cornerFacelet[URF.ordinal()][2] give the position of the other two facelets
    // of the URF corner (clockwise).
    public readonly static Facelet[,] cornerFacelet = { { Facelet.U9, Facelet.R1, Facelet.F3 }, { Facelet.U7, Facelet.F1, Facelet.L3 }, 
        { Facelet.U1, Facelet.L1, Facelet.B3 }, { Facelet.U3, Facelet.B1, Facelet.R3 }, { Facelet.D3, Facelet.F9, Facelet.R7 }, 
        { Facelet.D1, Facelet.L9, Facelet.F7 }, { Facelet.D7, Facelet.B9, Facelet.L7 }, { Facelet.D9, Facelet.R9, Facelet.B7 } };

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Map the edge positions to facelet positions. edgeFacelet[UR.ordinal()][0] e.g. gives the position of the facelet in
    // the UR edge position, which defines the orientation.<br>
    // edgeFacelet[UR.ordinal()][1] gives the position of the other facelet
    public readonly static Facelet[,] edgeFacelet = { { Facelet.U6, Facelet.R2 }, { Facelet.U8, Facelet.F2 }, { Facelet.U4, Facelet.L2 }, 
        { Facelet.U2, Facelet.B2 }, { Facelet.D6, Facelet.R8 }, { Facelet.D2, Facelet.F8 }, { Facelet.D4, Facelet.L8 }, { Facelet.D8, Facelet.B8 }, 
        { Facelet.F6, Facelet.R4 }, { Facelet.F4, Facelet.L6 }, { Facelet.B6, Facelet.L4 }, { Facelet.B4, Facelet.R6 } };

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Map the corner positions to facelet colors.
    public readonly static Color[,] cornerColor = { { Color.U, Color.R, Color.F }, { Color.U, Color.F, Color.L }, { Color.U, Color.L, Color.B }, 
        { Color.U, Color.B, Color.R }, { Color.D, Color.F, Color.R }, { Color.D, Color.L, Color.F }, { Color.D, Color.B, Color.L }, { Color.D, Color.R, Color.B } };

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Map the edge positions to facelet colors.
    public readonly static Color[,] edgeColor = { { Color.U, Color.R }, { Color.U, Color.F }, { Color.U, Color.L }, { Color.U, Color.B }, 
        { Color.D, Color.R }, { Color.D, Color.F }, { Color.D, Color.L }, { Color.D, Color.B },
            { Color.F, Color.R }, { Color.F, Color.L }, { Color.B, Color.L }, { Color.B, Color.R } };

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    public FaceCube()
    {
        string s = "UUUUUUUUURRRRRRRRRFFFFFFFFFDDDDDDDDDLLLLLLLLLBBBBBBBBB";
        for (int i = 0; i < 54; i++)
        {
            // U, R, F, D, L, B
            if ((Color)s[i] == Color.B)
                f[i] = Color.B;
            else if ((Color)s[i] == Color.D)
                f[i] = Color.D;
            else if ((Color)s[i] == Color.F)
                f[i] = Color.F;
            else if ((Color)s[i] == Color.L)
                f[i] = Color.L;
            else if ((Color)s[i] == Color.R)
                f[i] = Color.R;
            else if ((Color)s[i] == Color.U)
                f[i] = Color.U;
        }

    }

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Construct a facelet cube from a string
    public FaceCube(string cubeString)
    {
        string s = cubeString;
        for (int i = 0; i < cubeString.Length; i++)
        {
            //f[i] = Color.valueOf(cubeString.substring(i, i + 1));
            /*
            if ((Color)s[i] == Color.B)
                f[i] = Color.B;
            else if ((Color)s[i] == Color.D)
                f[i] = Color.D;
            else if ((Color)s[i] == Color.F)
                f[i] = Color.F;
            else if ((Color)s[i] == Color.L)
                f[i] = Color.L;
            else if ((Color)s[i] == Color.R)
                f[i] = Color.R;
            else if ((Color)s[i] == Color.U)
                f[i] = Color.U;
                */
            if (s[i] == 'B')
                f[i] = Color.B;
            else if (s[i] == 'D')
                f[i] = Color.D;
            else if (s[i] == 'F')
                f[i] = Color.F;
            else if (s[i] == 'L')
                f[i] = Color.L;
            else if (s[i] == 'R')
                f[i] = Color.R;
            else if (s[i] == 'U')
                f[i] = Color.U;
        }
    }

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Gives string representation of a facelet cube
    public string to_String()
    {
        string s = "";
        for (int i = 0; i < 54; i++)
            s += f[i].ToString();
        return s;
    }

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    // Gives CubieCube representation of a faceletcube
    public CubieCube toCubieCube()
    {
        int ori;
        CubieCube ccRet = new CubieCube();
        for (int i = 0; i < 8; i++)
            ccRet.cp[i] = Corner.URF;// invalidate corners
        for (int i = 0; i < 12; i++)
            ccRet.ep[i] = Edge.UR;// and edges
        Color col1, col2;
        foreach (Corner i in (Corner[])Enum.GetValues(typeof(Corner)))
        {
            // get the colors of the cubie at corner i, starting with U/D
            for (ori = 0; ori < 3; ori++)
                if((f[(int)cornerFacelet[(int)i, ori]] == Color.U) || f[(int)cornerFacelet[(int)i, ori]] == Color.D)
                    break;
            col1 = f[(int)cornerFacelet[(int)i, (ori + 1) % 3]];
            col2 = f[(int)cornerFacelet[(int)i, (ori + 2) % 3]];

            foreach (Corner j in (Corner[])Enum.GetValues(typeof(Corner)))
            {
                if (col1 == cornerColor[(int)j, 1] && col2 == cornerColor[(int)j, 2])
                {
                    // in cornerposition i we have cornercubie j
                    ccRet.cp[(int)i] = j;
                    ccRet.co[(int)i] = (ori % 3);
                    break;
                }
            }
        }
        foreach (Edge i in (Edge[])Enum.GetValues(typeof(Edge)))
            foreach (Edge j in (Edge[])Enum.GetValues(typeof(Edge)))
            {
                if (f[(int)edgeFacelet[(int)i, 0]] == edgeColor[(int)j,0]
                        && f[(int)edgeFacelet[(int)i, 1]] == edgeColor[(int)j, 1])
                {
                    ccRet.ep[(int)i] = j;
                    ccRet.eo[(int)i] = 0;
                    break;
                }
                if (f[(int)edgeFacelet[(int)i, 0]] == edgeColor[(int)j, 1]
                        && f[(int)edgeFacelet[(int)i, 1]] == edgeColor[(int)j, 0])
                {
                    ccRet.ep[(int)i] = j;
                    ccRet.eo[(int)i] = 1;
                    break;
                }
            }
        return ccRet;
    }
}
