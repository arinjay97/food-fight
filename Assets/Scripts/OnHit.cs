using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "food" && collision.relativeVelocity.magnitude > 1)
          if(!GetComponent<AudioSource>().isPlaying) 
                GetComponent<AudioSource>().Play();
    }
}
