using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
namespace Horde
{

    [RequireComponent(typeof(Interactable))]
    public class HordeArrow : MonoBehaviour
    {
        [SerializeField] float speed = 10f;
        [SerializeField] Score score;
        [SerializeField] GameObject sparkles;
        Interactable pickUpTrigger;
        Rigidbody phyBody;
        bool impact = false;
        HordeBow cb;
        GameObject enemyCollection;
        Transform origin;

        void Awake()
        {
            pickUpTrigger = GetComponent<Interactable>();
            enemyCollection = GameObject.FindGameObjectWithTag("AllEnemies");
        }

        void Start()
        {
            sparkles.SetActive(false);
           pickUpTrigger.enabled = false;
        }


        void OnCollisionEnter(Collision collision)
        {
            ContactPoint contact = collision.contacts[0];

            if (contact.otherCollider.tag == "Enemy")
            {
                HitEnemy(contact.otherCollider.transform);
            }

            pickUpTrigger.enabled= true;
            sparkles.SetActive(true);
            enemyCollection.BroadcastMessage("HeardNoise", transform.position);
        }

        public void HitEnemy(Transform t)
        {
            EnemyLifeManager ec = t.GetComponent<EnemyLifeManager>();
            if (!ec.Dead)
            {
                ec.Dead = true;
                Debug.Log(t.name + " is Dead.");
                SpawnManager.INSTANCE.EnemiesLeft -= 1;
            }
        }


        void OnTriggerEnter(Collider p)
        {
            if (p.CompareTag("Player"))
            {
                PickedUp();
            }
        }

        void PickedUp()
        {
            //cb.AddAmmo();
            Destroy(this.gameObject);
        }
    }
}