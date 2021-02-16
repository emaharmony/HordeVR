using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour 
{
	public static PlayerHealth Instance{ get; private set; }
	[Range(5,10)][SerializeField]int maxHitPoints; 
	[SerializeField]float healTimer;

	bool dead;
	int currHP;
	float timer;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		dead = false;
		timer = 0;
		currHP = maxHitPoints;
	}

	public void KillPlayer()
	{
		dead = true;
	}

	public void HurtPlayer(int dmg)
	{
		currHP -= dmg;
		dead = currHP <= 0;
	}

	public int MaxHP
	{
		get{ return maxHitPoints;}
	}

}
