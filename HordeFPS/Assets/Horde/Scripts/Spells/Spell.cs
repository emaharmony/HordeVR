using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
	[SerializeField]protected float activeTime = 5f;
	[SerializeField]protected float moveSpeed = 2f;

	public abstract void DestroySpell ();
	public abstract void HitEnemy ();
}
