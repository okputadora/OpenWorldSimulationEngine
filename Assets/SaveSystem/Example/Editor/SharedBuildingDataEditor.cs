using UnityEditor;
using UnityEngine;
public class SharedBuildingDataEditor : Editor
{
  SerializedProperty buildingType;
  void OnEnable()
  {
    buildingType = serializedObject.FindProperty("buildingType");
  }
  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();
    var sharedBuildingData = (SharedBuildingData)target;
    if (GUILayout.Button("Save build Pieces"))
    {

    }
  }
}