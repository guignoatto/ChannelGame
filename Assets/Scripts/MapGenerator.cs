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

    private List<Vector2Int> occupiedPositions; // list of occupied positions by special chunks

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        Vector3 mapCenter = new Vector3((mapWidth - 1) * chunkWidth / 2f, (mapHeight - 1) * chunkHeight / 2f, 0);

        occupiedPositions = new List<Vector2Int>();

        // Generate the special chunks first
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
            while (IsPositionOccupied(randomX, randomY));

            GameObject instantiatedSpecialChunk = Instantiate(newSpecialChunk, new Vector3(randomX * chunkWidth, randomY * chunkHeight, 0) - mapCenter, Quaternion.identity);
            instantiatedSpecialChunk.transform.parent = transform;

            // Mark the position of the special chunk as occupied
            occupiedPositions.Add(new Vector2Int(randomX, randomY));
        }

        // Generate the regular chunks, skipping the occupied positions
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (!IsPositionOccupied(x, y))
                {
                    GameObject newChunk = Instantiate(chunks[Random.Range(0, chunks.Length)], new Vector3(x * chunkWidth, y * chunkHeight, 0) - mapCenter, Quaternion.identity);
                    newChunk.transform.parent = transform;
                }
            }
        }
    }

    bool IsPositionOccupied(int x, int y)
    {
        foreach (Vector2Int position in occupiedPositions)
        {
            if (position.x == x && position.y == y)
                return true;
        }
        return false;
    }
}
