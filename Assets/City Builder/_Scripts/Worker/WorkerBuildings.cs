using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class WorkerBuildings : LoadMonoBehaviour
    {
        [SerializeField] protected BuildingController workBuilding;
        [SerializeField] protected BuildingController homeBuilding;
        [SerializeField] protected List<BuildingController> inBuildings;
        [SerializeField] protected List<BuildingController> relaxBuildings;

        public virtual void AssignWork(BuildingController buildingController)
        {
            this.workBuilding = buildingController;
        }

        public virtual BuildingController GetWork()
        {
            return this.workBuilding;
        }

        public virtual void AssignHome(BuildingController buildingController)
        {
            this.homeBuilding = buildingController;
        }

        public virtual BuildingController GetHome()
        {
            return this.homeBuilding;
        }
    }
}