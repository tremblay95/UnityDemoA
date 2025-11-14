using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace ScratchPad.SubclassSelectorAttribute
{
    public static class TypeCacheUtility
    {
        private static Dictionary<Type, string[]> _displayNameCache = new();

        public static List<Type> GetSubclassesOf(Type baseType)
        {
            return TypeCache.GetTypesDerivedFrom(baseType)
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .ToList();
        }
        
        public static string[] GetSubclassNames(Type baseType)
        {
            if (_displayNameCache.TryGetValue(baseType, out var names))
                return names;

            names = GetSubclassesOf(baseType).Select(t => t.Name).ToArray();
            _displayNameCache[baseType] = names;
            return names;
        }

        public static void ClearDisplayNameCache() => _displayNameCache.Clear();
    }
}