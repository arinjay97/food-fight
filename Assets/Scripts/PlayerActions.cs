using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public Transform camera;
    public GameObject PlayerBody;
    public GameObject banana;
    public GameObject donut;
    public GameObject burger;
    public AudioSource PlayerAudioSource;
    public GameObject PauseScreen;
    private GameObject throwable = null;
    public Transform grab;
    public Supply supply;
    public bool grabbed = false;
    public bool pickup = false;
    public float shootpower;
    public float donutshotpower;
    public float bananashotpower;
    public float burgershotpower;
    public bool inAim = false;
    bool pickbanana = true;
    bool pickdonut = false;
    bool pickburger = false;
    public bool doging = false;
    public bool grabbing = false;
    private bool left = false, right = false;
    [SerializeField] bool cursor = true;
   

   

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

       

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&& GameController.paused)
        {
            GameController.paused = false;
            Time.timeScale = 1f;
            PauseScreen.SetActive(false);
            cursor = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !GameController.paused)
        {
            GameController.paused = true;
            Time.timeScale = 0f;
            PauseScreen.SetActive(true);
            cursor = false;
        }
        if (!GameController.paused)
        {
            PickingObjects();
            //Grabbed();
            Aim();
            Shoot();
            Dodge();
        }

        // This press c to lock or unlock the cursor so that we can stop the game in the editor.
        //In the Final game when we build it we will change this to lock. 
        if(Input.GetKeyDown(KeyCode.C))
        {
            if (cursor)
                cursor = false;
            else
                cursor = true;

        }

        if (cursor)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

        void Grabbed()
    {
        if (grabbed == true && throwable !=null)
        {
            throwable.transform.position = grab.transform.position;
            throwable.transform.rotation = grab.transform.rotation;
        }
    }

        /*
        void BananaGrab()
        {

            banana.transform.position = grab.transform.position;
            banana.transform.rotation = grab.transform.rotation;

        }
        private void DonutGrab()
        {
            donut.transform.position = grab.transform.position;
            donut.transform.rotation = grab.transform.rotation;
        }

        private void BurgerGrab()
        {
            burger.transform.position = grab.transform.position;
            burger.transform.rotation = grab.transform.rotation;


        }

        */
        // if the player clicks on the X key then player plays movetotable animation and camera is rotated to a fixed spot so that the animation works corrrectly.
        void PickingObjects()
        {
        if (supply.bag) // if bag in reach only then pick up items from bag
        {           
            if (Input.GetKey(KeyCode.X) && throwable == null)
            {
                grabbing = true;
                pickup = true;
                animator.Play("movetotable");
                //camera.rotation = Quaternion.Euler(supply.GetBagTransform().localPosition);
                camera.transform.LookAt(supply.GetBagTransform().position);
                //camera.rotation = Quaternion.Euler(0, 0, 0);
                //I wanted there to be a slight delay while player picks the object so I added a corotuine.
                
            }
        }
    }

    public void ObjectSpawn()
    {
        if (pickbanana)
        {
            //When the game start pick banana is true so the player picks the banana first.
            throwable = Instantiate(banana, grab);
          
            shootpower = bananashotpower;
        }
        else if (pickburger)
        {
            throwable = Instantiate(burger, grab);
            shootpower = burgershotpower;
        }
        else
        {

            throwable = Instantiate(donut, grab);
            shootpower = donutshotpower;
        }
        StartCoroutine(Waitingandshooting());
    }
    bool Gabbing()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("movetotable") || animator.GetCurrentAnimatorStateInfo(0).IsName("pickfood"))
            return true;
         else
            return false;
    }

    void Aim()
    {
        if (Input.GetMouseButtonDown(1) && inAim == false && throwable != null && !Gabbing())
        {
            pickup = false;
            inAim = true;
        }
        if(Input.GetMouseButtonUp(1) && inAim == true && throwable != null)
        {
            inAim = false;
            grabbed = true;
        }
    }


    void Shoot()
    {
        if(inAim)
        {
            if (Input.GetMouseButton(0))
            {
               
               
                animator.Play("Throwstart");
            }
        }

        
    }

    public void ObjectRelease()
    {
        grabbed = false;
        throwable.tag = "food";
        throwable.transform.parent = null;
        throwable.GetComponentInChildren<Rigidbody>().isKinematic = false;
        if (pickbanana)
        {
            throwable.GetComponentInChildren<Rigidbody>().velocity = grab.transform.up * (shootpower);
            pickburger = true;
            pickbanana = false;

        }
        else if (pickburger)
        {
            throwable.GetComponentInChildren<Rigidbody>().velocity = grab.transform.up * (shootpower);
            pickdonut = true;
            pickburger = false;

        }
        else if (pickdonut)
        {
            throwable.GetComponentInChildren<Rigidbody>().velocity = grab.transform.up * (shootpower);
            pickbanana = true;
            pickdonut = false;

        }

        throwable = null;
        inAim = false;
        PlayerAudioSource.Play();
       

    }

    private void Dodge()
    {
        
        if (!grabbing && !inAim)
        {

            if (Input.GetKeyDown(KeyCode.A) && !doging)
            {


                PlayerBody.transform.position += Vector3.left * 1.5f;
                left = true;
                doging = true;
                Debug.Log("Dodging left");
              
                
            }
            



            if (Input.GetKeyDown(KeyCode.D) && !doging )
            {
                right = true;
                doging = true;
                PlayerBody.transform.position += Vector3.right * 1.5f;
                Debug.Log("Dodging right");
               
                
            }

            if (Input.GetKeyUp(KeyCode.A) && doging && left)
            {
                
                left = false;
                PlayerBody.transform.position -= Vector3.left * 1.5f;
                doging = false;
              


            }
            else if (Input.GetKeyUp(KeyCode.D) && doging && right)
            {
                
                right = false;
                doging = false;
                PlayerBody.transform.position -= Vector3.right * 1.5f;
               
            }

        }

    }

    IEnumerator ReturnFromDodge(float time)
    {
        
        if (doging && left)
        {
            yield return new WaitForSeconds(time);
            left = false;
            PlayerBody.transform.position -= Vector3.left * 1.5f;
            doging = false;
            PlayerBody.GetComponent<BoxCollider>().enabled = true;


        }
        else if  (doging && right)
        {
            yield return new WaitForSeconds(time);
            right = false;
            doging = false;
            PlayerBody.transform.position -= Vector3.right * 1.5f;
            PlayerBody.GetComponent<BoxCollider>().enabled = true;
        }
    }


    IEnumerator Waitingandshooting()
    {
        yield return new WaitForSeconds(1);
        grabbed = true;
        grabbing = false;
    }


}
