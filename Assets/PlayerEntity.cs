using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    public EntityGenerator entityGenerator;
    public SpatialHashGrid grid;
    public SpatialHashing spatialHashing;

    public bool useHash;

    public float minDist;

    private void Start()
    {
        if (useHash)
            StartCoroutine(CheckWithHashingCoroutine());
        else
            StartCoroutine(CheckWithoutHashCoroutine());
    }

    public IEnumerator CheckWithoutHashCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            yield return null;
            entityGenerator.DisableEnabledEntities();
            int myCell = grid.HashFunction(new Vector2(transform.position.x, transform.position.y));
            CheckWhitoutHashing(myCell);
        }
    }

    public IEnumerator CheckWithHashingCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            yield return null;
            entityGenerator.DisableEnabledEntities();
            spatialHashing.SpatialHashUpdate();

            List<int> myCell = grid.HashFunction(new Vector2(transform.position.x, transform.position.y), minDist);
            CheckPlayerCell(myCell);
        }
    }

   
    private void CheckPlayerCell(List<int> myCell)
    {
        List<int> indexOfTheGridCells = new List<int>();
        
        foreach(int cell in myCell)
        {
            indexOfTheGridCells.Add(cell);
        }
        
        List<Entity> entitiesToCheck = new List<Entity>();

        foreach (int index in indexOfTheGridCells)
        {
            for (int i = spatialHashing.Initial[index]; i < spatialHashing.Final[index + 1]; i++)
            {
                entitiesToCheck.Add(spatialHashing.HashTable[i]);
            }
        }
        CheckEntityDistance(entitiesToCheck);
    }

    public void CheckEntityDistance(List<Entity> entities)
    {
        foreach (Entity e in entities)
        {
            if (Vector2.Distance(transform.position, e.transform.position) < minDist)
            {
                if (e.sprite.enabled == false)
                {
                    e.sprite.enabled = true;
                    entityGenerator.enabledEntities.Add(e);
                }
            }
        }
    }

    public void CheckWhitoutHashing(int cell)
    {
        foreach(Entity e in entityGenerator.entities)
        {
            if(Vector2.Distance(transform.position, e.transform.position) < minDist)
            {
                if (e.sprite.enabled == false)
                {
                    e.sprite.enabled = true;
                    entityGenerator.enabledEntities.Add(e);
                }
            }
        }
    }

}
