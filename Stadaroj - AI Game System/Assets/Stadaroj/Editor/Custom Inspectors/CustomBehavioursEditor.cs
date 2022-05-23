using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(CustomisedBehaviour))]
public class CustomBehavioursEditor : Editor
{
    /* Variables */
    private CustomisedBehaviour cb;
    private ReorderableList reorderableList;

    private void OnEnable()
    {
        cb = (CustomisedBehaviour)target;
        reorderableList = new ReorderableList(cb.behaviours, typeof(CustomisedBehaviour), true, true, true, true); //Populate the reordable list

        //reorderableList.drawElementCallback += DrawElement;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        reorderableList.DoLayoutList(); //Draws the list
        serializedObject.ApplyModifiedProperties();

        /* Validation Check */
        if (cb.behaviours == null || cb.behaviours.Length == 0)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("No behaviours in array.", MessageType.Warning);
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            DrawTitles();
            DrawFields(cb); 
        }

        DrawButtons();
    }

    /// <summary>
    /// Draw the respect titles for the respective componenets, behaviours and their modifiers
    /// </summary>
    private void DrawTitles()
    {
        EditorGUILayout.BeginHorizontal(); //Begin H draw

        EditorGUILayout.LabelField("Behaviours", GUILayout.MinWidth(60f), GUILayout.MaxWidth(290f));
        EditorGUILayout.LabelField("Weights", GUILayout.MinWidth(65f), GUILayout.MaxWidth(65f));

        EditorGUILayout.EndHorizontal(); //End H draw
    }

    /// <summary>
    /// Draw the fields components for the behaviours and their respective modifiers
    /// </summary>
    /// <param name="cb"></param>
    private void DrawFields(CustomisedBehaviour cb)
    {
        EditorGUI.BeginChangeCheck(); //Begin Changes Check


        if (cb.weights.Length != cb.behaviours.Length)
        {
            cb.AddWeight();
        }

        for (int i = 0; i < cb.behaviours.Length; i++)
        {
            EditorGUILayout.BeginHorizontal(); //Begin H draw

            EditorGUILayout.LabelField(i.ToString(), GUILayout.MinWidth(20f), GUILayout.MaxWidth(20f)); //Draw label field to number elements

            cb.behaviours[i] = (FlockBehaviours)EditorGUILayout.ObjectField(cb.behaviours[i], typeof(FlockBehaviours), false, GUILayout.MinWidth(20F)); //Draw behaviours as ObjectField
            cb.weights[i] = EditorGUILayout.FloatField((float)cb.weights[i], GUILayout.MinWidth(60F), GUILayout.MaxWidth(60F)); //Draw behaviour modifiers as FloatField

            EditorGUILayout.EndHorizontal(); //End H draw
        }

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
            GUIUtility.ExitGUI();
        }
    }

    private void DrawButtons()
    {
        EditorGUILayout.BeginHorizontal(); //Begin H draw

        /* Add Button */
        if (GUILayout.Button("Add Behaviour"))
        {
            cb.AddBehaviour();
            GUIUtility.ExitGUI();
        }

        /* Remove Button */
        if (cb.behaviours != null && cb.behaviours.Length > 0)
        {
            if (GUILayout.Button("Remove Behaviour"))
            {
                cb.RemoveBehaviour();
                GUIUtility.ExitGUI();
            }
        }
        EditorGUILayout.EndHorizontal(); //End H draw
    }

    private void DrawElement(Rect rect, int index, bool active, bool focused)
    {
        CustomisedBehaviour currentItem = (CustomisedBehaviour)cb.behaviours[index];
        float item = (float)cb.weights[index];

        if (currentItem != null)
        {
            EditorGUI.BeginChangeCheck(); //Begin Changes Check

            ///
            ///stuff goes here
            ///

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);
                GUIUtility.ExitGUI();
            }
        }
    }
}