using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpell : Spell {

	#region implemented abstract members of Spell
	public override void DestroySpell ()
	{
		Destroy (this.gameObject);
	}

	public override void HitEnemy ()
	{
		throw new System.NotImplementedException ();
	} 
	#endregion

	// Use this for initialization
	void Start () {
		Invoke ("DestroySpell", activeTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
	}
}
