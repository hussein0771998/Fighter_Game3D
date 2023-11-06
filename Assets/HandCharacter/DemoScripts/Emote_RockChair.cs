using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace skills
{
    public class Emote_RockChair : MonoBehaviour
    {
        //How far down chair will start
        public float startPosition;
        public float speed;
        public bool cast;

        [SerializeField] private VisualEffect visualEffect_Flame;
        [SerializeField] private VisualEffect visualEffect_Stone;
        [SerializeField] private GameObject chairModel;

        void Start()
        {
            //Move chair model to start position
            chairModel.transform.localPosition = new Vector3(0, -startPosition, 0);
        }

        void Update()
        {
            if (cast && chairModel.transform.localPosition.y < 0)
            {
                visualEffect_Flame.Play();
                visualEffect_Stone.Play();
                chairModel.transform.localPosition = new Vector3(0, chairModel.transform.localPosition.y + (speed * Time.deltaTime), 0);
            }

        }

        public IEnumerator Destroy(float endBreak)
        {
            //Wait for character animation
            yield return new WaitForSeconds(endBreak);
            chairModel.SetActive(false);

            //Play destroy vfx and wait for it to end
            visualEffect_Stone.SendEvent("Destroy");
            yield return new WaitForSeconds(4);

            Destroy(gameObject);
        }
    }
}