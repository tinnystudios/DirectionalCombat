using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColorModifer : MonoBehaviour, IFlicker<Color>
{
    public Renderer m_Renderer;
    private Color cachedColor;
    private Material m_Material;

    private Renderer[] m_Renderers;
    private Dictionary<Material, Color> mMatLookUp = new Dictionary<Material, Color>();

    private void Awake()
    {
        m_Renderers = GetComponentsInChildren<Renderer>();
        foreach (var rend in m_Renderers)
        {
            mMatLookUp.Add(rend.material, rend.material.color);
        }
    }

    public void Flicker(Color color)
    {
        StartCoroutine(Flick(color));
    }

    IEnumerator Flick(Color color)
    {
        foreach (var mat in mMatLookUp)
        {
            mat.Key.color = color;
        }

        yield return new WaitForSeconds(0.2F);

        foreach (var mat in mMatLookUp)
        {
            mat.Key.color = mat.Value;
        }

    }
}

