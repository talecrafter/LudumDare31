using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Core;

public class Rooms : MonoBehaviour {

	private List<Room> _rooms = null;

	public void Awake()
	{
		_rooms = new List<Room>(FindObjectsOfType<Room>());
	}

	public List<Room> GetPath(Room start, Room end)
	{
		if (start == end)
		{
			return new List<Room>() { start };
		}

		Dictionary<Room, Room> visited = new Dictionary<Room, Room>();

		List<Room> openList = new List<Room>();
		openList.Add(start);
		Room current = null;
		visited.Add(start, null);

		while (!visited.ContainsKey(end) && openList.Count > 0)
		{
			current = openList.PopRandom();

			List<Room> neighbours = current.neighbours.Clone();
			neighbours.Shuffle();

			for (int i = 0; i < neighbours.Count; i++)
			{
				Room neighbour = neighbours[i];

				if (visited.ContainsKey(neighbour))
					continue;

				visited[neighbour] = current;

				if (neighbour == end)
					break;

				openList.Add(neighbour);
			}
		}

		// gather the path
		List<Room> path = new List<Room>();
		current = end;
		while(current != null)
		{
			Room from = visited[current];
			if (from != null)
			path.Add(from);

			current = from;
		}
		path.Reverse();

		return path;
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

	public Room PickRandom()
	{
		return _rooms.PickRandom();
	}

	public Room PickRandomDistant(Room fromRoom)
	{
		List<Room> validRooms = new List<Room>();

		foreach (var room in _rooms)
		{
			if (room != fromRoom && !fromRoom.neighbours.Contains(room))
				validRooms.Add(room);
		}

		return validRooms.PickRandom();
	}
}