using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EngineAudioHandler : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource = null;

    [Header("Pitch")]
    [SerializeField][Range(0f, 5f)] private float _minPitch = 0.0f;
    [SerializeField][Range(0f, 5f)] private float _maxPitch = 1.0f;
    [SerializeField][Range(0f, 6f)] private float _maxBoostPitch = 5.0f;

    //[Header("Volume")]
    //[SerializeField][Range(0.0f, 1.0f)] private float _minVolume = 0.0f;
    //[SerializeField][Range(0.0f, 1.0f)] private float _maxVolume = 1.0f;
    //[SerializeField][Range(0, 20)] private int _volumeModAmt = 0;

    //private AudioSource _audioSource;
    private PlayerShip _playerShip;
    private Rigidbody _rb;
    private float initialPitch, initialVolume;

    private void Awake() {
        initialPitch = _audioSource.pitch;
        initialVolume = _audioSource.volume;


        _playerShip = GetComponent<PlayerShip>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        float percentSpeed = Mathf.InverseLerp(0.0f, _playerShip._moveSpeed, Mathf.Abs(_rb.velocity.magnitude));

        percentSpeed *= (10 * _maxPitch);
        // value is 300 because Lerp needs to be mult by 100 for percentage and then 3 to adjust for pitch max
        // Debug.Log("PercentSpeed = " +  percentSpeed);

        _audioSource.pitch = Mathf.Clamp(_audioSource.pitch, _minPitch, _maxBoostPitch);
        _audioSource.pitch = Mathf.MoveTowards(_audioSource.pitch, percentSpeed, Time.deltaTime);
    }

}
