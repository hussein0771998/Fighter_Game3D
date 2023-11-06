using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using skills;

public class Target1 : MonoBehaviour
{
    public float health = 100f;
    public Animator Enemy;
    public Transform Player;
    public float moveSpeed = 1.0f;
    public float Distance;
    public Image healthImage;
    public SkillCast Cast;
    public AudioSource audio1;
    public AudioClip die,pain;
   
   /* private void Start()
    {
        StartCoroutine(playDieSoun());
    }

     IEnumerator playDieSoun()
     {
        bool time = false;
        while (!time)
        {
            yield return new WaitForSeconds(10f);
            if (health > 0f)
            {
                audio1.PlayOneShot(die);
                Debug.Log("yes");
            }

        }

    }*/

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthImage.fillAmount = health/ 100;
        Debug.Log("health is " + health);
       
        if (health <= 0f)
        {
           
            Enemy.SetBool("Emote1", true);
            Enemy.SetBool("Walking", false);
            Enemy.SetBool("AttackAuto", false);
            audio1.PlayOneShot(pain);
            Destroy(gameObject, 2f);
        }
    }
    private void Update()
    {
       
        Distance = Vector3.Distance(transform.position, Player.position);
        //Debug.Log("Distance " + Distance);

        if (Distance < 1.8f)
        {
            /* Debug.Log("Kill him " + Distance);*/
            Enemy.SetBool("AttackAuto", true);
            Enemy.SetBool("Walking", false);
            Cast.autoAttackCast = true;
          
        }
        
        else if (health>0)
        {
            Enemy.SetBool("Walking", true);
            Enemy.SetFloat("Walking_Speed", 1f);
            Enemy.SetBool("AttackAuto", false);
            Vector3 direction = Player.position - transform.position;
            direction.Normalize();  // Normalize to get a unit vector

            // Move the enemy in the direction of the player
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
       
        transform.LookAt(Player);
       
    }
}
