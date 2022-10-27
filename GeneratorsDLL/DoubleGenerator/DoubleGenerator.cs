using FakerLibrary;
namespace DoubleGenerator
{
    public class DoubleGenerator : IGenerator
    {
        public object Generate(Type type, GeneratorContext context)
        {
            return (context.Random.NextDouble() - 0.5) * double.MaxValue;
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(double);
        }
    }
}