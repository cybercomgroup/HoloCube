using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            

            Solve();
            //KeyInput();
               
        }

        private static void Solve()
        {
            RubikCube cube = new RubikCube();
            Console.WriteLine(cube);
            Solver.Solve(cube);
            Console.WriteLine(cube);
            Console.ReadKey();
        }

        private static void KeyInput()
        {

            RubikCube cube = new RubikCube();
            Console.WriteLine(cube);

            char key;

            while (true)
            {
                key = Console.ReadKey().KeyChar;
                switch (key)
                {
                    case 'f':
                        cube.RotateFront(false);
                        break;
                    case 'F':
                        cube.RotateFront(true);
                        break;
                    case 'r':
                        cube.RotateRight(false);
                        break;
                    case 'R':
                        cube.RotateRight(true);
                        break;
                    case 'l':
                        cube.RotateRight(false);
                        break;
                    case 'L':
                        cube.RotateRight(true);
                        break;
                    case 'u':
                        cube.RotateUp(false);
                        break;
                    case 'U':
                        cube.RotateUp(true);
                        break;
                    case 'd':
                        cube.RotateDown(false);
                        break;
                    case 'D':
                        cube.RotateDown(true);
                        break;


                }

                Console.WriteLine(cube);
            }
        }
    }
}
