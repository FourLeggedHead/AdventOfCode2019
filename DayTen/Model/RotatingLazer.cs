﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayTen.Model
{
    public class RotatingLazer
    {
        public IAsteroid Location { get; set; }

        public RotatingLazer(IAsteroid asteroid)
        {
            if (asteroid == null) throw new ArgumentNullException(nameof(asteroid));
            Location = asteroid;
        }

        public IAsteroid VaporizeAsteroids(Map map, int count)
        {
            if (map == null) throw new ArgumentNullException(nameof(map));

            var asteroidsToVaporize = new List<Asteroid>();

            foreach (var location in map.Asteroids)
            {
                if (location.GetType() == typeof(Asteroid) && !location.Equals(Location))
                {
                    var asteroid = location as Asteroid;
                    asteroid.GetPolarCoordinatesFrom(Location);
                    asteroidsToVaporize.Add(asteroid);
                }
            }

            if (asteroidsToVaporize.Count < count) return new NullAsteroid();

            asteroidsToVaporize = OrderAsteroidsByAngleAndRadius(asteroidsToVaporize);

            return asteroidsToVaporize[count - 1];
        }

        private static List<Asteroid> OrderAsteroidsByAngleAndRadius(List<Asteroid> asteroidsToVaporize)
        {
            var orderedList = new List<Asteroid>();

            var groups = asteroidsToVaporize.GroupBy(a => a.Angle);
            foreach (var group in groups)
            {
                var orderedGroup = group.OrderBy(a => a.Radius).ToArray();
                for (int i = 0; i < orderedGroup.Count(); i++)
                {
                    orderedGroup[i].Angle += 2 * i * Math.PI;
                }

                orderedList.AddRange(orderedGroup);
            }

            asteroidsToVaporize = orderedList.OrderBy(a => a.Angle).ToList();

            return asteroidsToVaporize;
        }
    }
}
