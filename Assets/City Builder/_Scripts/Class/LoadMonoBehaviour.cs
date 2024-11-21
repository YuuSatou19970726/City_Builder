using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class LoadMonoBehaviour : MonoBehaviour
    {
        protected virtual void Awake()
        {
            this.LoadComponents();
        }

        protected virtual void Start()
        {

        }

        protected virtual void FixedUpdate()
        {

        }

        protected virtual void Reset()
        {
            this.LoadComponents();
        }

        protected virtual void LoadComponents()
        {
            // For Override
        }

        protected virtual void OnDisable()
        {
            // For Override
        }
    }
}
