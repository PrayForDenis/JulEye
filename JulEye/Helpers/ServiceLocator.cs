using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JulEye.Helpers
{
    internal static class ServiceLocator
    {
        private static Dictionary<Type, object> _dictionary = new Dictionary<Type, object>();

        public static void Register<T>(T instance)
        {
            _dictionary[typeof(T)] = instance;
        }

        public static void UnRegister<T>()
        {
            _dictionary.Remove(typeof(T));
        }

        public static T Take<T>()
        {
            if (!_dictionary.ContainsKey(typeof(T)))
            {
                throw new ArgumentException($"{nameof(ServiceLocator)} does not contains instance of {typeof(T)}");
            }
            else
            {
                return (T)_dictionary[typeof(T)];
            }
        }

        public static bool TryTake<T>(out T instance)
        {
            if (!_dictionary.ContainsKey(typeof(T)))
            {
                instance = default;
                return false;
            }
            else
            {
                instance = (T)_dictionary[typeof(T)];
                return true;
            }
        }
    }
}
