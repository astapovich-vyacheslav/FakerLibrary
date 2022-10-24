namespace FakerLibrary.Generators;

public class ULongGenerator : IGenerator
{
    public object Generate(Type type, GeneratorContext context)
    {
        return (ulong)context.Random.NextInt64();
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(ulong);
    }
}