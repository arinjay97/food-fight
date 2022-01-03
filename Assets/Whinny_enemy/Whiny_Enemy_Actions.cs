using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiny_Enemy_Actions : MonoBehaviour
{
    Material cry;
    Material sad;
    Material eat;
    Material complaint;
    Material hit;
    Material happy;
    Material[] Firstmats;

   public Transform face;
    // Start is called before the first frame update
    void Start()
    {
        // The below function will collect the material array of the default materials set in the editor.
        CollectDefaultMats();
    }

    // Update is called once per frame
    void Update()
    {
        //Below is an example code which needs to be changed according to the Behaviour of the NPC
       if(Input.GetKey(KeyCode.E))
        {
            //The below function will set the expressions to default.
            ChangeBackToDefault();
            transform.GetComponent<Animator>().Play("eating_action");
            // the below function will change the expression.
            set_skinned_mat("whinyEnemy", 2);
        }

       if(Input.GetKey(KeyCode.S))
        {
            ChangeBackToDefault();
            transform.GetComponent<Animator>().Play("Complaint");
            set_skinned_mat("whinyEnemy", 5);
            
        }

       
    }


    void set_skinned_mat(string obj_name, int Mat_Nr)
    {
        GameObject obj = GameObject.Find(obj_name);

        SkinnedMeshRenderer renderer = obj.GetComponentInChildren<SkinnedMeshRenderer>();

        Material[] mats = renderer.materials;
        Material[] updatedmats = renderer.materials;
        renderer.materials = updatedmats;

        happy = mats[0];
        cry = mats[1];
        eat = mats[2];
        hit = mats[3];
        sad = mats[4];
        complaint = mats[5];

        
        Material[] eatmats = new Material[] { eat };
        Material[] crymats = new Material[] { cry };
        Material[] hitmats = new Material[] { hit };
        Material[] sadmats = new Material[] { sad };
        Material[] happymats = new Material[] { happy };
        Material[] complainmats = new Material[] { complaint };

        if(Mat_Nr == 0)
        {
            renderer.materials = happymats;
        }

        if (Mat_Nr == 1)
        {
            renderer.materials = crymats;
        }


        if (Mat_Nr == 2)
        {
            renderer.materials = eatmats;
        }

        if (Mat_Nr == 3)
        {
            renderer.materials = hitmats;
        }

        if (Mat_Nr == 4)
        {
            renderer.materials = sadmats;
        }

        if (Mat_Nr == 5)
        {
            renderer.materials = complainmats;
        }


        //StartCoroutine(ChangeBackToDefault());

        
    }

    void CollectDefaultMats()
    {
        GameObject obj = GameObject.Find("whinyEnemy");

        SkinnedMeshRenderer renderer = obj.GetComponentInChildren<SkinnedMeshRenderer>();

        Firstmats = renderer.materials;
       
        renderer.materials = Firstmats;

    }

   void ChangeBackToDefault()

    {
        //yield return new WaitForSeconds(1);

        GameObject obj = GameObject.Find("whinyEnemy");

        SkinnedMeshRenderer renderer = obj.GetComponentInChildren<SkinnedMeshRenderer>();

        renderer.materials = Firstmats;

    }

}
