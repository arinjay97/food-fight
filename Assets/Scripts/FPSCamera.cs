using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{

    [SerializeField] Transform Camera = null;
    [SerializeField] float mouseSpeed = 3.5f;
    PlayerActions playerActions;
    // Start is called before the first frame update
    void Start()
    {
        playerActions = FindObjectOfType<PlayerActions>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(playerActions.pickup == false && !GameController.paused)
        UpdateFPScamera();
    }

    void UpdateFPScamera()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        float clampedmove = Mathf.Clamp(mouseDelta.y, -90, 90);

        transform.Rotate(Vector3.up * mouseDelta.x * mouseSpeed);
        Camera.Rotate(Vector3.right * -clampedmove * mouseSpeed);
    }
}
