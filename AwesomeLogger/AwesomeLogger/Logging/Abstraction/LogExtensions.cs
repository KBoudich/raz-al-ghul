using System;
using System.Collections.Concurrent;

namespace Logging.Abstraction
{
    public static class LogExtensions
    {
        /// <summary>
        /// Concurrent dictionary that ensures only one instance of a logger for a type.
        /// </summary>
        private static readonly Lazy<ConcurrentDictionary<string, ILog>>
            Dictionary = new Lazy<ConcurrentDictionary<string, ILog>>(()
                => new ConcurrentDictionary<string, ILog>());

        /// <summary>
        /// Gets the logger for <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type to get the logger for.</param>
        /// <returns>Instance of a logger for the object.</returns>
        public static ILog Log<T>(this T type)
        {
            return Dictionary.Value.GetOrAdd(typeof(T).FullName, LogManager.GetLog(typeof(T)));
        }

    }
}
