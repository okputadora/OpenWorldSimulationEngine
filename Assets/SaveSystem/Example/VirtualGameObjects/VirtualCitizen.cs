using UnityEngine;

public class VirtualCitizen : VirtualGameObject, ISimulatable
{

  public CitizenData data;

  public void Simulate(float deltaTime)
  {
    // if (inDanger) {

    // }
    // if (isHungry) {
    //    if(!eating) {
    // 
    // }
    // }
    // if (isTired) {z
    //  if (!asleep) {
    //      StartSleep(Time.time)
    //  } else {
    //    UpdateSleep(deltaTime)
    // }
    //      return;
    //  }
    // }
    // get data.currentTask
    // if (!currentTask) {
    // Simulate idle behavior, 
    // }
    // else {
    //  SimulateOccupation();
    // }
  }
}

public enum CitizenState
{
  sleeping,
  eating,
  idle,
  working
}