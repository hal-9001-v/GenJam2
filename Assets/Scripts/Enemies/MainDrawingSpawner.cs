using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDrawingSpawner : MonoBehaviour
{
    [Header("Spawn Prefabs")]
    [SerializeField] private GameObject[] spawnablePrefabs;


    
    [Header("Spawn Data")]
    [SerializeField] public Vector2 spawnCenter;
    [SerializeField] float spawnRadius; 
    [SerializeField] float spawnAngle;
    [SerializeField] [Range(0, 5)] int time;

    [Header("Control Bool")]
    public bool canSpawn;

    private void Awake() {
        
        
    }
    
    private void Start() {
        canSpawn = true;
        time = 1;
    }

    private void Update() {
        Time.timeScale = (float) time;
        spawnCenter = (Vector2)transform.position;

        //Uncomment to change center to drawing pos
        if(!canSpawn) return;


        StartCoroutine(SpawnWait(4f));
        canSpawn = false;
    }

    private IEnumerator SpawnWait(float s){
        
        
        yield return new WaitForSeconds(s);
        SpawnInstance(spawnablePrefabs[Random.Range(0,3)]);
        canSpawn = true;

    }

    public void SpawnInstance(GameObject spawnable){

        float angle = GetSpawnAngle();
        float radius = GetSpawnRadius();
        Instantiate(spawnable, GetSpawnPos(angle, radius), this.transform.rotation);

    }

    private Vector2 GetSpawnPos(float angle, float radius){

      
        Vector2 spawnPos = new Vector2(Mathf.Sin((angle) * Mathf.Deg2Rad)*radius,
            Mathf.Cos((angle)* Mathf.Deg2Rad)*radius) + (Vector2)(transform.position);
        
        return spawnPos;

    }  

    private float GetSpawnAngle(){
        
        spawnAngle = Random.Range(0, 360);
        return spawnAngle;

    }

    private float GetSpawnRadius(){

        spawnRadius = 30f;
        return spawnRadius;

    }

    #region editor
    [ContextMenu("ForceSpawn")]
    public void forceSpawn(){

        SpawnInstance(spawnablePrefabs[Random.Range(0,3)]);

    }
    #endregion
   
}
