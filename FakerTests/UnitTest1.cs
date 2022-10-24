using System.Linq.Expressions;
using System.Reflection;
using FakerLibrary.Generators;
using FakerLibrary;

namespace FakerTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ValueTypes()
        {
            GeneratorContext generatorContext = new GeneratorContext();
            BoolGenerator boolGenerator = new BoolGenerator();
            IntegerGenerator integerGenerator = new IntegerGenerator();
            DoubleGenerator doubleGenerator = new DoubleGenerator();
            Assert.IsTrue(boolGenerator.CanGenerate(typeof(bool)) && integerGenerator.CanGenerate(typeof(int)) && doubleGenerator.CanGenerate(typeof(double)) &&
                boolGenerator.Generate(typeof(bool), generatorContext) is bool &&
                integerGenerator.Generate(typeof(int), generatorContext) is int &&
                doubleGenerator.Generate(typeof(double), generatorContext) is double);
        }
    }
}