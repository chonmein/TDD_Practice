using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace TaxTest
{
    public class TaxTest
    {
        [TestCase(0)]
        [TestCase(-1)]
        public void GetBasicTax_計算0天與負1天_回覆0(double day)
        {
            CarType carType = CarType.機車;
            CCType ccType = CCType._0_150CC;
            double expected = 0; //預期結果

            //製造假物件
            var mock = new Mock<TaxRepository>();
            mock.Setup(m => m.GetTaxTable())
                .Returns(new Dictionary<CCType, int[]>()
                {
                    {CCType._0_150CC, new int[] {0, 900, 0, 1620, 900}}
                }).Verifiable();

            var sut = new Tax(mock.Object);
            var actual = sut.GetBasicTax(carType, ccType, day);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBasicTax_計算大於365天_回覆一年最大值()
        {
            CarType carType = CarType.貨車;
            CCType ccType = CCType._0_150CC;
            var day = 366;
            double expected = 900; //預期結果

            //製造假物件
            var mock = new Mock<TaxRepository>();
            mock.Setup(m => m.GetTaxTable())
                .Returns(new Dictionary<CCType, int[]>()
                {
                    {CCType._0_150CC,new int[]{0,900,0,1620,900}}
                }).Verifiable();

            var sut = new Tax(mock.Object);
            var actual = sut.GetBasicTax(carType, ccType, day);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(-1)]
        [TestCase(5)]
        public void GetBasicTax_車種超出範圍_回覆0(CarType carType)
        {
            CCType ccType = CCType._0_150CC;
            var day = 365;
            double expected = 0; //預期結果

            //製造假物件
            var mock = new Mock<TaxRepository>();
            mock.Setup(m => m.GetTaxTable())
                .Returns(new Dictionary<CCType, int[]>()
                {
                    {CCType._0_150CC,new int[]{0,900,0,1620,900}}
                }).Verifiable();

            var sut = new Tax(mock.Object);
            var actual = sut.GetBasicTax(carType, ccType, day);
            Assert.AreEqual(expected, actual);
        }

        [TestCase(-1)]
        [TestCase(2)]
        public void GetBasicTax_排氣量超出範圍_回覆0(CCType ccType)
        {
            CarType carType = CarType.貨車;
            var day = 366;
            double expected = 0; //預期結果

            //製造假物件
            var mock = new Mock<TaxRepository>();
            mock.Setup(m => m.GetTaxTable())
                .Returns(new Dictionary<CCType, int[]>()
                {
                    {CCType._0_150CC,new int[]{0,900,0,1620,900}}
                }).Verifiable();

            var sut = new Tax(mock.Object);
            var actual = sut.GetBasicTax(carType, ccType, day);
            Assert.AreEqual(expected, actual);
        }
    }

    public interface TaxRepository
    {
        Dictionary<CCType, int[]> GetTaxTable();
    }
}
