using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTargetSystem : MonoBehaviour
{

    public List<TargetCube> m_Targets;

    public void SetCombatTargetCue(int index)
    {

        for (int i = 0; i < m_Targets.Count; i++)
        {
            if (i == index)
            {
                m_Targets[i].SetActive(true);
            }
            else
            {
                m_Targets[i].SetActive(false);
            }
        }
    }


}
