using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace CityBuilder
{
    public class ForestHutTask : BuildingTask
    {
        [Header("Forest Hut")]
        [SerializeField] protected GameObject plantTreeObj;
        [SerializeField] protected List<GameObject> treePrefabs;
        [SerializeField] protected float treeRange = 27f;
        [SerializeField] protected float treeDistance = 7f;
        [SerializeField] protected int treeMax = 7;
        [SerializeField] private List<GameObject> trees;
        [SerializeField] private int storeMax = 7;
        [SerializeField] private int storeCurrent = 0;
        [SerializeField] private float chopSpeed = 7f;
        [SerializeField] private float treeRemoveSpeed = 16;

        protected override void Start()
        {
            base.Start();
            this.LoadNearByTrees();
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadObjects();
            this.LoadTreePrefab();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            StopAllCoroutines();
        }

        protected virtual void LoadObjects()
        {
            if (this.plantTreeObj != null) return;
            this.plantTreeObj = Resources.Load<GameObject>(ResourcesTags.BUILDING_MASK_POSITION_OBJECT);
        }

        protected virtual void LoadTreePrefab()
        {
            if (this.treePrefabs.Count > 0) return;
            GameObject tree_1 = Resources.Load<GameObject>(ResourcesTags.RES_TREE);
            this.treePrefabs.Add(tree_1);
        }

        protected virtual void LoadNearByTrees()
        {
            List<GameObject> allTrees = TreeManager.Instance.Trees();
            if (allTrees.Count <= 0) return;

            float dis;
            foreach (GameObject tree in allTrees)
            {
                dis = Vector3.Distance(tree.transform.position, transform.position);

                if (dis > this.treeRange) continue;
                this.TreeAdd(tree);
            }
        }

        protected virtual void TreeAdd(GameObject tree)
        {
            if (this.trees.Contains(tree)) return;
            this.trees.Add(tree);
        }

        public override void DoingTask(WorkerController workerController)
        {
            // if (!this.IsTimeToWork()) return;
            // string message = workerController.name + " Working at " + transform.name;
            // this.PlantTree(workerController);

            switch (workerController.workerTasks.TaskCurrent())
            {
                case TaskType.PLANT_TREE:
                    this.PlantTree(workerController);
                    break;
                case TaskType.FIND_TREE_TO_CHOP:
                    this.FindTreeToChop(workerController);
                    break;
                case TaskType.CHOP_TREE:
                    this.ChopTree(workerController);
                    break;
                case TaskType.BRING_RESOURCE_BACK:
                    this.BringTreeBack(workerController);
                    break;
                case TaskType.GO_TO_WORK_STATION:
                    this.BackToWorkStation(workerController);
                    break;
                default:
                    if (this.IsTimeToWork()) this.Planning(workerController);
                    break;
            }
        }

        protected virtual void PlantTree(WorkerController workerController)
        {
            Transform target = workerController.workerMovement.GetTarget();
            if (target == null) target = this.GetPlantPlace();
            if (target == null) return;

            workerController.workerTasks.TaskWorking.GoOutBuilding();
            workerController.workerMovement.SetTarget(target);

            if (workerController.workerMovement.IsCloseToTarget())
            {
                workerController.workerMovement.SetTarget(null);
                Destroy(target.gameObject); //TODO: not done yet
                this.Planting(workerController.transform);

                if (!this.NeedMoreTree())
                {
                    workerController.workerTasks.TaskCurrentDone();
                    workerController.workerTasks.TaskAdd(TaskType.GO_TO_WORK_STATION);
                }
            }
        }

        protected virtual void Planting(Transform transform)
        {
            GameObject treeObjPrefab = this.GetTreePrefab();
            GameObject treeObj = Instantiate<GameObject>(treeObjPrefab);
            treeObj.transform.position = transform.position;
            treeObj.transform.rotation = transform.rotation;
            this.trees.Add(treeObj);
            TreeManager.Instance.TreeAdd(treeObj);
        }

        protected virtual GameObject GetTreePrefab()
        {
            int rand = Random.Range(0, this.treePrefabs.Count - 1);
            return this.treePrefabs[rand];
        }

        protected virtual Transform GetPlantPlace()
        {
            Vector3 newTreePos = this.RandomPlaceForTree();
            if (!IsPointOnNavMesh(newTreePos)) return null;

            float dis = Vector3.Distance(transform.position, newTreePos);
            if (dis < this.treeDistance) return null;

            GameObject treePlace = Instantiate(this.plantTreeObj);
            treePlace.transform.position = newTreePos;

            return treePlace.transform;
        }

        protected virtual bool IsPointOnNavMesh(Vector3 pointToCheck)
        {
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(pointToCheck, out navMeshHit, 1.0f, NavMesh.AllAreas)) return true;
            return false;
        }

        protected virtual Vector3 RandomPlaceForTree()
        {
            Vector3 position = transform.position;
            position.x += Random.Range(this.treeRange * -1, this.treeRange);
            position.y = 0;
            position.z += Random.Range(this.treeRange * -1, this.treeRange);

            return position;
        }

        protected virtual void Planning(WorkerController workerController)
        {
            if (!this.buildingController.Warehouse.IsFull())
            {
                workerController.workerTasks.TaskAdd(TaskType.BRING_RESOURCE_BACK);
                workerController.workerTasks.TaskAdd(TaskType.CHOP_TREE);
                workerController.workerTasks.TaskAdd(TaskType.FIND_TREE_TO_CHOP);
            }

            if (this.NeedMoreTree())
            {
                workerController.workerMovement.SetTarget(null);
                workerController.workerTasks.TaskAdd(TaskType.PLANT_TREE);
            }
        }

        protected virtual bool NeedMoreTree()
        {
            return this.treeMax >= this.trees.Count;
        }

        protected virtual bool IsStoreFull()
        {
            return this.storeCurrent >= this.storeMax;
        }

        protected virtual void ChopTree(WorkerController workerController)
        {
            // TreeController treeController = this.GetNearestTree();
            // workerController.workerMovement.SetTarget(treeController.transform);
            if (workerController.workerMovement.IsWorking) return;

            StartCoroutine(Chopping(workerController, workerController.workerTasks.TaskTarget));
        }

        IEnumerator Chopping(WorkerController workerController, Transform tree)
        {
            workerController.workerMovement.SetIsWorking(true);
            yield return new WaitForSeconds(this.chopSpeed);

            TreeController treeController = tree.GetComponent<TreeController>();
            treeController.treeLevel.ShowLastBuild();
            List<Resource> resources = treeController.logwood.TakeAll();
            treeController.choper = null;
            this.trees.Remove(treeController.gameObject);
            TreeManager.Instance.Trees().Remove(treeController.gameObject);

            workerController.workerMovement.SetIsWorking(false);
            workerController.workerTasks.SetTaskTarget(null);
            workerController.resourceCarrier.AddByList(resources);
            workerController.workerTasks.TaskCurrentDone();

            StartCoroutine(RemoveTree(tree));
        }

        IEnumerator RemoveTree(Transform tree)
        {
            yield return new WaitForSeconds(this.treeRemoveSpeed);
            Destroy(tree.gameObject);
        }

        protected virtual TreeController GetNearestTree()
        {
            foreach (GameObject tree in this.trees)
            {
                TreeController treeController = tree.GetComponent<TreeController>();
                if (treeController.treeLevel.IsMaxLevel()) return treeController;
            }

            return null;
        }

        protected virtual void FindTreeToChop(WorkerController workerController)
        {
            WorkerTasks workerTasks = workerController.workerTasks;
            if (workerTasks.InHouse) workerTasks.TaskWorking.GoOutBuilding();

            if (workerController.workerTasks.TaskTarget == null)
            {
                this.FindNearestTree(workerController);
            }
            else if (workerController.workerMovement.TargetDistance() <= 1.5f)
            {
                workerController.workerMovement.SetTarget(null);
                workerController.workerTasks.TaskCurrentDone();
            }
        }

        protected virtual void FindNearestTree(WorkerController workerController)
        {
            foreach (GameObject tree in this.trees)
            {
                TreeController treeController = tree.GetComponent<TreeController>();//TODO: can make it faster
                if (treeController == null) continue;
                if (!treeController.treeLevel.IsMaxLevel()) continue;
                if (treeController.choper != null) return;

                treeController.choper = workerController;
                workerController.workerTasks.SetTaskTarget(treeController.transform);
                workerController.workerMovement.SetTarget(treeController.transform);
                return;
            }
        }

        protected virtual void BringTreeBack(WorkerController workerController)
        {
            WorkerTask workerTask = workerController.workerTasks.TaskWorking;
            workerTask.GoToBuilding();
            if (!workerController.workerMovement.IsCloseToTarget()) return;

            List<Resource> resources = workerController.resourceCarrier.TakeAll();
            this.buildingController.Warehouse.AddByList(resources);
            workerTask.GoIntoBuilding();

            workerController.workerTasks.TaskCurrentDone();
        }
    }
}