using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialHashGrid : MonoBehaviour
{
    public int gridResolution;
    public float cellSize;

    public int HashFunction(Vector2 pos)
    {
        if (pos.x < 0 || pos.x > gridResolution * cellSize || pos.y < 0 || pos.y > gridResolution * cellSize)
            return -1;

        int i = (int)(pos.x / cellSize);
        int j = (int)(pos.y / cellSize);

        return (gridResolution * j) + i;
    }
    public List<int> HashFunction(Vector2 pos, float dist)
    {
        List<int> cells = new List<int>();

        float topX = pos.x - dist;
        float topY = pos.y + dist;

        float bottomX = pos.x + dist;
        float bottomY = pos.y - dist;

        for(float x = topX; x < bottomX + cellSize; x += cellSize)
        {
            for(float y = topY; y > bottomY - cellSize; y -= cellSize)
            {
                int index = HashFunction(new Vector2(x, y));
                if(index != -1)
                    cells.Add(index); 
            }
        }

        return cells;
    }

    private void OnDrawGizmos()
    {
        for(int i = 0; i < gridResolution + 1; i++)
        {
            //Horizontal draw
            Vector3 from = new Vector3(0f, i * cellSize, 0f);
            Vector3 to = new Vector3(cellSize * gridResolution, i * cellSize, 0f);
            Gizmos.DrawLine(from, to);

            //Vertical draw
            from = new Vector3(i * cellSize, 0f, 0f);
            to = new Vector3(i * cellSize, cellSize * gridResolution, 0f);
            Gizmos.DrawLine(from, to);
        }
    }
}
