using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class ResourceManager : MonoBehaviour
    {
        private static ResourceManager instance;
        public static ResourceManager Instance => instance;

        [SerializeField] protected List<Resource> resources;

        protected void Awake()
        {
            if (instance != null)
                Debug.LogError("Only one ResourcesManager");

            instance = this;
        }

        public virtual void AddResource(ResourceName resourceName, int number)
        {
            Debug.Log("add " + resourceName + " " + number);
            Resource resource = this.GetResByName(resourceName);

            resource.number += number;
        }

        private Resource GetResByName(ResourceName resourceName)
        {
            Resource resource = this.resources.Find((x) => x.resourceName == resourceName);
            if (resource == null)
            {
                resource = new Resource();
                resource.resourceName = resourceName;
                resource.number = 0;

                this.resources.Add(resource);
            }

            return resource;
        }
    }
}