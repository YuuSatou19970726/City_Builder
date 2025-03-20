using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CityBuilder
{
    public class ResourceNameParser
    {
        public static ResourceName FromString(string name)
        {
            name = name.ToLower();
            return (ResourceName)Enum.Parse(typeof(ResourceName), name);
        }
    }
}