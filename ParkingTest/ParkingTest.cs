using System;
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

        [TestCase(31)]
        [TestCase(59)]
        public void GetParkingFee_31到59分鐘_回覆5元(int minutes)
        {
            Parking parking = new Parking();
            var expected = 5;
            var actual = parking.GetParkingFee(minutes);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(120)]
        public void GetParkingFee_每小時_回覆5元(int minutes)
        {
            Parking parking = new Parking();
            var expected = 10;
            var actual = parking.GetParkingFee(minutes);

            Assert.AreEqual(expected, actual);
        }
    }

    public class Parking
    {
        public int GetParkingFee(int minutes)
        {
            if (minutes >= 31 && minutes <= 59)
                return 5;
            if (minutes >= 11 && minutes <= 30)
                return 2;
            return 0;
        }
    }
}
