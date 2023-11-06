using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace skills
{
    public class Skill_FireCircle : MonoBehaviour
    {
        public float duration = 4f;
        [SerializeField] private VisualEffect visualEffect;
        public bool cast = false;

        void Start()
        {
            visualEffect = gameObject.GetComponent<VisualEffect>();
        }

        void Update()
        {
            if (cast)
            {
                cast = false;
                StartCoroutine(Cast());
            }

        }

        IEnumerator Cast()
        {
            yield return new WaitForSeconds(duration);
            visualEffect.Stop();
            yield return new WaitForSeconds(duration * 2);

            //Destroy object after end of the vfx
            Destroy(gameObject);
        }
    }
}