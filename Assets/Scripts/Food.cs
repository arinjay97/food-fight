using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public ParticleSystem FoodParticle;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision) { 
        if (collision.relativeVelocity.magnitude > 2 && transform.parent == null) 
        {
            Instantiate(FoodParticle, collision.transform);
            DestroySelf(2f);
        }
    }

    private void DestroySelf(float sec)
    {
        Object.Destroy(gameObject, sec);
    }
}
