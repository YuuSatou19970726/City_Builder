using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class ForestHut : Warehouse
    {
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.buildingType = BuildingType.WORK_STATION;
        }
    }
}
