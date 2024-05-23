using System.Collections.Generic;
using XNode;
public class ReactiveSequenceNode : BTCitizenNode
{

  [Input] public List<BTResult> inResults;

  public override BTResult OnEvaluate()
  {
    NodePort inPort = GetPort("inResults");
    if (inPort != null)
    {
      List<NodePort> connections = inPort.GetConnections();
      for (int i = 0; i < connections.Count; i++)
      {
        NodePort currentChild = connections[i];
        switch ((BTResult)currentChild.GetOutputValue())
        {
          case BTResult.FAILURE:
            return BTResult.FAILURE;
          case BTResult.RUNNING:
            return BTResult.RUNNING;
        }
      }
      return BTResult.SUCCESS;
    }
    return BTResult.FAILURE;
  }
}