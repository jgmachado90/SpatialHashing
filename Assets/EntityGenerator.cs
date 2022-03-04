using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGenerator : MonoBehaviour
{
    public SpatialHashGrid grid;

    public List<Entity> entities;
    public List<Entity> enabledEntities;

    [Range(5, 200000)]
    public int entityQuantity;

    public Entity entityPrefab;



    private void Start()
    {
        GenerateEntities();
    }

    public void DisableEnabledEntities()
    {
        foreach(Entity e in enabledEntities)
        {
            e.sprite.enabled = false;
        }
        enabledEntities.Clear();
    }

    private void GenerateEntities()
    {
        for (int i = 0; i < entityQuantity; i++)
        {
            float posX = Random.Range(0f, grid.gridResolution * grid.cellSize);
            float posY = Random.Range(0, grid.gridResolution * grid.cellSize);

            Vector3 position = new Vector3(posX, posY, 0);

            Entity newEntity = Instantiate(entityPrefab, position, Quaternion.identity, transform);

            newEntity.sprite.enabled = false; 

            entities.Add(newEntity);
        }
    }
}
