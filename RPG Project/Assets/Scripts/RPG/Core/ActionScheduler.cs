using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    // Action Scheduler is the decider mechanism inorder to prevent dependency between Combat and Movement scripts.
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;
        public void StartAction(IAction action)
        {
            // There will be no cancel if action is not changed or null.
            
            if (action == currentAction) return;
           
            if (currentAction != null)
            {
                currentAction.Cancel(); 
            }
            currentAction = action;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
        
        
    }
}

