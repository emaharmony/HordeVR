using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Horde.AI
{
	public class StalkerBrain : GeneralAIBrain 
	{
		[Header("Stalker Idle Variables")]
		[Range(10,20)]
		[SerializeField] float maxDistToTravelOnIdle = 10;
		[Range(1,10)]
		[SerializeField] float minDistToTravelOnIdle = 8;
		[Range(1,20)]
		[SerializeField] float maxWaitTime = 10;

		float timerIdle = 0, randomMagnitude = 0, timer = 0;
		float randomDirToGo;
		bool movingIdle;

		[Header("Stalker Hunting Variables")]
		[SerializeField] float maxLookAroundDistance;

		bool onTheHunt = false;


		void Awake() 
		{
			SetRandomVariables ();
		}

		void SetRandomVariables()
		{
			timerIdle = Random.Range (0, maxWaitTime);
			randomMagnitude = Random.Range (minDistToTravelOnIdle, maxDistToTravelOnIdle);
			randomDirToGo = Random.Range(0,360.0f);
			movingIdle = false;
		}


		IEnumerator IdleTargetChange() 
		{
			float rotTimer= 0;
			while (rotTimer < 1) 
			{
				rotTimer += Time.deltaTime;
				transform.Rotate(Vector3.up, randomDirToGo * Time.deltaTime);
				yield return null;
			}

			_agent.destination = transform.position + (transform.forward * randomMagnitude);
			while (_agent.remainingDistance > _agent.stoppingDistance) 
			{
				yield return null;
			}

			SetRandomVariables ();
			yield return null;
		}

		IEnumerator HuntProcess()
		{
			_agent.destination = (Investigating ? _posOfInterest : _agent.destination);
			while (Investigating) 
			{
				if (_agent.remainingDistance <= _agent.stoppingDistance) 
				{
					Investigating = false;
				}

				yield return null;
			}

			yield return null;
		}

		#region implemented abstract members of GeneralAIBrain

		protected override void Idle ()
		{
            if (!enemyLifeManager.Dead)
            {
                if (timer >= timerIdle && !movingIdle)
                {
                    movingIdle = true;
                    timer = 0;
                    StartCoroutine(IdleTargetChange());
                }
                else if (movingIdle)
                {
                    return;
                }

                timer += Time.deltaTime;
            }
		}

		protected override void Retreat ()
		{
			throw new System.NotImplementedException ();
		}

		protected override void Hunt ()
		{
            if (!enemyLifeManager.Dead)
            {

                if (movingIdle)
                    StopCoroutine("IdleTargetChange");

                if (Investigating)
                {
                    StartCoroutine("HuntProcess");
                }
            }

		}

		protected override void Follow ()
		{
            if (!enemyLifeManager.Dead)
            {
                Debug.Log(transform.name + ": Following");
            }
		}

		protected override void Attack ()
		{
            if (!enemyLifeManager.Dead)
            {
                Debug.Log(transform.name + ": Attacking");
                _agent.destination = _player.position;
            }
            else
            {

				_agent.destination = Vector3.zero ;
            }
		}

		#endregion
	}
}