using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace vtb.Core.Utils.Extensions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetTypesInNamespace(this Assembly assembly, string namespaceName)
        {
            return assembly.GetTypes()
                .Where(t => t.IsClass && t.Namespace == namespaceName);
        }
    }
}
