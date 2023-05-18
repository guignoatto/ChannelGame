using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] chunks; // an array of pre-made chunk game objects
    public GameObject[] specialChunks; // an array of special chunk game objects

    public int chunkWidth = 10; // the width of each chunk
    public int chunkHeight = 10; // the height of each chunk
    public int mapWidth = 6; // the number of chunks to generate horizontally
    public int mapHeight = 6; // the number of chunks to generate vertically
    public int numSpecialChunks = 2; // the number of special chunks to generate

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        Vector3 mapCenter = new Vector3((mapWidth - 1) * chunkWidth / 2f, (mapHeight - 1) * chunkHeight / 2f, 0);

        // Generate all the regular chunks
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                GameObject newChunk = Instantiate(chunks[Random.Range(0, chunks.Length)], new Vector3(x * chunkWidth, y * chunkHeight, 0) - mapCenter, Quaternion.identity);
                newChunk.transform.parent = transform;
            }
        }

        // Generate the special chunks
        for (int i = 0; i < numSpecialChunks; i++)
        {
            int randomX;
            int randomY;
            GameObject newSpecialChunk;

            do
            {
                randomX = Random.Range(0, mapWidth);
                randomY = Random.Range(0, mapHeight);

                newSpecialChunk = specialChunks[Random.Range(0, specialChunks.Length)];
            }
            while (IsChunkSpecial(newSpecialChunk));

            GameObject instantiatedSpecialChunk = Instantiate(newSpecialChunk, new Vector3(randomX * chunkWidth, randomY * chunkHeight, 0) - mapCenter, Quaternion.identity);
            instantiatedSpecialChunk.transform.parent = transform;
        }
    }

    bool IsChunkSpecial(GameObject chunk)
    {
        GameObject[] specialChunks = GameObject.FindGameObjectsWithTag("SpecialChunk");
        foreach (GameObject specialChunk in specialChunks)
        {
            if (specialChunk.name == chunk.name)
                return true;
        }
        return false;
    }
}

