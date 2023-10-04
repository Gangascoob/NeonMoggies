using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public List<float> tileWeights;

    public Transform player;
    public float forceMagnitude = 100.0f; // The force magnitude applied to the tiles.
    public float tileLength = 5.0f; // Length of each tile.
    public int numTilesOnScreen = 20; // Number of tiles to keep on-screen.
    public float spawnInterval = 2.0f;
    private float deleteThreshold = -10.0f;
    public int maxActiveTiles = 20;

    private List<Rigidbody> tileRigidbodies = new List<Rigidbody>();
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
        if (spawnTimer >= spawnInterval && tileRigidbodies.Count < maxActiveTiles)
        {
            SpawnTile();
            // DeleteTile();
            spawnTimer = 0.0f;
        }

        ApplyForceToTiles();
    }

    private void SpawnTile()
    {
        int randomTileIndex = ChooseRandomTileType(); // Get the randomly chosen tile type.
        GameObject tileObject = Instantiate(tilePrefabs[randomTileIndex], transform.forward * spawnZ, Quaternion.identity);
        Rigidbody tileRigidbody = tileObject.GetComponent<Rigidbody>();

        if (tileRigidbody != null)
        {
            tileRigidbodies.Add(tileRigidbody);
            
        }

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
            if (randomValue <= cumulativeWeight)
            {
                return i;
            }
        }

        return tileWeights.Count - 1;
    }

    private void ApplyForceToTiles()
    {
        foreach (Rigidbody tileRigidbody in tileRigidbodies)
        {
            tileRigidbody.AddForce(Vector3.left * forceMagnitude, ForceMode.Force);
        }
    }


    private void MoveTiles()
    {
        for (int i = tileRigidbodies.Count - 1; i >= 0; i--)
        {
            Rigidbody tileRigidbody = tileRigidbodies[i];

            if (tileRigidbody.transform.position.x < deleteThreshold)
            {
                Destroy(tileRigidbody.gameObject);
                tileRigidbodies.RemoveAt(i);
            }
        }
    }
}
