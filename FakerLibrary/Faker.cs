using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace FakerLibrary;

public class Faker : IFaker
{
    private readonly List<IGenerator> _generators;
    private readonly GeneratorContext _context;
    private readonly HashSet<Type> _typesBeingCreated = new();

    public Faker(Random random)
    {
        _generators = GetGenerators();
        _context = new GeneratorContext(this, random);
    }

    public Faker() : this(new Random())
    {
    }

    private static List<IGenerator> GetGenerators()
    {
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetInterfaces().Contains(typeof(IGenerator)))
            .Select(t => (IGenerator)Activator.CreateInstance(t)).ToList();

    }

    public T Create<T>()
    {
        return (T)Create(typeof(T));
    }

    public object Create(Type type)
    {
        foreach (var generator in _generators)
        {
            if (generator.CanGenerate(type))
            {
                return generator.Generate(type, _context);
            }
        }

        if (_typesBeingCreated.Contains(type))
            return GetDefaultValue(type);

        _typesBeingCreated.Add(type);
        var obj = CreateComplex(type);
        if (obj == null)
        {
            return null;
        }
        //FillFields(obj);
        //FillProps(obj);
        _typesBeingCreated.Remove(type);
        return obj;
    }

    private object CreateComplex(Type type)
    {
        var constructors = type.GetConstructors().ToList()
            .OrderByDescending(c => c.GetParameters().Length).ToList();
        foreach (var constructor in constructors)
        {
            try
            {
                return constructor.Invoke(constructor.GetParameters()
                    .Select(info => GenerateMemberValue(type, info.Name, info.ParameterType)).ToArray());
            }
            catch (Exception) { }
        }
        return GetDefaultValue(type);
    }

    private object GenerateMemberValue(Type objectType, string memberName, Type memberType)
    {
        return Create(memberType);
    }

    private static object GetDefaultValue(Type t)
    {
        return t.IsValueType ? Activator.CreateInstance(t) : null;
    }
}