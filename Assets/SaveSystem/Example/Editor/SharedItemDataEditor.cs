using UnityEditor;

[CustomEditor(typeof(SharedItemData))]
public class SharedItemDataEditor : Editor
{
  SerializedProperty sourceType;

  SerializedProperty recipe;
  SerializedProperty pickable;
  SerializedProperty animal;
  SerializedProperty destructible;
  SerializedProperty destructible2;

  void OnEnable()
  {
    sourceType = serializedObject.FindProperty("sourceType");
    recipe = serializedObject.FindProperty("recipe");
    pickable = serializedObject.FindProperty("pickable");
    animal = serializedObject.FindProperty("animal");
    destructible = serializedObject.FindProperty("destructible");
    destructible2 = serializedObject.FindProperty("destructible2");
  }

  public override void OnInspectorGUI()
  {
    serializedObject.Update();
    DrawPropertiesExcluding(serializedObject, "pickable", "animal", "recipe", "destructible", "destructible2");
    if (sourceType.intValue == 0)
    {
      EditorGUILayout.PropertyField(recipe);
    }
    else if (sourceType.intValue > 1 && sourceType.intValue < 4)
    {
      EditorGUILayout.PropertyField(pickable);
    }
    else if (sourceType.intValue > 3 && sourceType.intValue < 6)
    {
      EditorGUILayout.PropertyField(animal);
    }
    else if (sourceType.intValue > 5)
    {
      EditorGUILayout.PropertyField(destructible);
      EditorGUILayout.PropertyField(destructible2);
    }
    serializedObject.ApplyModifiedProperties();
  }

}
