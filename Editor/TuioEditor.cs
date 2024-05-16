using TuioUnity.Common;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace TuioUnity
{
    public class TuioEditor
    {
        [MenuItem("GameObject/TUIO/TUIO 1.1 Session", false, 1)]
        static void CreateTuio11Manager(MenuCommand menuCommand)
        {
            CreateSession(menuCommand, TuioVersion.Tuio11);
        }
        
        [MenuItem("GameObject/TUIO/TUIO 2.0 Session", false, 2)]
        static void CreateTuio20Manager(MenuCommand menuCommand)
        {
            CreateSession(menuCommand, TuioVersion.Tuio20);
        }

        private static void CreateSession(MenuCommand menuCommand, TuioVersion version)
        {
            var name = version switch
            {
                TuioVersion.Tuio11 => "TUIO 1.1 Session",
                TuioVersion.Tuio20 => "TUIO 2.0 Session"
            };
            GameObject go = new GameObject(name);
            var session = go.AddComponent<TuioSession>();
            session.TuioVersion = version;
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeObject = go;
        }
    }
}
#endif