﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DayThree.Model
{
    public class Section
    {
        public Direction Direction { get; set; }
        public int Length { get; set; }

        public Section(string direction, int lenght)
        {
            try
            {
                Direction = GetDirection(direction);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }

            Length = lenght;
        }

        public Section(string sectionString)
        {
            var match = Regex.Match(sectionString, @"(?<Direction>[URDL])(?<Length>\d+)");

            if (match == null)
            {
                Direction = Direction.Up;
                Length = 0;
            }

            try
            {
                Direction = GetDirection(match.Groups["Direction"].Value);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            Length = int.Parse(match.Groups["Length"].Value);
        }

        private Direction GetDirection(string direction)
        {
            if (!Regex.IsMatch(direction, @"[URDL]")) throw new ArgumentException($"{nameof(direction)} is invalid.");

            return direction switch
            {
                "U" => Direction.Up,
                "R" => Direction.Right,
                "D" => Direction.Down,
                "L" => Direction.Left,
            };
        }
    }
}