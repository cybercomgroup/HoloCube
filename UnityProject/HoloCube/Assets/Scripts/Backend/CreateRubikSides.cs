using System;
using System.Collections.Generic;
//using System.Numerics;
using System.Text;
using ColorThings;
using UnityEngine;

namespace Backend


{
    
    public class CreateRubikSides {
     
        public List<RubicColors> backYellow;
        public List<RubicColors> topBlue;
        public List<RubicColors> frontWhite;
        public List<RubicColors> leftOrange;
        public List<RubicColors> rightRed;
        public List<RubicColors> bottomGreen;
    
    
        
        public CubeColor[] backColors;
        public CubeColor[] topColors;
        public CubeColor[] frontColors;
        public CubeColor[] leftColors;
        public CubeColor[] rightColors;
        public CubeColor[] bottomColors;



    //Skicka in alla inscannade sidor och spara dem i arrayer
    
        public CreateRubikSides (
            List<RubicColors> backYellow,
            List<RubicColors> topBlue,
            List<RubicColors> frontWhite,
            List<RubicColors> leftOrange,
            List<RubicColors> rightRed,
            List<RubicColors> bottomGreen) {
            
            this.backYellow = backYellow;
            this.topBlue = topBlue;
            this.frontWhite = frontWhite;
            this.leftOrange = leftOrange;
            this.rightRed = rightRed;
            this.bottomGreen = bottomGreen;
 
 
           backColors = new CubeColor[8];
           topColors = new CubeColor[8];
           frontColors = new CubeColor[8];
           leftColors = new CubeColor[8];
           rightColors = new CubeColor[8];
           bottomColors = new CubeColor[8];
           
           //Loop för att lägga in rätt färger på rätt ställen som man kan bygga cubies. Gör till egen metod för att slippa så många rader?
           for (int i = 0; i < 8; i++) {
               backColors[i] = setColor(backYellow[i]);
           }
           for (int i = 0; i < 8; i++) {
               topColors[i] = setColor(topBlue[i]);
           }
           for (int i = 0; i < 8; i++) {
               frontColors[i] = setColor(frontWhite[i]);
           }
           for (int i = 0; i < 8; i++) {
               leftColors[i] = setColor(leftOrange[i]);
           }
           for (int i = 0; i < 8; i++) {
               rightColors[i] = setColor(rightRed[i]);
           }
           for (int i = 0; i < 8; i++) {
               bottomColors[i] = setColor(bottomGreen[i]);
           }
           
         
         RubikCube kuben = new RubikCube(createCube());
           List<Move> listanMedDragen = Solver.Solve(kuben);
           
           foreach(Move drag in listanMedDragen) {
               Debug.Log(drag.ToString());
           }
           
            
        }
        
        //Hjälpmetod för att sätta rätt färg så att du enkelt kan skapa cubies då färgerna inte är av samma typ (I think). Kanske göra om till en switch-case?
        public CubeColor setColor(ColorThings.RubicColors color) {
            if(color == ColorThings.RubicColors.Yellow) {
                return CubeColor.Yellow;
            } else if (color == ColorThings.RubicColors.White) {
                return CubeColor.White;
            } else if (color == ColorThings.RubicColors.Orange) {
                return CubeColor.Orange;
            } else if (color == ColorThings.RubicColors.Green) {
                return CubeColor.Green;
            } else if (color == ColorThings.RubicColors.Blue) {
                return CubeColor.Blue;
            } else if (color == ColorThings.RubicColors.Red) {
                return CubeColor.Red;
            } else {
                return CubeColor.Empty;
            }

        }
        
        
        //inte klar än hehe
        //Denna ska väl kanske ligga inne i RubicCube sen?
        //Gör varje sida till en ihopsättning av cubies. Blir det fel om man tar med ett hörn två gånger osv? Kolla upp!
        //Hur i helvete numrerar man dessa? jag tror jag gjort fel
        //jag valde att kopiera hans färger och hoppas på det bästa
        
        public Cubie[, ,] createCube () {
            CubeColor empty = CubeColor.Empty;
            Cubie[, ,] cube = new Cubie[3,3,3];

            //corners
            //white / front
            cube[0,0,0] = new Cubie(frontColors[2], leftColors[2], bottomColors[0]);
            cube[2,0,0] = new Cubie(frontColors[0], rightColors[0], bottomColors[2]);
            cube[0,2,0] = new Cubie(frontColors[4], leftColors[0], topColors[2]);
            cube[2,2,0] = new Cubie(frontColors[6], rightColors[2], topColors[0]);
            //yellow / back
            cube[0,0,2] = new Cubie(backColors[2], leftColors[4], bottomColors[6]);
            cube[2,0,2] = new Cubie(backColors[4], rightColors[6], bottomColors[4]);
            cube[0,2,2] = new Cubie(backColors[0], leftColors[6], topColors[4]);
            cube[2,2,2] = new Cubie(backColors[6], rightColors[4], topColors[6]);
            
            //middle pieces
            //white / front
            cube[1,0,0] = new Cubie(frontColors[1], empty, bottomColors[1]);
            cube[0,1,0] = new Cubie(frontColors[3], leftColors[1], empty);
            cube[2,1,0] = new Cubie(frontColors[7], rightColors[1], empty);
            cube[1,2,0] = new Cubie(frontColors[5], empty, topColors[1]);
            //yellow / back
            cube[1,0,2] = new Cubie(backColors[3], empty, bottomColors[5]);
            cube[0,1,2] = new Cubie(backColors[1], leftColors[5], empty);
            cube[2,1,2] = new Cubie(backColors[5], rightColors[5], empty);
            cube[1,2,2] = new Cubie(backColors[7], empty, topColors[5]);
            //the remaining sides
            cube[2,2,1] = new Cubie(empty, rightColors[3], topColors[7]);
            cube[0,2,1] = new Cubie(empty, leftColors[7], topColors[3]);
            cube[0,0,1] = new Cubie(empty, leftColors[3], bottomColors[7]);
            cube[2,0,1] = new Cubie(empty, rightColors[7], bottomColors[3]);
            
            //faces
            cube[1,1,2] = new Cubie(CubeColor.Yellow, empty, empty);
            cube[1, 1, 0] = new Cubie(CubeColor.White, empty, empty);
            cube[1, 2, 1] = new Cubie(empty, empty, CubeColor.Blue);
            cube[0, 1, 1] = new Cubie(empty, CubeColor.Orange, empty);
            cube[2, 1, 1] = new Cubie(empty, CubeColor.Red, empty);
            cube[1, 0, 1] = new Cubie(empty, empty, CubeColor.Green);
            
            //middle
            cube[1,1,1] = new Cubie(empty, empty, empty);

            
            return cube;
        }

    }



}
