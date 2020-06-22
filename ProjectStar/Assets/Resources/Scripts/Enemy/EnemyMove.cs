using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("INSERT A GRIDMANAGER SO :")]
    public GridManager_SO GridManager;


    [Header("ENEMY MOVE VARIABLES - INSTANCE :")]
    public EnemyMove_SO enemyMoveVariables;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        if (GridManager.stackTilePath.Count > 0)
        {
            AdvancedTile t = GridManager.stackTilePath.Peek();
            Vector3 destinationCoordinates = t.transform.position;

            destinationCoordinates.y += enemyMoveVariables.halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, destinationCoordinates) >= 0.05f)
            {
                bool jump = (transform.position.y != destinationCoordinates.y);

                if (jump)
                {
                    Jump(destinationCoordinates);
                }
                else
                {
                    SetMovementDirection(destinationCoordinates);
                    SetRunningVelocity();
                }
                transform.forward = enemyMoveVariables._movementDirection;
                transform.position += enemyMoveVariables._velocity * Time.deltaTime;
            }
            else
            {
                transform.position = destinationCoordinates;
                GridManager.stackTilePath.Pop();
            }
        }
        else
        {
            GridManager.ClearSelectableTiles();
            enemyMoveVariables.isMoving = false;
            enemyMoveVariables.isTilesFound = false;

            //PUT EVENT TO HIDE TILES
        }
    }

    private void SetMovementDirection(Vector3 destinationCoordinates)
    {
        enemyMoveVariables._movementDirection = destinationCoordinates - transform.position;
        enemyMoveVariables._movementDirection.Normalize();
    }

    private void SetRunningVelocity()
    {
        enemyMoveVariables._velocity = enemyMoveVariables._movementDirection * enemyMoveVariables.moveSpeed;
    }

    public void CoverMode(bool option)
    {
        //gameObject.GetComponentInChildren<Animator>().SetBool("IsInCoverState", option);
        if (option)
        {
            Debug.Log("TODO: PUT COVER ANIMATION!!!");
        }
    }

    public void Jump(Vector3 target)
    {
        if (enemyMoveVariables.fallingDown)
        {
            FallDownward(target);
        }
        else if (enemyMoveVariables.jumpingUp)
        {
            JumpUpward(target);
        }
        else if (enemyMoveVariables.movingEdge)
        {
            MoveToEdge();
        }
        else
        {
            PrepareJump(target);
        }
    }

    public void PrepareJump(Vector3 target)
    {
        float targetY = target.y;
        target.y = transform.position.y;

        SetMovementDirection(target);

        if (transform.position.y > targetY)
        {
            enemyMoveVariables.fallingDown = false;
            enemyMoveVariables.jumpingUp = false;
            enemyMoveVariables.movingEdge = true;

            enemyMoveVariables.jumpTarget = transform.position + ((target - transform.position) / 2.0f);
        }
        else
        {
            enemyMoveVariables.fallingDown = false;
            enemyMoveVariables.jumpingUp = true;
            enemyMoveVariables.movingEdge = false;

            enemyMoveVariables._velocity = enemyMoveVariables._movementDirection * (enemyMoveVariables.moveSpeed / 3.0f); //THis Is the Horizontal Speed Component of The Jumping Process
            //_velocity = Vector3.zero; // zero horizontal speed to climb latter
            float difference = targetY - transform.position.y;

            enemyMoveVariables._velocity.y = enemyMoveVariables.jumpVelocity * (0.5f + (difference / 2));
        }
    }

    public void FallDownward(Vector3 target)
    {
        enemyMoveVariables._velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y <= target.y)
        {
            enemyMoveVariables.fallingDown = false;
            enemyMoveVariables.jumpingUp = false;
            enemyMoveVariables.movingEdge = false;

            Vector3 p = transform.position;

            p.y = target.y;
            transform.position = p;

            enemyMoveVariables._velocity = new Vector3();
        }
    }

    public void JumpUpward(Vector3 target)
    {
        enemyMoveVariables._velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y > target.y)
        {
            enemyMoveVariables.jumpingUp = false;
            enemyMoveVariables.fallingDown = true;
        }
    }

    public void MoveToEdge()
    {
        SetRunningVelocity();

        RaycastHit hit;

        if (!Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            enemyMoveVariables.movingEdge = false;
            enemyMoveVariables.fallingDown = true;
            
            enemyMoveVariables._velocity /= 5.0f;
            enemyMoveVariables._velocity.y = 1.5f;
        }
    }
}
