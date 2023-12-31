using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlotVoronoi : MonoBehaviour
{
    [Range(1, 10)] public int locationCount = 5;

    private void OnValidate()
    {
        Texture2D texture = new Texture2D(1024, 1024);
        GetComponent<Renderer>().sharedMaterial.mainTexture = texture;

        Dictionary<Vector2Int, Color> locations = new Dictionary<Vector2Int, Color>();

        while(locations.Count < locationCount)
        {
            int x = Random.Range(0, texture.width);
            int y = Random.Range(0, texture.height);
            Color col = new Color(
                Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f));
            if (!locations.ContainsKey(new Vector2Int(x, y)))
            {
                locations.Add(new Vector2Int(x, y), col);
                //texture.SetPixel(x, y, Color.black);
            }
        }
        Color color = Color.white;
        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                float distance = Mathf.Infinity;
                color = Color.white;
                foreach (var val in locations)
                {
                    float distTo = Vector2Int.Distance(val.Key, new Vector2Int(x, y));
                    if (distTo < distance)
                    {
                        color = val.Value;
                        distance = distTo;
                    }
                }
                texture.SetPixel(x,y,color);
            }
        }

        texture.Apply();
    }
}
