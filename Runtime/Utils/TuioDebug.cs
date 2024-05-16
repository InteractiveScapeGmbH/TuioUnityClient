using TMPro;
using TuioUnity.Common;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TuioUnity.Utils
{
    [RequireComponent(typeof(TuioBehaviour))]
    public class TuioDebug : MonoBehaviour
    {
        [SerializeField] private TMP_Text _debugText;
        [SerializeField] private Image _background;

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
