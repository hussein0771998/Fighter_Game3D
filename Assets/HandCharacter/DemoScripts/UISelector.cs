using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace skills
{
    public class UISelector : MonoBehaviour
    {
        [SerializeField] private SkillCast skillCast;
        [SerializeField] private GameObject[] buttons;
        [SerializeField] private Transform buttonsCanvas;
        private void Start()
        {
            buttons = new GameObject[buttonsCanvas.childCount];

            //Get all buttons
            for (int i = 0; i < buttonsCanvas.childCount; i++)
            {
                buttons[i] = buttonsCanvas.GetChild(i).gameObject;
            }
        }

        private void Update()
        {
            if (skillCast.selectedSkill == null)
            {
                changeColor("null");
            }
        }

        //Change selected skill button to blue based on button game object name
        void changeColor(string item)
        {
            foreach (GameObject i in buttons)
            {
                if (i.name == item)
                    i.GetComponent<Image>().color = Color.blue;
                else
                    i.GetComponent<Image>().color = Color.white;
            }
        }
        public void fireAura()
        {
            if (skillCast.selectedSkill == null)
            {
                changeColor("FireAura");
                skillCast.selectedSkill = SkillCast.skills.FireAura;
            }
        }
        public void running()
        {
            if (skillCast.selectedSkill == null)
            {
                changeColor("Running");
                skillCast.selectedSkill = SkillCast.skills.Running;
            }
        }
        public void walking()
        {
            if (skillCast.selectedSkill == null)
            {
                changeColor("Walking");
                skillCast.selectedSkill = SkillCast.skills.Walking;
            }
        }
        public void emote2()
        {
            if (skillCast.selectedSkill == null)
            {
                changeColor("Emote2");
                skillCast.selectedSkill = SkillCast.skills.Emote2;
            }
        }
        public void autoAttack2()
        {
            if (skillCast.selectedSkill == null)
            {
                changeColor("AutoAttack2");
                skillCast.selectedSkill = SkillCast.skills.AutoAttack2;
            }
        }
        public void autoAttack()
        {
            if (skillCast.selectedSkill == null)
            {
                changeColor("AutoAttack");
                skillCast.selectedSkill = SkillCast.skills.AutoAttack;
            }
        }
        public void skillWall()
        {
            if (skillCast.selectedSkill == null)
            {
                changeColor("Wall");
                skillCast.selectedSkill = SkillCast.skills.Wall;
            }
        }
        public void skillCircle()
        {
            if (skillCast.selectedSkill == null)
            {
                changeColor("FireCircle");
                skillCast.selectedSkill = SkillCast.skills.FireCircle;
            }
        }
        public void skillBlade()
        {
            if (skillCast.selectedSkill == null)
            {
                changeColor("Blade");
                skillCast.selectedSkill = SkillCast.skills.Blade;
            }
        }

        public void emoteRockChair()
        {
            if (skillCast.selectedSkill == null)
            {
                changeColor("Emote1");
                skillCast.selectedSkill = SkillCast.skills.RockChair;
            }
        }
    }
}