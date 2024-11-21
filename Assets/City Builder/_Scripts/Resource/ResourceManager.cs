using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class ResourceManager : MonoBehaviour
    {
        private static ResourceManager instance;
        public static ResourceManager Instance { get { return instance; } }

        [SerializeField]
        protected List<Resource> resources;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        public virtual Resource AddResource(ResourceName resourceName, int number)
        {
            Resource res = this.GetResByName(resourceName);
            res.number += number;
            return res;
        }

        protected virtual Resource GetResByName(ResourceName resourceName)
        {
            Resource res = this.resources.Find((value) => value.name == resourceName);

            if (res == null)
            {
                res = new Resource
                {
                    name = resourceName,
                    number = 0
                };

                this.resources.Add(res);
            }

            return res;
        }
    }
}

