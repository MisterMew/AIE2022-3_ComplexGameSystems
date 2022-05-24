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

        reorderableList.drawHeaderCallback = DrawHeader; //Delegate to draw the header
        reorderableList.drawElementCallback = DrawList; //Delegate to draw the elements on the list
    }


    public override void OnInspectorGUI()
    {

        /* Validation Check */
        if (cb.behaviours == null || cb.behaviours.Length == 0)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("No behaviours in array.", MessageType.Warning);
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            /* Draw Reordable List */
            serializedObject.Update(); //Update the array property's representation in the inspector
            reorderableList.DoLayoutList(); //Draws the reordable list
            serializedObject.ApplyModifiedProperties(); //Applies the editor changes (Unity's way of saving changes)

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

    void DrawHeader(Rect rect)
    {
        string name = "Custom Behaviours";
        EditorGUI.LabelField(rect, name);
    }

    private void DrawList(Rect rect, int index, bool active, bool focused)
    {
        EditorGUI.BeginChangeCheck(); //Begin Change Check

        if (cb.weights.Length != cb.behaviours.Length)
        {
            cb.AddWeight();
        }

        EditorGUILayout.BeginHorizontal(); //Begin H draw
        ///
        EditorGUI.LabelField(rect, index.ToString()); //Draw label field to number elements
        cb.behaviours[index] = (FlockBehaviours)EditorGUI.ObjectField(new Rect(rect.x + 16, rect.y, 232, EditorGUIUtility.singleLineHeight), cb.behaviours[index], typeof(FlockBehaviours), false); //Draw behaviours as ObjectField
        cb.weights[index] = EditorGUI.FloatField(new Rect(rect.x + 256, rect.y, 64, EditorGUIUtility.singleLineHeight), cb.weights[index]); //Draw behaviour modifiers as FloatField
        ///
        EditorGUILayout.EndHorizontal(); //End H draw

        if (EditorGUI.EndChangeCheck()) //End Change Check
        {
            EditorUtility.SetDirty(target);
            GUIUtility.ExitGUI();
        }
    }

    internal void DoAddButton(ReorderableList list, Object value) {
        cb.AddBehaviour();
    }
}