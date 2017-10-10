using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Solver
    {
        public static List<Move> Solve(RubikCube cube)
        {
            List<Move> moves = new List<Move>();

            // Top corners
            moves.AddRange(SolveTopCorners(cube, CubeColor.White));

            return moves;
        }

        private static List<Move> SolveTopCorners(RubikCube cube, CubeColor crossColor)
        {
            List<Move> moves = new List<Move>();

            moves.AddRange(PlaceTopCorner(cube, true));
            cube.RotateY();
            moves.AddRange(PlaceTopCorner(cube, false));
            cube.RotateY();
            moves.AddRange(PlaceTopCorner(cube, false));
            cube.RotateY();
            moves.AddRange(PlaceTopCorner(cube, false));
            cube.RotateY();

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

            while (cubie.Item2.Z != pos.Z || cubie.Item2.X != pos.X)
            {
                if(cubie.Item2.Y == 0)
                {
                    ExecuteMove(cube,CubeAction.Down, moves);
                }
                else
                {
                    if (firstCorner)
                    {
                        ExecuteMove(cube, CubeAction.Up, moves);
                    }
                    else
                    {
                        if(cubie.Item2.X == 0)
                        {
                            ExecuteMove(cube, CubeAction.Back, moves);
                            ExecuteMove(cube, CubeAction.Down, moves);
                            ExecuteMove(cube, CubeAction.BackI, moves);
                        }
                        else
                        {
                            ExecuteMove(cube, CubeAction.BackI, moves);
                            ExecuteMove(cube, CubeAction.Down, moves);
                            ExecuteMove(cube, CubeAction.Back, moves);
                        }
                    }
                }
                
                cubie = cube.FindCorner(topColor, frontColor, rightColor);

            }

            /*if (cubie.Item1.xColor == frontColor && cubie.Item1.yColor == rightColor && cubie.Item1.zColor == topColor)
            {
                return moves;
            }*/

            if(cubie.Item2.Y == 0)
            {
                if(cubie.Item1.xColor == topColor)
                {
                    ExecuteMove(cube, CubeAction.Down, moves);
                    ExecuteMove(cube, CubeAction.RightI, moves);
                    ExecuteMove(cube, CubeAction.DownI, moves);
                    ExecuteMove(cube, CubeAction.Right, moves);
                }
                else if (cubie.Item1.yColor == topColor)
                {
                    ExecuteMove(cube, CubeAction.RightI, moves);
                    ExecuteMove(cube, CubeAction.Down, moves);
                    ExecuteMove(cube, CubeAction.Right, moves);
                }
                else if(cubie.Item1.zColor == topColor)
                {
                    ExecuteMove(cube, CubeAction.RightI, moves);
                    ExecuteMove(cube, CubeAction.DownI, moves);
                    ExecuteMove(cube, CubeAction.Right, moves);
                    ExecuteMove(cube, CubeAction.DownI, moves);
                    ExecuteMove(cube, CubeAction.DownI, moves);
                    ExecuteMove(cube, CubeAction.RightI, moves);
                    ExecuteMove(cube, CubeAction.Down, moves);
                    ExecuteMove(cube, CubeAction.Right, moves);
                }
            }
            else
            {
                if (cubie.Item1.xColor == topColor)
                {
                    ExecuteMove(cube, CubeAction.Front, moves);
                    ExecuteMove(cube, CubeAction.DownI, moves);
                    ExecuteMove(cube, CubeAction.Front, moves);
                    ExecuteMove(cube, CubeAction.DownI, moves);
                    ExecuteMove(cube, CubeAction.DownI, moves);
                    ExecuteMove(cube, CubeAction.RightI, moves);
                    ExecuteMove(cube, CubeAction.DownI, moves);
                    ExecuteMove(cube, CubeAction.Right, moves);
                }
                else if(cubie.Item1.yColor == topColor)
                {
                    ExecuteMove(cube, CubeAction.RightI, moves);
                    ExecuteMove(cube, CubeAction.Down, moves);
                    ExecuteMove(cube, CubeAction.Right, moves);
                    ExecuteMove(cube, CubeAction.DownI, moves);
                    ExecuteMove(cube, CubeAction.RightI, moves);
                    ExecuteMove(cube, CubeAction.Down, moves);
                    ExecuteMove(cube, CubeAction.Right, moves);
                }
            }

            return moves;
        }

        private static List<Move> CrossLeftOrRight(RubikCube cube, Tuple<Cubie, CubePos> piece, CubeColor crossColor, CubeColor sideColor)
        {
            /*// If already correct
            if (piece.Item1.xColor == crossColor && piece.Item1.yColor == sideColor)
            {
                return null;
            }

            List<Move> moves = new List<Move>();

            // Check piece at z = 0
            if(piece.Item2.Z == 0)
            {
                CubePos pos;
                int counter = 0;
                while((pos = cube.FindEdge(piece.Item1)) != new CubePos(0, 1, 0))
                {
                    ExecuteMove(cube, CubeAction.Front, moves);
                    counter++;
                    if(counter > 4)
                    {
                        Console.WriteLine("Stuck in loop");
                        return null;
                    }
                }
            }
            else
            {

            }*/

            return null;

        }

        private static void ExecuteMove(RubikCube cube, CubeAction action, List<Move> moves)
        {
            Move move = new Move(action);
            cube.ExecuteMove(move);
            moves.Add(move);
        }
        
    }
}
