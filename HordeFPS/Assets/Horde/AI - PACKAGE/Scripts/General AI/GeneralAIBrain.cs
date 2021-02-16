/// <summary>
/// General AI brain.
/// Emmanuel Vinas
/// 09/25/2017
/// 
/// Will be the foundation for a basic AI. All brains will inherit from this script
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

namespace Horde.AI
{
	[RequireComponent(typeof(NavMeshAgent))]
	[RequireComponent(typeof(GeneralAIEars))]
    [RequireComponent(typeof(EnemyLifeManager))]
	public abstract class GeneralAIBrain : MonoBehaviour 
	{
		public enum AI_STATE
		{
			IDLE,
			RETREAT,		//will force AI to back off
			HUNT,			//AI will lok for the player
			FOLLOW,			//AI will follow the player in a secretive manner
			ATTACK			//if found or ready to attack, it will Attack the player
		}

		//Variables for Player
		protected Vector3 _lastKnownPlayerPos; 	//the last seen position of the player to the AI
		protected Transform _player;				//The transform of thew player will not be used until follow or attack.

		//Variables for Hunt
		protected Vector3 _posOfInterest;				//The position of any noise or anything.
		bool _investigating;

		//AI Variables 
		protected GeneralAIEars _ears;				//the ears to identify sound position
		protected GeneralAIEyes _eyes;				//the eyes to identify objects in front of AI
		protected bool _playerIsFound; 			//flag that indicates that the player was found by the AI
		protected bool _aiIsFound; 					//flag to indicate that player has seen this AI

		protected NavMeshAgent _agent; 			//NavMeshAgent to control the movement
        protected EnemyLifeManager enemyLifeManager;

		protected bool imDead = false;

		AI_STATE _currState = AI_STATE.IDLE;

		protected void Start () 
		{
			_player = GameObject.FindGameObjectWithTag ("Player").transform;
			_agent = GetComponent<NavMeshAgent> ();
			_ears = GetComponent<GeneralAIEars> ();
			_eyes = GetComponentInChildren<GeneralAIEyes> ();
            enemyLifeManager = GetComponent<EnemyLifeManager>();
			_playerIsFound = false;
			_aiIsFound = false;
		}


		void Update ()
		{
            if (enemyLifeManager.Dead && !imDead)
			{
				Die();
                return;
            }

            CheckAIState ();
		}

		void CheckAIState()
		{
			switch (_currState) 
			{
				case AI_STATE.IDLE:
					Idle ();
					break;
				case AI_STATE.RETREAT: 
					Retreat ();
					break;
				case AI_STATE.HUNT: 
					Hunt ();
					break;
				case AI_STATE.FOLLOW:
					Follow ();
					break;
				case AI_STATE.ATTACK:
					Attack ();
					break;
			}
		}

		protected abstract void Idle ();
		protected abstract void Retreat ();
		protected abstract void Hunt ();
		protected abstract void Follow ();
		protected abstract void Attack ();

		protected virtual void Die()
		{
			_agent.Stop();
			_agent.destination = Vector3.zero;
			imDead = true;
		}

		public bool PlayerIsFound 
		{
			get { return _playerIsFound;}
			set { _playerIsFound = value; _lastKnownPlayerPos = _player.position;}
		}

		public Transform PlayerPosition
		{
			get{ return _player; }
		}

		public Vector3 PlaceOfInterest 
		{
			get { return _posOfInterest; }
			set { _posOfInterest = value; }
		}

		public AI_STATE CurrentState 
		{
			get { return _currState; }
			set { _currState = value; }
		}
			
		public bool Investigating 
		{
			get { return _investigating; }
			set { _investigating = value; }
		}

	}
}