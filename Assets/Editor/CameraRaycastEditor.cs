using System;
using UnityEditor;
using UnityEngine.Collections;

//TODO consider changing to a property drawer
[CustomEditor(typeof(CameraRaycast))]
public class CameraRaycastEditor : Editor {
    
    //store the UI state
    private bool isLayerPrioritiesUnfolded = true; 

    public override void OnInspectorGUI() {
        //Serialize cameraRaycast instance
        serializedObject.Update();

        //Do stuff to UI
        isLayerPrioritiesUnfolded = EditorGUILayout.Foldout(isLayerPrioritiesUnfolded, "Layer Priorities");
        if (isLayerPrioritiesUnfolded) {
            EditorGUI.indentLevel++;
            {
                BindArraySize();
                BindArrayElements();
            }
            EditorGUI.indentLevel--;
        }

        //De-serialize back to cameraRaycast (and create undo point)
        serializedObject.ApplyModifiedProperties();
    }
    
    private void BindArraySize() {
        int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue;
        int requiredArraySize = EditorGUILayout.IntField("Size", currentArraySize);
        if (requiredArraySize != currentArraySize) {
            serializedObject.FindProperty("layerPriorities.Array.size").intValue = requiredArraySize;
        }
    }

    private void BindArrayElements() {
        int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue;
        for (int i = 0; i < currentArraySize; i++) {
            SerializedProperty prop = serializedObject.FindProperty(string.Format("layerPriorities.Array.data[{0}]", i));
            prop.intValue = EditorGUILayout.LayerField(string.Format("Layer {0}", i), prop.intValue);
        }
    }

}
