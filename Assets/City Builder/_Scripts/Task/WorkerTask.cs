using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class WorkerTask : LoadMonoBehaviour
    {
        [SerializeField] protected WorkerController workerController;
        public WorkerController WorkerController { get { return workerController; } }
        [SerializeField] protected float buildingDistance = 0;
        [SerializeField] protected float buildDisllimit = 0.9f;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (this.GetBuilding()) this.GettingReadyForWork();
            else this.FindBuilding();

            if (workerController.workerTasks.ReadyForTask) this.Working();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            this.GoOutBuilding();

            this.workerController.workerTasks.SetReadyForTask(false);
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadWorkerController();
        }

        protected virtual void LoadWorkerController()
        {
            if (workerController != null) return;
            this.workerController = transform.parent.parent.GetComponent<WorkerController>();
        }

        protected virtual void FindBuilding()
        {
            BuildingController buildingController = BuildingManager.Instance.FindBuilding(transform, this.GetBuildingType());
            if (buildingController == null) return;
            this.AssignBuilding(buildingController);
        }

        // protected virtual void WorkPlanning()
        // {
        //     if (this.IsAtBuilding()) this.GoIntoBuilding();
        //     else this.GoToBuilding();

        //     if (this.workerController.workerTasks.InHouse) this.Working();
        // }

        protected virtual void GettingReadyForWork()
        {
            if (this.workerController.workerTasks.ReadyForTask) return;

            if (!this.IsAtBuilding())
            {
                this.GoToBuilding();
                return;
            }

            this.workerController.workerTasks.SetReadyForTask(true);
            this.GoIntoBuilding();
        }

        public virtual bool IsAtBuilding()
        {
            return this.BuildingDistance() < this.buildDisllimit;
        }

        protected virtual float BuildingDistance()
        {
            BuildingController buildingController = this.GetBuilding();
            this.buildingDistance = Vector3.Distance(transform.position, buildingController.Door.transform.position);
            return this.buildingDistance;
        }

        public virtual void GoIntoBuilding()
        {
            if (this.workerController.workerTasks.InHouse) return;

            this.workerController.workerMovement.SetTarget(null);
            this.workerController.workerTasks.SetInHouse(true);
            this.workerController.workerModel.gameObject.SetActive(false);
        }

        public virtual void GoOutBuilding()
        {
            if (!this.workerController.workerTasks.InHouse) return;

            this.workerController.workerTasks.SetInHouse(false);
            this.workerController.workerModel.gameObject.SetActive(true);
        }

        public virtual void GoToBuilding()
        {
            BuildingController buildingController = this.GetBuilding();
            this.workerController.workerMovement.SetTarget(buildingController.Door);
        }

        protected virtual void Working()
        {
            this.GetBuilding().BuildingTask.DoingTask(this.workerController);
        }

        protected virtual BuildingController GetBuilding()
        {
            return null;
        }

        protected virtual void AssignBuilding(BuildingController buildingController)
        {
            // For overide
        }

        protected virtual BuildingType GetBuildingType()
        {
            return BuildingType.NONE;
        }
    }
}