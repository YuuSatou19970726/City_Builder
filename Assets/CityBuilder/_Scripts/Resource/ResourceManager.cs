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

        [SerializeField] protected List<ResourceHolder> resourceHolders;

        protected void Awake()
        {
            if (instance != null)
                Debug.LogError("Only one ResourcesManager");

            instance = this;
        }

        public virtual void AddResource(ResourceName resourceName, int number)
        {
            Debug.Log("add " + resourceName + " " + number);
            ResourceHolder resourceHolder = this.GetResByName(resourceName);

            resourceHolder.number += number;
        }

        private ResourceHolder GetResByName(ResourceName resourceName)
        {
            ResourceHolder resourceHolder = this.resourceHolders.Find((x) => x.resourceName == resourceName);
            if (resourceHolder == null)
            {
                resourceHolder = new ResourceHolder();
                resourceHolder.resourceName = resourceName;
                resourceHolder.number = 0;

                this.resourceHolders.Add(resourceHolder);
            }

            return resourceHolder;
        }
    }
}