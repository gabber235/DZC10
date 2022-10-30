using System;
using UnityEngine;

namespace Items
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ItemAttribute : PropertyAttribute
    {
    }
}