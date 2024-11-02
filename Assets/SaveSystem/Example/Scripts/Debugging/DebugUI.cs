using UnityEngine;
public class DebugUI : MonoBehaviour
{
  [SerializeField] CivilizationDebugUI civilizationDebugUI;
  public void Update()
  {
    if (Input.GetKeyDown(KeyCode.F1))
    {
      OpenCivilizationDebug();
    }
  }

  public void OpenCivilizationDebug()
  {
    civilizationDebugUI.SetActive();
  }
}