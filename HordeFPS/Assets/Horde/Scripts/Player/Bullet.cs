using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	[SerializeField]float timer = 3f;
	[SerializeField]float speed = 10f;

	[SerializeField]Score score;

	void Awake()
	{
		score = GameObject.FindGameObjectWithTag ("Score").GetComponent<Score> ();
	}

	void Start()
	{
		Invoke ("Die", timer);	

	}

	void Update()
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}


	void OnTriggerEnter(Collider e)
	{
		if (e.CompareTag ("Enemy"))
		{
			CancelInvoke ();
			Destroy (e.gameObject);
			score.Add (100);
			Die ();
		}

	}

	void Die()
	{
		Destroy (gameObject);
	}
}
