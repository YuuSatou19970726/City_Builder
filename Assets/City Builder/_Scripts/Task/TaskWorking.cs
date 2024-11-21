using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class TaskWorking : WorkerTask
    {
        protected override void Working()
        {
            base.Working();
        }

        protected override BuildingController GetBuilding()
        {
            return this.workerController.workerBuildings.GetWork();
        }

        protected override void AssignBuilding(BuildingController buildingController)
        {
            this.workerController.workerBuildings.AssignWork(buildingController);
        }

        protected override BuildingType GetBuildingType()
        {
            return BuildingType.WORK_STATION;
        }
    }
}