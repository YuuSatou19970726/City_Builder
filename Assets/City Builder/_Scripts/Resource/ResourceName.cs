using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class ResNameParser
    {
        public static ResourceName FromString(string name)
        {
            name = name.ToLower();
            return (ResourceName)Enum.Parse(typeof(ResourceName), name);
        }
    }

    public enum ResourceName
    {
        noResource = 0,

        //Money
        gold = 1,
        dimond = 2,

        //Material Lvel 1
        water = 1000,
        logwood = 1001,
        iron_ore = 1002,

        //Material Lvel 2
        blank = 2001,
        iron_ignot = 2002
    }
}
