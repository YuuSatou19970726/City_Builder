using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class Workers : LoadMonoBehaviour
    {
        [SerializeField] protected int maxWorker = 1;
        [SerializeField] protected List<Transform> workers;

        protected override void LoadComponents()
        {
            this.LoadWorkers();
        }

        protected virtual void LoadWorkers()
        {
            if (this.workers.Count > 0) return;

            Transform workersTemp = transform.Find(GameObjectTags.WORKERS);
            foreach (Transform worker in workersTemp)
            {
                this.workers.Add(worker);
            }
        }

        public virtual bool IsNeedWorker()
        {
            if (this.workers.Count >= this.maxWorker) return false;
            return true;
        }

        public virtual void AddWorker(Transform worker)
        {
            this.workers.Add(worker);
        }
    }
}