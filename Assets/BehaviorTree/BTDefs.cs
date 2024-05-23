using System;
using System.Collections.Generic;
public enum BTResult
{
  SUCCESS,
  FAILURE,
  RUNNING
}

[Serializable]
public class BTContext
{
#if UNITY_EDITOR
  public List<string> behaviourHistory = new List<string>();
#endif //UNITY EDITOR
}

[Serializable]
public class BTCitizenContext : BTContext
{
  public VirtualCitizen citizen;
}