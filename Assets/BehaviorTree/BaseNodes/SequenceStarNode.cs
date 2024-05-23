using System.Collections.Generic;
using XNode;

/// <summary>Saves last successful index and retries from the subsequent one on the next tick</summary>
public class SequenceStarNode : BTCitizenNode
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

}