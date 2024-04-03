using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject ship = null;
    [SerializeField] Transform bufferPos = null;

    [SerializeField] float followSpeed = 1.0f;
    [SerializeField] float rotateSpeed = 0.0f;
    
    [Tooltip("Furthest Distance the Camera can lag behind")][SerializeField] 
    [Range(0,5)]float forwardDistanceBuffer = 3.0f;

    public bool playerCamera = false;

    private Transform _posToFollow;
    private float velStorage = 0.0f;
    private Vector3 v3VelStorage = Vector3.zero;
    private float angStorage = 0.0f;
    private float dist = 0.0f;
    private float latAccel = 0.0f;
    private float angAccel = 0.0f;

    void Awake(){
        _posToFollow = ship.transform;
        //_objOffset = this.transform.position - _posToFollow.position;
        transform.position = _posToFollow.position;
        transform.LookAt(ship.transform);
    }

    private void RotateCamera(){
        float step = rotateSpeed * Time.deltaTime;

        Rigidbody _rb = ship.GetComponent<Rigidbody>();
        float gForce = (_rb.angularVelocity.magnitude - angStorage) / step;
        angStorage = _rb.angularVelocity.magnitude;

        angAccel = Mathf.Lerp(angAccel, gForce * 3.5f, step);
        Vector3 lookDir = ship.transform.position - transform.forward;
        Vector3 correctedUp = Vector3.SmoothDamp( Vector3.up, ship.transform.up, ref v3VelStorage, step);
        
        if(!playerCamera){
            Quaternion targetRot = Quaternion.LookRotation(ship.transform.forward, correctedUp);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, step);
            //transform.LookAt(_posToFollow.transform, Vector3.up);

        }
        else {
            //Quaternion targetRot = Quaternion.LookRotation(ship.transform.forward, ship.transform.up);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, step);
            transform.LookAt(_posToFollow.transform, Vector3.up);
        }
        //Quaternion targetRot =  Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(ship.transform.forward, ship.transform.up), step);
        //transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(-angAccel, angAccel, angAccel), step);
        //float angBuffer = 4; //this is a temp fill in if it works then move to serialized @TODO
        //float rotDist = Mathf.Pow(Quaternion.Angle(transform.rotation, ship.transform.localRotation), angBuffer * Time.deltaTime);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, ship.transform.localRotation, rotDist * Time.deltaTime); //_posToFollow.rotaiton is probably wrong


        //Quaternion targetRot = Quaternion.LookRotation(transform.forward, lookDir);
        //Quaternion targetRot = Quaternion.LookRotation(transform.forward, lookDir);

        //transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(-angAccel, 0,0), step);

    }
    //-2.829 = Parenty

    //-14 = childX
    private void MoveCamera(){
        float step = followSpeed * Time.deltaTime;

        Rigidbody _rb = ship.GetComponent<Rigidbody>();
        float gForce = (_rb.velocity.magnitude - velStorage) / step;
        velStorage = _rb.velocity.magnitude;
        v3VelStorage = _rb.velocity;

        Vector3 newCamPos = _posToFollow.position - (_posToFollow.forward * 2.0f);
        latAccel = Mathf.Lerp(latAccel, gForce * 3.5f, step);

        transform.position = Vector3.MoveTowards(this.transform.position, bufferPos.position, step);
        dist = Mathf.Pow(Vector3.Distance(this.transform.position, newCamPos), forwardDistanceBuffer * Time.deltaTime);
        transform.position = Vector3.MoveTowards(this.transform.position, newCamPos, dist * Time.deltaTime);

    }
    // Update is called once per frame
    private void LateUpdate(){
        Cursor.lockState = CursorLockMode.Locked;

        _posToFollow = ship.transform;
        MoveCamera();
        RotateCamera();

        //this.transform.LookAt(_posToFollow, ship.transform.up);

        //this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, _posToFollow.rotation, angleMultiplier * Time.deltaTime);
        //this.transform.position = Vector3.MoveTowards(this.transform.position, _posToFollow.position, step);

    }
}
