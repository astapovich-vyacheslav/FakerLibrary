using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLibrary;
internal interface IGenerator
{
    object Generate(Type type, GeneratorContext context);
    bool CanGenerate(Type type);
}