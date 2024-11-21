using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class Warehouse : LoadMonoBehaviour
    {
        [SerializeField] protected List<ResourceHolder> resHolders;
        [SerializeField] protected BuildingType buildingType;
        public BuildingType BuildingType { get { return buildingType; } }

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
    }
}
