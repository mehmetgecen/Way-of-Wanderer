using UnityEngine;

namespace RPG.UI
{
    public class DestroyText : MonoBehaviour
    {
        [SerializeField] private GameObject targetToDestroy = null;
        
        public void DestroyTextObject()
        {
            Destroy(targetToDestroy);
        }

       
    }
}
