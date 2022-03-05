using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VerbaOven))]
[CanEditMultipleObjects]

public class VerbaOvenEditor : Editor
{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();


        if(GUILayout.Button("Bake!")){
            ((VerbaOven)target).Bake();
        }
    }
}
