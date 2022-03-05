using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VerbaBakeMaster))]
[CanEditMultipleObjects]
public class VerbaBakeMasterEditor : Editor
{
    public override void OnInspectorGUI(){
        DrawDefaultInspector();


        if(GUILayout.Button("Bake!")){
            ((VerbaBakeMaster)target).Bake();
        }
    }
}
