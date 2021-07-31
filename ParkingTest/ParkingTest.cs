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

        [TestCase(1451)]
        [TestCase(1469)]
        public void GetParkingFee_超過1天且11到29分鐘_給32元(int minutes)
        {
            Parking parking = new Parking();
            var expected = 32;
            var actual = parking.GetParkingFee(minutes);

            Assert.AreEqual(expected, actual);
        }
    }

    public class Parking
    {
        private const int OneDayMinutes = 1440;
        private const int OneHourMinutes = 60;
        private const int ThirtyMinutes = 30;
        private const int TenMinutes = 10;

        private const int OneDayMaxFee = 30;
        private const int TenMinutesFee = 0;
        private const int ThirtyMinutesFee = 2;
        private const int PerHourFee = 5;

        private int _days = 0;
        private int _horus = 0;
        private int _oneDayOfRemainMinutes = 0;
        private int _oneHourOfRemainMinutes = 0;

        private int MaxFeeMinutes
        {
            get
            {
                return OneDayMaxFee / PerHourFee * OneHourMinutes;
            }
        }

        public int GetParkingFee(int minutes)
        {
            _days = minutes / OneDayMinutes;
            _horus = minutes / OneHourMinutes;
            _oneDayOfRemainMinutes = minutes % OneDayMinutes;
            _oneHourOfRemainMinutes = minutes % OneHourMinutes;

            if (minutes <= MaxFeeMinutes)
                return GetLessDayFee(minutes);

            if (minutes > OneDayMinutes)
                return _days * OneDayMaxFee + GetLessDayFee(_oneDayOfRemainMinutes);

            return OneDayMaxFee;

        }

        private int GetLessDayFee(int minutes)
        {
            if (minutes <= OneHourMinutes)
            {
                if (minutes > ThirtyMinutes && minutes < OneHourMinutes)
                    return PerHourFee;
                if (minutes > TenMinutes && minutes <= ThirtyMinutes)
                    return ThirtyMinutesFee;
                return TenMinutesFee;
            }

            if (minutes <= MaxFeeMinutes)
            {
                if (_oneHourOfRemainMinutes > ThirtyMinutes)
                    return (_horus + 1) * PerHourFee;
                else if (_oneHourOfRemainMinutes > 0)
                    return _horus * PerHourFee + ThirtyMinutesFee;
                return _horus * PerHourFee;
            }

            return OneDayMaxFee;
        }
    }
}
