using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class ResourceGenerator : MonoBehaviour
    {
        [SerializeField] protected ResourceName resourceName;
        [SerializeField] protected float spped = 2f;
        [SerializeField] protected float timer = 0;
        [SerializeField] protected int number = 1;

        private void FixedUpdate()
        {
            this.Generating();
        }

        protected virtual void Generating()
        {
            if (this.resourceName == ResourceName.noResource) return;

            this.timer += Time.fixedDeltaTime;
            if (this.timer < this.spped) return;
            this.timer = 0;

            // Debug.Log("Add:" + this.resourceName);
            ResourceManager.Instance.AddResource(this.resourceName, this.number);
        }
    }
}