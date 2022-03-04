using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialHashing : MonoBehaviour
{
    public SpatialHashGrid grid;
    public EntityGenerator entityGenerator;

    [Header("SpatialHash Variables")]
    private int index;
    private int[] used;
    private int[] initial;
    private int[] final;
    private int[] objectIndex;
    private Entity[] hashTable;

    public int Index { get => index; }
    public int[] Used { get => used;  }
    public int[] Initial { get => initial;  }
    public int[] Final { get => final;}
    public int[] ObjectIndex { get => objectIndex; }
    public Entity[] HashTable { get => hashTable; }

    private void Start()
    {
        int cellQuantity = grid.gridResolution * grid.gridResolution;
        used = new int[cellQuantity];
        initial = new int[cellQuantity];
        final = new int[cellQuantity + 1];

        objectIndex = new int[entityGenerator.entityQuantity];
        hashTable = new Entity[entityGenerator.entityQuantity];
    }
    public void SpatialHashUpdate()
    {
        for (int i = 0; i < (grid.gridResolution * grid.gridResolution); i++)
            used[i] = 0;
  
        index = 0;

        for (int e = 0; e < entityGenerator.entities.Count; e++)
        {
            index = grid.HashFunction(entityGenerator.entities[e].transform.position);
            ObjectIndex[e] = index;
            used[index]++;
        }

        int accum = 0;
        for (int e = 0; e < Used.Length; e++)
        {
            initial[e] = accum;
            accum += Used[e];
            final[e] = accum;
        }

        final[Used.Length] = entityGenerator.entityQuantity;
        for (int e = 0; e < entityGenerator.entities.Count; e++)
        {
            hashTable[final[objectIndex[e]] - 1] = entityGenerator.entities[e];
            final[objectIndex[e]] -= 1;
        }
    }
}
