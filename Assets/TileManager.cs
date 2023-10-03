using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public List<float> tileWeights;

    public Transform player;
    public float moveSpeed = 5.0f;
    public float tileLength = 5.0f; // Length of each tile.
    public int numTilesOnScreen = 20; // Number of tiles to keep on-screen.
    public float spawnInterval = 2.0f;
    private float deleteThreshold = -10.0f;
    public int maxActiveTiles = 20;

    private List<GameObject> activeTiles = new List<GameObject>();
    private float spawnZ = 0.0f;
    private float spawnTimer = 0.0f;

    private void Start()
    {
        for (int i = 0; i < numTilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    private void FixedUpdate()
    {
        MoveTiles();

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval && activeTiles.Count < maxActiveTiles)
        {
            SpawnTile();
            //DeleteTile();
            spawnTimer = 0.0f;
        }
    }

    private void SpawnTile()
    {
        /*
        int randomIndex = Random.Range(0, tilePrefabs.Length);
        GameObject tile = Instantiate(tilePrefabs[randomIndex], transform.forward * spawnZ, Quaternion.identity);
        activeTiles.Add(tile);
        spawnZ += tileLength;
        */

        
        int randomTileIndex = ChooseRandomTileType(); // Get the randomly chosen tile type.
        GameObject tile = Instantiate(tilePrefabs[randomTileIndex], transform.forward * spawnZ, Quaternion.identity);
        activeTiles.Add(tile);
        spawnZ += tileLength;
        
    }

   
    private int ChooseRandomTileType()
    {
        float totalWeight = 0;
        for (int i = 0; i < tileWeights.Count; i++)
        {
            totalWeight += tileWeights[i];
        }

        float randomValue = Random.Range(0, totalWeight);
        float cumulativeWeight = 0;

        for (int i = 0; i < tileWeights.Count; i++)
        {
            cumulativeWeight += tileWeights[i];
            if(randomValue <= cumulativeWeight)
            {
                return i;
            }
        }

        return tileWeights.Count - 1;
    }
   

    private void MoveTiles()
    {
        for (int i = activeTiles.Count - 1; i >= 0; i--)
        {
            GameObject tile = activeTiles[i];
            tile.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            if (tile.transform.position.x < deleteThreshold)
            {
                Destroy(tile);
                activeTiles.RemoveAt(i);
            }
        }
    }
}
