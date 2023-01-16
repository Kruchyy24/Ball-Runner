using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    [SerializeField] private GameObject spikePrefab;
    [SerializeField] private GameObject diamondPrefab;

    public GameObject test;
    
    public List<float> spawnPointsX;

    private void Awake()
    {
        spawnPointsX = new List<float>();
        AddSpawnPointsXToList();
        
        GenerateSpikes();
        GenerateDiamonds();
    }

    private void GenerateSpikes()
    {
        // z= 0.3, 0.0, -0.3
        // x= 0.450, 0.300, 0.150, 0.0, -0.150, 0.300 ,-0.450
        // y= 1
        
        List<float> spikesPosZ = new List<float>();
        spikesPosZ.Add(0.3f);
        spikesPosZ.Add(0f);
        spikesPosZ.Add(-0.3f);

        foreach (var posZ in spikesPosZ)
        {
            int randomSpikesNumber = UnityEngine.Random.Range(1,6);
            while (randomSpikesNumber != 0)
            {
                GameObject spikeGameObject = Instantiate(spikePrefab, transform, true);
                float randomSpikesPosX = spawnPointsX[Random.Range(0,spawnPointsX.Count)];

                spikeGameObject.transform.localPosition = new Vector3(randomSpikesPosX, 1f, posZ);
                
                randomSpikesNumber--;
            }
        }
    }

    private void GenerateDiamonds()
    {
        List<float> diamondsPosZ = new List<float>();
        diamondsPosZ.Add(0.5f);
        diamondsPosZ.Add(-0.5f);

        foreach (var posZ in diamondsPosZ)
        {
            GameObject diamondGameObject = Instantiate(diamondPrefab, transform, true);
            float randomDiamondsPosX = spawnPointsX[Random.Range(0,spawnPointsX.Count)];
            
            diamondGameObject.transform.localPosition = new Vector3(randomDiamondsPosX, 1.5f, posZ);
        }
    }

    private void AddSpawnPointsXToList()
    {
        spawnPointsX.Add(0.45f);
        spawnPointsX.Add(0.3f);
        spawnPointsX.Add(0.15f);
        spawnPointsX.Add(0f);
        spawnPointsX.Add(-0.15f);
        spawnPointsX.Add(-0.3f);
        spawnPointsX.Add(-0.45f);
    }
}
