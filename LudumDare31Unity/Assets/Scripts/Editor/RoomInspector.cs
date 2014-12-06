using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(Room))]
public class RoomInspector : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (GUILayout.Button("Find Neighbours"))
		{
			(target as Room).FindNeighbourRooms();
			EditorUtility.SetDirty(target);
		}
	}

}