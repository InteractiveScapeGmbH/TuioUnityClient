using UnityEditor;
using UnityEngine;

public class TuioEditor : MonoBehaviour
{
    [MenuItem("GameObject/TUIO/TUIO 1.1 Manager", false, 1)]
    static void CreateTuio11Manager(MenuCommand menuCommand)
    {
        GameObject go = new GameObject("TUIO 1.1 Manager");
        go.AddComponent<Tuio11Manager>();
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
    
    [MenuItem("GameObject/TUIO/TUIO 2.0 Manager", false, 2)]
    static void CreateTuio20Manager(MenuCommand menuCommand)
    {
        GameObject go = new GameObject("TUIO 2.0 Manager");
        go.AddComponent<Tuio20Manager>();
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
}