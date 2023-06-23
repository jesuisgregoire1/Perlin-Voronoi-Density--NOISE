using System.Collections.Generic;
using UnityEngine;

public static class MeshUtils
{
    public static int[,] voronoiMap = null;
    public static float fBM(float x,float y, int octaves)
    {
        float total = 0;
        float frequency = 1f;
        for (int i = 0; i < octaves; ++i)
        {
            total += Mathf.PerlinNoise(x * frequency, y * frequency);
            frequency *= 2;
        }
        return total / (float) octaves;
    }

    public static void GenerateVoronoi(int numDistricts, int width, int heigth)
    {
        voronoiMap = new int[width, heigth];
        Dictionary<Vector2Int, int> locations = new Dictionary<Vector2Int, int>();
        int i = 0;
        while(locations.Count < numDistricts)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, heigth);
            
            if (!locations.ContainsKey(new Vector2Int(x, y)))
            {
                locations.Add(new Vector2Int(x, y), i);
                i++;
            }
        }
        for (int y = 0; y < heigth; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float distance = Mathf.Infinity;
              
                foreach (var val in locations)
                {
                    float distTo = Vector2Int.Distance(val.Key, new Vector2Int(x, y));
                    if (distTo < distance)
                    {
                        voronoiMap[x,y] = val.Value;
                        distance = distTo;
                    }
                }
            }
        }
    }
}
