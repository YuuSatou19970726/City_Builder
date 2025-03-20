using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class ResourceGenerator : CustomMonoBehaviour
    {
        [SerializeField] protected List<ResourceHolder> resourceHolders;
        [SerializeField] protected List<Resource> resourceCreate;
        [SerializeField] protected List<Resource> resourceRequire;
        [SerializeField] protected float createTimer = 0f;
        [SerializeField] protected float createDelay = 2f;

        protected override void FixedUpdate()
        {
            this.Creating();
        }

        protected override void LoadComponents()
        {
            this.LoadHolders();
        }

        protected virtual void LoadHolders()
        {
            Transform res = transform.Find(ObjectTags.RES);

            foreach (Transform resTran in res)
            {
                // Debug.Log(resTran.name);
                ResourceHolder resourceHolder = resTran.GetComponent<ResourceHolder>();
                if (resourceHolder == null) continue;
                this.resourceHolders.Add(resourceHolder);
            }

            // Debug.Log(transform.name + ": LoadHolders");
        }

        protected virtual void Creating()
        {
            this.createTimer += Time.fixedDeltaTime;
            if (this.createTimer < this.createDelay) return;
            this.createTimer = 0;

            if (!this.IsRequireEnough()) return;

            foreach (Resource resource in this.resourceCreate)
            {
                ResourceHolder resourceHolder = this.resourceHolders.Find((holder) => holder.Name() == resource.resourceName);
                resourceHolder.Add(resource.number);
            }
        }

        protected virtual bool IsRequireEnough()
        {
            if (this.resourceRequire.Count < 1) return true;

            //TODO: this is not done yet
            return false;
        }
    }
}