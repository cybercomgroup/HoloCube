using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Solver
    {
        /// <summary>
        /// Tries to solve the cube. Returns a list of
        /// moves required for the solution.
        /// </summary>
        /// <param name="cube">The Rubik'scube object.</param>
        /// <returns>List of all moves for the solution.</returns>
        public static List<Move> Solve(RubikCube cube)
        {
            List<Move> moves = new List<Move>();

            // Top corners
            moves.AddRange(SolveTopCorners(cube));

            // Top edges
            moves.AddRange(SolveTopEdges(cube));

            // Roate mid layer
            moves.AddRange(SolveMidLayer(cube));
            
            return Move.Trim(moves);
        }

        private static List<Move> SolveTopCorners(RubikCube cube)
        {
            List<Move> moves = new List<Move>();

            moves.AddRange(PlaceTopCorner(cube, true));
            cube.RotateY(false);
            moves.AddRange(PlaceTopCorner(cube, false));
            cube.RotateY(false);
            moves.AddRange(PlaceTopCorner(cube, false));
            cube.RotateY(false);
            moves.AddRange(PlaceTopCorner(cube, false));
            cube.RotateY(false);

            return moves;
        }

        private static List<Move> PlaceTopCorner(RubikCube cube, bool firstCorner)
        {
            List<Move> moves = new List<Move>();
            CubePos pos = new CubePos(2,2,0);
            CubeColor topColor = cube.FaceColor(CubeSide.Top);
            CubeColor frontColor = cube.FaceColor(CubeSide.Front);
            CubeColor rightColor = cube.FaceColor(CubeSide.Right);
            Tuple<Cubie, CubePos> cubie = cube.FindCorner(topColor, frontColor, rightColor);

            if (CubePos.EqualPos(cubie.Item2, pos) && cubie.Item1.xColor == frontColor && cubie.Item1.yColor == rightColor && cubie.Item1.zColor == topColor)
            {
                return moves;
            }

            // Adjust the cubie to a valid start location.
            while (cubie.Item2.Z != pos.Z || cubie.Item2.X != pos.X)
            {
                if(cubie.Item2.Y == 0)
                {
                    ExecuteMove(cube,CubeAction.DI, moves);
                }
                else if(firstCorner)
                {
                    ExecuteMove(cube, "U", moves);
                }
                else if (cubie.Item2.X == 0)
                {
                    ExecuteMove(cube, "B DI BI", moves);
                }
                else
                {
                    ExecuteMove(cube, "BI DI B", moves);
                }
                
                cubie = cube.FindCorner(topColor, frontColor, rightColor);

            }

            if(cubie.Item2.Y == 0)
            {
                if(cubie.Item1.xColor == topColor)
                {
                    ExecuteMove(cube, "DI RI D R", moves);
                }
                else if (cubie.Item1.yColor == topColor)
                {
                    ExecuteMove(cube, "RI DI R", moves);
                }
                else if(cubie.Item1.zColor == topColor)
                {
                    ExecuteMove(cube, "RI D R D D RI DI R", moves);
                }
            }
            else
            {
                if (cubie.Item1.xColor == topColor)
                {
                    ExecuteMove(cube, "F D F D D RI D R", moves);
                }
                else if(cubie.Item1.yColor == topColor)
                {
                    ExecuteMove(cube, "RI DI R D RI DI R", moves);
                }
            }

           
            return moves;
        }

        private static List<Move> SolveTopEdges(RubikCube cube)
        {
            List<Move> moves = new List<Move>();
            moves.AddRange(SolveTopEdge(cube));
            cube.RotateY(true);
            moves.AddRange(SolveTopEdge(cube));
            cube.RotateY(true);
            moves.AddRange(SolveTopEdge(cube));
            cube.RotateY(true);
            moves.AddRange(SolveTopEdge(cube));
            cube.RotateY(true);


            return moves;
        }

        private static List<Move> SolveTopEdge(RubikCube cube)
        {
            List<Move> moves = new List<Move>();
            CubePos pos = new CubePos(1, 2, 0);
            CubeColor topColor = cube.FaceColor(CubeSide.Top);
            CubeColor frontColor = cube.GetCubicColor(new CubePos(0, 2, 0), 0);
            Tuple<Cubie, CubePos> cubie = cube.FindEdge(topColor, frontColor);
            // If already in place
            if(CubePos.EqualPos(cubie.Item2,pos) && cubie.Item1.xColor == frontColor && cubie.Item1.zColor == topColor)
            {
                return moves;
            }

            while (cubie.Item2.Z != pos.Z || cubie.Item2.X == 0)
            {
                // In Bottom layer
                if (cubie.Item2.Y == 0)
                {
                    ExecuteMove(cube, "DI", moves);
                }
                // In mid layer
                else if (cubie.Item2.Y == 1)
                {
                    ExecuteMove(cube, "EI", moves);
                }
                // In top layer
                else
                {
                    if(cubie.Item2.X == 1 && cubie.Item2.Z == 0)
                    {
                        break;
                    }
                    else if(cubie.Item2.X == 0 && cubie.Item2.Z == 1)
                    {
                        ExecuteMove(cube, "SI D S", moves);
                    }
                    else if (cubie.Item2.X == 2 && cubie.Item2.Z == 1)
                    {
                        ExecuteMove(cube, "S DI SI", moves);
                    }
                    else
                    {
                        ExecuteMove(cube, "MI DI M", moves);
                    }
                }
                
                cubie = cube.FindEdge(topColor, frontColor);

            }

            if(cubie.Item1.xColor == topColor)
            {
                // 2
                if (cubie.Item2.Y == 0)
                {
                    ExecuteMove(cube, "DI M D MI", moves);
                }
                // 4
                else if (cubie.Item2.Y == 1)
                {
                    ExecuteMove(cube, "E FI EI EI F", moves);
                }
                // 5
                else
                {
                    ExecuteMove(cube, "M DI DI MI DI M D MI", moves);
                }
            }
            // 1
            else if (cubie.Item1.zColor == topColor)
            {
                ExecuteMove(cube, "M DI DI MI", moves);
            }
            // 3
            else
            {
                ExecuteMove(cube, "E F EI FI", moves);
            }

            return moves;
        }

        private static List<Move> SolveMidLayer(RubikCube cube)
        {
            List<Move> moves = new List<Move>();

            while(cube.GetCubicColor(new CubePos(1,2,0), 0) != cube.FaceColor(CubeSide.Front))
            {
                ExecuteMove(cube, CubeAction.EI, moves);
            }

            return moves;
        }

        private static void ExecuteMove(RubikCube cube, CubeAction action, List<Move> moves)
        {
            Move move = new Move(action, cube.FaceColor(CubeSide.Front), cube.FaceColor(CubeSide.Top));
            cube.ExecuteMove(move);
            moves.Add(move);
        }

        private static void ExecuteMove(RubikCube cube, String actionString, List<Move> moves)
        {
            string[] actionStrings = actionString.Split(' ');
            foreach(String s in actionStrings)
            {
                ExecuteMove(cube, StringToCubeAction(s), moves);
            }
        }

        private static CubeAction StringToCubeAction(String s)
        {
            return (CubeAction)Enum.Parse(typeof(CubeAction), s);
        }
        
    }
}
