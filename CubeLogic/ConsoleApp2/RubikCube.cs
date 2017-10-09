using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ConsoleApp2
{
    public class RubikCube
    {
        private Dictionary<CubeAction, Action> moveMap = new Dictionary<CubeAction, Action>();
        private Piece[,,] pieces = new Piece[3,3,3];
        
        public RubikCube()
        {

            moveMap.Add(CubeAction.Up, () => RotateUp(false));
            moveMap.Add(CubeAction.UpI, () => RotateUp(true));
            moveMap.Add(CubeAction.Down, () => RotateDown(false));
            moveMap.Add(CubeAction.DownI, () => RotateDown(true));
            moveMap.Add(CubeAction.Left, () => RotateLeft(false));
            moveMap.Add(CubeAction.LeftI, () => RotateLeft(true));
            moveMap.Add(CubeAction.Right, () => RotateRight(false));
            moveMap.Add(CubeAction.RightI, () => RotateRight(true));
            moveMap.Add(CubeAction.Front, () => RotateFront(false));
            moveMap.Add(CubeAction.FrontI, () => RotateFront(true));


            // Corners
            pieces[0, 0, 0] = new Piece(CubeColor.White, CubeColor.Orange, CubeColor.Green);
            pieces[2, 0, 0] = new Piece(CubeColor.White, CubeColor.Red, CubeColor.Green);
            pieces[0, 2, 0] = new Piece(CubeColor.White, CubeColor.Orange, CubeColor.Blue);
            pieces[2, 2, 0] = new Piece(CubeColor.White, CubeColor.Red, CubeColor.Blue);
            pieces[0, 0, 2] = new Piece(CubeColor.Yellow, CubeColor.Orange, CubeColor.Green);
            pieces[2, 0, 2] = new Piece(CubeColor.Yellow, CubeColor.Red, CubeColor.Green);
            pieces[0, 2, 2] = new Piece(CubeColor.Yellow, CubeColor.Orange, CubeColor.Blue);
            pieces[2, 2, 2] = new Piece(CubeColor.Yellow, CubeColor.Red, CubeColor.Blue);

            // Middle pieces
            pieces[1, 0, 0] = new Piece(CubeColor.White, CubeColor.Empty, CubeColor.Green);
            pieces[0, 1, 0] = new Piece(CubeColor.White, CubeColor.Orange, CubeColor.Empty);
            pieces[2, 1, 0] = new Piece(CubeColor.White, CubeColor.Red, CubeColor.Empty);
            pieces[1, 2, 0] = new Piece(CubeColor.White, CubeColor.Empty, CubeColor.Blue);

            pieces[0, 0, 1] = new Piece(CubeColor.Empty, CubeColor.Orange, CubeColor.Green);
            pieces[2, 0, 1] = new Piece(CubeColor.Empty, CubeColor.Red, CubeColor.Green);
            pieces[0, 2, 1] = new Piece(CubeColor.Empty, CubeColor.Orange, CubeColor.Blue);
            pieces[2, 2, 1] = new Piece(CubeColor.Empty, CubeColor.Red, CubeColor.Blue);

            pieces[1, 0, 2] = new Piece(CubeColor.Yellow, CubeColor.Empty, CubeColor.Green);
            pieces[0, 1, 2] = new Piece(CubeColor.Yellow, CubeColor.Orange, CubeColor.Empty);
            pieces[2, 1, 2] = new Piece(CubeColor.Yellow, CubeColor.Red, CubeColor.Empty);
            pieces[1, 2, 2] = new Piece(CubeColor.Yellow, CubeColor.Empty, CubeColor.Blue);


            // Faces
            pieces[1, 1, 2] = new Piece(CubeColor.Yellow, CubeColor.Empty, CubeColor.Empty);
            pieces[1, 2, 1] = new Piece(CubeColor.Empty, CubeColor.Empty, CubeColor.Blue);
            pieces[1, 1, 0] = new Piece(CubeColor.White, CubeColor.Empty, CubeColor.Empty);
            pieces[0, 1, 1] = new Piece(CubeColor.Empty, CubeColor.Orange, CubeColor.Empty);
            pieces[2, 1, 1] = new Piece(CubeColor.Empty, CubeColor.Red, CubeColor.Empty);
            pieces[1, 0, 1] = new Piece(CubeColor.Empty, CubeColor.Empty, CubeColor.Green);



        }

        int Mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        public void RotateUp(bool inverse)
        {
            RotateHorizontal(2, inverse);
        }

        public void RotateMid(bool inverse)
        {
            RotateHorizontal(1, inverse);
        }

        public void RotateDown(bool inverse)
        {
            RotateHorizontal(0, inverse);
        }

        public void RotateRight(bool inverse)
        {
            RotateVertical(2, inverse);
        }

        public void RotateLeft(bool inverse)
        {
            RotateVertical(0, inverse);
        }

        public void RotateFront(bool inverse)
        {
            RotateZ(0, inverse);
        }

        public void RotateBack(bool inverse)
        {
            RotateZ(2, inverse);
        }


        /// <summary>
        /// Horizontal rotation of the cube.
        /// </summary>
        /// <param name="y">Cube layer.</param>
        /// <param name="inverse">True if counterclockwise.</param>
        public void RotateHorizontal(int y, bool inverse)
        {
            List<Tuple<int, int, int, Piece>> layer = new List<Tuple<int, int, int, Piece>>();

            for (int i = 0; i < 3; i++)
            {
                layer.Add(new Tuple<int, int, int, Piece>(i, y, 2, pieces[i, y, 2]));
            }

            layer.Add(new Tuple<int, int, int, Piece>(2, y, 1, pieces[2, y, 1]));
            
            for (int i = 0; i < 3; i++)
            {
                layer.Add(new Tuple<int, int, int, Piece>(2 - i, y, 0, pieces[2 - i, y, 0]));
            }

            layer.Add(new Tuple<int, int, int, Piece>(0, y, 1, pieces[0, y, 1]));
            
            for (int i=0; i < layer.Count; i++)
            {
                int direction = inverse ? -2 : 2;
                Tuple<int, int, int, Piece> next = layer[Mod((i + direction), layer.Count)];
                Tuple<int, int, int, Piece> piece = layer[i];
                pieces[next.Item1, next.Item2, next.Item3] = piece.Item4;

                if(piece.Item4 != null)
                {
                    piece.Item4.RotateHorizontal();
                }
            }
                    
        }

        public void RotateVertical(int x, bool inverse)
        {
            List<Tuple<int, int, int, Piece>> layer = new List<Tuple<int, int, int, Piece>>();

            for (int i = 0; i < 3; i++)
            {
                layer.Add(new Tuple<int, int, int, Piece>(x, i, 2, pieces[x, i, 2]));
            }

            layer.Add(new Tuple<int, int, int, Piece>(x, 2, 1, pieces[x, 2, 1]));
            
            for (int i = 0; i < 3; i++)
            {
                layer.Add(new Tuple<int, int, int, Piece>(x, 2 - i, 0, pieces[x, 2 - i, 0]));
            }

            layer.Add(new Tuple<int, int, int, Piece>(x, 0, 1, pieces[x, 0, 1]));
            
            for (int i=0; i < layer.Count; i++)
            {
                int direction = inverse ? 2 : -2;
                Tuple<int, int, int, Piece> next = layer[Mod((i + direction), layer.Count)];
                Tuple<int, int, int, Piece> piece = layer[i];
                pieces[next.Item1, next.Item2, next.Item3] = piece.Item4;

                if(piece.Item4 != null)
                {
                    piece.Item4.RotateVertical();
                }
            }
                    
        }

        public void RotateZ(int z, bool inverse)
        {
            List<Tuple<int, int, int, Piece>> layer = new List<Tuple<int, int, int, Piece>>();

            for (int i = 0; i < 3; i++)
            {
                layer.Add(new Tuple<int, int, int, Piece>(2, i, z, pieces[2, i, z]));
            }

            layer.Add(new Tuple<int, int, int, Piece>(1, 2, z, pieces[1, 2, z]));

            for (int i = 0; i < 3; i++)
            {
                layer.Add(new Tuple<int, int, int, Piece>(0, 2 - i, z, pieces[0, 2 - i, z]));
            }

            layer.Add(new Tuple<int, int, int, Piece>(1, 0, z, pieces[1, 0, z]));

            for (int i = 0; i < layer.Count; i++)
            {
                int direction = inverse ? 2 : -2;
                Tuple<int, int, int, Piece> next = layer[Mod((i + direction), layer.Count)];
                Tuple<int, int, int, Piece> piece = layer[i];
                pieces[next.Item1, next.Item2, next.Item3] = piece.Item4;

                if (piece.Item4 != null)
                {
                    piece.Item4.RotateZ();
                }
            }

        }

        public override String ToString()
        {
            String s = "";


            // Back
            s += "Back\n";
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Piece p = pieces[j, 0 + i, 2];
                    s += p != null ? Piece.ColorToString(p.xColor) + " " : "X ";
                }
                s += "\n";
            }
           

            // Top
            s += "\nTop\n";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Piece p = pieces[j, 2, 2 - i];
                    s += p != null ? Piece.ColorToString(p.zColor) + " " : "X ";
                }

                s += "\n";
            }

            // Front
            s += "\nFront\n";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Piece p = pieces[j, 2 - i, 0];
                    s += p != null ? Piece.ColorToString(p.xColor) + " " : "X ";
                }

                s += "\n";
            }

            // Left
            s += "\nLeft\n";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Piece p = pieces[0, 2 - i, 2 - j];
                    s += p != null ? Piece.ColorToString(p.yColor) + " " : "X ";
                }

                s += "\n";
            }

            // Right
            s += "\nRight\n";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Piece p = pieces[2, 2 - i, j];
                    s += p != null ? Piece.ColorToString(p.yColor) + " " : "X ";
                }

                s += "\n";
            }

            // Bot
            s += "\nBottom\n";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Piece p = pieces[j, 0, i];
                    s += p != null ? Piece.ColorToString(p.zColor) + " " : "X ";
                }

                s += "\n";
            }

            return s;
     
        }

        public void ExecuteMove(Move move)
        {
            moveMap[move.Action].Invoke();
        }

        public void Scramble()
        {
            Random random = new Random();
            for(int i=0; i<50; i++)
            {
                Array values = Enum.GetValues(typeof(CubeAction));
                CubeAction randomBar = (CubeAction)values.GetValue(random.Next(values.Length));
                (moveMap[randomBar]).Invoke();
            }
        }
    }

}
