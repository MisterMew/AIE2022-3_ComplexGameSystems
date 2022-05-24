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
        reorderableList.drawElementCallback = DrawListItems; //Delegate to draw the elements on the list
        reorderableList.onAddCallback += AddItem; //Delegate to add an item to the list
        reorderableList.onRemoveCallback += RemoveItem; //Delegate to remove an item from the list
        Repaint();
    }


    public override void OnInspectorGUI()
    {
        /* Validation Check */
        if (cb.behaviours == null || cb.behaviours.Count == 0)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("No behaviours in array.", MessageType.Warning);
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            serializedObject.Update(); //Update the array property's representation in the inspector
            if (reorderableList != null)
            {
                reorderableList.DoLayoutList(); //Draws the reordable list
            }
            serializedObject.ApplyModifiedProperties(); //Applies the editor changes (Unity's way of saving changes)
        }
    }

    /// <summary>
    /// Draws the header for the reorderble list
    /// </summary>
    /// <param name="rect"></param>
    void DrawHeader(Rect rect)
    {
        string behavioursString = "Behaviours";
        Rect behavioursRect = new Rect(rect.x + 32, rect.y, 64, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(behavioursRect, behavioursString);

        string weightsString = "Weights";
        Rect weigthsRect = new Rect(rect.width - 48, rect.y, 64, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(weigthsRect, weightsString);
    }

    /// <summary>
    /// Draws the reordable lsit and all its items
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="index"></param>
    /// <param name="active"></param>
    /// <param name="focused"></param>
    private void DrawListItems(Rect rect, int index, bool active, bool focused)
    {
        EditorGUI.BeginChangeCheck(); //Begin Change Check

        /* Validate Lists */
        if (cb.weights.Count != cb.behaviours.Count)
        {
            cb.AddWeight();
        }

        /* Draw Lists */
        for (int i = 0; i < cb.behaviours.Count; i++)
        {
            EditorGUILayout.BeginHorizontal(); //Begin H draw

            EditorGUI.LabelField(rect, index.ToString()); //Draw label field to number elements
            cb.behaviours[index] = (FlockBehaviours)EditorGUI.ObjectField(new Rect(rect.x + 16, rect.y, rect.width - 100, EditorGUIUtility.singleLineHeight), cb.behaviours[index], typeof(FlockBehaviours), false); //Draw behaviours as ObjectField
            cb.weights[index] = EditorGUI.FloatField(new Rect(rect.width - 32, rect.y, 64, EditorGUIUtility.singleLineHeight), cb.weights[index]); //Draw behaviour modifiers as FloatField

            EditorGUILayout.EndHorizontal(); //End H draw
        }

        if (EditorGUI.EndChangeCheck()) //End Change Check
        {
            EditorUtility.SetDirty(target);
            GUIUtility.ExitGUI();
        }
    }

    /// <summary>
    /// Add and item to the reorderble list
    /// </summary>
    private void AddItem(ReorderableList mlist)
    {
        cb.AddBehaviour();
        Repaint();
    }

    /// <summary>
    /// Remove an item from the reorderble list
    /// </summary>
    /// <param name="list"></param>
    private void RemoveItem(ReorderableList list)
    {
        cb.RemoveBehaviour();
        Repaint();
    }
}