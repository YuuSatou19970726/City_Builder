using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class WorkerTasks : LoadMonoBehaviour
    {
        private WorkerController workerController;
        public WorkerController WorkerController { get { return workerController; } }
        [SerializeField] protected bool isNightTime = false;
        [SerializeField] protected WorkerTask taskWorking;
        public WorkerTask TaskWorking { get { return taskWorking; } }
        [SerializeField] protected WorkerTask taskGoHome;
        private bool inHouse = false;
        public bool InHouse { get { return inHouse; } }
        private bool readyForTask = false;
        public bool ReadyForTask { get { return readyForTask; } }
        [SerializeField] protected List<TaskType> tasks;
        [SerializeField] protected Transform taskTarget;
        public Transform TaskTarget { get { return taskTarget; } }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (this.isNightTime) this.GoToHome();
            else this.GoToWork();
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadWorkerController();
        }

        protected virtual void LoadWorkerController()
        {
            if (this.workerController != null) return;
            this.workerController = GetComponent<WorkerController>();
        }

        protected virtual void GoToWork()
        {
            this.taskGoHome.gameObject.SetActive(false);
            this.taskWorking.gameObject.SetActive(true);
        }

        protected virtual void GoToHome()
        {
            this.taskWorking.gameObject.SetActive(false);
            this.taskGoHome.gameObject.SetActive(true);
        }

        public virtual void SetReadyForTask(bool checkReadyForTask)
        {
            this.readyForTask = checkReadyForTask;
        }

        public virtual void SetInHouse(bool checkInHouse)
        {
            this.inHouse = checkInHouse;
        }

        public virtual void TaskAdd(TaskType taskType)
        {
            TaskType currentTask = this.TaskCurrent();
            if (taskType == currentTask) return;
            this.tasks.Add(taskType);
        }

        public virtual TaskType TaskCurrent()
        {
            if (this.tasks.Count <= 0) return TaskType.NONE;
            return this.tasks[this.tasks.Count - 1];
        }

        public virtual void TaskCurrentDone()
        {
            this.tasks.RemoveAt(this.tasks.Count - 1);
        }

        public virtual void SetTaskTarget(Transform transform)
        {
            this.taskTarget = transform;
        }
    }
}