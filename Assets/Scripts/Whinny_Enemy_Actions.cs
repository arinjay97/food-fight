using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whinny_Enemy_Actions : MonoBehaviour
{
    public Transform player;
    public GameObject body;
    public LineOfSight lunchLadyScript;
    public bool AlertStatus = false;
    public int score = 10;
    [Range(0, 100)] public float AlertVal = 30; 
    private Material[] mats;


    // Start is called before the first frame update
    void Start()
    {
        SetUpMaterials();
        set_skinned_mat(0);
    }

    IEnumerator WaitToAlert(int sec)
    {
        yield return new WaitForSeconds(sec);
        set_skinned_mat(4);
        AlterOthers();
    }

    void AlterOthers()
    {
        transform.GetComponent<Animator>().Play("crying_action");
        set_skinned_mat(1);
        StartCoroutine(Alert(2));
    }

    IEnumerator Alert(int time)
    {
        yield return new WaitForSeconds(time);
        AlertStatus = true;
        AlertSequence();
    }

    void AlertSequence()
    {
        transform.GetComponent<Animator>().Play("Complaint");
        set_skinned_mat(5);
        float dist = Vector3.Distance(player.position, transform.position);
        LookAtAPlayer();
        Debug.Log(dist);
        lunchLadyScript.StudentAlert(AlertVal);
        StartCoroutine(WaitForComplaint(2));
    }

    IEnumerator WaitForComplaint(int time)
    {
        yield return new WaitForSeconds(time);
        ReturnToSad();
    }

    void ReturnToSad()
    {
        set_skinned_mat(4);
        GetComponent<Animator>().Play("idle");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "food")
        {
            collision.gameObject.tag = "Untagged";
            Debug.Log("The Enemy was hit by "+collision.gameObject.name);
            if(!AlertStatus) FindObjectOfType<ScoreCount>().score += score;
            GetComponent<Animator>().Play("hit_action");
            set_skinned_mat(3);
            this.GetComponent<AudioSource>().Play();
            LookAtAPlayer();
            StartCoroutine(WaitToAlert(1));
        }
    }

    void LookAtAPlayer() { body.transform.LookAt(new Vector3(player.position.x, 0, player.position.z)); }

    void SetUpMaterials() { mats = this.GetComponentInChildren<SkinnedMeshRenderer>().materials; }

    void set_skinned_mat(int Mat_Nr)
    {
        if (Mat_Nr < mats.Length && Mat_Nr >= 0)
        {
            Material[] materials = new Material[6];
            materials[0] = mats[Mat_Nr];
            this.GetComponentInChildren<SkinnedMeshRenderer>().materials = materials;
        }
        else
            Debug.LogError("No Material Found of that index");
    }

}
