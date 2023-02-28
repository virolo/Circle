using UnityEngine;

namespace DefaultNamespace.Extensions
{
    public static class StringExtensions
    {
        public static void Log(this string log)
        {
            Debug.Log(log);
        }
    }
}