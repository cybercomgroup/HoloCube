using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ConsoleApp2


{
    /// <summary>
    ///  Helper class for positions in the cube matrix.
    /// </summary>
    public class CubePos
    {
        public int X;
        public int Y;
        public int Z;

        public CubePos(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Determines whether two positions are equal.
        /// </summary>
        /// <param name="p1">First position-</param>
        /// <param name="p2">Second position.</param>
        /// <returns>True if equal, else false.</returns>
        public static bool EqualPos(CubePos p1, CubePos p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z;
        }
    }

    /// <summary>
    /// Representation of all six cube sides.
    /// </summary>
    public enum CubeSide
    {
        Back,
        Top,
        Front,
        Left,
        Right,
        Bottom
    }

    /// <summary>
    /// A representation of a Rubik's cube.
    /// Contains methods for manipulating the cube using rotations.
    /// </summary>
    public class RubikCube
    {
        private Dictionary<CubeAction, Action> moveMap = new Dictionary<CubeAction, Action>();
        private Cubie[,,] cubies = new Cubie[3, 3, 3];

        public Cubie[,,] Cubies => cubies;


        public RubikCube()
        {
            // Face
            moveMap.Add(CubeAction.U, () => RotateXZ(2, false));
            moveMap.Add(CubeAction.UI, () => RotateXZ(2, true));
            moveMap.Add(CubeAction.L, () => RotateYZ(0, true));
            moveMap.Add(CubeAction.LI, () => RotateYZ(0, false));
            moveMap.Add(CubeAction.F, () => RotateXY(0, false));
            moveMap.Add(CubeAction.FI, () => RotateXY(0, true));
            moveMap.Add(CubeAction.R, () => RotateYZ(2, false));
            moveMap.Add(CubeAction.RI, () => RotateYZ(2, true));
            moveMap.Add(CubeAction.B, () => RotateXY(2, true));
            moveMap.Add(CubeAction.BI, () => RotateXY(2, false));
            moveMap.Add(CubeAction.D, () => RotateXZ(0, true));
            moveMap.Add(CubeAction.DI, () => RotateXZ(0, false));

            // Slice
            moveMap.Add(CubeAction.E, () => RotateXZ(1, true));
            moveMap.Add(CubeAction.EI, () => RotateXZ(1, false));
            moveMap.Add(CubeAction.M, () => RotateYZ(1, true));
            moveMap.Add(CubeAction.MI, () => RotateYZ(1, false));
            moveMap.Add(CubeAction.S, () => RotateXY(1, false));
            moveMap.Add(CubeAction.SI, () => RotateXY(1, true));

            // Whole
            moveMap.Add(CubeAction.X, () => RotateX(false));
            moveMap.Add(CubeAction.XI, () => RotateX(true));
            moveMap.Add(CubeAction.Y, () => RotateY(false));
            moveMap.Add(CubeAction.YI, () => RotateY(true));
            moveMap.Add(CubeAction.Z, () => RotateZ(false));
            moveMap.Add(CubeAction.ZI, () => RotateZ(true));

            /*   // Corners
               cubies[0, 0, 0] = new Cubie(CubeColor.White, CubeColor.Orange, CubeColor.Green);
               cubies[2, 0, 0] = new Cubie(CubeColor.White, CubeColor.Red, CubeColor.Green);
               cubies[0, 2, 0] = new Cubie(CubeColor.White, CubeColor.Orange, CubeColor.Blue);
               cubies[2, 2, 0] = new Cubie(CubeColor.White, CubeColor.Red, CubeColor.Blue);
               cubies[0, 0, 2] = new Cubie(CubeColor.Yellow, CubeColor.Orange, CubeColor.Green);
               cubies[2, 0, 2] = new Cubie(CubeColor.Yellow, CubeColor.Red, CubeColor.Green);
               cubies[0, 2, 2] = new Cubie(CubeColor.Yellow, CubeColor.Orange, CubeColor.Blue);
               cubies[2, 2, 2] = new Cubie(CubeColor.Yellow, CubeColor.Red, CubeColor.Blue);
   
               // Middle pieces
               cubies[1, 0, 0] = new Cubie(CubeColor.White, CubeColor.Empty, CubeColor.Green);
               cubies[0, 1, 0] = new Cubie(CubeColor.White, CubeColor.Orange, CubeColor.Empty);
               cubies[2, 1, 0] = new Cubie(CubeColor.White, CubeColor.Red, CubeColor.Empty);
               cubies[1, 2, 0] = new Cubie(CubeColor.White, CubeColor.Empty, CubeColor.Blue);
   
               cubies[0, 0, 1] = new Cubie(CubeColor.Empty, CubeColor.Orange, CubeColor.Green);
               cubies[2, 0, 1] = new Cubie(CubeColor.Empty, CubeColor.Red, CubeColor.Green);
               cubies[0, 2, 1] = new Cubie(CubeColor.Empty, CubeColor.Orange, CubeColor.Blue);
               cubies[2, 2, 1] = new Cubie(CubeColor.Empty, CubeColor.Red, CubeColor.Blue);
   
               cubies[1, 0, 2] = new Cubie(CubeColor.Yellow, CubeColor.Empty, CubeColor.Green);
               cubies[0, 1, 2] = new Cubie(CubeColor.Yellow, CubeColor.Orange, CubeColor.Empty);
               cubies[2, 1, 2] = new Cubie(CubeColor.Yellow, CubeColor.Red, CubeColor.Empty);
               cubies[1, 2, 2] = new Cubie(CubeColor.Yellow, CubeColor.Empty, CubeColor.Blue);
   
   
               // Faces
               cubies[1, 1, 2] = new Cubie(CubeColor.Yellow, CubeColor.Empty, CubeColor.Empty);
               cubies[1, 2, 1] = new Cubie(CubeColor.Empty, CubeColor.Empty, CubeColor.Blue);
               cubies[1, 1, 0] = new Cubie(CubeColor.White, CubeColor.Empty, CubeColor.Empty);
               cubies[0, 1, 1] = new Cubie(CubeColor.Empty, CubeColor.Orange, CubeColor.Empty);
               cubies[2, 1, 1] = new Cubie(CubeColor.Empty, CubeColor.Red, CubeColor.Empty);
               cubies[1, 0, 1] = new Cubie(CubeColor.Empty, CubeColor.Empty, CubeColor.Green);
   
               // Middle
               cubies[1, 1, 1] = new Cubie(CubeColor.Empty, CubeColor.Empty, CubeColor.Empty);*/
        }

        // Mod method to handle negative cases for mod.
        private int Mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        /// <summary>
        /// Rotates the entire cube around the x-axis-
        /// </summary>
        /// <param name="inverse">True if counterclockwise.</param>
        public void RotateX(bool inverse)
        {
            for (int i = 0; i < 3; i++)
            {
                RotateYZ(i, inverse);
            }
        }

        /// <summary>
        /// Rotates the entire cube around the y-axis-
        /// </summary>
        /// <param name="inverse">True if counterclockwise.</param>
        public void RotateY(bool inverse)
        {
            for (int i = 0; i < 3; i++)
            {
                RotateXZ(i, inverse);
            }
        }

        /// <summary>
        /// Rotates the entire cube around the z-axis-
        /// </summary>
        /// <param name="inverse">True if counterclockwise.</param>
        public void RotateZ(bool inverse)
        {
            for (int i = 0; i < 3; i++)
            {
                RotateXY(i, inverse);
            }
        }

        /// <summary>
        /// Invokes the corresponding rotation method to the given
        /// CubeAction.
        /// </summary>
        /// <param name="action">Action to be exectued.</param>
        public void Rotate(CubeAction action)
        {
            moveMap[action].Invoke();
        }

        /// <summary>
        /// Rotation of one block around the y-axis.
        /// </summary>
        /// <param name="y">Cube layer determined by y-value.</param>
        /// <param name="inverse">True if counterclockwise.</param>
        public void RotateXZ(int y, bool inverse)
        {
            List<Tuple<int, int, int, Cubie>> layer = new List<Tuple<int, int, int, Cubie>>();

            for (int i = 0; i < 3; i++)
            {
                layer.Add(new Tuple<int, int, int, Cubie>(i, y, 2, cubies[i, y, 2]));
            }

            layer.Add(new Tuple<int, int, int, Cubie>(2, y, 1, cubies[2, y, 1]));

            for (int i = 0; i < 3; i++)
            {
                layer.Add(new Tuple<int, int, int, Cubie>(2 - i, y, 0, cubies[2 - i, y, 0]));
            }

            layer.Add(new Tuple<int, int, int, Cubie>(0, y, 1, cubies[0, y, 1]));

            for (int i = 0; i < layer.Count; i++)
            {
                int direction = inverse ? -2 : 2;
                Tuple<int, int, int, Cubie> next = layer[Mod((i + direction), layer.Count)];
                Tuple<int, int, int, Cubie> piece = layer[i];
                cubies[next.Item1, next.Item2, next.Item3] = piece.Item4;

                if (piece.Item4 != null)
                {
                    piece.Item4.RotateHorizontal();
                }
            }
        }

        /// <summary>
        /// Rotation of one block around the x-axis.
        /// </summary>
        /// <param name="x">Cube layer determined by x-value.</param>
        /// <param name="inverse">True if counterclockwise.</param>
        public void RotateYZ(int x, bool inverse)
        {
            List<Tuple<int, int, int, Cubie>> layer = new List<Tuple<int, int, int, Cubie>>();

            for (int i = 0; i < 3; i++)
            {
                layer.Add(new Tuple<int, int, int, Cubie>(x, i, 2, cubies[x, i, 2]));
            }

            layer.Add(new Tuple<int, int, int, Cubie>(x, 2, 1, cubies[x, 2, 1]));

            for (int i = 0; i < 3; i++)
            {
                layer.Add(new Tuple<int, int, int, Cubie>(x, 2 - i, 0, cubies[x, 2 - i, 0]));
            }

            layer.Add(new Tuple<int, int, int, Cubie>(x, 0, 1, cubies[x, 0, 1]));

            for (int i = 0; i < layer.Count; i++)
            {
                int direction = inverse ? 2 : -2;
                Tuple<int, int, int, Cubie> next = layer[Mod((i + direction), layer.Count)];
                Tuple<int, int, int, Cubie> piece = layer[i];
                cubies[next.Item1, next.Item2, next.Item3] = piece.Item4;

                if (piece.Item4 != null)
                {
                    piece.Item4.RotateVertical();
                }
            }
        }

        /// <summary>
        /// Rotation of one block around the z-axis.
        /// </summary>
        /// <param name="z">Cube layer determined by z-value.</param>
        /// <param name="inverse">True if counterclockwise.</param>
        public void RotateXY(int z, bool inverse)
        {
            List<Tuple<int, int, int, Cubie>> layer = new List<Tuple<int, int, int, Cubie>>();

            for (int i = 0; i < 3; i++)
            {
                layer.Add(new Tuple<int, int, int, Cubie>(2, i, z, cubies[2, i, z]));
            }

            layer.Add(new Tuple<int, int, int, Cubie>(1, 2, z, cubies[1, 2, z]));

            for (int i = 0; i < 3; i++)
            {
                layer.Add(new Tuple<int, int, int, Cubie>(0, 2 - i, z, cubies[0, 2 - i, z]));
            }

            layer.Add(new Tuple<int, int, int, Cubie>(1, 0, z, cubies[1, 0, z]));

            for (int i = 0; i < layer.Count; i++)
            {
                int direction = inverse ? 2 : -2;
                Tuple<int, int, int, Cubie> next = layer[Mod((i + direction), layer.Count)];
                Tuple<int, int, int, Cubie> piece = layer[i];
                cubies[next.Item1, next.Item2, next.Item3] = piece.Item4;

                if (piece.Item4 != null)
                {
                    piece.Item4.RotateZ();
                }
            }
        }


        /// <summary>
        ///  Rotates the whole cube to the orientation given. (Front/Top colors)
        /// </summary>
        /// <param name="front">The color you want at the front</param>
        /// <param name="top">The color you want at the top</param>
        /// <exception cref="ArgumentException">The colors you have chosen, cant be fitted as front and topcolor together.</exception>
        public int rotateCubeToChosenColor(CubeColor front, CubeColor top)
        {
            int zRotations = 0;
            
            for (int i = 0; i < 6; i++)
            {
                if (FaceColor(CubeSide.Front) == front) break;

                RotateY(true);
                if (FaceColor(CubeSide.Front) == front) break;

                RotateX(false);
                if (FaceColor(CubeSide.Front) == front) break;
            }

            for (int i = 0; i < 4; i++)
            {
                if (FaceColor(CubeSide.Top) == top) return zRotations; 

                RotateZ(false);
                zRotations++;
                if (FaceColor(CubeSide.Top) == top) return zRotations;
            }
            if (FaceColor(CubeSide.Front) != front || FaceColor(CubeSide.Top) != top) throw new ArgumentException("Error! The frontcolor (" + front + ") and top color (" + top + ") is incompatible");
            return zRotations;
        }

        /// <summary>
        /// Finds the color of a cubic on a specified side.
        /// </summary>
        /// <param name="pos">Matrix position of the cubie.</param>
        /// <param name="vec">Cube side in [0..2]. </param>
        /// <returns>Color on the specified position.</returns>
        public CubeColor GetCubicColor(CubePos pos, int vec)
        {
            Cubie cubie = cubies[pos.X, pos.Y, pos.Z];
            if (vec == 0) return cubie.xColor;
            else if (vec == 1) return cubie.yColor;
            else return cubie.yColor;
        }

        /// <summary>
        /// Finds the current face color of a given cube side.
        /// </summary>
        /// <param name="side">Side of the cube.</param>
        /// <returns>Face color of the given side.</returns>
        public CubeColor FaceColor(CubeSide side)
        {
            switch (side)
            {
                case CubeSide.Back:
                    return cubies[1, 1, 2].xColor;
                case CubeSide.Top:
                    return cubies[1, 2, 1].zColor;
                case CubeSide.Front:
                    return cubies[1, 1, 0].xColor;
                case CubeSide.Left:
                    return cubies[0, 1, 1].yColor;
                case CubeSide.Right:
                    return cubies[2, 1, 1].yColor;
                case CubeSide.Bottom:
                    return cubies[1, 0, 1].zColor;
                default:
                    throw new Exception();
            }
        }

        public override String ToString()
        {
            String s = "";

            // Back
            s += "Back\n";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Cubie p = cubies[j, 0 + i, 2];
                    s += p != null ? Cubie.ColorToString(p.xColor) + " " : "X ";
                }
                s += "\n";
            }

            // Top
            s += "\nTop\n";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Cubie p = cubies[j, 2, 2 - i];
                    s += p != null ? Cubie.ColorToString(p.zColor) + " " : "X ";
                }

                s += "\n";
            }

            // Front
            s += "\nFront\n";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Cubie p = cubies[j, 2 - i, 0];
                    s += p != null ? Cubie.ColorToString(p.xColor) + " " : "X ";
                }

                s += "\n";
            }

            // Left
            s += "\nLeft\n";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Cubie p = cubies[0, 2 - i, 2 - j];
                    s += p != null ? Cubie.ColorToString(p.yColor) + " " : "X ";
                }

                s += "\n";
            }

            // Right
            s += "\nRight\n";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Cubie p = cubies[2, 2 - i, j];
                    s += p != null ? Cubie.ColorToString(p.yColor) + " " : "X ";
                }

                s += "\n";
            }

            // Bot
            s += "\nBottom\n";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Cubie p = cubies[j, 0, i]; //[j, 2, 2 - i];
                    s += p != null ? Cubie.ColorToString(p.zColor) + " " : "X ";
                }

                s += "\n";
            }

            return s;
        }


        /// <summary>
        /// Executes a given move by invoking the corresponding method.
        /// </summary>
        /// <param name="move">Move to be executed.</param>
        public void ExecuteMove(Move move)
        {
            moveMap[move.Action].Invoke();
        }

        /// <summary>
        /// Finds the face-cubie matching the given color.
        /// </summary>
        /// <param name="c1">Face color.</param>
        /// <returns>Tuple of the cubie and its position in the matrix.</returns>
        public Tuple<Cubie, CubePos> FindFace(CubeColor c1)
        {
            return FindCorner(c1, CubeColor.Empty, CubeColor.Empty);
        }


        /// <summary>
        /// Finds the edge-cubie matching the given colors.
        /// </summary>
        /// <param name="c1">First color.</param>
        /// <param name="c2">Second color.</param>
        /// <returns>Tuple of the cubie and its position in the matrix.</returns>
        public Tuple<Cubie, CubePos> FindEdge(CubeColor c1, CubeColor c2)
        {
            return FindCorner(c1, c2, CubeColor.Empty);
        }

        /// <summary>
        /// Finds the corner-cubie matching the given colors.
        /// </summary>
        /// <param name="c1">First color.</param>
        /// <param name="c2">Second color.</param>
        /// <param name="c3">Third color.</param>
        /// <returns>Tuple of the cubie and its position in the matrix.</returns>
        public Tuple<Cubie, CubePos> FindCorner(CubeColor c1, CubeColor c2, CubeColor c3)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Cubie piece = cubies[i, j, k];
                        if (piece.ContainsColor(c1) && piece.ContainsColor(c2) && piece.ContainsColor(c3))
                        {
                            return new Tuple<Cubie, CubePos>(piece, new CubePos(i, j, k));
                        }
                    }
                }
            }

            return null;
        }


        /// <summary>
        /// Scrambles the cube using 50 randomly generated moves.
        /// </summary>
        public void Scramble()
        {
            Scramble(50);
        }

        /// <summary>
        /// Scrambles the cube using a given number of randomly generated moves.
        /// </summary>
        /// <param name="iterations">Number of random moves.</param>
        public void Scramble(int iterations)
        {
            Random random = new Random();
            for (int i = 0; i < iterations; i++)
            {
                Array values = Enum.GetValues(typeof(CubeAction));
                CubeAction randomBar = (CubeAction) values.GetValue(random.Next(values.Length));
                (moveMap[randomBar]).Invoke();
            }
        }
    }
}