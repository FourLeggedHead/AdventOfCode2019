﻿using FourLeggedHead.Model;
using System;
using System.IO;
using System.Linq;

namespace DayNine
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = $"Resources/input.txt";

            try
            {
                if (!File.Exists(path)) throw new FileNotFoundException();
                var lines = File.ReadAllLines(path);

                if (lines.Length == 0) throw new Exception($"File is empty.");
                var inputCode = lines[0].Split(",").Select(long.Parse);

                var computer = new IntcodeComputer(inputCode);

                Console.WriteLine($"What is the input?");
                var input = new long[] { int.Parse(Console.ReadLine()) };

                var status = computer.RunProgram(input);
                Console.WriteLine(computer.Output.Last());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
