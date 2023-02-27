using TMPro;
using TuioUnity.Common;
using UnityEngine;

namespace TuioUnity.Utils
{
    public class DebugText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _id;
        [SerializeField] private TMP_Text _angle;
        [SerializeField] private TMP_Text _position;
        
        public void SetTextColor(Color color)
        {
            _id.color = color;
            _angle.color = color;
            _position.color = color;
        }
        
        public void SetId(uint id)
        {
            _id.text = $"ID: {id}";
        }
        
        private void SetAngle(float angle)
        {
            _angle.text = $"{angle:f2} \u00ba";
        }
        
        private void SetPosition(Vector2 positon)
        {
            _position.text = $"x: {positon.x:f2}, \ny: {positon.y:f2}";
        }

        public void UpdateText(TuioBehaviour tuioBehaviour)
        {
            SetAngle(tuioBehaviour.transform.eulerAngles.z);
            SetPosition(tuioBehaviour.transform.position);
            transform.position = tuioBehaviour.transform.position;
        }
    }
}
