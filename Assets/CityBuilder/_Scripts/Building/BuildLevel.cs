using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class BuildLevel : CustomMonoBehaviour
    {
        [SerializeField] protected List<Transform> levels;
        [SerializeField] protected int currentLevel = 0;

        protected override void OnEnable()
        {
            this.ShowBuilding();
            InvokeRepeating(nameof(ShowNextBuild), 3, 2);
        }

        protected override void LoadComponents()
        {
            this.LoadLevels();
        }

        protected virtual void LoadLevels()
        {
            if (this.levels.Count > 0) return;
            Transform buildTransform = transform.Find(ObjectTags.BUILDINGS);

            foreach (Transform child in buildTransform)
            {
                this.levels.Add(child);
                child.gameObject.SetActive(false);
            }

            Debug.Log(transform.name + ": Loadbuildings");
        }

        protected virtual void ShowBuilding()
        {

        }

        protected virtual void ShowNextBuild()
        {

        }
    }
}