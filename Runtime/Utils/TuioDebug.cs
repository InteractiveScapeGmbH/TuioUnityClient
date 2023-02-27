using TuioUnity.Common;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TuioUnity.Utils
{
    public class TuioDebug : MonoBehaviour
    {
        [SerializeField] private TuioBehaviour _tuioBehaviour;
        [SerializeField] private Image _image;
        [SerializeField] private DebugText _debugTextPrefab;

        private DebugText _debugText;
        private void Start()
        {
            Random.InitState((int)_tuioBehaviour.Id);
            var color = Random.ColorHSV(0f, 1f, 0.7f, 0.8f, 1f, 1f);
            _image.color = color;

            _debugText = Instantiate(_debugTextPrefab, transform.parent);
            _debugText.SetTextColor(color);
            _debugText.SetId(_tuioBehaviour.Id);
            _debugText.UpdateText(_tuioBehaviour);
            _tuioBehaviour.OnUpdate += UpdateText;
        }

        private void OnDestroy()
        {
            _tuioBehaviour.OnUpdate -= UpdateText;
            Destroy(_debugText.gameObject);
        }

        private void UpdateText()
        {
            _debugText.UpdateText(_tuioBehaviour);
        }
    }
}
