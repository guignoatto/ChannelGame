using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] chunks; // an array of pre-made chunk game objects
    public GameObject specialChunk1; // the first special chunk
    public GameObject specialChunk2; // the second special chunk

    public int chunkWidth; // the width of each chunk
    public int chunkHeight; // the height of each chunk
    public int mapWidth; // the number of chunks to generate horizontally
    public int mapHeight; // the number of chunks to generate vertically

    private void Awake()
    {
  
    }
    void Start()
    {
        GenerateMap();
    }
    

    void GenerateMap()
    {
        Vector3 mapCenter = new Vector3((mapWidth - 1) * chunkWidth / 2f, (mapHeight - 1) * chunkHeight / 2f, 0);

        // Generate all the regular chunks
        for (int x = -mapWidth / 2; x < mapWidth / 2; x++)
        {
            for (int y = -mapHeight / 2; y < mapHeight / 2; y++)
            {
                GameObject newChunk = Instantiate(chunks[Random.Range(0, chunks.Length)], new Vector3(x * chunkWidth + mapWidth / 2 * chunkWidth, y * chunkHeight + mapHeight / 2 * chunkHeight, 0) - mapCenter, Quaternion.identity);
                newChunk.transform.parent = transform;
            }
        }

        // Generate the first special chunk at a random position
        int randomX = Random.Range(-mapWidth / 2, mapWidth / 2);
        int randomY = Random.Range(-mapHeight / 2, mapHeight / 2);
        GameObject newSpecialChunk1 = Instantiate(specialChunk1, new Vector3(randomX * chunkWidth + mapWidth / 2 * chunkWidth, randomY * chunkHeight + mapHeight / 2 * chunkHeight, 0) - mapCenter, Quaternion.identity);
        newSpecialChunk1.transform.parent = transform;

        // Generate the second special chunk at a different random position
        do
        {
            randomX = Random.Range(-mapWidth / 2, mapWidth / 2);
            randomY = Random.Range(-mapHeight / 2, mapHeight / 2);
        } while (randomX == newSpecialChunk1.transform.position.x / chunkWidth - mapWidth / 2 && randomY == newSpecialChunk1.transform.position.y / chunkHeight - mapHeight / 2);
        GameObject newSpecialChunk2 = Instantiate(specialChunk2, new Vector3(randomX * chunkWidth + mapWidth / 2 * chunkWidth, randomY * chunkHeight + mapHeight / 2 * chunkHeight, 0) - mapCenter, Quaternion.identity);
        newSpecialChunk2.transform.parent = transform;

       

    }
}


