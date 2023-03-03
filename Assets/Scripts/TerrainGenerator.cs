using System.Collections.Generic;
using UnityEngine;


public class TerrainGenerator : MonoBehaviour
{
    [SerializeField] private GameObject terrainPrefab;
    [SerializeField] private float chunkSize;
    [SerializeField] private Transform player;
    [SerializeField] private float chunkUpdateThreshold;
    [SerializeField] private int renderDistance;
    
    private List<GameObject> _instantiatedChunks;
    private Vector2 _chunkCenter;

    private void Start()
    {
        _instantiatedChunks = new List<GameObject>();
        _chunkCenter = new Vector2(player.position.x + chunkUpdateThreshold, player.position.z);
    }

    private void Update()
    {
        if ((new Vector2(player.position.x, player.position.z) - _chunkCenter).magnitude < chunkUpdateThreshold)
            return;

        foreach (GameObject chunk in _instantiatedChunks)
        {
            Destroy(chunk);
        }

        _chunkCenter = new Vector2(player.position.x, player.position.z);
        
        for (int i = 0; i < renderDistance * 2; i++)
        {
            for (int j = 0; j < renderDistance * 2; j++)
            {
                GameObject instantiatedTerrain = Instantiate(terrainPrefab, transform);
                instantiatedTerrain.transform.position = new Vector3((i - renderDistance) * chunkSize * 2, 0, 
                    (j - renderDistance) * chunkSize * 2) + new Vector3(player.position.x, 0, player.position.z);
                instantiatedTerrain.transform.localScale = new Vector3(chunkSize, 1, chunkSize);
                instantiatedTerrain.GetComponent<MeshFilter>().mesh.bounds = new Bounds(
                    instantiatedTerrain.transform.position,
                    new Vector3(100000000, 100000000, 100000000));
                _instantiatedChunks.Add(instantiatedTerrain);
            }
        }
    }
}


