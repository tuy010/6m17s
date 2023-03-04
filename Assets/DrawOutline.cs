using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOutline : MonoBehaviour
{
    Material outline;

    Renderer renderers;
    List<Material> materialList = new List<Material>();

    void Start()
    {
        outline = new Material(Shader.Find("Draw/OutlineShader"));
    }

    public void VisibleOutline(GameObject go)
    {
        renderers = go.GetComponent<Renderer>();

        materialList.Clear();
        materialList.AddRange(renderers.sharedMaterials);
        materialList.Add(outline);

        renderers.materials = materialList.ToArray();
    }

    public void InvisibleOutline(GameObject go)
    {
        Renderer renderer = go.GetComponent<Renderer>();

        materialList.Clear();
        materialList.AddRange(renderer.sharedMaterials);       
        materialList.Remove(outline);

        renderer.materials = materialList.ToArray();  
    }
}
