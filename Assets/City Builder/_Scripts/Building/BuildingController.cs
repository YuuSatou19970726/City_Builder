using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class BuildingController : LoadMonoBehaviour
    {
        [SerializeField] private Workers workers;
        public Workers Workers { get { return workers; } }
        [SerializeField] private Transform door;
        public Transform Door { get { return door; } }
        [SerializeField] Warehouse warehouse;
        public Warehouse Warehouse { get { return warehouse; } }
        [SerializeField] private BuildingTask buildingTask;
        public BuildingTask BuildingTask { get { return buildingTask; } }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadWorkers();
            this.LoadDoor();
            this.LoadWareHouse();
            this.LoadBuildingTask();
        }

        protected virtual void LoadWorkers()
        {
            if (this.workers != null) return;

            this.workers = GetComponent<Workers>();
        }

        protected virtual void LoadDoor()
        {
            if (this.door != null) return;
            this.door = transform.Find(GameObjectTags.DOOR);
        }

        protected virtual void LoadWareHouse()
        {
            if (this.warehouse != null) return;
            this.warehouse = GetComponent<Warehouse>();
        }

        protected virtual void LoadBuildingTask()
        {
            if (this.buildingTask != null) return;
            this.buildingTask = GetComponent<BuildingTask>();
        }
    }
}