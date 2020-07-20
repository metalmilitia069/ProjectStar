using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAIDetection", menuName = "ScriptableVariables/Type: EnemyAIDetection")]
public class EnemyDetectionAI_SO : ScriptableObject
{
    public Collider detectionCollider = default;
    public bool isAlertMode = false;
}
