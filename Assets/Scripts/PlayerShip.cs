using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{
    [SerializeField] public float _moveSpeed = 1f;
    [SerializeField] float _mouseSens = .5f;
    [SerializeField] float _rollSpeed = .05f;

    [SerializeField] public GameObject _baseModel;
    [SerializeField] public GameObject _swapModel;

    [HideInInspector]private float _speedMod = 0.0f;

    [HideInInspector]public bool invisible = false;
    [HideInInspector]public bool isDead = false;
    [HideInInspector]public bool winGame = false;
    Rigidbody _rb = null;

    [HideInInspector]public bool playGame = false;
    private float t = 0.0f;
    private bool boostUsed = false;
    private float fovStorage;
    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        transform.LookAt(Vector3.forward);
        fovStorage = Camera.main.fieldOfView;
    }

    public float GetSpeedMod(){ 
        return _speedMod;
    }

    private void FOVBySpeed(){
        Camera[] cam = GameObject.FindObjectsByType<Camera>(FindObjectsSortMode.None);

        for(int i = 0; i < cam.Length; i++){
            if(_speedMod > 0.0f) {
                t += Time.deltaTime / 10.0f;
                cam[i].fieldOfView = Mathf.Lerp(cam[i].fieldOfView, fovStorage + 10, t);
                //Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fovStorage + 5, t);
                boostUsed = true;
            }

            if(_speedMod <= 0.0f) {
                t += Time.deltaTime / 10.0f;
                cam[i].fieldOfView = Mathf.Lerp(cam[i].fieldOfView, fovStorage, t);
                //Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fovStorage, t);

                Debug.Log("Decreasing FOV");
            }
        
            if(t >= 1.0){ t = 0.0f; }
        }
    }

    void MoveShip(){ 
        float moveAmountThisFrame = Input.GetAxisRaw("Vertical") * _moveSpeed * Time.deltaTime;
        Vector3 moveDirection = transform.forward * moveAmountThisFrame;
        _rb.AddForce(moveDirection);
    }

    void TurnShip(){
        float turnAmountThisFrame = Input.GetAxisRaw("Horizontal") * _mouseSens * Time.deltaTime;
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame,0);
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }

    public void Kill(){
        isDead = true;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameInput>().gameActive = false;
        Debug.Log("Player has been killed!");
        playGame = false;
        invisible = true;
        this.gameObject.SetActive(false);
    }

    void ControlPlayerShip(){
        _rb.AddForce(_rb.transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * (_moveSpeed + _speedMod), ForceMode.Force); //Fwd Back
        //_rb.AddForce(_rb.transform.TransformDirection(Vector3.right) * Input.GetAxis("Roll") * -(_moveSpeed + _speedMod), ForceMode.Force); //Left Right Strafe
        _rb.AddTorque(_rb.transform.forward * -_rollSpeed * Input.GetAxis("Horizontal"), ForceMode.Acceleration); //Barrel Roll
        _rb.AddTorque(_rb.transform.TransformDirection(Vector3.right) * -_rollSpeed * 0.5f * Input.GetAxis("Roll"), ForceMode.Force); //Barrel Roll

        _rb.AddTorque(_rb.transform.right * _mouseSens * Input.GetAxis("Mouse Y") * -1, ForceMode.VelocityChange); //Mouse Input
        _rb.AddTorque(_rb.transform.up * _mouseSens * Input.GetAxis("Mouse X"), ForceMode.VelocityChange);
    }


    public void SetMoveSpeed(float _moveSpeedMod, ForceMode typeForce = ForceMode.Force) {
        if (typeForce == ForceMode.Impulse) {
            float temp = _moveSpeedMod + _moveSpeed;
            
            _rb.AddForce(_rb.transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * temp, typeForce); //Fwd Back

            //_speedMod += _moveSpeedMod;
            //Debug.Log("Impulse force of " + _moveSpeedMod + " applied to player.");
        }
        _speedMod += _moveSpeedMod;
        Debug.Log("Player Speed Modified by " + _moveSpeedMod); 
    }

    private void FixedUpdate() {
        //MoveShip();
        //TurnShip();
        Cursor.lockState = CursorLockMode.Confined;

        if(playGame){
            ControlPlayerShip();
            FOVBySpeed();
        }

    }

}
