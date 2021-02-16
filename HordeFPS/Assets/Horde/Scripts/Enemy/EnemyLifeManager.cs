using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class EnemyLifeManager : MonoBehaviour {
	[SerializeField]Color deadColor;
	[SerializeField]Color aliveColor;
	[SerializeField]NavMeshAgent agent;
	Transform target;
	Renderer rend; 

	[SerializeField]bool dead = false; 

	void Awake()
    {
		target = GameObject.FindGameObjectWithTag ("Player").transform;	
		rend = GetComponent<Renderer> ();
	}
		
	void Start()
	{
		rend.material.color = aliveColor;
	}

	void Update()
	{
		//agent.SetDestination ((dead) ? transform.position : target.position);
	}

	void OnTriggerEnter(Collider p)
	{
		if ((p.tag == "Player" || p.tag == "PlayerCollider") && !dead) {
            //SceneManager.LoadScene (0);
            Debug.Log("YOU ARE DEAD");
		}
	}

	public void DeathStuff() 
	{
		rend.material.color = (dead ? deadColor : aliveColor);

		transform.position = new Vector3(transform.position.x, dead ? 0 : transform.position.y, transform.position.z);
		transform.eulerAngles = new Vector3(270, transform.eulerAngles.y, transform.eulerAngles.z);

		if (dead)
			SpawnManager.INSTANCE.EnemiesLeft -= 1;
	}
	public bool Dead
    {
		get{ return dead; }
		set { dead = value; if (dead) DeathStuff(); }
	}
}
