using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class BuildLevel : LoadMonoBehaviour
    {
        [SerializeField] protected List<Transform> levels;
        [SerializeField] protected int currentLevel = 0;

        private void OnEnable()
        {
            this.ShowBuilding();
            // InvokeRepeating("ShowNextBuild", 3, 2);
        }

        protected override void LoadComponents()
        {
            this.LoadLevels();
        }

        protected virtual void LoadLevels()
        {
            if (this.levels.Count > 0) return;
            Transform buildTranform = transform.Find(GameObjectTags.BUILDINGS);
            foreach (Transform child in buildTranform)
            {
                this.levels.Add(child);
                child.gameObject.SetActive(false);
            }
        }

        protected virtual void ShowBuilding()
        {
            this.HideLastBuild();
            Transform currentBuild = this.levels[this.currentLevel];
            currentBuild.gameObject.SetActive(true);
        }

        protected virtual void HideLastBuild()
        {
            int lastBuildIndex = this.currentLevel - 1;
            if (lastBuildIndex < 0) return;
            Transform lastBuild = this.levels[lastBuildIndex];
            lastBuild.gameObject.SetActive(false);
        }

        // Call from InvokeRepeating
        protected virtual void ShowNextBuild()
        {
            if (this.currentLevel >= this.levels.Count - 2) return;
            // if (this.currentLevel >= this.levels.Count - 1) this.currentLevel = 0;

            this.currentLevel++;
            this.ShowBuilding();
        }

        public virtual void ShowLastBuild()
        {
            this.currentLevel = this.levels.Count - 1;
            this.ShowBuilding();
        }
    }
}