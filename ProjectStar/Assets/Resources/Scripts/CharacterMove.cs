using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [Header("INSERT A GRIDMANAGER SO :")]
    public GridManager_SO GridManager;


    [Header("CHARACTER MOVE VARIABLES - INSTANCE :")]
    public CharacterMove_SO characterMoveVariables;

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

            destinationCoordinates.y += characterMoveVariables.halfHeight + t.GetComponent<Collider>().bounds.extents.y;

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
                transform.forward = characterMoveVariables._movementDirection;
                transform.position += characterMoveVariables._velocity * Time.deltaTime;
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
            characterMoveVariables.isMoving = false;
            characterMoveVariables.isTilesFound = false;

            //PUT EVENT TO HIDE TILES
        }
    }

    private void SetMovementDirection(Vector3 destinationCoordinates)
    {
        characterMoveVariables._movementDirection = destinationCoordinates - transform.position;
        characterMoveVariables._movementDirection.Normalize();
    }

    private void SetRunningVelocity()
    {
        characterMoveVariables._velocity = characterMoveVariables._movementDirection * characterMoveVariables.moveSpeed;
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
        if (characterMoveVariables.fallingDown)
        {
            FallDownward(target);
        }
        else if (characterMoveVariables.jumpingUp)
        {
            JumpUpward(target);
        }
        else if (characterMoveVariables.movingEdge)
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
            characterMoveVariables.fallingDown = false;
            characterMoveVariables.jumpingUp = false;
            characterMoveVariables.movingEdge = true;

            characterMoveVariables.jumpTarget = transform.position + ((target - transform.position) / 2.0f);
        }
        else
        {
            characterMoveVariables.fallingDown = false;
            characterMoveVariables.jumpingUp = true;
            characterMoveVariables.movingEdge = false;

            characterMoveVariables._velocity = characterMoveVariables._movementDirection * (characterMoveVariables.moveSpeed / 3.0f); //THis Is the Horizontal Speed Component of The Jumping Process
            //_velocity = Vector3.zero; // zero horizontal speed to climb latter
            float difference = targetY - transform.position.y;

            characterMoveVariables._velocity.y = characterMoveVariables.jumpVelocity * (0.5f + (difference / 2));
        }
    }

    public void FallDownward(Vector3 target)
    {
        characterMoveVariables._velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y <= target.y)
        {
            characterMoveVariables.fallingDown = false;
            characterMoveVariables.jumpingUp = false;
            characterMoveVariables.movingEdge = false;

            Vector3 p = transform.position;

            p.y = target.y;
            transform.position = p;

            characterMoveVariables._velocity = new Vector3();
        }
    }

    public void JumpUpward(Vector3 target)
    {
        characterMoveVariables._velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y > target.y)
        {
            characterMoveVariables.jumpingUp = false;
            characterMoveVariables.fallingDown = true;
        }
    }

    public void MoveToEdge()
    {
        SetRunningVelocity();

        RaycastHit hit;

        if (!Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            characterMoveVariables.movingEdge = false;
            characterMoveVariables.fallingDown = true;

            characterMoveVariables._velocity /= 5.0f;
            characterMoveVariables._velocity.y = 1.5f;
        }
    }

    //public void AddPlayerToTeamList()
    //{
    //    //TurnManager.instance.playerTeam.Add((IPlayerTeam)this);
    //    TurnManager.instance.playerTeamList.Add((CharacterStats)this);
    //}
}
