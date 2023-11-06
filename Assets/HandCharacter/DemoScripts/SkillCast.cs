using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace skills
{
    public class SkillCast : MonoBehaviour
    {
        //Input Variables
        [SerializeField] private Camera mainCamera;
        //Raycast Plane
        private Plane plane;

        //Skills list
        public enum skills { Wall, FireCircle, Blade, RockChair, RockChairEnd, AutoAttack, AutoAttack2, Emote2, Walking, WalkingEnd, Running, RunningEnd, FireAura, FireAuraEnd };
        public skills? selectedSkill;

        //Skill_Wall 
        [SerializeField] private GameObject wallPrefab;
        [SerializeField] private Transform wallStartPoint;
        //Trail Speed
        [SerializeField] private float wallSpeed;
        [SerializeField] private bool wallCast;
        //Max cast distance
        [SerializeField] private float maxDistance;

        //Wait until wall trail start moving
        private float wallStartBreak;


        //Skill_FireCircle
        [SerializeField] private GameObject circlePrefab;
        [SerializeField] private Transform circleStartPoint;
        [SerializeField] private float circleDuration;
        [SerializeField] private bool circleCast;

        //Skill_Blade
        [SerializeField] private GameObject bladePrefab;
        [SerializeField] private Transform bladeStartPoint;
        [SerializeField] private float bladeForce;
        [SerializeField] private float bladeLifetime;
        [SerializeField] private bool bladeCast;

        //Wait until blade shoot
        [SerializeField] private float bladeStartBrake;

        //Emote_RockChair
        [SerializeField] private GameObject rockChairPrefab;
        [SerializeField] private Transform rockChairStartPoint;
        [SerializeField] private bool rockChairCast;

        //Wait before playing character RockChair start animation
        [SerializeField] private float rockChairStartBreak;

        //Wait before playing character RockChair end animation
        [SerializeField] private float rockChairEndBreak;

        //Auto Attack
        [SerializeField] public bool autoAttackCast;
        //Wait before playing slash effect
        [SerializeField] private float autoAttackStartBreak;
        //List of trails objects, attached to character finger bones
        [SerializeField] private GameObject[] autoAttackTrailsList;

        //Auto Attack2
        [SerializeField] private bool autoAttack2Cast;

        //Emote2
        [SerializeField] private bool emote2Cast;

        //Walking
        [SerializeField] private bool walkingCast;
        [SerializeField] private float walkingSpeed;

        //Running
        [SerializeField] private bool runningCast;
        [SerializeField] private float runningSpeed;

        //Fire Aura
        [SerializeField] private bool fireAuraCast;
        [SerializeField] private VisualEffect fireAuraObject;

        //Animations
        [SerializeField] private Animator animator;

        private Vector3 castEndPoint;

        void Start()
        {
            wallCast = false;
            circleCast = false;
            bladeCast = false;
            rockChairCast = false;
            autoAttackCast = false;
            autoAttack2Cast = false;
            emote2Cast = false;
            walkingCast = false;
            runningCast = false;
            fireAuraCast = false;

            selectedSkill = null;

            animator = gameObject.GetComponent<Animator>();
            plane = new Plane(Vector3.up, 0);

            foreach (GameObject obj in autoAttackTrailsList)
                obj.SetActive(false);
        }

        void Update()
        {
            //Skills activated after mouse click
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                float dist;
                if (plane.Raycast(ray, out dist))
                {
                    castEndPoint = ray.GetPoint(dist);

                    if (selectedSkill == skills.Wall)
                        wallCast = true;
                    else if (selectedSkill == skills.Blade)
                        bladeCast = true;
                    else if (selectedSkill == skills.RockChairEnd)
                    {
                        selectedSkill = null;
                        animator.SetBool("Emote1", false);
                        StartCoroutine(chairObject.GetComponent<Emote_RockChair>().Destroy(rockChairEndBreak));
                        rockChairCast = false;
                    }
                    else if (selectedSkill == skills.FireAuraEnd)
                    {
                        selectedSkill = null;
                        fireAuraObject.SendEvent("Stop");
                        fireAuraCast = false;
                    }
                    else if (selectedSkill == skills.WalkingEnd)
                    {
                        selectedSkill = null;
                        animator.SetBool("Walking", false);
                        walkingCast = false;
                    }
                    else if (selectedSkill == skills.RunningEnd)
                    {
                        selectedSkill = null;
                        animator.SetBool("Running", false);
                        runningCast = false;
                    }
                }
            }

            if (wallCast)
                StartCoroutine(Cast_Wall(castEndPoint));
            else if (selectedSkill == skills.FireCircle || circleCast)
                StartCoroutine(CircleCast());
            else if (bladeCast)
                StartCoroutine(BladeCast(castEndPoint));
            else if (selectedSkill == skills.RockChair || (rockChairCast && selectedSkill == null))
                StartCoroutine(Emote1Cast());
            //if cast was disablen in inspector
            else if (selectedSkill == skills.RockChairEnd && !rockChairCast)
            {
                selectedSkill = null;
                animator.SetBool("Emote1", false);
                StartCoroutine(chairObject.GetComponent<Emote_RockChair>().Destroy(rockChairEndBreak));
            }
            else if (selectedSkill == skills.AutoAttack || autoAttackCast)
                StartCoroutine(AutoAttackCast());
            else if (selectedSkill == skills.AutoAttack2 || autoAttack2Cast)
                StartCoroutine(AutoAttack2Cast());
            else if (selectedSkill == skills.Emote2 || emote2Cast)
                StartCoroutine(Emote2Cast());
            else if (selectedSkill == skills.FireAura || (fireAuraCast && selectedSkill == null))
            {
                fireAuraCast = true;
                //Stop on next mouse click
                selectedSkill = skills.FireAuraEnd;
                fireAuraObject.SendEvent("Play");
            }
            //if cast was disablen in inspector
            else if (selectedSkill == skills.FireAuraEnd && !fireAuraCast)
            {
                selectedSkill = null;
                fireAuraObject.SendEvent("Stop");
            }
            else if (selectedSkill == skills.Walking || (walkingCast && selectedSkill == null))
                StartCoroutine(WalkingCast());
            else if (selectedSkill == skills.Running || (runningCast && selectedSkill == null))
                StartCoroutine(RunningCast());
            //Update walking animation speed at runtime
            else if (selectedSkill == skills.WalkingEnd)
            {
                animator.SetFloat("Walking_Speed", walkingSpeed);
                //if cast was disablen in inspector
                if (!walkingCast)
                {
                    selectedSkill = null;
                    animator.SetBool("Walking", false);
                }
            }
            //Update walking animation speed at runtime
            else if (selectedSkill == skills.RunningEnd)
            {
                animator.SetFloat("Running_Speed", runningSpeed);
                //if cast was disablen in inspector
                if (!runningCast)
                {
                    selectedSkill = null;
                    animator.SetBool("Running", false);
                }
            }


        }

        IEnumerator RunningCast()
        {
            runningCast = true;

            animator.SetFloat("Running_Speed", runningSpeed);

            //Stop on next mouse click
            selectedSkill = skills.RunningEnd;

            animator.SetBool("Running", true);

            yield return new WaitForSeconds(0.1f);
        }

        IEnumerator WalkingCast()
        {
            walkingCast = true;

            animator.SetFloat("Walking_Speed", walkingSpeed);

            //Stop on next mouse click
            selectedSkill = skills.WalkingEnd;

            animator.SetBool("Walking", true);

            yield return new WaitForSeconds(0.1f);
        }

        IEnumerator Emote2Cast()
        {
            emote2Cast = false;
            selectedSkill = null;

            animator.SetBool("Emote2", true);

            yield return new WaitForSeconds(1);

            animator.SetBool("Emote2", false);
        }

        IEnumerator AutoAttack2Cast()
        {
            autoAttack2Cast = false;
            selectedSkill = null;

            animator.SetBool("AttackAuto2", true);

            yield return new WaitForSeconds(0.5f);

            animator.SetBool("AttackAuto2", false);
        }

        IEnumerator AutoAttackCast()
        {
            autoAttackCast = false;
            selectedSkill = null;

            animator.SetBool("AttackAuto", true);

            yield return new WaitForSeconds(autoAttackStartBreak);

            foreach (GameObject obj in autoAttackTrailsList)
                obj.SetActive(true);

            animator.SetBool("AttackAuto", false);

            yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

            foreach (GameObject obj in autoAttackTrailsList)
                obj.SetActive(false);

        }

        private GameObject chairObject;
        IEnumerator Emote1Cast()
        {
            rockChairCast = true;

            //Play emote ending on next mouse click
            selectedSkill = skills.RockChairEnd;

            //Instantiate chair object
            chairObject = Instantiate(rockChairPrefab);
            chairObject.transform.position = rockChairStartPoint.position;

            Emote_RockChair castProperties = chairObject.GetComponent<Emote_RockChair>();

            castProperties.cast = true;

            //Wait until start character animation
            yield return new WaitForSeconds(rockChairStartBreak);

            animator.SetBool("Emote1", true);
        }

        IEnumerator BladeCast(Vector3 endPoint)
        {
            bladeCast = false;
            selectedSkill = null;

            Vector3 direction = endPoint / endPoint.magnitude;
            direction.y = 0;

            //Animate charactar
            animator.SetBool("Attack3", true);

            //Wait until start moving
            yield return new WaitForSeconds(0.3f);

            animator.SetBool("Attack3", false);

            //Instantiate blade object
            GameObject bladeObject = Instantiate(bladePrefab);
            bladeObject.transform.position = bladeStartPoint.position;
            bladeObject.transform.LookAt(endPoint);

            Skill_Blade castProperties = bladeObject.GetComponent<Skill_Blade>();
            castProperties.animationBreak = bladeStartBrake;
            castProperties.direction = direction;
            castProperties.force = bladeForce;
            castProperties.lifetime = bladeLifetime;

            //Cast
            castProperties.cast = true;
        }

        IEnumerator CircleCast()
        {
            circleCast = false;
            selectedSkill = null;

            //Instantiate circle vfx object
            GameObject circleObject = Instantiate(circlePrefab);
            circleObject.transform.position = circleStartPoint.position;

            Skill_FireCircle castProperties = circleObject.GetComponent<Skill_FireCircle>();
            castProperties.duration = circleDuration;

            //Animate charactar
            animator.SetBool("Attack2", true);

            //Start circle vfx
            castProperties.cast = true;

            //Wait until start moving
            yield return new WaitForSeconds(1);

            animator.SetBool("Attack2", false);
        }

        IEnumerator Cast_Wall(Vector3 endPoint)
        {
            wallCast = false;
            selectedSkill = null;

            //Calculate animation speed based of distance
            float dist = Vector3.Distance(wallStartPoint.position, endPoint);

            if (dist <= maxDistance)
            {
                float animationSpeed = wallSpeed / Mathf.Floor(dist);
                animator.SetFloat("Attack1_Speed", animationSpeed);

                wallStartBreak = dist / 20;

                //Instantiate wall vfx object
                GameObject wallObject = Instantiate(wallPrefab);
                wallObject.transform.position = wallStartPoint.position;

                Skill_Wall castProperties = wallObject.GetComponent<Skill_Wall>();
                castProperties.endPoint = endPoint;
                castProperties.speed = wallSpeed + 3;

                //Animate charactar
                animator.SetBool("Attack1", true);

                //Wait until start moving
                yield return new WaitForSeconds(wallStartBreak);

                //Start wall vfx
                castProperties.cast = true;

                animator.SetBool("Attack1", false);
            }
        }
    }
}