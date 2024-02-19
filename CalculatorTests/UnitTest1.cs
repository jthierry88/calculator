using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TwoPlusTwo()
        {
            var model = new Calculator.ViewModel.MainViewModel();
            model.ButtonPressed("2");
            model.ButtonPressed("Add");
            model.ButtonPressed("2");
            model.ButtonPressed("Equals");
            Assert.AreEqual(model.OutputText, "4");

        }

        [TestMethod]
        public void TenMinusFive()
        {
            var model = new Calculator.ViewModel.MainViewModel();
            model.ButtonPressed("10");
            model.ButtonPressed("Subtract");
            model.ButtonPressed("5");
            model.ButtonPressed("Equals");
            Assert.AreEqual(model.OutputText, "5");

        }

        [TestMethod]
        public void TenPlusFiveMinusThree()
        {
            var model = new Calculator.ViewModel.MainViewModel();
            model.ButtonPressed("10");
            model.ButtonPressed("Add");
            model.ButtonPressed("5");
            model.ButtonPressed("Subtract");
            model.ButtonPressed("3");
            model.ButtonPressed("Equals");
            Assert.AreEqual(model.OutputText, "12");

        }

        [TestMethod]
        public void TwentyFiveTimesThirty()
        {
            var model = new Calculator.ViewModel.MainViewModel();
            model.ButtonPressed("25");
            model.ButtonPressed("Multiply");
            model.ButtonPressed("30");
            model.ButtonPressed("Equals");
            Assert.AreEqual(model.OutputText, "750");

        }

        [TestMethod]
        public void OneHundredDividedByFifty()
        {
            var model = new Calculator.ViewModel.MainViewModel();
            model.ButtonPressed("100");
            model.ButtonPressed("Divide");
            model.ButtonPressed("50");
            model.ButtonPressed("Equals");
            Assert.AreEqual(model.OutputText, "2");

        }

        [TestMethod]
        public void FortySevenDividedByZero()
        {
            var model = new Calculator.ViewModel.MainViewModel();
            model.ButtonPressed("47");
            model.ButtonPressed("Divide");
            model.ButtonPressed("0");
            model.ButtonPressed("Equals");
            Assert.AreEqual(model.OutputText, "Cannot divide by zero");

        }

        [TestMethod]
        public void ClearsNumberForNewButton()
        {
            var model = new Calculator.ViewModel.MainViewModel();
            model.ButtonPressed("3");
            model.ButtonPressed(".");
            model.ButtonPressed("4");
            model.ButtonPressed("Add");
            model.ButtonPressed("2");
            model.ButtonPressed(".");
            model.ButtonPressed("3");
            model.ButtonPressed("Equals");
            Assert.AreEqual(model.OutputText, "5.7");
            model.ButtonPressed("6");
            Assert.AreEqual(model.OutputText, "6");

        }

        [TestMethod]
        public void TypeDecimal()
        {
            var model = new Calculator.ViewModel.MainViewModel();
            model.ButtonPressed(".");
            model.ButtonPressed("4");
            Assert.AreEqual(model.OutputText, "0.4");

        }

        [TestMethod]
        public void TypeDecimal2()
        {
            var model = new Calculator.ViewModel.MainViewModel();
            model.ButtonPressed("4");
            model.ButtonPressed(".");
            model.ButtonPressed("2");
            model.ButtonPressed("Add");
            model.ButtonPressed("4");
            model.ButtonPressed(".");
            model.ButtonPressed("2");
            model.ButtonPressed("Equals");
            Assert.AreEqual(model.OutputText, "8.4");

        }

        [TestMethod]
        public void AddLargeDecimalNumber()
        {
            var model = new Calculator.ViewModel.MainViewModel();
            model.ButtonPressed("6");
            model.ButtonPressed("6");
            model.ButtonPressed("6");
            model.ButtonPressed(".");
            model.ButtonPressed("6");
            model.ButtonPressed("6");
            model.ButtonPressed("6");
            model.ButtonPressed("6");
            model.ButtonPressed("6");
            model.ButtonPressed("6");
            model.ButtonPressed("6");
            model.ButtonPressed("6");
            model.ButtonPressed("6");
            model.ButtonPressed("Add");
            model.ButtonPressed("3");
            model.ButtonPressed("Equals");
            Assert.AreEqual(model.OutputText, "669.6667");
        }

        [TestMethod]
        public void SubtractDecimalNumber()
        {
            var model = new Calculator.ViewModel.MainViewModel();
            model.ButtonPressed("7");
            model.ButtonPressed("5");
            model.ButtonPressed(".");
            model.ButtonPressed("2");
            model.ButtonPressed("1");
            model.ButtonPressed("8");
            model.ButtonPressed("3");
            model.ButtonPressed("Subtract");
            model.ButtonPressed("8");
            model.ButtonPressed(".");
            model.ButtonPressed("5");
            model.ButtonPressed("7");
            model.ButtonPressed("5");
            model.ButtonPressed("Equals");
            Assert.AreEqual(model.OutputText, "66.6433");
        }

        [TestMethod]
        public void MultiplyDecimalNumber()
        {
            var model = new Calculator.ViewModel.MainViewModel();
            model.ButtonPressed("4");
            model.ButtonPressed("4");
            model.ButtonPressed("6");
            model.ButtonPressed(".");
            model.ButtonPressed("8");
            model.ButtonPressed("9");
            model.ButtonPressed("6");
            model.ButtonPressed("Multiply");
            model.ButtonPressed(".");
            model.ButtonPressed("0");
            model.ButtonPressed("3");
            model.ButtonPressed("8");
            model.ButtonPressed("9");
            model.ButtonPressed("Equals");
            Assert.AreEqual(model.OutputText, "17.3843");
        }

        [TestMethod]
        public void DivideDecimalNumber()
        {
            var model = new Calculator.ViewModel.MainViewModel();
            model.ButtonPressed("1");
            model.ButtonPressed(".");
            model.ButtonPressed("2");
            model.ButtonPressed("2");
            model.ButtonPressed("6");
            model.ButtonPressed("Divide");
            model.ButtonPressed("7");
            model.ButtonPressed("Equals");
            Assert.AreEqual(model.OutputText, "0.1751");
        }
    }
}
