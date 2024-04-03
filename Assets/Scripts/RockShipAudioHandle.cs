using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockShipAudioHandle : MonoBehaviour
{
    [SerializeField] private AudioClip _audio = null;
    [SerializeField] private float volume = 0.5f;
    private void OnParticleCollision(GameObject other) {
       if (other.tag == "Player"){
            AudioHelper.PlayClip2D(_audio, volume);
        }
    }
}
