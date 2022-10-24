namespace FakerLibrary.Generators;

public class ByteGenerator : IGenerator
{
    public object Generate(Type type, GeneratorContext context)
    {
        return (byte)context.Random.Next(byte.MaxValue + 1);
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(byte);
    }
}