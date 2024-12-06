using UnityEngine;

namespace TuioUnity.Common
{
    public static class TuioTransform
    {
        private static Camera s_cam;

        public static Camera Camera
        {
            get
            {
                if (!s_cam)
                {
                    s_cam = Camera.main;
                }

                return s_cam;
            }
        }
        
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