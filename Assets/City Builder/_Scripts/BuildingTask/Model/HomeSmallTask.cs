using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder
{
    public class HomeSmallTask : BuildingTask
    {
        public override void DoingTask(WorkerController workerController)
        {
            if (!this.IsTimeToWork()) return;
            string message = workerController.name + " Working at " + transform.name;
        }
    }
}