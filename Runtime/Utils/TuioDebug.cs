using TMPro;
using TuioUnity.Common;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TuioUnity.Utils
{
    /// <summary>
    /// Simple component to display properties of tuio objects in the scene and set a random color for easier
    /// distinction between objects or touches.
    /// </summary>
    [RequireComponent(typeof(TuioBehaviour))]
    public class TuioDebug : MonoBehaviour
    {
        [SerializeField] private TMP_Text _debugText;
        [SerializeField] private MaskableGraphic _background;

        private TuioBehaviour _tuioBehaviour;
        private void Start()
        {
            _tuioBehaviour = GetComponent<TuioBehaviour>();
            var color = Random.ColorHSV(0f, 1f, 0.7f, 0.8f, 1f, 1f);
            _background.color = color;
            _debugText.color = color;
        }

        private void Update()
        {
            _debugText.text = _tuioBehaviour.DebugText();
        }
    }
}
