using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvisibiltyPowerUp : MonoBehaviour {
    
    [Header("FX")]
    [SerializeField]
    private GameObject _artToDisable = null;
    [SerializeField]
    private ParticleSystem _particles = null;
    [SerializeField]
    [Tooltip("If no Audio is selected, nothing will play.")]
    private AudioClip _pickupSound = null;//@TODO allow this to be a list.
    [SerializeField][Range(0.0f, 1.0f)] private float volume = 1.0f;

    [Header("Mechanics")]
    [SerializeField]private GameObject _invisibleModel = null;
    [SerializeField]private GameObject _baseModel = null;
    [SerializeField] private AudioSource _engineAudio = null;
    [SerializeField][Range(0, 1)] private float _cloakedVolume = 1.0f;
    [SerializeField][Range(0,30)]private float _powerUpDur = 10f;
    private Collider _collider;

    private float volStorage;

    private void Start() {
        _collider = GetComponent<Collider>();
        //_invisibleModel = GameObject.FindGameObjectWithTag("ModelSwap");
        //_baseModel = GameObject.FindGameObjectWithTag("BaseModel");
    }

    private void OnTriggerEnter(Collider other) {
        PlayerShip ship = other.GetComponent<PlayerShip>();
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if (ship != null) {
            _invisibleModel = ship._swapModel;
            _baseModel = ship._baseModel;
            _engineAudio = other.GetComponentInChildren<AudioSource>();

            if (_pickupSound != null) { AudioHelper.PlayClip2D(_pickupSound, volume); }

            if(_particles){_particles.Play(); }
              StartCoroutine(PowerupSequence(ship));
        }
    }

    IEnumerator PowerupSequence(PlayerShip ship) {
        ship.invisible = true;
        _collider.enabled = false;
        _artToDisable.SetActive(false);
       
        _baseModel.SetActive(false);
        _invisibleModel.SetActive(true);

        volStorage = _engineAudio.volume;
        _engineAudio.volume = _cloakedVolume;
        yield return new WaitForSeconds(_powerUpDur);
        
        ship.invisible = false;
        _invisibleModel.SetActive(false);
        _baseModel.SetActive(true);
        _engineAudio.volume = volStorage;

        //DeactivatePowerUp(ship);

        Destroy(this.gameObject); //can change this to make it temp disabled for a time.
        Debug.Log("Invisbility Powerup finished");
    }
}
