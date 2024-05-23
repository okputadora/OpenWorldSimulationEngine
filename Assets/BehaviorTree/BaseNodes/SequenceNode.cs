using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class SequenceNode : BTCitizenNode
{
  [Input] public List<BTResult> inResults;
  private int currentIndex = 0;
  public override BTResult OnEvaluate()
  {
    NodePort inPort = GetPort("inResults");
    // if (nodeDescription == "ChopTreesSequence")
    // {
    //   Debug.Log("chop trees sequence, next log should be looking for targets");
    // }
    // Debug.Log(nodeDescription);
    // Debug.Log(currentIndex);
    if (inPort != null)
    {
      List<NodePort> connections = inPort.GetConnections();
      BTResult btResult;
      while (currentIndex < connections.Count)
      {
        NodePort currentChild = connections[currentIndex];
        switch ((BTResult)currentChild.GetOutputValue())
        {
          case BTResult.FAILURE:
            currentIndex = 0;
            btResult = BTResult.FAILURE;
            // HaltAllChildren()? wouldnt this happen by default
            return btResult;
          case BTResult.RUNNING:
            return BTResult.RUNNING;
          case BTResult.SUCCESS:
            currentIndex++;
            break;
        }
      }
      currentIndex = 0;
      // Debug.Log(nodeDescription + " SUCCESS");
      return BTResult.SUCCESS;
    }
    return BTResult.FAILURE;
  }

  // protected override void OnEnter() { currentIndex = 0; }
}
