using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RBProjectileGun : MonoBehaviour
{
    [Header("Mechanics")]
    [SerializeField]private GameObject projectile = null;
    [SerializeField]private Transform _spawnLocation = null;

    [Header("Audio")]
    [SerializeField]private AudioClip _projectileSound = null;
    [SerializeField][Range(0,1)]private float volume = 1.0f;

    [Header("Muzzle Flash")]
    [SerializeField] private bool useMuzzleFlash = false;
    //[SerializeField]private Light _light = null;
    [SerializeField]private ParticleSystem _flashParticles = null;
    [SerializeField][Range (0,1)] private float _flashDuration = 0.5f;
    [SerializeField]Transform _flashLocation = null;

    [Header("Animation")][Tooltip("Currently does not do anything...")]
    [SerializeField]private Animator _projectileAnim = null;
    [SerializeField]string _triggerName = null;

    [Header("Input Type")]
    [SerializeField] bool _mouse = true;
    [SerializeField][Range(0, 3)] int _mouseButton = 0;
    [SerializeField] bool _keyboard = false;
    [SerializeField] string _key = null;

    enum WeaponType { pickleLasers, mayoMissile }

    [Header("Weapon Type")]
    [SerializeField] private WeaponType typeWeapon = WeaponType.pickleLasers;

    //@TODO this one's gonna need a Custom Editor

    private AudioSource tempAudio;
    // Update is called once per frame
    void Update()
    {
        //@TODO reference ShootingObject's velocity and add it to the force of the projectile
        if(_mouse && Input.GetMouseButtonDown(_mouseButton)){
            //@TODO add Ammo
            
            
            if(typeWeapon == WeaponType.pickleLasers) {
                Instantiate(projectile, _spawnLocation.position, _spawnLocation.rotation);
                playFX();
            
            }
            if(typeWeapon == WeaponType.mayoMissile) {
                PlayerInventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
                if (inventory != null && inventory.mayoCollected > 0) {
                    inventory.mayoCollected--;
                    playFX();
                    Instantiate(projectile, _spawnLocation.position, _spawnLocation.rotation);
                    Debug.Log("Mayo fired. Inventory Count = " + inventory.mayoCollected);  
                }
                else { }// Play empty Sound }

            }
        }
        //if(_keyboard){ } //Do Things @TODO
    }

    private void playFX(){
        if (_projectileAnim != null) {
            _projectileAnim.Play(_triggerName);
        }
        AudioHelper.PlayClip2D(_projectileSound, volume);
        if (useMuzzleFlash) { StartCoroutine(MuzzleFlash()); }
    }

    IEnumerator MuzzleFlash() { 
        ParticleSystem psStorage = Instantiate(_flashParticles, _flashLocation);
        Debug.Log("Created Muzzle Flash");
        //Light tempLight = Instantiate(_light, _flashLocation); //@INFO may not need light if you give the PS probes but still nice to have.
        yield return new WaitForSeconds(_flashDuration);
        Debug.Log("Removing Muzzle Flash");
        Destroy(psStorage);
        //Destroy(tempLight);
    }
}
