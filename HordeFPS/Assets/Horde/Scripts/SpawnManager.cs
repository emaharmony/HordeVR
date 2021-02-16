/// <summary>
/// Spawn manager.
/// Emmanuel Vinas
/// 8/25/17
/// 
/// Regulates spawn frequency at spawn points. Using a timer it calls a spawn point to spawn a random amount of enemies to chase the player.
/// This script also controls the UI that involves spawns and waves
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnManager : MonoBehaviour 
{
	public static SpawnManager INSTANCE { get; private set;}		//instance of this class that can be called

	[Range(1,10)][SerializeField] float spawnTimerMax;				//max amount of time to wait before invoking a spawn
	[Range(5,15)][SerializeField] float newWaveWaitTime = 10f;		//that break time between waves
	[SerializeField]TextMeshProUGUI enemiesLeftUI;								//UI text that shows how many enemies are left
	[SerializeField]TextMeshProUGUI currentWaveUI;								//UI that tracks which wave you're at
	[SerializeField]CanvasGroup newWaveText;

	float timerSpawn, currTimer;
	[SerializeField]List<SpawnPoint> spawnPoints;

    bool started = true;
	int wave, enemyCount, enemiesLeft;
 
	void Awake()
	{
		INSTANCE = this;
		spawnPoints = new List<SpawnPoint> ();
		GameObject[] x = GameObject.FindGameObjectsWithTag ("Spawn");

		foreach (GameObject i in x) 
		{
			spawnPoints.Add( i.GetComponent<SpawnPoint> ());
		}
		for (int i = 0; i < spawnPoints.Count; i++) 
		{
			spawnPoints [i].enabled = true;	
		}
	}

	void Start()
	{
		currTimer = 0;
		wave = 1;
		enemiesLeft = enemyCount = 20;
		EnemiesSpawned = 0;
		timerSpawn = Random.Range(3f, spawnTimerMax);
		UpdateSpawnUI ();
	}

	void Update()
	{
        if(started)
		    SpawnSystem ();
	}

	void SpawnSystem()
	{
		if (currTimer >= timerSpawn) {
			SpawnEnemies ();
		} else 	{
			currTimer += Time.deltaTime;
		}
	}

	void SpawnEnemies ()
	{
		int index = Random.Range (0, spawnPoints.Count);
		spawnPoints [index].Spawn ();
		timerSpawn = Random.Range (0.5f, spawnTimerMax);
		currTimer = 0;
	}

	public int MaxSpawnCount 
	{
		get{ return enemyCount; }
	}

	public int EnemiesSpawned { get; set;}

	public int EnemiesLeft { get{ return enemiesLeft; } 
		set{
			enemiesLeft = value; 
			if (enemiesLeft <= 0)
				EndWave ();
			UpdateSpawnUI(); 
		}
	}

	void EndWave()
	{
		Invoke ("StartNewWave", newWaveWaitTime);
	}

	void StartNewWave() 
	{
		//Reset EnemiesSpawned
		//INcrement the total enemies and set the enemies left to that value
		//Increment Wave Number
		//resetTimer
		//Update UI() --> also include a creepy message before each wave.
		EnemiesSpawned = 0;
		enemyCount += (enemyCount / 2) + 5;
		enemiesLeft = enemyCount;
		wave++;
		UpdateSpawnUI (true);
		currTimer = 0;

	}

    public void StartGame()
    {
        started = true;
    }

	void UpdateSpawnUI(bool nW = false)
	{
		currentWaveUI.text = "Wave: " + wave;
		enemiesLeftUI.text = "Enemies Left: " + enemiesLeft;

		if (nW) 
		{
			StartCoroutine (FadeInOut());
		}
	}

	IEnumerator FadeInOut()
	{
		while (newWaveText.alpha < 1) {
			newWaveText.alpha += Time.deltaTime;
			yield return null;
		}

		yield return new WaitForSeconds (3.5f);

		while (newWaveText.alpha > 0) {
			newWaveText.alpha -= Time.deltaTime;
			yield return null;
		}
	}

	public bool isMaxedOut() 
	{
		return EnemiesSpawned >= MaxSpawnCount;
	}
}

