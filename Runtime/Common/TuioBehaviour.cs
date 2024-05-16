using UnityEngine;

namespace TuioUnity.Common
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class TuioBehaviour : MonoBehaviour
    {
        public abstract string DebugText();
        
        protected RectTransform RectTransform;

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }
    }
}