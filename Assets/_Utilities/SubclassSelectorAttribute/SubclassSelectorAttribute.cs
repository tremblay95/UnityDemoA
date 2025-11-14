using System;
using UnityEngine;

namespace Utilities.SubclassSelectorAttribute
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SubclassSelectorAttribute : PropertyAttribute
    {
        public Type BaseType { get; }
        
        public SubclassSelectorAttribute(Type baseType)
        {
            BaseType = baseType;
        }
    }
}