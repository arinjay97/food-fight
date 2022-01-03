using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController controller;
    private float playerSpeed = 2.0f;
    private Vector3 moveDirection = Vector3.zero;
    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);

        moveDirection *= playerSpeed;

        controller.Move(moveDirection * Time.deltaTime);

        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical")== 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
}
