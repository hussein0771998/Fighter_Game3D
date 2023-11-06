using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace skills
{
    public class Skill_Wall : MonoBehaviour
    {
        //Wall Spawn Point
        public Vector3 endPoint;
        //Trail Speed
        public float speed = 100;

        public bool cast = false;


        [SerializeField] private VisualEffect visualEffect;
        //Time after which fire VFX is stopped
        [SerializeField] private float fireLifetime = 4.5f;

        private Vector3 startPoint;

        void Start()
        {
            visualEffect = gameObject.GetComponent<VisualEffect>();
            startPoint = gameObject.transform.position;
        }

        void Update()
        {
            if (cast)
            {
                //Move and rotete trail to end point
                transform.position = Vector3.MoveTowards(transform.position, endPoint, Time.deltaTime * speed);
                transform.LookAt(endPoint, Vector3.up);

                //Check if trail is on the ground
                Vector3 rayPoint = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                RaycastHit hit;
                if (Physics.Raycast(rayPoint, transform.TransformDirection(-Vector3.up), out hit))
                {
                    //0.1f need to be added to y position for better vfx rocks spawn
                    transform.position = new Vector3(transform.position.x, hit.point.y + 0.05f, transform.position.z);
                }

                //Check if trail reached end point
                if (Vector3.Distance(new Vector3(transform.position.x, endPoint.y, transform.position.z), endPoint) <= 0.1f)
                {
                    StartCoroutine(Cast());
                    cast = false;
                }
            }
        }

        IEnumerator Cast()
        {
            //Rotate wall toward start point
            transform.rotation = Quaternion.LookRotation(startPoint, Vector3.up);
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, 1);

            //Start and end wall vfx
            ; visualEffect.SendEvent("WallStart");
            yield return new WaitForSeconds(fireLifetime);
            visualEffect.SendEvent("WallStop");
            yield return new WaitForSeconds(fireLifetime);

            //Destroy object after end of the vfx
            Destroy(gameObject);
        }

    }
}