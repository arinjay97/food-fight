using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressiveEnemy : MonoBehaviour 
{
    public int Health;
    public Transform hand;
    public GameObject foodItem;
    public Transform player;
    public float shootpower = 10;

    private GameObject throwable = null;
    private Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        StartCoroutine(Grab());
    }

    IEnumerator Grab()
    {
        Debug.Log("Grab");
        yield return new WaitForSeconds(5);
        GrabAndAim();
    }

    void GrabAndAim()
    {
        if (throwable == null)
        {
            transform.LookAt(player);
            animator.Play("GrabAndAim");
            throwable = Instantiate(foodItem);
            throwable.GetComponent<Rigidbody>().isKinematic = true;
            throwable.transform.position = hand.position;
            throwable.transform.rotation = hand.rotation;
            throwable.transform.parent = hand;
            StartCoroutine(WaitWhileAim());
        }

    }

    IEnumerator WaitWhileAim()
    {
        yield return new WaitForSeconds(2);
        Shoot();
    }

    void Shoot()
    {
        transform.LookAt(player);
        transform.GetComponent<Animator>().Play("EnemyThrow");
        throwable.GetComponent<Rigidbody>().isKinematic = false;
        throwable.GetComponent<Rigidbody>().velocity = hand.transform.up * shootpower;
        throwable.transform.parent = null;
        throwable.tag = "projectile";
        throwable = null;
        StartCoroutine(Grab());
    }

    // Update is called once per frame
}
