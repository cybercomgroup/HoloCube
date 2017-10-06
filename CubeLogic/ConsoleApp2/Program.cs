using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            RubikCube cube = new RubikCube();
            Console.WriteLine("====\nBEFORE\n====\n");
            Console.WriteLine(cube);
            Console.WriteLine("====\nAFTER\n====\n");
            cube.RotateUp(false);
            cube.RotateRight(false);
            cube.RotateLeft(true);
            cube.RotateDown(false);

            cube.RotateDown(true);
            cube.RotateLeft(false);
            cube.RotateRight(true);
            cube.RotateUp(true);
            
            Console.WriteLine(cube);
            Console.ReadKey();
        }
    }
}
