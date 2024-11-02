using UnityEditor;

[CustomEditor(typeof(SharedGatherOccupationData))]
public class SharedGatherOccupationDataEditor : Editor
{
  SerializedProperty hasToAttackToPickup;
  SerializedProperty attackTargets;

  void OnEnable()
  {
    hasToAttackToPickup = serializedObject.FindProperty("hasToAttackToPickup");
    attackTargets = serializedObject.FindProperty("attackTargets");
  }

  public override void OnInspectorGUI()
  {
    serializedObject.Update();
    DrawPropertiesExcluding(serializedObject, "attackTargets");
    if (hasToAttackToPickup.boolValue)
    {
      EditorGUILayout.PropertyField(attackTargets);
    }
    serializedObject.ApplyModifiedProperties();
  }
}