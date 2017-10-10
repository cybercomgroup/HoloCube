using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Solver
    {
        public static List<Move> Solve(RubikCube cube)
        {
            List<Move> moves = new List<Move>();

            // White cross
            moves.AddRange(SolveCross(cube, CubeColor.White));

            return moves;
        }

        private static List<Move> SolveCross(RubikCube cube, CubeColor crossColor)
        {
            List<Move> moves = new List<Move>();

            /*Tuple<Piece, CubePos> pLeft = cube.GetPiece(CubeColor.Orange, crossColor);
            Tuple<Piece, CubePos> pRight = cube.GetPiece(CubeColor.Red, crossColor);
            Tuple<Piece, CubePos> pUp = cube.GetPiece(CubeColor.Blue, crossColor);
            Tuple<Piece, CubePos> pDown = cube.GetPiece(CubeColor.Green, crossColor);

            // Left edge
            //CrossLeftOrRight(cube, pLeft, crossColor, CubeColor.Orange);

            // Right edge
            //CrossLeftOrRight(cube, pRight, crossColor, CubeColor.Red);*/

            // Start with corners

            //Tuple<Piece, CubePos> p1 = ;

            moves.AddRange(PlaceTopCorner(cube));
            cube.RotateY();
            moves.AddRange(PlaceTopCorner(cube));
            cube.RotateY();
            moves.AddRange(PlaceTopCorner(cube));
            cube.RotateY();
            moves.AddRange(PlaceTopCorner(cube));
            cube.RotateY();
            //ExecuteMove(cube, CubeAction.Up, moves);

            /*# place the UP-LEFT piece

            self._cross_left_or_right(fl_piece, self.left_piece, self.cube.left_color(), "L L", "E L Ei Li")
            self._cross_left_or_right(fr_piece, self.right_piece, self.cube.right_color(), "R R", "Ei R E Ri")

            self.move("Z")
            self._cross_left_or_right(fd_piece, self.down_piece, self.cube.left_color(), "L L", "E L Ei Li")
            self._cross_left_or_right(fu_piece, self.up_piece, self.cube.right_color(), "R R", "Ei R E Ri")
            self.move("Zi")*/

            return moves;
        }

        private static List<Move> PlaceTopCorner(RubikCube cube)
        {
            List<Move> moves = new List<Move>();
            CubePos pos = new CubePos(2,2,0);
            CubeColor topColor = cube.FaceColor(CubeSide.Top);
            CubeColor frontColor = cube.FaceColor(CubeSide.Front);
            CubeColor rightColor = cube.FaceColor(CubeSide.Right);
            Tuple<Cubie, CubePos> cubie = cube.FindCorner(topColor, frontColor, rightColor);

            int counter = 0;
            while (cubie.Item2.Z != pos.Z || cubie.Item2.X != pos.X)
            {
                if(cubie.Item2.Y == 0)
                {
                    ExecuteMove(cube,CubeAction.Down, moves);
                }
                else if(cubie.Item2.Y == 1)
                {
                    ExecuteMove(cube, CubeAction.Mid, moves);
                }
                else
                {
                    ExecuteMove(cube, CubeAction.Up, moves);
                }
                
                counter++;
                if (counter > 4)
                {
                    Console.WriteLine("Unsolvable");
                    return null;
                }

                cubie = cube.FindCorner(topColor, frontColor, rightColor);

            }

            if (cubie.Item1.xColor == frontColor && cubie.Item1.yColor == rightColor && cubie.Item1.zColor == topColor)
            {
                return moves;
            }

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
