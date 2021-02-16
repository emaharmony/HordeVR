using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horde.AI
{
	public class GeneralAIEyes : MonoBehaviour
	{
		GeneralAIBrain brain;
		Camera eyes; 
		Plane[] planes; 
		Collider playerCollider;

        private void Awake()
        {
            playerCollider = GameObject.FindGameObjectWithTag("PlayerCollider").GetComponent<Collider>();
            eyes = GetComponent<Camera>();
            brain = GetComponentInParent<GeneralAIBrain>();
        }
        // Use this for initialization
        void Start () 
		{
            eyes.transform.localPosition = new Vector3(0, 0.5f,0.5f);
		}
		
		// Update is called once per frame
		void Update () 
		{
			planes = GeometryUtility.CalculateFrustumPlanes (eyes);
			if (GeometryUtility.TestPlanesAABB (planes, playerCollider.bounds) && brain.CurrentState != GeneralAIBrain.AI_STATE.ATTACK) 
			{
				Debug.Log("Found player");
				Vector3 forward = brain.PlayerPosition.TransformDirection (Vector3.forward);
				Vector3 otherPos = transform.position - brain.PlayerPosition.position;
				brain.CurrentState = (Vector3.Dot(forward, otherPos) < 0) ? GeneralAIBrain.AI_STATE.FOLLOW : GeneralAIBrain.AI_STATE.ATTACK;
			}
		}
	}
}