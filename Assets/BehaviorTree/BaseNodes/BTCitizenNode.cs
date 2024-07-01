using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public abstract class BTCitizenNode : Node
{
  public string nodeDescription;
  public bool shouldDebug;
  [HideInInspector] public BTCitizenContext context;

  [Output] public BTResult outResult;
  private bool started = false;
  protected override void Init()
  {
    base.Init();
    started = false;
  }

  public BTResult Evaluate()
  {
    // Debug.Log("Started for citizen (" + nodeDescription + "): " + context.citizen.data.id + ": " + started);
    if (!started)
    {
      OnEnter();
      started = true;
    }
    // Debug.Log("evaluating node: " + nodeDescription);
    BTResult result = OnEvaluate();
    if (result == BTResult.FAILURE || result == BTResult.SUCCESS)
    {
      // Debug.Log("BT RESULT = " + result);
      OnExit();
      started = false;
    }
    else
    {
      // Debug.Log("BT RESULT =" + result);
    }
    if (!((BehaviorTreeGraph)this.graph).ignoreNodeDebug && shouldDebug)
    {
      // Debug.Log(nodeDescription + ", " + result);
    }
    return result;
  }

  protected virtual void OnEnter() { }
  protected virtual void OnExit() { }

  // change to protected
  public abstract BTResult OnEvaluate();
  public override object GetValue(NodePort port)
  {
    if (!Application.isPlaying || port == null || port.Connection == null) { return BTResult.FAILURE; }

    BTCitizenNode parentNode = port.Connection.node as BTCitizenNode;

    if (parentNode != null)
    {
      context = parentNode.context;
      //Debug.Log("From " + parentNode.GetType() + " To " + GetType());
    }

    // if (context.citizen == null)
    // {
    //   return BTResult.FAILURE;
    // }
    return Evaluate();
    //     else
    //     {
    // #if UNITY_EDITOR
    //       context.behaviourHistory.Add(nodeDescription == "" ? GetType().ToString() : nodeDescription);
    // #endif //UNITY_EDITOR
    //       Debug.Log("evaluating node " + nodeDescription);
    //     }
  }
}