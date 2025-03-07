using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class ResourceCarrier : MonoBehaviour
    {
        [SerializeField] protected List<Resource> resources;

        public virtual Resource AddResource(ResourceName resourceName, float number)
        {
            Resource resource = this.GetResByName(resourceName);
            resource.number += number;
            return resource;
        }

        public virtual void AddByList(List<Resource> addResources)
        {
            foreach (Resource resource in addResources)
            {
                this.AddResource(resource.name, resource.number);
            }
        }

        public virtual List<Resource> TakeAll()
        {
            List<Resource> resourcesTemp = new List<Resource>(this.resources);
            this.resources = new List<Resource>();
            return resourcesTemp;
        }

        protected virtual Resource GetResByName(ResourceName resourceName)
        {
            Resource resource = this.resources.Find((x) => x.name == resourceName);

            if (resource == null)
            {
                resource = new Resource
                {
                    name = resourceName
                };

                this.resources.Add(resource);
            }

            return resource;
        }
    }
}