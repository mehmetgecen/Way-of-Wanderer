using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    // Action Scheduler is the decider mechanism inorder to prevent dependency between Combat and Movement scripts.
    public class ActionScheduler : MonoBehaviour
    {
        MonoBehaviour currentAction;
        public void StartAction(MonoBehaviour action)
        {
            // There will be no cancel if action is not changed or null.
            
            if (action == currentAction) return;
           
            if (currentAction != null)
            {
                print("Cancelling Action " + currentAction); 
            }
            currentAction = action;
        }
        
    }
}

