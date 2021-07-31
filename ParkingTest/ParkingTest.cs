﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ParkingTest
{
    public class ParkingTest
    {
        [Test]
        public void GetParkingFee_小於10分鐘_回覆0元()
        {
            Parking parking = new Parking();
            var expected = 0;
            var actual = parking.GetParkingFee(9);
            
            Assert.AreEqual(expected,actual);
        }

        [TestCase(11)]
        [TestCase(30)]
        public void GetParkingFee_11到30分鐘_回覆2元(int minutes)
        {
            Parking parking = new Parking();
            var expected = 2;
            var actual = parking.GetParkingFee(minutes);

            Assert.AreEqual(expected, actual);
        }
    }

    public class Parking
    {
        public int GetParkingFee(int minutes)
        {
            return 0;
        }
    }
}
