using System.Linq.Expressions;
using System.Reflection;
using FakerLibrary.Generators;
using FakerLibrary;

namespace FakerTests
{
    [TestClass]
    public class UnitTest1
    {
        Faker faker = new Faker();

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

        //Class to test
        public class TestClass
        {
            public int a;
            public int b;
            public int c;

            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }

            public TestClass(int a, int x)
            {
                this.a = a;
                X = x;
            }
        }

        [TestMethod]
        public void CheckFieldsAndPropsFilling()
        {
            TestClass testObj = faker.Create<TestClass>();
            Assert.IsNotNull(testObj);
            Assert.IsNotNull(testObj.a);
            Assert.IsNotNull(testObj.b);
            Assert.IsNotNull(testObj.c);
            Assert.IsNotNull(testObj.X);
            Assert.IsNotNull(testObj.Y);
            Assert.IsNotNull(testObj.Z);
        }

        public class CyclingTestClass : TestClass
        {
            public List<CyclingTestClass> testList;
            public CyclingTestClass innerObj;
            public CyclingTestClass(int a, int x) : base(a, x) { }
        }

        [TestMethod]
        public void TestCyclingClass()
        {
            CyclingTestClass cyclingTestClass = faker.Create<CyclingTestClass>();
            Assert.IsNotNull(cyclingTestClass);
            Assert.IsNotNull(cyclingTestClass.testList);
            Assert.IsNull(cyclingTestClass.testList[0]);
            Assert.IsNull(cyclingTestClass.innerObj);
        }
    }
}