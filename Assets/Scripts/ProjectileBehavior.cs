using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
     enum WeaponType {pickleLasers, mayoMissile}

    [SerializeField] private WeaponType typeWeapon = WeaponType.pickleLasers; 
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float projectileSpeed, projectileLife;
    private PlayerShip _ship;

    void Start(){
        _ship = FindFirstObjectByType<PlayerShip>();
        Rigidbody _shipRB = _ship.GetComponent<Rigidbody>();
        _rb.velocity = _shipRB.velocity;

        _rb.AddForce( (_ship.transform.forward * projectileSpeed), ForceMode.Impulse);
        StartCoroutine(RemoveProjectile());

    }
    //@TODO make this interactable via collision or triggers and filter by layers

    IEnumerator RemoveProjectile(){
        yield return new WaitForSeconds(projectileLife);
        Debug.Log("ProjectileDestroyed");
        Destroy(this.gameObject);
    }
}
