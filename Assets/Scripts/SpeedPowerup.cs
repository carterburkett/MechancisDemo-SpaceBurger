using NUnit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeedPowerup : MonoBehaviour
{
    [SerializeField] bool useOnImpact = false;
    [Header("FX")]
    [SerializeField]
    private GameObject _artToDisable = null;
    [SerializeField]
    private ParticleSystem _particles = null;
    [InspectorName("Pickup Sound")][SerializeField]
    [Tooltip("If no Audio is selected, nothing will play.")]
    private AudioClip _pickupSound = null;//@TODO allow this to be a list.
    [SerializeField][Range(0.0f,1.0f)]private float volume = 1.0f;
    [SerializeField][Range(0.1f,180.0f)]private float _tempFOV = 1.0f;
    [SerializeField][Range(0.1f,180.0f)]private float _currentFOV = 1.0f;

    [Header("Acceleration")]
    [SerializeField]
    [InspectorLabel("Amount Increased")]
    private float _speedIncreaseAmount = 20f;
    [SerializeField]
    [InspectorName("Powerup Duration")]
    private float _powerUpDur = 3f;
    [SerializeField][Tooltip("What physics method used to speed ship up.\nDefault is ForceMode.Force")]
    private ForceMode forceType = ForceMode.Force;


    private Collider _collider = null;
    private Camera _camera;
    private float t = 0.0f;

    //@TODO when invetory is full make useOnImpact true
    private void Start() {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other) {
        PlayerShip ship = other.GetComponent<PlayerShip>();
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if(ship != null ){
            if (_pickupSound != null) { AudioHelper.PlayClip2D(_pickupSound, volume); }

            _particles.Play();

            if(useOnImpact){
                StartCoroutine(PowerupSequence(ship));

            }
            else{ 
                if (inventory != null) { 
                    inventory.boostsCollected++;
                    StartCoroutine(PlayFXOnly());
                }
            }
        }
    }

    IEnumerator PowerupSequence(PlayerShip ship){
        _collider.enabled = false;
        _artToDisable.SetActive(false);

        ActivatePowerUp(ship);

        yield return new WaitForSeconds(_powerUpDur);

        DeactivatePowerUp(ship);

        Destroy(gameObject); //can change this to make it temp disabled for a time.
        Debug.Log("Speed Powerup finished");
    }

    IEnumerator PlayFXOnly(){
        _collider.enabled = false;
        _artToDisable.SetActive(false);

        yield return new WaitForSeconds(_powerUpDur);

        Destroy(gameObject); //can change this to make it temp disabled for a time.
        Debug.Log("Speed Powerup finished");
    }

    public IEnumerator UseBoost(PlayerShip ship){
        ActivatePowerUp(ship);

        yield return new WaitForSeconds(_powerUpDur);

        DeactivatePowerUp(ship);
        Debug.Log("Speed Powerup finished");
    }
    
    private void ActivatePowerUp(PlayerShip ship){
        //if (alterFOV) { Camera.main.fieldOfView = _tempFOV; }

        ship.SetMoveSpeed(_speedIncreaseAmount, forceType);
    }
    private void DeactivatePowerUp(PlayerShip ship) {
        ship.SetMoveSpeed(-_speedIncreaseAmount); //@INFO This functionality may be better for like a ring boost or tp kind of feel
    }
}
