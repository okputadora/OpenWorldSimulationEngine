using UnityEditor;

[CustomEditor(typeof(ItemRecipe))]
public class ItemRecipeEditor : Editor
{
  SerializedProperty hasIngredients;
  SerializedProperty ingredients;
  SerializedProperty oneOfEachRequirements;

  void OnEnable()
  {
    hasIngredients = serializedObject.FindProperty("hasIngredients");
    ingredients = serializedObject.FindProperty("ingredients");
    oneOfEachRequirements = serializedObject.FindProperty("oneOfEachRequirements");
  }

  public override void OnInspectorGUI()
  {
    serializedObject.Update();
    DrawPropertiesExcluding(serializedObject, "ingredients", "oneOfEachRequirements");
    if (hasIngredients.boolValue)
    {
      EditorGUILayout.PropertyField(ingredients);
    }
    else
    {
      EditorGUILayout.PropertyField(oneOfEachRequirements);
    }
    serializedObject.ApplyModifiedProperties();
  }

}
