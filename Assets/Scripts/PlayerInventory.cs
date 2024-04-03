using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [HideInInspector] public int boostsCollected;
    [HideInInspector] public int mayoCollected;
    [HideInInspector] public int asteroidCount;
    [HideInInspector] public int possibleAsteroids;
    [HideInInspector] public float t;
    [HideInInspector] public int health;
    public int maxHealth;
    public SpeedPowerup speedPrefab = null;
    public MayoPowerUp mayoPrefab = null;

    private void Start() {
        boostsCollected = 0;
        mayoCollected = 0;
        asteroidCount = 0;
        possibleAsteroids = GameObject.FindGameObjectsWithTag("Boulder").Length;
        health = maxHealth;
        t = 0;
    }

    private void Update() {
        t += Time.smoothDeltaTime;
    }
}
