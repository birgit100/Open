using System;
using System.Collections.Generic;
using System.Reflection;

namespace Open.Aids
{
    public static class CreateNew
    {
        public static T Instance<T>()
        {
            return Safe.Run(() =>
            {
                var type = typeof(T);
                return (T) Instance(type);
            }, default(T));
        }

        public static object Instance(Type t)
        {
            return Safe.Run(() =>
            {
                var constructor = getFirstOrDefaultConstructorInfo(t);
                var parameters = constructor.GetParameters();
                var values = setRandomParameterValues(parameters);
                return invokeConstructor(constructor, values);
            }, null);
        }

        private static object invokeConstructor(ConstructorInfo ci, object[] values)
        {
            return values.Length == 0 ? ci.Invoke(null) : ci.Invoke(values);
        }

        private static object[] setRandomParameterValues(ParameterInfo[] parameters)
        {
            var values = new List<object>();
            foreach (var p in parameters)
            {
                var t = p.ParameterType;
                var value = GetRandom.Value(t);
                values.Add(value);
            }
            return values.ToArray();
        }

        private static ConstructorInfo getFirstOrDefaultConstructorInfo(Type t)
        {
            var constructors = t.GetConstructors();
            return constructors.Length == 0 ? null : constructors[0];
        }
    }
}
