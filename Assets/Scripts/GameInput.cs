using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInput : MonoBehaviour
{   
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private GameObject playerCameraParent = null;
    [SerializeField] private GameObject winCamera = null;
    [HideInInspector]public bool gameActive = true;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update() {
        Cursor.lockState = CursorLockMode.Locked;

        if (Input.GetKeyDown(KeyCode.Backspace)){
            ReloadLevel();
            Debug.Log("Level Reloaded");

        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
            Debug.Log("Application Quit");
            //@TODO make a menu and tie it to this
        }
        if (Input.GetKeyDown(KeyCode.Space)) {

            //PlayerInventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

            if(inventory != null && inventory.boostsCollected > 0) {
                 PlayerShip ship = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShip>();

                inventory.boostsCollected--;
                StartCoroutine(inventory.speedPrefab.UseBoost(ship));
            }
        }
    }

    private void ReloadLevel() {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }
}
