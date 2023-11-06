using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArea : MonoBehaviour
{
    public AudioSource APain;
    public AudioClip die,ok;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            APain.PlayOneShot(die);
            StartCoroutine(OkSound());
        }
    }
   IEnumerator OkSound()
    {
        yield return new WaitForSeconds(3f);
        APain.PlayOneShot(ok);
    }
}
