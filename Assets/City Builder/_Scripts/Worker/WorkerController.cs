using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    [RequireComponent(typeof(WorkerBuildings))]
    [RequireComponent(typeof(WorkerMovement))]
    [RequireComponent(typeof(WorkerTasks))]
    public class WorkerController : LoadMonoBehaviour
    {
        public WorkerBuildings workerBuildings;
        public WorkerMovement workerMovement;
        public WorkerTasks workerTasks;
        public Transform workerModel;
        private Animator animator;

        public ResourceCarrier resourceCarrier;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadWorkerBuildings();
            this.LoadWorkerMovement();
            this.LoadWorkerTasks();
            this.LoadAnimator();
            this.LoadResourceCarrier();
        }

        protected virtual void LoadWorkerBuildings()
        {
            if (this.workerBuildings != null) return;
            this.workerBuildings = GetComponent<WorkerBuildings>();
        }

        protected virtual void LoadWorkerMovement()
        {
            if (this.workerMovement != null) return;
            this.workerMovement = GetComponent<WorkerMovement>();
        }

        protected virtual void LoadWorkerTasks()
        {
            if (this.workerTasks != null) return;
            this.workerTasks = GetComponent<WorkerTasks>();
        }

        protected virtual void LoadAnimator()
        {
            if (this.animator != null) return;
            this.animator = GetComponentInChildren<Animator>();
            this.workerModel = this.animator.transform;
        }

        protected virtual void LoadResourceCarrier()
        {
            if (this.resourceCarrier != null) return;
            this.resourceCarrier = GetComponent<ResourceCarrier>();
        }
    }
}