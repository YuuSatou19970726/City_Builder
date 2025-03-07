using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class Warehouse : LoadMonoBehaviour
    {
        [Header("Warehouse")]
        [SerializeField] protected List<ResourceHolder> resHolders;
        [SerializeField] protected BuildingType buildingType;
        public BuildingType BuildingType { get { return buildingType; } }
        [SerializeField] protected bool isFull = false;

        protected override void LoadComponents()
        {
            this.LoadHolders();
        }

        protected virtual void LoadHolders()
        {
            if (this.resHolders.Count > 0) return;

            Transform res = transform.Find(GameObjectTags.RES);

            foreach (Transform resTransform in res)
            {
                ResourceHolder resHolder = resTransform.GetComponent<ResourceHolder>();
                if (resHolder == null) continue;
                this.resHolders.Add(resHolder);
            }
        }

        public virtual ResourceHolder GetHolder(ResourceName name)
        {
            return this.resHolders.Find((holder) => holder.Name() == name);
        }

        public virtual void AddByList(List<Resource> addResources)
        {
            foreach (Resource resource in addResources)
                this.AddResource(resource.name, resource.number);
        }

        public virtual ResourceHolder AddResource(ResourceName resourceName, float number)
        {
            ResourceHolder resourceHolder = this.GetHolder(resourceName);
            resourceHolder.Add(number);
            return resourceHolder;
        }

        public virtual bool IsFull()
        {
            foreach (ResourceHolder resourceHolder in this.resHolders)
                if (!resourceHolder.IsMax()) return false;

            return true;
        }
    }
}
