using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Framework;

[CustomEditor(typeof(Rooms))]
public class RoomsInspector : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (GUILayout.Button("Find Neighbours"))
		{
			FindNeighbours();
		}
	}

	private void FindNeighbours()
	{
		var rooms = FindObjectsOfType<Room>();

		foreach (var item in rooms)
		{
			item.FindNeighbourRooms();
			EditorUtility.SetDirty(item);
		}
	}

	private void UpdateDepthForAllRooms()
	{
		var rooms = FindObjectsOfType<Room>();

		foreach (var item in rooms)
		{
			RenderDepthUpdate updateScript = item.GetComponent<RenderDepthUpdate>();
			item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.position.y + updateScript.offset);
		}
	}

}