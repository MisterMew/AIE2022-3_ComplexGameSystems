using UnityEngine;
using UnityEditor;

/*
Fixes a mesh's rotation after importing it from Blender. In Blender, the Z axis points in the
"up" direction, while in Unity, Z points in the "forward" direction. This script rotates the
mesh's vertices so that it is upright again.

To use this, create an object from a mesh in the hierarchy view, then locate the Mesh Filter
in the inspector. There, click on the new "Fix Rotation" button.

Unity forum thread: http://forum.unity3d.com/threads/axis-rotation-blender-vs-unity.312481/

See also: http://answers.unity3d.com/questions/304805/rotate-only-mesh-not-whole-gameobject.html
*/

/// <summary>
/// Allows the adjustment of a models mesh rotation
/// </summary>
[CustomEditor(typeof(MeshFilter))]
public class FixMeshRotationEditor : Editor
{
	/* Variabels */
	private Vector3 mMeshRotation;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI(); //Used to draw the default inspector

		EditorGUILayout.Space(8);
		
		EditorGUILayout.LabelField("Mesh Rotation Correction", EditorStyles.boldLabel);
		mMeshRotation = EditorGUILayout.Vector3Field("Corrected Rotation: ", mMeshRotation);

		/* Fix Rotation Button */
		if (GUILayout.Button("Fix Rotation"))
		{
			MeshFilter meshFilter = (MeshFilter)target;
			Mesh mesh = meshFilter.sharedMesh;
			Vector3[] vertices = mesh.vertices;
			Vector3[] newVertices = new Vector3[vertices.Length];
			Quaternion rotation = Quaternion.Euler(mMeshRotation.x, mMeshRotation.y, mMeshRotation.z);
			for (int i = 0; i < vertices.Length; i++)
			{
				Vector3 vertex = vertices[i];
				newVertices[i] = rotation * vertex;
			}
			mesh.vertices = newVertices;
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();

			EditorUtility.SetDirty(meshFilter);

		}

		EditorGUILayout.Space(8);
	}
}