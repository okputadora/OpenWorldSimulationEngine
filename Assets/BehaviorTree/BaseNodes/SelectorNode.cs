﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
/// <summary>
///  
/// </summary>
public class SelectorNode : BTCitizenNode
{

  [Input] public List<BTResult> inResults;


  public override BTResult OnEvaluate()
  {
    // Debug.Log("evaluating selector node");
    NodePort inPort = GetPort("inResults");
    // Debug.Log("selector: " + nodeDescription);
    if (inPort != null)
    {
      List<NodePort> connections = inPort.GetConnections();
      foreach (NodePort port in connections)
      {
        BTResult result = (BTResult)port.GetOutputValue();
        if (result == BTResult.SUCCESS)
        {
          return BTResult.SUCCESS;
        }
        if (result == BTResult.RUNNING)
        {
          return BTResult.RUNNING;
        }

      }
      return BTResult.FAILURE;
    }
    else return BTResult.FAILURE;
  }

}