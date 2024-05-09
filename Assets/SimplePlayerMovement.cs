using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LayerMask hoverMask;
    public float sprintSpeed;
    public float moveSpeed;
    [SerializeField] private GameObject currentHoverable;
    [SerializeField] private GameObject carriedObject;
    void Start()
    {

    }

    void Update()
    {
        //Assuming this is 3rd-person movement and the default Input Manager configuration is used.
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        //Use the value of "sprintSpeed" if left-shift is held down, otherwise use the value of "moveSpeed";
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        //Update the GameObject's position with the detected move direction and speed.
        transform.position += moveDirection * speed * Time.deltaTime;
        FindHoverable();
        if (carriedObject != null && Input.GetKeyDown("e"))
        {
            carriedObject.transform.SetParent(null); // we'd really like to set parent to the zone object
            ZoneSystem.instance.ReparentObject(carriedObject);
            carriedObject = null;
        }
        if (currentHoverable != null && Input.GetKeyDown("e"))
        {
            currentHoverable.transform.position = transform.position;
            currentHoverable.transform.SetParent(transform);
            carriedObject = currentHoverable;
            currentHoverable = null;

        }
    }

    private void FindHoverable()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10f, hoverMask); // @TODO change to hoverableLayer
        if (hit.transform)
        {
            hit.transform.TryGetComponent(out IHoverable hoverable);
            if (hoverable != null)
            {
                currentHoverable = ((MonoBehaviour)hoverable).gameObject;
                return;
            }
            if (hit.transform.parent == null) return;
            hit.transform.parent.TryGetComponent<IHoverable>(out IHoverable pHoverable); // We should just standardize the gameobject structure (colliders and scripts should be on same component)
            if (pHoverable != null)
            {
                currentHoverable = ((MonoBehaviour)pHoverable).gameObject;
                return;
            }
        }
        currentHoverable = null;
    }

}
