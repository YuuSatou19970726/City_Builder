using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public abstract class BuildingTask : LoadMonoBehaviour
    {
        [Header("Building Task")]
        private BuildingController buildingController;
        [SerializeField] protected float taskTimer = 0;
        [SerializeField] protected float taskDelay = 5f;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadBuildingController();
        }

        protected virtual void LoadBuildingController()
        {
            if (this.buildingController != null) return;
            this.buildingController = GetComponent<BuildingController>();
        }

        protected virtual bool IsTimeToWork()
        {
            this.taskTimer += Time.fixedDeltaTime;
            if (this.taskTimer < this.taskDelay) return false;
            this.taskTimer = 0;
            return true;
        }

        protected virtual void BackToWorkStation(WorkerController workerController)
        {
            WorkerTask workerTask = workerController.workerTasks.TaskWorking;
            workerTask.GoToBuilding();
            if (workerController.workerMovement.IsCloseToTarget())
            {
                workerTask.GoIntoBuilding();
                workerController.workerTasks.TaskCurrentDone();
            }
        }

        public abstract void DoingTask(WorkerController workerController);
    }
}