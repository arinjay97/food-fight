using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{ 
    public Slider slider;

    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    private void OnEnable()
    {
       // StartCoroutine(Disable(5));
    }

    public void setHealth(int health)
    {
        slider.value = health;
    }

    IEnumerator Disable(int sec)
    {
        yield return new WaitForSeconds(sec);
        this.gameObject.SetActive(false);
    }
}
