using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Horde.AI
{
	public class GeneralAIEars : MonoBehaviour 
	{
		GeneralAIBrain brain;
		Vector3 posOfSound;

		[SerializeField]float audioMaxDetectRange = 20;

		void Start() 
		{
			brain = GetComponent<GeneralAIBrain> ();
		}

		public void HeardNoise (Vector3 src)
		{
			if (Mathf.Abs (Vector3.Distance (transform.position, src)) <= audioMaxDetectRange) 
			{
				posOfSound = src;
				brain.PlaceOfInterest = posOfSound;
				brain.Investigating = true;

				if (brain.CurrentState == GeneralAIBrain.AI_STATE.IDLE)
					brain.CurrentState = GeneralAIBrain.AI_STATE.HUNT;
			}
		}

		public Vector3 PositionOfSound 
		{
			get{ return posOfSound; } 
		}
	}
}