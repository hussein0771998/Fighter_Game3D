using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
    public bool walking;
    public Transform playerTrans;
    /*  public float jumpForce = 10f;*/
    private AudioSource audioSource;
    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip shootSound;
    public GameObject start;

    void Start()
    {
        StartCoroutine(OnStart());
        audioSource = GetComponent<AudioSource>();
    }
    IEnumerator OnStart()
    {
        yield return new WaitForSeconds(3f);
        start.SetActive(false);
       // transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerRigid.velocity = transform.forward * w_speed * Time.deltaTime;
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(walkSound);
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerRigid.velocity = -transform.forward * wb_speed * Time.deltaTime;
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(walkSound);
        }
    }
    
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.W))
            {
                playerAnim.SetTrigger("walk");
                playerAnim.ResetTrigger("idle");
                walking = true;
            //steps1.SetActive(true);
            PlayerPrefs.SetInt("walk", 1);
        }
            if (Input.GetKeyUp(KeyCode.W))
            {
                playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle");
            PlayerPrefs.SetInt("walk", 0);
            walking = false;
            //steps1.SetActive(false);
            audioSource.Stop();
        }
            if (Input.GetKeyDown(KeyCode.S))
            {
                playerAnim.SetTrigger("walkback");
                playerAnim.ResetTrigger("idle");
            //steps1.SetActive(true);
            PlayerPrefs.SetInt("walk", 1);
        }
            if (Input.GetKeyUp(KeyCode.S))
            {
                playerAnim.ResetTrigger("walkback");
            playerAnim.SetTrigger("idle");
            PlayerPrefs.SetInt("walk", 0);
          
            //steps1.SetActive(false);
            audioSource.Stop();
            }
            if (Input.GetKey(KeyCode.A))
            {
                playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0);
            }
            if (walking == true)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                PlayerPrefs.SetInt("run", 1);
                //steps1.SetActive(false);
                //steps2.SetActive(true);
                w_speed = w_speed + rn_speed;
                    playerAnim.SetTrigger("run");
                    playerAnim.ResetTrigger("walk");
                    if (!audioSource.isPlaying)
                        audioSource.PlayOneShot(runSound);
                }
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                PlayerPrefs.SetInt("run", 0);
                //steps1.SetActive(true);
                //steps2.SetActive(false);
                w_speed = olw_speed;
                    playerAnim.ResetTrigger("run");
                    playerAnim.SetTrigger("walk");
                }
            }

        }

}

