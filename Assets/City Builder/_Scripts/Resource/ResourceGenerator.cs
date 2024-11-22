using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class ResourceGenerator : Warehouse
    {
        [SerializeField] protected List<Resource> resCreate;
        [SerializeField] protected List<Resource> resRequire;
        [SerializeField] protected float createTimer = 0f;
        [SerializeField] protected float createDelay = 2f;

        protected override void FixedUpdate()
        {
            this.Creating();
        }

        protected virtual void Creating()
        {
            this.createTimer += Time.fixedDeltaTime;
            if (this.createTimer < this.createDelay) return;
            this.createTimer = 0;

            if (!this.IsRequireEnough()) return;

            foreach (Resource res in this.resCreate)
            {
                ResourceHolder resHolder = this.GetHolder(res.name);
                resHolder.Add(res.number);
            }
        }

        protected virtual bool IsRequireEnough()
        {
            if (this.resRequire.Count < 1) return true;

            return false;
        }

        public virtual float GetCreateDelay()
        {
            return this.createDelay;
        }

        public virtual List<Resource> TakeAll()
        {
            List<Resource> resources = new List<Resource>();
            foreach (ResourceHolder resourceHolder in this.resHolders)
            {
                Resource newResource = new Resource
                {
                    name = resourceHolder.Name(),
                    number = (int)resourceHolder.TakeAll(),
                };

                resources.Add(newResource);
            }
            return resources;
        }
    }
}
