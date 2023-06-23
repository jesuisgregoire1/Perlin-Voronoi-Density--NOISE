using System;
using UnityEngine;

[ExecuteInEditMode]
public class PopulationDesity : MonoBehaviour
{

    [Range(1, 8)] public int octaves = 2;
  
    [Range(0.001f,0.01f)]public float xScale = 0.003f;
    [Range(0.001f,0.01f)]public float yScale = 0.003f;
  
    [Range(0, 1000)]public int xOffset = 0;
    [Range(0,1000)]public int yOffset = 0;

    [Range(0.0f, 1.0f)] public float lowCutoff = 0;
    [Range(0.0f, 1.0f)] public float mediumLowCutoff = 0;
    [Range(0.0f, 1.0f)] public float mediumCutoff = 0;
    [Range(0.0f, 1.0f)] public float mediumHighCutoff = 0;
    [Range(0.0f, 1.0f)] public float highCutoff = 0;
    private void OnValidate()
    {
        Texture2D texture2d = new Texture2D(1024, 1024);
        GetComponent<Renderer>().sharedMaterial.mainTexture = texture2d;

        float perlin;
        Color color = Color.white; 

        for (int y = 0; y < texture2d.height; y++)
        {
            for (int x = 0; x < texture2d.width; x++)
            {
                perlin = fBM((x+xOffset) * xScale, (y+yOffset) * yScale,octaves);
            
                if (perlin < lowCutoff) color = new Color(0,0,0);
                else if (perlin < mediumLowCutoff) color = new Color(0.4f,0.4f,0.4f);
                else if(perlin < mediumCutoff)color = new Color(0.6f,0.6f,0.6f);
                else if(perlin < mediumHighCutoff)color = new Color(0.8f,0.8f,0.8f);
                else if(perlin < highCutoff)color = new Color(1,1,1);

                texture2d.SetPixel(x,y,color);
            }
        }
        texture2d.Apply();
    }

    public float fBM(float x,float y, int octaves)
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
}