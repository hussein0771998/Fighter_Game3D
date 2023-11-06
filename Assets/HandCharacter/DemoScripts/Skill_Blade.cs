using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace skills
{
    public class Skill_Blade : MonoBehaviour
    {
        [SerializeField] private VisualEffect visualEffect;
        [SerializeField] private Material material;

        [SerializeField] private bool destroyOnImpact;
        public bool cast = false;

        private float fade = 0f;
        public float fadeSpeed = 0.3f;
        //Time to wait before shooting 
        public float animationBreak = 2f;

        //Shooting direction
        public Vector3 direction;
        public float force = 100f;
        //Lifetime after shoot
        public float lifetime = 1f;

        private Rigidbody bladeRigidbody;


        void Start()
        {
            bladeRigidbody = gameObject.GetComponent<Rigidbody>();
            material = gameObject.GetComponent<Renderer>().material;
            //Change range from 0-1 to 0-100 for vfx Fade
            visualEffect.SetFloat("Fade", fade * 100);
            material.SetFloat("_Fade", fade);
        }

        void Update()
        {
            if (cast)
            {
                if (fade < 1)
                {
                    visualEffect.SendEvent("FadeIn");
                    fade += fadeSpeed * Time.deltaTime;
                    //Mutiply by 120 so vfx move faster then material
                    visualEffect.SetFloat("Fade", fade * 120);
                    material.SetFloat("_Fade", fade);

                    if (fade > 0.2f)
                        visualEffect.SendEvent("SmokeIn");
                }
                else
                {
                    //Cast after fade in
                    visualEffect.SendEvent("FadeOut");
                    StartCoroutine(Cast());
                    cast = false;
                }

            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Play destroy vfx after collision
            if (destroyOnImpact)
            {
                destroyOnImpact = false;
                Destroy(transform.Find("Trails").gameObject);

                visualEffect.SendEvent("SmokeOut");

                bladeRigidbody.velocity = Vector3.zero;
                material.SetFloat("_Fade", 0);
                visualEffect.SendEvent("Destroy");
            }

        }


        IEnumerator Cast()
        {
            yield return new WaitForSeconds(animationBreak);

            bladeRigidbody.AddForce(direction * force, ForceMode.Impulse);

            yield return new WaitForSeconds(lifetime);

            //Play destroy vfx after lifetime
            visualEffect.SendEvent("SmokeOut");

            bladeRigidbody.velocity = Vector3.zero;
            material.SetFloat("_Fade", 0);

            if (destroyOnImpact)
                visualEffect.SendEvent("Destroy");

            yield return new WaitForSeconds(1);

            Destroy(gameObject);
        }
    }
}
