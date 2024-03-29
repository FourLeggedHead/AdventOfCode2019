﻿using FourLeggedHead.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DayFourteen.Model.Tests
{
    [TestClass()]
    public class ReactorTests
    {
        [DataTestMethod()]
        [DataRow("TestData31", 31)]
        [DataRow("TestData165", 165)]
        [DataRow("TestData13312", 13312)]
        [DataRow("TestData180697", 180697)]
        [DataRow("TestData2210736", 2210736)]
        public void ProduceTest(string file, int expectedCount)
        {
            var reactor = new Reactor(FileReader.ReadAllLines($"Resources/{file}.txt"));

            Assert.AreEqual(expectedCount, reactor.RequiredToProduce("ORE", "FUEL", 1));
        }

        [DataTestMethod()]
        [DataRow("TestData13312", 82892753)]
        [DataRow("TestData180697", 5586022)]
        [DataRow("TestData2210736", 460664)]
        public void MaxFuelProducedWithTest(string file, int expectedCount)
        {
            var reactor = new Reactor(FileReader.ReadAllLines($"Resources/{file}.txt"));

            Assert.AreEqual(expectedCount, reactor.MaxFuelProducedWith(1000000000000));
        }
    }
}