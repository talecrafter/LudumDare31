using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Core;
using CraftingLegends.Framework;

public class Arena : MonoBehaviour {

	public PlayerCharacter heroPrefab;
	public Actor enemyPrefab;

	private List<Room> _rooms;

	public PlayerCharacter currentPlayerCharacter = null;

	private PlayerDisplay _playerDisplay;

	// ================================================================================
	//  unity methods
	// --------------------------------------------------------------------------------

    void Awake() {
		_playerDisplay = FindObjectOfType<PlayerDisplay>();

		_rooms = new List<Room>(FindObjectsOfType<Room>());
    }

	void Start()
	{
		StartCoroutine(SpawnPlayer());
	}

	// ================================================================================
	//  public methods
	// --------------------------------------------------------------------------------

	public void Reset()
	{
		StopAllCoroutines();

		DetachFromPlayerCharacter();

		StartCoroutine(SpawnPlayer());
	}

	public IEnumerator SpawnPlayer()
	{
		Room startRoom = ShowSingleRoom();
		yield return new WaitForSeconds(0.3f);

		Vector3 spawnPos = SpawnPosFromRoom(startRoom);
		PlayerCharacter character = GameObjectFactory.Instantiate<PlayerCharacter>(heroPrefab, position:spawnPos);

		AttachToPlayerCharacter(character);

		List<Room> roomsToSpawn = startRoom.neighbours.Clone();

		while (roomsToSpawn.Count > 0)
		{
			yield return new WaitForSeconds(0.3f);
			Room newRoom = roomsToSpawn.PopRandom();
			newRoom.Show();
		}
	}

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

	// ================================================================================
	//  private methods
	// --------------------------------------------------------------------------------

	private void AttachToPlayerCharacter(PlayerCharacter character)
	{
		currentPlayerCharacter = character;

		Game.Instance.inputController.SetInput(character);
		_playerDisplay.AttachToPlayer(character);
	}

	private void DetachFromPlayerCharacter()
	{
		Game.Instance.inputController.DisableInput();
		_playerDisplay.DetachFromPlayer();

		if (currentPlayerCharacter != null)
		{
			currentPlayerCharacter.Die();
			currentPlayerCharacter = null;
		}
	}

	private Vector3 SpawnPosFromRoom(Room room)
	{
		Vector3 spawnPos = room.transform.position;
		Vector2 randomOffset = Random.insideUnitCircle * 0.2f;
		return new Vector3(spawnPos.x + randomOffset.x, spawnPos.y + randomOffset.y, 0f);
	}
}