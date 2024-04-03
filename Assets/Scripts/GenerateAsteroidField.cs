using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenerateAsteroidField : MonoBehaviour
{
    [SerializeField] private GameObject[] AsteroidPrefab;
    [SerializeField] private Vector3 spawnRadius = Vector3.zero;
    [SerializeField] private int spawnCount = 10;

    [SerializeField]bool useDZ = false;
    [Tooltip("This should be smaller than the spawn radius")][SerializeField]
        private Vector3 deadzoneRadius = Vector3.zero;

    private Vector3 spawnZone;

    //@TODO randomize scale in b/w range. have bool option to trigger this
    // Start is called before the first frame update
    void Start(){

        
        
        for(int i = 0; i < spawnCount; i++) {
            int randAst = Random.Range(0, AsteroidPrefab.Length - 1);
            Debug.Log("Instantiating Asteroid count: " + randAst);
            spawnZone = Vector3.zero;
            
            if (!useDZ){
                float aX = Random.insideUnitSphere.x * spawnRadius.x;
                float ay = Random.insideUnitSphere.y * spawnRadius.y;
                float az = Random.insideUnitSphere.z * spawnRadius.z;
                spawnZone = new Vector3(aX, ay, az);
                Instantiate(AsteroidPrefab[randAst], spawnZone + transform.position, Quaternion.identity);
            }
            else {
                Debug.Log("DeadZone was Used");
                StartCoroutine(HandleDeadZone());
            }
            randAst = Random.Range(0, AsteroidPrefab.Length - 1);
        }
    }
    IEnumerator HandleDeadZone(){
        float srSAx = Random.insideUnitSphere.x * spawnRadius.x; //Surface area of Deadzone
        float srSAy = Random.insideUnitSphere.y * spawnRadius.y; //Surface area of Deadzone
        float srSAz = Random.insideUnitSphere.z * spawnRadius.z; //Surface area of Deadzone
        int randAst = Random.Range(0, AsteroidPrefab.Length - 1);

        Vector3 absSpawn = new Vector3(Mathf.Abs(srSAx), Mathf.Abs(srSAy), Mathf.Abs(srSAz));
        bool validLocation = false;
        bool xValid, yValid, zValid;

        float emergentTimeBreak = Time.time;

        while (!validLocation) {
            if (Mathf.Abs(srSAx) < deadzoneRadius.x) {
                srSAx = Random.insideUnitSphere.x * spawnRadius.x;
                xValid = false;
            }
            else{ xValid = true; }

            if (Mathf.Abs(srSAy) < deadzoneRadius.y) {
                srSAy = Random.insideUnitSphere.y * spawnRadius.y;
                yValid = false;
            }
            else{ yValid = true; }

            if (Mathf.Abs(srSAz) < deadzoneRadius.z) {
                srSAz = Random.insideUnitSphere.z * spawnRadius.z;
                zValid = false;
            }
            else { zValid = true; }

            if(xValid && yValid && zValid) {
                spawnZone = new Vector3(srSAx, srSAy, srSAz);
                validLocation = true;
                break;
            }

            if(emergentTimeBreak - Time.time > 20.0f){
                Debug.Log("Asteroid Spawner ran for longer than 20sec editor broken");
                Debug.Break();  
                break; 
            }
        }
                
        if(validLocation) { Instantiate(AsteroidPrefab[randAst], spawnZone + transform.position, Quaternion.identity); }
        



        yield return null;
    }
}
