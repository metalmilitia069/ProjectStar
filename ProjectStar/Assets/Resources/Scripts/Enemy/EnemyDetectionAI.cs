using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionAI : MonoBehaviour
{
    [Header("ENEMY DETECTION AI VARIABLES - INSTANCE :")]
    public EnemyDetectionAI_SO enemyDetectionAIVariables;
    [Header("ENEMY DETECTION SETUP")]
    public Collider detectionCollider = default;
    public float detectionRadius = default;
    
    
    
    
    private List<Collider> listOfColliders = new List<Collider>();

    // Start is called before the first frame update
    void Start()
    {
        enemyDetectionAIVariables.detectionCollider = detectionCollider;
        detectionCollider.GetComponentInParent<SphereCollider>().radius = detectionRadius;
    }   

    public void AIDetectHostiles(bool option)
    {
        //if(option)
        //{
        //    listOfColliders.Clear();
        //    Debug.Log("option: " + option);
        //}
        detectionCollider.enabled = option;        
    }

    public IEnumerator WaitForDetection()
    {
        yield return new WaitForSeconds(1);
        EnterAlertStateAndAlertOthers();
        if (enemyDetectionAIVariables.isAlertMode)
        {            
            AIDetectHostiles(false);
            listOfColliders.Clear();
            yield break;
        }
        AIDetectHostiles(false);
        GetComponent<EnemyInput>().EnemyTurnVariables.actionPoints--;
    }

    private void OnTriggerStay(Collider other)
    {        
        listOfColliders.Add(other);
        //Debug.Log("List Of Colliders: " + listOfColliders.Count);
    }

    public void EnterAlertStateAndAlertOthers()
    {
        foreach (var col in listOfColliders)
        {
            if (col != null)
            {
                if (col.gameObject.GetComponent<CharacterInput>())
                {
                    enemyDetectionAIVariables.isAlertMode = true;
                }
            }
        }

        foreach (var col in listOfColliders)
        {
            if (col != null)
            {
                if (enemyDetectionAIVariables.isAlertMode)
                {
                    if (col.gameObject.GetComponent<EnemyInput>())
                    {
                        col.gameObject.GetComponent<EnemyInput>().enemyDetectionAIVariables.isAlertMode = true;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
