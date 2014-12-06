using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Core;

public class Arena : MonoBehaviour {

	private List<Room> _rooms;

	// ================================================================================
	//  unity methods
	// --------------------------------------------------------------------------------

    void Awake() {
		_rooms = new List<Room>(FindObjectsOfType<Room>());
    }

	void Start()
	{
		ShowSingleRoom();
	}

	// ================================================================================
	//  public methods
	// --------------------------------------------------------------------------------

	public void HideAllRooms()
	{
		foreach (var room in _rooms)
		{
			room.Hide();
		}
	}

	public Room ShowSingleRoom()
	{
		Room spawnRoom = _rooms.PickRandom();

		foreach (var room in _rooms)
		{
			if (room == spawnRoom)
				room.Show();
			else
				room.Hide();
		}

		return spawnRoom;
	}

}