using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class BuildingManager : LoadMonoBehaviour
    {
        private static BuildingManager instance;
        public static BuildingManager Instance { get { return instance; } }

        [SerializeField] protected List<BuildingController> buildingControllers;

        protected override void Awake()
        {
            base.Awake();
            if (instance == null) instance = this;
            else Destroy(gameObject);
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadBuildingControllers();
        }

        protected virtual void LoadBuildingControllers()
        {
            if (this.buildingControllers.Count > 0) return;
            foreach (Transform child in transform)
            {
                BuildingController controller = child.GetComponent<BuildingController>();
                if (controller == null) return;
                this.buildingControllers.Add(controller);
            }
        }

        public virtual BuildingController FindBuilding(Transform worker, BuildingType buildingType)
        {
            BuildingController buildingController;
            for (int i = 0; i < this.buildingControllers.Count; i++)
            {
                buildingController = this.buildingControllers[i];
                if (!buildingController.Workers.IsNeedWorker()) continue;
                if (buildingController.Warehouse.BuildingType != buildingType) continue;

                buildingController.Workers.AddWorker(worker);
                return buildingController;
            }
            return null;
        }
    }
}