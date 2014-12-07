using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftingLegends.Core;
using CraftingLegends.Framework;

public class Arena : MonoBehaviour {

	public PlayerCharacter heroPrefab;
	public EnemyCharacter enemyPrefab;

	private Rooms _rooms;

	public PlayerCharacter currentPlayerCharacter = null;

	private PlayerDisplay _playerDisplay;

	private Narrator _narrator;

	private List<EnemyCharacter> _enemies = new List<EnemyCharacter>();
	private List<PlayerCharacter> _playerCharacters = new List<PlayerCharacter>();

	private int deathCount = 0;

	// ================================================================================
	//  unity methods
	// --------------------------------------------------------------------------------

    void Awake() {
		_rooms = FindObjectOfType<Rooms>();
		_narrator = FindObjectOfType<Narrator>();
		_playerDisplay = FindObjectOfType<PlayerDisplay>();
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

		DespawnPlayers();
		DetachFromPlayerCharacter();

		DespawnEnemies();

		StartCoroutine(SpawnPlayer());
	}

	public IEnumerator SpawnPlayer()
	{
		// select start room
		Room startRoom = _rooms.ShowSingleRoom();

		yield return new WaitForSeconds(0.3f);

		// spawn player
		Vector3 spawnPos = SpawnPosFromRoom(startRoom);
		PlayerCharacter character = GameObjectFactory.Instantiate<PlayerCharacter>(heroPrefab, position:spawnPos);
		character.transform.parent = transform;
		character.Spawn(startRoom);
		_playerCharacters.Add(character);
		AttachToPlayerCharacter(character);
		_narrator.GreetNewHero(character);

		// spawn neighbour rooms
		List<Room> roomsToSpawn = startRoom.neighbours.Clone();
		while (roomsToSpawn.Count > 0)
		{
			yield return new WaitForSeconds(0.3f);
			Room newRoom = roomsToSpawn.PopRandom();
			newRoom.Show();
		}

		// spawn enemies
		Room enemyRoom = _rooms.PickRandomDistant(startRoom);
		SpawnEnemies(enemyRoom, Random.Range(3, 5));
		var pathToEnemies = _rooms.GetPath(startRoom, enemyRoom);
		foreach (var item in pathToEnemies)
		{
			item.Show();
		}
	}

	public IEnumerator NextGame()
	{
		DetachFromPlayerCharacter();

		yield return new WaitForSeconds(1f);

		DespawnPlayers();
		DespawnEnemies();

		yield return new WaitForSeconds(1f);

		StartCoroutine(SpawnPlayer());
	}

	public void SpawnEnemies()
	{
		var room = _rooms.PickRandom();

		int enemyCount = Random.Range(3, 5);

		SpawnEnemies(room, enemyCount);
	}

	public void SpawnEnemies(Room spawnRoom, int enemyCount)
	{
		EnemyGroup group = new EnemyGroup();

		deathCount = 0;

		if (!spawnRoom.isActive)
			spawnRoom.Show();

		for (int i = 0; i < enemyCount; i++)
		{
			Vector3 spawnPos = SpawnPosFromRoom(spawnRoom);
			EnemyCharacter enemy = GameObjectFactory.Instantiate<EnemyCharacter>(enemyPrefab, position:spawnPos);
			enemy.transform.parent = transform;
			enemy.Spawn(spawnRoom);
			enemy.DeathEvent += EnemyDied;
			_enemies.Add(enemy);
			group.Add(enemy);
		}
	}

	// ================================================================================
	//  private methods
	// --------------------------------------------------------------------------------

	void EnemyDied(BaseCharacter character)
	{
		deathCount++;

		if (deathCount >= _enemies.Count)
		{
			RoundWon();
		}
	}

	private void RoundWon()
	{
		StartCoroutine(EndRound());
	}

	private IEnumerator EndRound()
	{
		DespawnEnemies();

		yield return new WaitForSeconds(0.5f);

		SpawnEnemies();
	}

	private void DespawnPlayers()
	{
		foreach (var player in _playerCharacters)
		{
			player.Despawn();
		}

		_playerCharacters.Clear();
	}

	private void DespawnEnemies()
	{
		foreach (var enemy in _enemies)
		{
			enemy.DeathEvent -= EnemyDied;
			enemy.Despawn();
		}

		_enemies.Clear();
	}

	private void AttachToPlayerCharacter(PlayerCharacter character)
	{
		currentPlayerCharacter = character;

		Game.Instance.inputController.SetInput(character);
		Game.Instance.inventory.AttachToCharacter(character);
		_playerDisplay.AttachToPlayer(character);

		currentPlayerCharacter.DeathEvent += PlayerDeathEvent;
	}

	void PlayerDeathEvent(BaseCharacter character)
	{
		StartCoroutine(NextGame());
	}

	private void DetachFromPlayerCharacter()
	{
		currentPlayerCharacter.DeathEvent -= PlayerDeathEvent;

		Game.Instance.inputController.DisableInput();
		Game.Instance.inventory.DetachFromCharacter();
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
		Vector2 randomOffset = Random.insideUnitCircle * 0.35f;
		return new Vector3(spawnPos.x + randomOffset.x, spawnPos.y + randomOffset.y, 0f);
	}
}