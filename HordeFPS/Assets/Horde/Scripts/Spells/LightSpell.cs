using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpell : Spell 
{
	[SerializeField] ParticleSystem trail;
	[SerializeField] Renderer rend; 
	[SerializeField] GameObject explosion;

	Vector2 materialOffset;

	void Start()
	{
		materialOffset = new Vector2 (Random.Range (0, 1), Random.Range (0, 1));
		rend.material.SetTextureOffset ("_MainTex", materialOffset);
		Invoke ("DestroySpell", activeTime);
	}

	void Update()
	{
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
		MaterialOffsetMovement ();
	}

	public override void DestroySpell()
	{
		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (this.gameObject);
	}

	public override void HitEnemy ()
	{
		throw new System.NotImplementedException ();
	} 

	void MaterialOffsetMovement ()
	{
		materialOffset += Vector2.one * Time.deltaTime;
		rend.material.SetTextureOffset ("_MainTex", materialOffset);
	}
}
