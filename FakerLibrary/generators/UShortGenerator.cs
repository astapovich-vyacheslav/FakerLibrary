namespace FakerLibrary.Generators;

public class UShortGenerator : IGenerator
{
    public object Generate(Type type, GeneratorContext context)
    {
        return (ushort)context.Random.Next(ushort.MaxValue + 1);
    }

    public bool CanGenerate(Type type)
    {
        return type == typeof(ushort); 
    }
}