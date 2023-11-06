using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace skills
{
    [CustomEditor(typeof(SkillCast))]
    [CanEditMultipleObjects]
    public class Editor_Skill : Editor
    {
        private string[] tabs = { "General", "Skill_Wall", "Skill_Circle", "Skill_Blade", "Emote1_RockChair", "AutoAttack", "AutoAttack2", "Emote2", "Walking", "Running", "FireAura" };
        private int selectedTab = 0;

        SerializedProperty mainCamera;
        SerializedProperty animator;

        SerializedProperty wallPrefab;
        SerializedProperty wallStartPoint;
        SerializedProperty wallSpeed;
        SerializedProperty wallCast;
        SerializedProperty maxDistance;

        SerializedProperty circlePrefab;
        SerializedProperty circleStartPoint;
        SerializedProperty circleDuration;
        SerializedProperty circleCast;

        SerializedProperty bladePrefab;
        SerializedProperty bladeStartPoint;
        SerializedProperty bladeForce;
        SerializedProperty bladeLifetime;
        SerializedProperty bladeStartBrake;
        SerializedProperty bladeCast;

        SerializedProperty rockChairPrefab;
        SerializedProperty rockChairStartPoint;
        SerializedProperty rockChairStartBreak;
        SerializedProperty rockChairEndBreak;
        SerializedProperty rockChairCast;

        SerializedProperty autoAttackStartBreak;
        SerializedProperty autoAttackTrailsList;
        SerializedProperty autoAttackCast;

        SerializedProperty autoAttack2Cast;

        SerializedProperty emote2Cast;

        SerializedProperty walkingSpeed;
        SerializedProperty walkingCast;

        SerializedProperty runningSpeed;
        SerializedProperty runningCast;

        SerializedProperty fireAuraObject;
        SerializedProperty fireAuraCast;

        private void OnEnable()
        {
            mainCamera = serializedObject.FindProperty("mainCamera");
            animator = serializedObject.FindProperty("animator");

            wallPrefab = serializedObject.FindProperty("wallPrefab");
            wallStartPoint = serializedObject.FindProperty("wallStartPoint");
            wallSpeed = serializedObject.FindProperty("wallSpeed");
            wallCast = serializedObject.FindProperty("wallCast");
            maxDistance = serializedObject.FindProperty("maxDistance");

            circlePrefab = serializedObject.FindProperty("circlePrefab");
            circleStartPoint = serializedObject.FindProperty("circleStartPoint");
            circleDuration = serializedObject.FindProperty("circleDuration");
            circleCast = serializedObject.FindProperty("circleCast");

            bladePrefab = serializedObject.FindProperty("bladePrefab");
            bladeStartPoint = serializedObject.FindProperty("bladeStartPoint");
            bladeForce = serializedObject.FindProperty("bladeForce");
            bladeLifetime = serializedObject.FindProperty("bladeLifetime");
            bladeCast = serializedObject.FindProperty("bladeCast");
            bladeStartBrake = serializedObject.FindProperty("bladeStartBrake");

            rockChairPrefab = serializedObject.FindProperty("rockChairPrefab");
            rockChairStartPoint = serializedObject.FindProperty("rockChairStartPoint");
            rockChairStartBreak = serializedObject.FindProperty("rockChairStartBreak");
            rockChairEndBreak = serializedObject.FindProperty("rockChairEndBreak");
            rockChairCast = serializedObject.FindProperty("rockChairCast");

            autoAttackStartBreak = serializedObject.FindProperty("autoAttackStartBreak");
            autoAttackTrailsList = serializedObject.FindProperty("autoAttackTrailsList");
            autoAttackCast = serializedObject.FindProperty("autoAttackCast");

            autoAttack2Cast = serializedObject.FindProperty("autoAttack2Cast");

            emote2Cast = serializedObject.FindProperty("emote2Cast");

            walkingSpeed = serializedObject.FindProperty("walkingSpeed");
            walkingCast = serializedObject.FindProperty("walkingCast");

            runningSpeed = serializedObject.FindProperty("runningSpeed");
            runningCast = serializedObject.FindProperty("runningCast");

            fireAuraObject = serializedObject.FindProperty("fireAuraObject");
            fireAuraCast = serializedObject.FindProperty("fireAuraCast");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical();
            selectedTab = GUILayout.SelectionGrid(selectedTab, tabs, 3);
            EditorGUILayout.EndVertical();

            if (selectedTab >= 0)
            {
                switch (tabs[selectedTab])
                {
                    case "General":
                        GeneralTab();
                        break;
                    case "Skill_Wall":
                        WallTab();
                        break;
                    case "Skill_Circle":
                        CircleTab();
                        break;
                    case "Skill_Blade":
                        BladeTab();
                        break;
                    case "Emote1_RockChair":
                        RockChairTab();
                        break;
                    case "AutoAttack":
                        AutoAttackTab();
                        break;
                    case "AutoAttack2":
                        AutoAttack2Tab();
                        break;
                    case "Emote2":
                        Emote2Tab();
                        break;
                    case "Walking":
                        WalkingTab();
                        break;
                    case "Running":
                        RunningTab();
                        break;
                    case "FireAura":
                        FireAura();
                        break;
                    default:
                        break;
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
        private void FireAura()
        {
            EditorGUILayout.PropertyField(fireAuraObject);
            EditorGUILayout.PropertyField(fireAuraCast);
        }

        private void RunningTab()
        {
            EditorGUILayout.PropertyField(runningSpeed);
            EditorGUILayout.PropertyField(runningCast);
        }

        private void WalkingTab()
        {
            EditorGUILayout.PropertyField(walkingSpeed);
            EditorGUILayout.PropertyField(walkingCast);
        }

        private void Emote2Tab()
        {
            EditorGUILayout.PropertyField(emote2Cast);
        }

        private void AutoAttack2Tab()
        {
            EditorGUILayout.PropertyField(autoAttack2Cast);
        }

        private void AutoAttackTab()
        {
            EditorGUILayout.PropertyField(autoAttackStartBreak);
            EditorGUILayout.PropertyField(autoAttackTrailsList);
            EditorGUILayout.PropertyField(autoAttackCast);
        }

        private void RockChairTab()
        {
            EditorGUILayout.PropertyField(rockChairPrefab);
            EditorGUILayout.PropertyField(rockChairStartPoint);
            EditorGUILayout.PropertyField(rockChairStartBreak);
            EditorGUILayout.PropertyField(rockChairEndBreak);
            EditorGUILayout.PropertyField(rockChairCast);
        }

        private void BladeTab()
        {
            EditorGUILayout.PropertyField(bladePrefab);
            EditorGUILayout.PropertyField(bladeStartPoint);
            EditorGUILayout.PropertyField(bladeForce);
            EditorGUILayout.PropertyField(bladeLifetime);
            EditorGUILayout.PropertyField(bladeStartBrake);
            EditorGUILayout.PropertyField(bladeCast);
        }

        private void CircleTab()
        {
            EditorGUILayout.PropertyField(circlePrefab);
            EditorGUILayout.PropertyField(circleStartPoint);
            EditorGUILayout.PropertyField(circleDuration);
            EditorGUILayout.PropertyField(circleCast);
        }

        private void WallTab()
        {
            EditorGUILayout.PropertyField(wallPrefab);
            EditorGUILayout.PropertyField(wallStartPoint);
            EditorGUILayout.PropertyField(wallSpeed);
            EditorGUILayout.PropertyField(maxDistance);
            EditorGUILayout.PropertyField(wallCast);
        }

        private void GeneralTab()
        {
            EditorGUILayout.PropertyField(mainCamera);
            EditorGUILayout.PropertyField(animator);
        }

    }
}

