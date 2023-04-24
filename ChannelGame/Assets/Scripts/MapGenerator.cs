using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] chunks; // an array of pre-made chunk game objects

    public int chunkWidth = 10; // the width of each chunk
    public int chunkHeight = 10; // the height of each chunk
    public int mapWidth; // the number of chunks to generate horizontally
    public int mapHeight; // the number of chunks to generate vertically

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = -mapWidth / 2; x < mapWidth / 2; x++)
        {
            for (int y = -mapHeight / 2; y < mapHeight / 2; y++)
                {
                GameObject newChunk = Instantiate(chunks[Random.Range(0, chunks.Length)], new Vector3(x * chunkWidth, y * chunkHeight, 0), Quaternion.identity);
                newChunk.transform.parent = transform;
            }
        }
    }
}

