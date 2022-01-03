using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{

    public HealthBar PlayerHealth;
    public int Health = 10, HealthMax = 10;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth.GetComponent<Slider>().maxValue = HealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "projectile")
        {
            collision.gameObject.tag = "Untagged";
            Health--;
            PlayerHealth.GetComponent<Slider>().value = Health;
            if (Health <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}
