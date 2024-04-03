using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MayoPowerUp : MonoBehaviour
{
    [SerializeField] bool useOnImpact = false;
    [Header("FX")]
    [SerializeField]
        private GameObject _artToDisable = null;
    [SerializeField]
        private ParticleSystem[] _particles;
    [SerializeField][Tooltip("If no Audio is selected, nothing will play.")]
        private AudioClip _pickupSound = null;//@TODO allow this to be a list.
    [SerializeField][Range(0.0f, 1.0f)] private float volume = 1.0f;


    private Collider _collider = null;

    //@TODO when invetory is full make useOnImpact true
    private void Awake() {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other) {
        PlayerShip ship = other.GetComponent<PlayerShip>();
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if (ship != null) {
            if (_pickupSound != null) { AudioHelper.PlayClip2D(_pickupSound, volume); }
            if(inventory != null) { inventory.mayoCollected++; }
            for(int i = 0; i < _particles.Length; i++) { _particles[i].Play(); }
            
            _collider.enabled = false;
            _artToDisable.SetActive(false);

            //StartCoroutine(PlayFXOnly());
        }
    }
}

