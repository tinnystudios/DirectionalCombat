using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCube : MonoBehaviour {

    public static TargetCube m_SelectedTarget = null;

    private Renderer m_Renderer;
    public Color m_ActiveColor;
    public Color m_InActiveColor;

    public void SetActive(bool state)
    {
        if (m_Renderer == null) m_Renderer = GetComponent<Renderer>();
        m_Renderer.material.color = state ? m_ActiveColor : m_InActiveColor;

        var size = new Vector3(1, 1, 0.1F);
        transform.localScale = state ? Vector3.one * 0.4F : Vector3.one * 0.25F;

        if(state == true)
            m_SelectedTarget = this;

        if (state == false && m_SelectedTarget == this)
            m_SelectedTarget = null;
    }
}
