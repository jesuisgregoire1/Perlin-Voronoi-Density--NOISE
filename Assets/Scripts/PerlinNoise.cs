using System;
using UnityEngine;

[ExecuteInEditMode]
public class PerlinNoise : MonoBehaviour
{

  [Range(1, 8)] public int octaves = 2;
  
  [Range(0.001f,0.01f)]public float xScale = 0.003f;
  [Range(0.001f,0.01f)]public float yScale = 0.003f;
  
  [Range(0, 1000)]public int xOffset = 0;
  [Range(0,1000)]public int yOffset = 0;

  [Range(0.0f, 1.0f)] public float greenCutoff = 0;
  [Range(0.0f, 1.0f)] public float blueCutoff = 0;
  [Range(0.0f, 1.0f)] public float yellowCutoff = 0;
  private void OnValidate()
  {
    Texture2D texture2d = new Texture2D(1024, 1024);
    GetComponent<Renderer>().sharedMaterial.mainTexture = texture2d;

    float perlin;
    Color color = Color.white;

    float perlinr;//residential
    float perlinc;//commercial
    float perlini;//industrial

    for (int y = 0; y < texture2d.height; y++)
    {
      for (int x = 0; x < texture2d.width; x++)
      {
        //perlin = fBM((x+xOffset) * xScale, (y+yOffset) * yScale,octaves);
        perlinr = fBM((x+xOffset) * xScale, (y+yOffset) * yScale,octaves);
        perlinc = fBM((x+xOffset+100) * xScale, (y+yOffset+100) * yScale,octaves);
        perlini = fBM((x+xOffset+5000) * xScale, (y+yOffset+5000) * yScale,octaves);
        
        if (perlinr < greenCutoff) color = Color.green;
        else if (perlinc < blueCutoff) color = Color.blue;
        else if (perlini < blueCutoff)color = Color.yellow;

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
