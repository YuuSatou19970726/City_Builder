using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CityBuilder
{
    [RequireComponent(typeof(WorkerController))]
    public class WorkerMovement : LoadMonoBehaviour
    {
        private WorkerController workerController;
        public WorkerController WorkerController { get { return workerController; } }
        [SerializeField] protected Transform target;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Animator animator;
        private bool isWalking = false;
        private bool isWorking = false;
        public bool IsWorking { get { return isWorking; } }
        [SerializeField] protected float walkLimit = 0.7f;
        [SerializeField] protected float targetDistance = 0f;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadWorkerController();
            this.LoadAgent();
            this.LoadAnimator();
        }

        protected override void FixedUpdate()
        {
            this.Moving();
            this.Animation();
            // this.FindHouse();
        }

        protected virtual void LoadWorkerController()
        {
            if (this.workerController != null) return;
            this.workerController = GetComponent<WorkerController>();
        }

        protected virtual void LoadAgent()
        {
            if (this.navMeshAgent != null) return;
            this.navMeshAgent = GetComponent<NavMeshAgent>();

            Debug.Log(transform.name + ": LoadAgent", gameObject);
        }

        protected virtual void LoadAnimator()
        {
            if (this.animator != null) return;
            this.animator = GetComponentInChildren<Animator>();

            Debug.Log(transform.name + ": LoadAnimator", gameObject);
        }

        public virtual void SetTarget(Transform trans)
        {
            this.target = trans;
        }

        public virtual Transform GetTarget()
        {
            return this.target;
        }

        protected virtual void Moving()
        {
            if (this.target == null || this.IsCloseToTarget())
            {
                this.navMeshAgent.isStopped = true;
                this.isWalking = false;
                return;
            }

            this.isWalking = true;
            this.navMeshAgent.isStopped = false;
            this.navMeshAgent.SetDestination(this.target.position);
        }


        protected virtual void Animation()
        {
            this.animator.SetBool(AnimationWoodCutterTags.BOOL_ISWALKING, this.isWalking);
            this.animator.SetBool(AnimationWoodCutterTags.BOOL_ISWORKING, this.isWorking);
        }

        public virtual bool IsCloseToTarget()
        {
            if (this.target == null) return false;
            Vector3 targetPos = this.target.position;
            targetPos.y = transform.position.y;

            this.targetDistance = Vector3.Distance(transform.position, targetPos);
            return this.targetDistance < this.walkLimit;
        }

        public virtual float TargetDistance()
        {
            return this.targetDistance;
        }

        public virtual void SetIsWorking(bool checkWorking)
        {
            this.isWorking = checkWorking;
        }

        // protected virtual void FindHouse()
        // {
        //     if (this.workerController.workerBuildings.GetWork() != null) return;

        //     BuildingController buildingController = BuildingManager.Instance.FindBuilding(transform);
        //     if (buildingController == null) return;
        //     this.workerController.workerBuildings.AssignWork(buildingController);
        //     this.target = buildingController.Door;
        // }
    }
}