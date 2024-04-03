using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinVolume : MonoBehaviour
{
    [SerializeField] ParticleSystem[] _particles = null;
    private void OnTriggerEnter(Collider other) {
        PlayerShip ship = other.GetComponent<PlayerShip>();

        if(ship!= null){
            ship.winGame = true; 
            ship.playGame = false;

        }

        for (int i =0; i < _particles.Length; i++) {
            _particles[i].Play();
        }
    }
}
