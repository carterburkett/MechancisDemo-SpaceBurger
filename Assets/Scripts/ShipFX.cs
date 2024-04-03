using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipFX : MonoBehaviour
{
    [Header("AUDIO")]
    [SerializeField] AudioSource _source=null;
    [SerializeField] private PlayerShip _ship = null;

    [Header("Pitch")]
    [SerializeField][Range(-3f,3f)] private float _minPitch = 0.0f;
    [SerializeField][Range(-3f,3f)] private float _maxPitch = 1.0f;
    [SerializeField][Range(0, 20)] private int _pitchModAmt = 0;

    [Header("Volume")]
    [SerializeField][Range(0.0f, 1.0f)] private float _minVolume = 0.0f;
    [SerializeField][Range(0.0f, 1.0f)] private float _maxVolume = 1.0f;
    [SerializeField][Range(0, 20)] private int _volumeModAmt = 0;

    private void Awake() {

    }
}
