using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HazardVolume : MonoBehaviour
{
    //@IMPORTANT break up this script and rename everything that is not relevant
            //This script should on be a kill volume or damageVolume

    [SerializeField] bool killPlayer = true;
    [SerializeField] bool spawnItems = false;
    [SerializeField] GameObject itemPrefab;
    [Header("Audio")]
    [SerializeField] private AudioClip _colFx;
    [SerializeField] [Range(0.0f,1.0f)]private float volume = 1.0f;
    //@TODO make this a lsit

    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem[] _ParticleSystems;
    
    [Header("Art to disable")]
    [SerializeField] private GameObject _art;


    //@TODO setup GUIEditor to allow for trigger or collision mode and move all of this into an asteroid specific script. Maybe just have two separate?
        //@TODO def setup two separate ones. The Collision script for the asteroids should apply an impulse force from the ship. How do i determine mass....?

    //@TODO set this up to use prefabs so the partSys can be instantiated at location 
    //@TODO this may be better setup with 3D audio so i can make a doppler effect witth the sound settings
    //Also would allow for me to use reverb zones
    private void OnTriggerEnter(Collider other) {
        PlayerShip ship = other.GetComponent<PlayerShip>(); 
        ProjectileBehavior _projectile = other.GetComponent<ProjectileBehavior>();
        PlayerInventory playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        if(_projectile != null){

            AudioHelper.PlayClip2D(_colFx, volume);
            this.GetComponent<Collider>().enabled = false;
            playerInventory.asteroidCount++;
            //GameObject.Destroy(_projectile.TryGetComponent<GameObject>);
            if (_art != null) { _art.SetActive(false); } //dear lord this crusty ass code
            
            //@TODO instantaite randomized item spawn

            for(int i = 0; i < _ParticleSystems.Length; i++){
                if(!_ParticleSystems[i].isPlaying){_ParticleSystems[i].Play(); }
            }

            if(spawnItems){
                Instantiate(itemPrefab, this.transform, true);
            }

            Destroy(other.gameObject);
            StartCoroutine(WaitSecondsThenDestroy(4));

        }


        if (ship != null){
            if(killPlayer && !ship.invisible){ ship.Kill(); }
            AudioHelper.PlayClip2D(_colFx, volume);
            if(_art != null){_art.SetActive(false); } //dear lord this crusty ass code
            
            for (int i = 0; i < _ParticleSystems.Length; i++) {
                _ParticleSystems[i].Play();
            }
            StartCoroutine(WaitSecondsThenDestroy(4));
        }
    }

    private IEnumerator WaitSecondsThenDestroy(float t){ 
        yield return new WaitForSeconds(t);
        Destroy(this.gameObject);
    }
}

