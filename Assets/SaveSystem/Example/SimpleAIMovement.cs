using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAIMovement : MonoBehaviour
{
  // Start is called before the first frame update
  public float moveSpeed;
  public VirtualGameObject vgo;
  void Start()
  {
    // vgo = GetComponent<DataSyncer>()
  }

  void Update()
  {
    //Assuming this is 3rd-person movement and the default Input Manager configuration is used.
    Vector3 moveDirection = new Vector3(0f, 0f, -1 * moveSpeed);


    //Update the GameObject's position with the detected move direction and speed.
    transform.position += moveDirection * Time.deltaTime;
    CheckZone();
  }

  private void CheckZone()
  {
    if (!ZoneSystem.instance.IsInLocalZone(transform.position))
    {
      Destroy(gameObject);
    }
    // ZoneSystem.instance.TryReparentZone();
  }


}
