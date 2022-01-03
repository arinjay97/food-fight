using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : MonoBehaviour
{
    public bool bag;
    private Transform bags_transform;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "bag") 
        { 
            bag = true;
            bags_transform = collision.transform;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "bag")
        {
            bag = false;
            bags_transform = null;
        }
    }

    public Transform GetBagTransform()
    {
        return bags_transform;
    }

}
