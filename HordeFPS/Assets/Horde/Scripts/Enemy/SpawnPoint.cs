using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	[SerializeField] GameObject prefab;
	[SerializeField] float repeatTime = 3f;

	float repeat;

	// Use this for initialization
	void Start () {
		repeat = Random.Range (1, repeatTime);
	}

	public void Spawn () 
	{
		#if UNITY_EDITOR
		if (DebugMode.Instance.SPAWN_DEBUG)
		#endif
			for (int i = 1; i-1 < repeat; i++) {
				Invoke("SpawnPrefab", 0.75f * i);
			}

		repeat = Random.Range(1, repeatTime);
	}

	void SpawnPrefab()
	{
		if (!SpawnManager.INSTANCE.isMaxedOut()) {
			GameObject x = Instantiate (prefab, transform.position, Quaternion.identity);
            x.transform.parent = transform;
            SpawnManager.INSTANCE.EnemiesSpawned += 1;
		} 
	}
}
