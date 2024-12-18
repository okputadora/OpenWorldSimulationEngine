using UnityEngine;
public class DebugUI : MonoBehaviour
{
  [SerializeField] CivilizationDebugUI civilizationDebugUI;
  private bool isOpen = false;
  public void Update()
  {
    if (Input.GetKeyDown(KeyCode.F1))
    {
      if (isOpen)
      {
        CloseCivilizationDebug();
      }
      else
      {
        OpenCivilizationDebug();
      }
    }
  }

  public void OpenCivilizationDebug()
  {
    civilizationDebugUI.SetActive();
    isOpen = true;
  }

  public void CloseCivilizationDebug()
  {
    foreach(Transform child in gameObject.transform)
    {
      child.gameObject.SetActive(false);
    }
    isOpen = false;
  }
}