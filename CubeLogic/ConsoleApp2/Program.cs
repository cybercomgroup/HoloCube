using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
//          Solve();
            Translator t = new Translator();
            var cubeAndFaceDict = t.setSides();
            var cube = cubeAndFaceDict.Item1;
            var faces = cubeAndFaceDict.Item2;
//            int zRotations;


            //this is now the prefered way to hold the cube.
            var wantedOrientation = new Dictionary<CubeColor, CubeColor>
            {
                {CubeColor.White,CubeColor.Green},
                {CubeColor.Red,CubeColor.Blue},
                {CubeColor.Orange,CubeColor.Blue},
                {CubeColor.Green,CubeColor.White},
                {CubeColor.Yellow,CubeColor.Green},
                {CubeColor.Blue,CubeColor.Orange},
            };
            
            
            foreach (CubeSide side in faces.Keys)
            {
                var wantedTopColor = wantedOrientation[faces[side].MiddleColor];
                cube.rotateCubeToChosenColor(faces[side].MiddleColor,  faces[side].TopColor);
                var zRotations = cube.rotateCubeToChosenColor(faces[side].MiddleColor,wantedTopColor);

                Console.WriteLine($"Before: face:{faces[side].MiddleColor}, top: {faces[side].TopColor}");
                
                for (int i = 0; i < faces[side].Colors.Count; i++)
                {
                    Console.WriteLine(faces[side].Colors[i]);
                }
                
                faces[side].RotateColorsClockwise(zRotations);
            
                
                Console.WriteLine($"After: face:{faces[side].MiddleColor}, top: {wantedTopColor}");

                
                for (int i = 0; i < faces[side].Colors.Count; i++)
                {
                    Console.WriteLine(faces[side].Colors[i]);
                }
                Console.WriteLine("\n");
             }
            
            //here is all faces in the right orientation.

            //Rotate the cube to be in a prefered way.
            var middle = faces[CubeSide.Front].MiddleColor;
            var wantedTop = wantedOrientation[faces[CubeSide.Front].MiddleColor];
            cube.rotateCubeToChosenColor(middle,wantedTop);

            //todo 
            /*
             * This is now where we can start to add cubies to the cube.
             * eg..
             *            cube.Cubies[0,0,2] = new Cubie(faces[CubeSide.Front].Colors[4],faces[CubeSide.Bottom].Colors[4],faces[CubeSide.Right].Colors[4]);
             * 
             */
            

          

            //            cube.ExecuteMove(new Move(CubeAction.U,CubeColor.White,CubeColor.Blue));
            //            cube.ExecuteMove(new Move(CubeAction.F,CubeColor.White,CubeColor.Blue));
        }


        private static void Solve()
        {
            RubikCube cube = new RubikCube();

            cube.Scramble();
            List<Move> moves = Solver.Solve(cube);
            Console.WriteLine(cube);
            Console.WriteLine("\nTotal number of moves: " + moves.Count + "\n");
            foreach (Move move in moves)
            {
                Console.WriteLine(move);
            }
            Console.ReadKey();
        }
    }
}