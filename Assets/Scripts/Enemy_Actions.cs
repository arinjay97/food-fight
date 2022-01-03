using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Actions : MonoBehaviour
{

    public Transform hand;
    public GameObject banana;
    public Transform player;
    private GameObject throwable = null;
    public float shootpower = 5;
    bool IsAim = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitingToGrab());
    }

    // Update is called once per frame
    void Update()
    {
     if(IsAim)
        {
            WaitingBeforeGrab();
            StartCoroutine(WaitWhileAim());
        }
    }

    IEnumerator WaitingToGrab()
    {
        yield return new WaitForSeconds(5);
        GrabAndAim();
    }

    void GrabAndAim()
    {
        IsAim = true;
        transform.GetComponent<Animator>().Play("GrabAndAim");
        if (throwable == null) throwable = Instantiate(banana, hand.transform);
    }

    void WaitingBeforeGrab()
    {
        throwable.transform.position = hand.transform.position;
        throwable.transform.rotation = hand.transform.rotation;   
    }

    IEnumerator WaitWhileAim()
    {
        yield return new WaitForSeconds(2);
        Shoot();
    }

    void Shoot()
    {
        transform.LookAt(player);
        IsAim = false;
        transform.GetComponent<Animator>().Play("EnemyThrow");
        throwable.GetComponent<Rigidbody>().velocity = hand.transform.up * shootpower;
        throwable.tag = "projectile";
        StartCoroutine(WaitingToGrab());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "food")
        {
            Debug.Log("The Enemy was hit by "+collision.gameObject.name);
            transform.GetComponent<Animator>().Play("TakeDamage");
        }
    }
}
