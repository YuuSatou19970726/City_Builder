using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class GeneratorTree : ResourceGenerator
    {
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadResourceCreate();
        }

        protected virtual void LoadResourceCreate()
        {
            Resource resource = new Resource
            {
                resourceName = ResourceName.logwood,
                number = 1,
            };


            // this.resourceCreate.Clear();
            this.resourceCreate.Add(resource);
        }
    }
}