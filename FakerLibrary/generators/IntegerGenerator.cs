namespace FakerLibrary.Generators;

public class IntegerGenerator : IGenerator
{
    public object Generate(Type type, GeneratorContext context)
    {
        return context.Random.Next(int.MinValue, int.MaxValue);
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(int);
    }
}