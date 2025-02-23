using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 RotationSpeeds = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotationSpeeds * Time.deltaTime);       
    }
}
