using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigidbody;
    float t;
    float r = 0f;
    float r1 = 0f;
    float speed = 300f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
          {
            t = transform.localPosition.z + 0.03f;
           
          }
        if(Input.GetKey(KeyCode.A))
          {
            
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
          }
    transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,t);
    transform.rotation = Quaternion.Euler(transform.rotation.x,r,transform.rotation.z);
    }
}
