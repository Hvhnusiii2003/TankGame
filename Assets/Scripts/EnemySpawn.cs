using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [Header("Tilemap Settings")]
    public Tilemap tilemap;

    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public int enemiesPerSpawn = 2;
    public int maxEnemiesOnMap = 20;

    [Header("Player Settings")]
    public Transform player;
    public float minimumDistance = 10f;

    [Header("Spawn Timing")]
    public float spawnInterval = 5f;

    [Header("Audio Settings")]
    public AudioClip enemyDeathSound;

    private List<Vector3> validPositions = new List<Vector3>();
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        CacheValidPositions();
        StartCoroutine(SpawnEnemiesRepeatedly());
    }

    void CacheValidPositions()
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (var position in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(position))
            {
                Vector3 worldPosition = tilemap.GetCellCenterWorld(position);
                validPositions.Add(worldPosition);
            }
        }
    }

    IEnumerator SpawnEnemiesRepeatedly()
    {
        while (true)
        {
            if (activeEnemies.Count < maxEnemiesOnMap)
            {
                for (int i = 0; i < enemiesPerSpawn; i++)
                {
                    Vector3 spawnPosition;
                    do
                    {
                        spawnPosition = validPositions[Random.Range(0, validPositions.Count)];
                    } while (Vector3.Distance(spawnPosition, player.position) < minimumDistance);

                    GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                    activeEnemies.Add(newEnemy);
                }
            }

            activeEnemies.RemoveAll(enemy => enemy == null);

            GameManager.instance.ShouldIncreaseSpawn(ref enemiesPerSpawn, ref spawnInterval);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
