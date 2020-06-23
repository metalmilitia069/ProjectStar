using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPathAI", menuName = "ScriptableVariables/Type: EnemyPathAI")]
public class EnemyPathAI_SO : ScriptableObject
{
    public GameObject characterTarget;

    public AdvancedTile actualTargetTile;
}
