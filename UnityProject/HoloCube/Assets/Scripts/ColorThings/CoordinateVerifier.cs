using System;
using System.Collections.Generic;
using OpenCVForUnity;
using OpenCVForUnityExample;
using UnityEngine;
using ColorMine.ColorSpaces;
using Rect = OpenCVForUnity.Rect;
using System.Collections;
using System.Linq;

public class CoordinateVerifier : MonoBehaviour {
    public static List<Rect> fixedRect = new List<Rect>();
    public static List<Rect> tempFix = new List<Rect>();
    public static List<Rect> L1 = new List<Rect>();
    public static List<Rect> L2 = new List<Rect>();
    public static List<Rect> L3 = new List<Rect>();

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static List<Rect> Verify(List<Rect> rects) {
        fixedRect = rects.OrderBy(o => o.y).ToList();
        fixedRect = ListFixer(fixedRect);
        return fixedRect;
        }

    public static List<Rect> ListFixer(List<Rect> rects)
    {
        L1.Add(rects[0]);
        L1.Add(rects[1]);
        L1.Add(rects[2]);

        L2.Add(rects[3]);
        L2.Add(rects[4]);
        L2.Add(rects[5]);

        L3.Add(rects[6]);
        L3.Add(rects[7]);
        L3.Add(rects[8]);

        L2 = L1.OrderBy(o => o.x).ToList();
        L2 = L2.OrderBy(o => o.x).ToList();
        L2 = L2.OrderBy(o => o.x).ToList();

        tempFix.AddRange(L1);
        tempFix.AddRange(L2);
        tempFix.AddRange(L3);

        return tempFix;
    }


    }

