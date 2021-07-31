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

            Assert.AreEqual(expected, actual);
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

        [TestCase(91)]
        public void GetParkingFee_超過30分鐘_算1小時5元價格(int minutes)
        {
            Parking parking = new Parking();
            var expected = 10;
            var actual = parking.GetParkingFee(minutes);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(61)]
        public void GetParkingFee_超過1到29分鐘_加上2元(int minutes)
        {
            Parking parking = new Parking();
            var expected = 7;
            var actual = parking.GetParkingFee(minutes);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(361)]
        [TestCase(391)]
        public void GetParkingFee_金額超過30元_給30元(int minutes)
        {
            Parking parking = new Parking();
            var expected = 30;
            var actual = parking.GetParkingFee(minutes);

            Assert.AreEqual(expected, actual);
        }
    }

    public class Parking
    {
        public int GetParkingFee(int minutes)
        {
            if (minutes <= 60)
            {
                if (minutes >= 31 && minutes <= 59)
                    return 5;
                if (minutes >= 11 && minutes <= 30)
                    return 2;
                return 0;
            }

            if (minutes <= 360)
            {
                if (minutes % 60 > 30)
                    return (minutes / 60 + 1) * 5;
                else if (minutes % 60 > 0)
                    return (minutes / 60) * 5 + 2;
                return (minutes / 60) * 5;
            }

            return 30;

        }
    }
}
