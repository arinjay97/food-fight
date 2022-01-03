using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Agro_Enemy_Actions : MonoBehaviour
{

    public Transform hand;
    public Transform body;
    public GameObject banana;
    public Transform player;
    public float shootpower = 1.5f;
    public HealthBar hb;
    public int Health, HMax = 3;
    private GameObject throwable = null;
    public bool AlertStatus = false;
    private bool IsAim = false;
    private Material[] mats;
    public float randomRange1=1f, randomRange2 = 3f;
    Material idle;
    Material hit;
    Material eat;

    Material[] MaterialMatrix;


    // Start is called before the first frame update
    void Start()
    {
        SetUpMaterials();
        set_skinned_mat(1);
        hb.GetComponentInChildren<Slider>().maxValue = HMax;
    }

    IEnumerator WaitingToGrab(int sec)
    {
        yield return new WaitForSeconds(sec);
        set_skinned_mat(0);
        GrabAndAim();
    }

    void GrabAndAim()
    {
        IsAim = true;
        transform.GetComponent<Animator>().Play("grabAndAim");
        if (throwable == null)
        {
            throwable = Instantiate(banana,hand);
            throwable.GetComponent<Rigidbody>().isKinematic = true;
        }
        StartCoroutine(WaitWhileAim());
    }

    IEnumerator WaitWhileAim()
    {
        yield return new WaitForSeconds(2);
        Shoot();
    }

    void Shoot()
    {
        IsAim = false;
        transform.GetComponent<Animator>().Play("Throw");
        throwable.GetComponent<Rigidbody>().isKinematic = false;
        throwable.tag = "projectile";
        float dist = Vector3.Distance(player.position, transform.position);
        LookAtAPlayer();
        throwable.transform.parent = null;
        Debug.Log(dist);
        throwable.GetComponent<Rigidbody>().AddForce((transform.forward)* dist* shootpower + (Vector3.right* Random.Range(randomRange1, randomRange2)) + (Vector3.up* Random.Range(randomRange1, randomRange2)), ForceMode.Impulse);
        throwable = null;
        StartCoroutine(WaitingToGrab(3));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "food")
        {
            collision.gameObject.tag = "Untagged";
            Debug.Log("The Enemy was hit by "+collision.gameObject.name);
            GetComponent<Animator>().Play("hit");
            set_skinned_mat(2);
            this.GetComponent<AudioSource>().Play();
            if (!AlertStatus)
            {
                hb.gameObject.SetActive(true);
                AlertStatus = true;
                LookAtAPlayer();
                StartCoroutine(WaitingToGrab(1));
            }
            else
            {
                Health--;
                hb.GetComponentInChildren<Slider>().value = Health;
                if (Health <= 0)
                {
                    Defeted();
                }
            }

        }
    }

    void Defeted() {
        set_skinned_mat(2);
        hb.gameObject.SetActive(false);
        StopAllCoroutines();
        GetComponent<Animator>().Play("defeated");
        this.enabled = false;
    }

    void LookAtAPlayer() { transform.LookAt(new Vector3(player.position.x, 0, player.position.z)); }

    void SetUpMaterials() { mats = this.GetComponentInChildren<SkinnedMeshRenderer>().materials; }

    void set_skinned_mat(int Mat_Nr)
    {
        if (Mat_Nr < mats.Length && Mat_Nr >= 0)
        {
            Material[] materials = new Material[3];
            materials[0] = mats[Mat_Nr];
            this.GetComponentInChildren<SkinnedMeshRenderer>().materials = materials;
        }
        else
            Debug.LogError("No Material Found of that index");
    }

}
