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

            //this is now the prefered way to hold the cube.
            var topColorByMiddleColor = new Dictionary<CubeColor, CubeColor>
            {
                {CubeColor.White, CubeColor.Green},
                {CubeColor.Red, CubeColor.Blue},
                {CubeColor.Orange, CubeColor.Blue},
                {CubeColor.Green, CubeColor.White},
                {CubeColor.Yellow, CubeColor.Green},
                {CubeColor.Blue, CubeColor.Orange},
            };

            foreach (CubeSide side in faces.Keys)
            {
                var wantedTopColor = topColorByMiddleColor[faces[side].MiddleColor];
                cube.rotateCubeToChosenColor(faces[side].MiddleColor, faces[side].TopColor);
                var zRotations = cube.rotateCubeToChosenColor(faces[side].MiddleColor, wantedTopColor);
                faces[side].RotateColorsClockwise(zRotations);
            }

            // Here all faces are in the right orientation. 

            //Rotate the cube to be in a prefered way.
            var middle = faces[CubeSide.Front].MiddleColor;
            var wantedTop = topColorByMiddleColor[faces[CubeSide.Front].MiddleColor];
            cube.rotateCubeToChosenColor(middle, wantedTop);

            //todo 
            /*
             * This is now where we can start to add cubies to the cube.
             * eg..
             * 
             *            cube.Cubies[0,0,2] = new Cubie(faces[CubeSide.Front].Colors[4],faces[CubeSide.Bottom].Colors[4],faces[CubeSide.Right].Colors[4]);
             * 
             */
            
            //faces[CubeSide.Front].Colors[6]
            
            // Corners
            cube.Cubies[0, 0, 0] = new Cubie(faces[CubeSide.Front].Colors[6], faces[CubeSide.Left].Colors[4], faces[CubeSide.Bottom].Colors[0]);
            cube.Cubies[2, 0, 0] = new Cubie(faces[CubeSide.Front].Colors[4], faces[CubeSide.Right].Colors[6], faces[CubeSide.Bottom].Colors[2]);
            cube.Cubies[0, 2, 0] = new Cubie(faces[CubeSide.Front].Colors[0], faces[CubeSide.Left].Colors[2], faces[CubeSide.Top].Colors[6]);
            cube.Cubies[2, 2, 0] = new Cubie(faces[CubeSide.Front].Colors[2], faces[CubeSide.Right].Colors[0], faces[CubeSide.Top].Colors[4]);
            
            cube.Cubies[0, 0, 2] = new Cubie(faces[CubeSide.Back].Colors[4], faces[CubeSide.Left].Colors[6], faces[CubeSide.Bottom].Colors[6]);
            cube.Cubies[2, 0, 2] = new Cubie(CubeColor.Yellow, CubeColor.Red, CubeColor.Green);
            cube.Cubies[0, 2, 2] = new Cubie(CubeColor.Yellow, CubeColor.Orange, CubeColor.Blue);
            cube.Cubies[2, 2, 2] = new Cubie(CubeColor.Yellow, CubeColor.Red, CubeColor.Blue);

            // Middle pieces
            cube.Cubies[1, 0, 0] = new Cubie(CubeColor.White, CubeColor.Empty, CubeColor.Green);
            cube.Cubies[0, 1, 0] = new Cubie(CubeColor.White, CubeColor.Orange, CubeColor.Empty);
            cube.Cubies[2, 1, 0] = new Cubie(CubeColor.White, CubeColor.Red, CubeColor.Empty);
            cube.Cubies[1, 2, 0] = new Cubie(CubeColor.White, CubeColor.Empty, CubeColor.Blue);

            cube.Cubies[0, 0, 1] = new Cubie(CubeColor.Empty, CubeColor.Orange, CubeColor.Green);
            cube.Cubies[2, 0, 1] = new Cubie(CubeColor.Empty, CubeColor.Red, CubeColor.Green);
            cube.Cubies[0, 2, 1] = new Cubie(CubeColor.Empty, CubeColor.Orange, CubeColor.Blue);
            cube.Cubies[2, 2, 1] = new Cubie(CubeColor.Empty, CubeColor.Red, CubeColor.Blue);

            cube.Cubies[1, 0, 2] = new Cubie(CubeColor.Yellow, CubeColor.Empty, CubeColor.Green);
            cube.Cubies[0, 1, 2] = new Cubie(CubeColor.Yellow, CubeColor.Orange, CubeColor.Empty);
            cube.Cubies[2, 1, 2] = new Cubie(CubeColor.Yellow, CubeColor.Red, CubeColor.Empty);
            cube.Cubies[1, 2, 2] = new Cubie(CubeColor.Yellow, CubeColor.Empty, CubeColor.Blue);

            // Faces
            cube.Cubies[1, 1, 2] = new Cubie(faces[CubeSide.Back].MiddleColor, CubeColor.Empty, CubeColor.Empty);
            cube.Cubies[1, 2, 1] = new Cubie(CubeColor.Empty, CubeColor.Empty, faces[CubeSide.Bottom].MiddleColor);
            cube.Cubies[1, 1, 0] = new Cubie(faces[CubeSide.Front].MiddleColor, CubeColor.Empty, CubeColor.Empty);
            cube.Cubies[0, 1, 1] = new Cubie(CubeColor.Empty, faces[CubeSide.Right].MiddleColor, CubeColor.Empty);
            cube.Cubies[2, 1, 1] = new Cubie(CubeColor.Empty, faces[CubeSide.Left].MiddleColor, CubeColor.Empty);
            cube.Cubies[1, 0, 1] = new Cubie(CubeColor.Empty, CubeColor.Empty, faces[CubeSide.Top].MiddleColor);

            Console.WriteLine(cube);
            
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