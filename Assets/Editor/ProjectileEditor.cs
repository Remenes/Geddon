using UnityEditor;

//TODO consider switching editors to property drawer
[CustomEditor(typeof(Projectile))]
public class ProjectileEditor : Editor {

    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();
        serializedObject.Update();

        OnBindSpeed();
        OnBindStringTag();    

        serializedObject.ApplyModifiedProperties();
    }

    private void OnBindSpeed() {
        float speedValue = serializedObject.FindProperty("speed").floatValue;
        float newSpeedValue = EditorGUILayout.FloatField("Speed", speedValue);
        serializedObject.FindProperty("speed").floatValue = newSpeedValue;
    }

    private void OnBindStringTag() {
        SerializedProperty currentStringTag = serializedObject.FindProperty("tagToDamage");
        currentStringTag.stringValue = EditorGUILayout.TagField("Tag to Damage", currentStringTag.stringValue);
        
    }

}
