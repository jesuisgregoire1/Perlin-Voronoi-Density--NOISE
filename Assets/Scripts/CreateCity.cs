using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreateCity : MonoBehaviour
{
    private int width = 100;
    private int depth = 100;

    public Material residential;
    public Material commercial;
    public Material industrial;

    private void Start()
    {
        MeshUtils.GenerateVoronoi(3,width,depth);
        for (int z = 0; z < depth; ++z)
        {
            for (int x = 0; x < width; ++x)
            {
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.position = new Vector3(x, 0, z);

                Renderer rend = go.GetComponent<Renderer>();
                if(MeshUtils.voronoiMap[x,z] == 0)
                    rend.material = residential;
                if(MeshUtils.voronoiMap[x,z] == 1)
                    rend.material = commercial;
                if(MeshUtils.voronoiMap[x,z] == 2)
                    rend.material = industrial;

                float perlin = MeshUtils.fBM(x * 0.003f, z * 0.003f, 3);
                
                int h = 1;
                if (perlin < 0.417f) h = 1;
                else if (perlin < 0.509f) h = 3;
                else if (perlin < 0.623f) h = 5;
                else if (perlin < 0.676f) h = 7;
                else  h = 10;
                
                
                go.transform.localScale = new Vector3(1, h, 1);
                go.transform.Translate(0,h/2.0f,0);
            }
        }
    }
}
