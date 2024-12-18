using System.Collections.Generic;
using UnityEngine;
using XNode;

/// <summary>Saves last successful index and retries from the subsequent one on the next tick</summary>
public class SequenceStarNode : BTCitizenNode
{
  [Input] public List<BTResult> inResults;
  private int currentIndex = 0;
  public override BTResult OnEvaluate()
  {
    NodePort inPort = GetPort("inResults");
    if (inPort != null)
    {
      List<NodePort> connections = inPort.GetConnections(); // we could do this once at initialization
      Debug.Log("current index: " + currentIndex);
      while (currentIndex < connections.Count)
      {
        NodePort currentChild = connections[currentIndex];
        switch ((BTResult)currentChild.GetOutputValue())
        {
          case BTResult.FAILURE:
            return BTResult.FAILURE;
          case BTResult.RUNNING:
            return BTResult.RUNNING;
          case BTResult.SUCCESS:
            Debug.Log("setting current index to " + (currentIndex + 1));
            currentIndex++;
            break;
        }
      }
      Debug.Log("setting current index to 0");
      currentIndex = 0;
       
      return BTResult.SUCCESS;
    }
    return BTResult.FAILURE;
  }

}