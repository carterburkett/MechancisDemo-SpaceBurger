using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateRandomMovement : MonoBehaviour
{

    [SerializeField] private Vector3 minRotationSpeed = Vector3.zero;
    [SerializeField] private Vector3 maxRotationSpeed = Vector3.zero;
    [SerializeField] private float minVelocity, maxVelocity;

    [SerializeField][Tooltip("Targets the Player on start and sets the movement in that direction")] private bool targetPlayer = false;
    [SerializeField][Tooltip("Asteroids will chase Player as game goes on")] private bool chasePlayer = false;
    [SerializeField] int chaseTriggerDistance;
    //[SerializeField] private PlayerShip _ship = null;
    
    private Vector3 angularOut;
    private float dirVelocity;
    void Start()
    {
        angularOut.x = Random.Range(minRotationSpeed.x, maxRotationSpeed.x);
        angularOut.y = Random.Range(minRotationSpeed.y, maxRotationSpeed.y);
        angularOut.z = Random.Range(minRotationSpeed.z, maxRotationSpeed.z);
        

        dirVelocity = Random.Range(minVelocity, maxVelocity);

        Rigidbody rb = GetComponent<Rigidbody>();
        //rb.AddForce(transform.forward *  dirVelocity, ForceMode.Impulse);

        if(!targetPlayer) {
            rb.AddForce(Random.onUnitSphere *  dirVelocity, ForceMode.Impulse);        
        }
        else if(targetPlayer){
            GameObject _player = GameObject.FindGameObjectWithTag("Player");
            rb.AddForce(_player.transform.position * dirVelocity, ForceMode.Impulse); //This Might be better in update with a force.foce type
                    
        }
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        GameObject _player = GameObject.FindGameObjectWithTag("Player");

        if (chasePlayer && _player != null) {
            PlayerShip ship = _player.GetComponent<PlayerShip>();
            if (this.GetComponentInChildren<Renderer>().isVisible && !ship.invisible){ 
                if(Vector3.Distance(this.transform.position, _player.transform.position) < chaseTriggerDistance){
                     transform.position = Vector3.MoveTowards(this.transform.position, _player.transform.position, dirVelocity * Time.deltaTime);
                }
            }

        }

        transform.Rotate(angularOut * Time.deltaTime);
    }
}
