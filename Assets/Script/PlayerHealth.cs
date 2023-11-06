using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public TextMeshProUGUI HealthText;
    public Image healthBar;
    public Animator Player;
    public AudioSource audio1;
    public AudioClip Hurt;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigggerd");
        if (other.tag == "Hit")
        {
            audio1.PlayOneShot(Hurt);
            health -= 1f;
            Player.SetTrigger("hit");
            HealthText.text = ((int)health).ToString();
            healthBar.fillAmount = health/100;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hit")
        {

            Player.ResetTrigger("hit");
            Player.SetTrigger("idle");

        }
    }

}
