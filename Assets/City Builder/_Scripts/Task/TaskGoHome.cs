using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class TaskGoHome : WorkerTask
    {
        protected override void Working()
        {
            base.Working();
        }

        protected override BuildingController GetBuilding()
        {
            return this.workerController.workerBuildings.GetHome();
        }

        protected override void AssignBuilding(BuildingController buildingController)
        {
            this.workerController.workerBuildings.AssignHome(buildingController);
        }

        protected override BuildingType GetBuildingType()
        {
            return BuildingType.HOME;
        }
    }
}