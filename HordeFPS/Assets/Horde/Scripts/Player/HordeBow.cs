using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
using TMPro;
namespace Horde
{
    public class HordeBow : MonoBehaviour
    {
        public static HordeBow Instance { get; private set; }
        [SerializeField] [Range(5, 20)] int maxAmmo = 10;
        //[SerializeField] Transform tip;
        //[SerializeField] GameObject bullet;
        //[SerializeField] GameObject flashlight;
        //[SerializeField] AudioClip reloadAudio;
        //[SerializeField] AudioClip shootAudio;
        //[SerializeField] float reloadTime = 2.0f;
        TextMeshProUGUI _ammoUI;

        bool _loaded;
        int _curAmmo = 10;

        void Awake()
        {
            Instance = this;
            _curAmmo = maxAmmo;
            _loaded = true;
            if (GameObject.FindGameObjectWithTag("Ammo") != null)
            {
                _ammoUI = GameObject.FindGameObjectWithTag("Ammo").GetComponent<TextMeshProUGUI>();
                UpdateUIAmmo();
            }
        }

        //   void Update()
        //{
        //	if ( Input.GetMouseButtonDown (0) ) 
        //	{
        //		Shoot ();
        //	}

        //	if (Input.GetKeyUp (KeyCode.E))
        //       {
        //		flashlight.SetActive( !flashlight.activeInHierarchy);
        //	}
        //}

        //   /// <summary>
        //   /// Shoot a prefab.
        //   /// handles launching projectile, soundeffects, and particle effects.
        //   /// </summary>
        //   void Shoot()
        //   {
        //	if ((_curAmmo != 0 || DebugMode.Instance.AMMO_DEBUG) && _loaded)
        //       { 
        //           Instantiate(bullet, tip.position, transform.rotation);
        //           _curAmmo--;
        //		UpdateUIAmmo ();
        //           _loaded = false;
        //           Invoke("LoadCrossbow", reloadTime);
        //       }
        //}

        ///// <summary>
        ///// Play audio of loading Crossbow
        ///// at the end of the clip set load
        ///// </summary>
        //void LoadCrossbow()
        //{
        //    _loaded = true;
        //}

        /// <summary>
        /// When player walking over arrow it adds to the count of arrows. 
        /// </summary>
        public void AddAmmo()
        {
            if (_curAmmo < maxAmmo)
            {
                _curAmmo++;
                UpdateUIAmmo();
                //if (ArrowHand.Instance != null)
                //    ArrowHand.Instance.isEmpty = false;
            }
        }
        public void LoseAmmo()
        {

            _curAmmo--;
            if (_curAmmo < 0)
            {
                _curAmmo = 0;
                //if (ArrowHand.Instance != null)
                //    ArrowHand.Instance.isEmpty = true;
            }

            UpdateUIAmmo();
        }

        public bool isEmpty()
        {
            return _curAmmo <= 0;
        }

        void UpdateUIAmmo()
        {
            if (_ammoUI != null)
                _ammoUI.text = _curAmmo + "/" + maxAmmo;
        }

    }
}
