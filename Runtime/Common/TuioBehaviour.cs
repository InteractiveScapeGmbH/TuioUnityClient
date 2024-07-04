using UnityEngine;

namespace TuioUnity.Common
{
    /// <summary>
    /// Base class for all Tuio (1.1 and 2.0) MonoBehaviours.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public abstract class TuioBehaviour : MonoBehaviour
    {
        public abstract string DebugText();
        
        protected RectTransform RectTransform;

        protected virtual void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }
    }
}