using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public Camera fpsCam;
    public ParticleSystem Fire, impact;
    public bool AutoFire = false;
    // Update is called once per frame
    public Animator player;
    public float nextTimeToFire = 0f;
    public GameObject CameraAim;
    bool canShoot = false;
    public GameObject shootIcon;
    public AudioSource Enemy;
    public AudioClip Shoot;
    public bool fireB;
    public int firecount = 20;
    public TextMeshProUGUI fireCountText;
    void Update()
    {
        fireCountText.text = firecount.ToString();
        if (AutoFire == false)
        {
            if (Input.GetMouseButtonDown(1) && Time.time >= nextTimeToFire)
            {
                // Right-click to prepare to shoot
                nextTimeToFire = Time.time + 1f / fireRate;
                Debug.Log("Preparing to shoot");
                // Optionally, you can play a different animation here.
                SwitchCam();
               
            }
            else if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire && canShoot == true )
            {
                // Left-click to shoot
                nextTimeToFire = Time.time + 1f / fireRate;
                Debug.Log("Shoot");
                shoot();
                player.SetTrigger("shootSingle");
            }
        }
        else
        {
            if (Input.GetMouseButton(1) && Time.time >= nextTimeToFire)
            {
                // Right-click to prepare to shoot (for automatic fire)
                nextTimeToFire = Time.time + 1f / fireRate;
                Debug.Log("Preparing to shoot");
                // Optionally, you can play a different animation here.
            }
            else if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
            {
                // Left-click to shoot (for automatic fire)
                nextTimeToFire = Time.time + 1f / fireRate;
                Debug.Log("Shoot");
                shoot();
                player.SetTrigger("shootFast");
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            // player.SetTrigger("idle");
            if (PlayerPrefs.GetInt("walk") == 1)
            {
                player.SetTrigger("walk");
            }
            if (PlayerPrefs.GetInt("run") == 1)
            {
                player.SetTrigger("run");
            }

            player.ResetTrigger("shootFast"); 
            player.ResetTrigger("shootSingle");

        }

        if (Input.GetKey(KeyCode.R))
        {
            if (firecount <= 19)
            {
                firecount = 20;
                player.SetTrigger("Reload");
            }
        }
        /* if (Input.GetButtonUp("Fire1"))
         {
             player.SetTrigger("idle");
         }*/
    }
    public void SwitchCam()
    {
        if (CameraAim.activeSelf)
        {
            CameraAim.SetActive(false);
            canShoot = false;
            shootIcon.SetActive(false);
        }
        else
        {
            CameraAim.SetActive(true);
            canShoot = true;
            shootIcon.SetActive(true);
        }
    }

    void shoot()
    {
        firecount--;
        if (firecount > 0)
        {
            Enemy.PlayOneShot(Shoot);
            Fire.Play();
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                Target1 target = hit.transform.GetComponent<Target1>();

                if (target != null)
                {
                    target.TakeDamage(damage);
                }
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
                ParticleSystem impactGO = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO.gameObject, 2f);
            }
        }
    }

}

