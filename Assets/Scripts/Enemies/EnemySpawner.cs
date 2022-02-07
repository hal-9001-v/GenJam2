using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Prefabs")]
    [SerializeField] private GameObject[] spawnablePrefab;

    [Header("Spawn Data")]
    [SerializeField] public Vector2 spawnCenter;
    [SerializeField] float spawnRadius;
    [SerializeField] float spawnAngle;
    [SerializeField] [Range(0, 5)] float spawnTime;

    [Header("Control Bool")]
    public bool canSpawn;

    EndGame _endGame;

    float elapsedTime = 0;

    private void Awake()
    {
        _endGame = FindObjectOfType<EndGame>();

        _endGame.endGameCallback += StopSpawn;
        _endGame.retryCallback += ResumeSpawn;
    }

    private void Start()
    {
        canSpawn = true;
    }

    private void Update()
    {
        if (!canSpawn) return;

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= spawnTime)
        {
            elapsedTime = 0;

            spawnCenter = (Vector2)transform.position;
            SpawnEnemy();
        }

    }

    public void StopSpawn()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            enemy.Die();
        }

        canSpawn = false;

    }

    public void ResumeSpawn()
    {
        elapsedTime = 0;
        canSpawn = true;
    }

    public void SpawnEnemy()
    {
        float angle = GetSpawnAngle();
        float radius = GetSpawnRadius();
        Instantiate(spawnablePrefab[Random.Range(0,3)], GetSpawnPos(angle, radius), this.transform.rotation);
    }

    private Vector2 GetSpawnPos(float angle, float radius)
    {


        Vector2 spawnPos = new Vector2(Mathf.Sin((angle) * Mathf.Deg2Rad) * radius,
            Mathf.Cos((angle) * Mathf.Deg2Rad) * radius) + (Vector2)(transform.position);

        return spawnPos;

    }
    
    private float GetSpawnAngle()
    {

        spawnAngle = Random.Range(0, 360);
        return spawnAngle;
    
    }
    
    private float GetSpawnRadius()
    {

        spawnRadius = 30f;
        return spawnRadius;

    }

    #region editor
    [ContextMenu("ForceSpawn")]
    public void forceSpawn()
    {

        SpawnEnemy();

    }
    #endregion

}
