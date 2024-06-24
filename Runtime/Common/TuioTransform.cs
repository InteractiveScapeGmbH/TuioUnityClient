using UnityEngine;

namespace TuioUnity.Common
{
    public static class TuioTransform
    {
        public static Camera Camera { get; set; } = Camera.main;
        
        public static Vector2 GetWorldPosition(Vector2 tuioPosition)
        {
            tuioPosition.y = (1 - tuioPosition.y);
            return Camera.ViewportToWorldPoint(tuioPosition);
        }
        
        public static Vector2 GetScreenPosition(Vector2 tuioPosition)
        {
            tuioPosition.y = (1 - tuioPosition.y);
            return Camera.ViewportToScreenPoint(tuioPosition);
        }

        public static Vector2 GetScreenSpaceSize(Vector2 size)
        {
            size.x *= Camera.pixelWidth;
            size.y *= Camera.pixelHeight;
            return size;
        }
    }
}