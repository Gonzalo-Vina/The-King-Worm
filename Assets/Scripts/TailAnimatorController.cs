using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailAnimatorController : MonoBehaviour
{
    [SerializeField] public Animator animatorController;
    [SerializeField] PlayerController playerHead;

    int indexBeforeBody;

    Vector3 directionBeforeBody;
    public Vector3 nextPosition;

    [SerializeField] Direction currentDirection;
    [SerializeField] Direction lastDirection;

    string currentState;
  

    public bool overGroundHead;
    public bool overGroundTail;
    public bool turning;

    

    #region Clips de Animator Body
    const string OG_TAIL_UP = "Tail-Up";
    const string OG_TAIL_DOWN = "Tail-Down";
    const string OG_TAIL_LEFT = "Tail-Left";
    const string OG_TAIL_RIGHT = "Tail-Right";
    const string OG_TAIL_ENTERING_UP = "Tail-Entering-Up";
    const string OG_TAIL_ENTERING_DOWN = "Tail-Entering-Down";
    const string OG_TAIL_ENTERING_LEFT = "Tail-Entering-Left";
    const string OG_TAIL_ENTERING_RIGHT = "Tail-Entering-Right";
    const string OG_TAIL_LEAVING_UP = "Tail-Leaving-Up";
    const string OG_TAIL_LEAVING_DOWN = "Tail-Leaving-Down";
    const string OG_TAIL_LEAVING_LEFT = "Tail-Leaving-Left";
    const string OG_TAIL_LEAVING_RIGHT = "Tail-Leaving-Right";
    const string UG_TAIL_UP = "Tail-Under-Up";
    const string UG_TAIL_DOWN = "Tail-Under-Down";
    const string UG_TAIL_LEFT = "Tail-Under-Left";
    const string UG_TAIL_RIGHT = "Tail-Under-Right";
    #endregion

    void Start()
    {
        currentDirection = (Direction)playerHead.currentDirectionHead;
        if(currentDirection == Direction.LEFT) { ChangeAnimationState(OG_TAIL_LEFT); }
        if(currentDirection == Direction.RIGHT) { ChangeAnimationState(OG_TAIL_RIGHT); }
        if(currentDirection == Direction.UP) { ChangeAnimationState(OG_TAIL_UP); }
        if(currentDirection == Direction.DOWN) { ChangeAnimationState(OG_TAIL_DOWN); }
        turning = true;
        overGroundTail = true;
        overGroundHead = true;
    }

    
    void Update()
    {
        CheckHiddenHead();
        CheckDirection();

        indexBeforeBody = playerHead.body.Count - 1;

        nextPosition = playerHead.lastPositionHead;
        
    }

    private void CheckDirection()
    {
        directionBeforeBody = playerHead.body[indexBeforeBody].position - this.transform.position;

        if (directionBeforeBody.x < 0)
        {
            if (overGroundHead == true && overGroundTail == true)
            {
                StartCoroutine(ChangeStateClip(OG_TAIL_LEFT));
            }
            else if (overGroundHead == false && overGroundTail == true)
            {
                StartCoroutine(ChangeStateGround(OG_TAIL_ENTERING_LEFT, false));
            } 
            else if (overGroundHead == false && overGroundTail == false)
            {
                StartCoroutine(ChangeStateClip(UG_TAIL_LEFT));
            } 
            else if (overGroundHead == true && overGroundTail == false)
            {
                StartCoroutine(ChangeStateGround(OG_TAIL_LEAVING_LEFT, true));
            }

        }
        else if (directionBeforeBody.x > 0)
        {
            if (overGroundHead == true && overGroundTail == true)
            {
                StartCoroutine(ChangeStateClip(OG_TAIL_RIGHT));
            }
            else if (overGroundHead == false && overGroundTail == true)
            {
                StartCoroutine(ChangeStateGround(OG_TAIL_ENTERING_RIGHT, false));
            }
            else if (overGroundHead == false && overGroundTail == false)
            {
                StartCoroutine(ChangeStateClip(UG_TAIL_RIGHT));
            }
            else if (overGroundHead == true && overGroundTail == false)
            {
                StartCoroutine(ChangeStateGround(OG_TAIL_LEAVING_RIGHT, true));
            }
        }
        else if (directionBeforeBody.y > 0)
        {
            if (overGroundHead == true && overGroundTail == true)
            {
                StartCoroutine(ChangeStateClip(OG_TAIL_UP));
            }
            else if (overGroundHead == false && overGroundTail == true)
            {
                StartCoroutine(ChangeStateGround(OG_TAIL_ENTERING_UP, false));
            }
            else if (overGroundHead == false && overGroundTail == false)
            {
                StartCoroutine(ChangeStateClip(UG_TAIL_UP));
            }
            else if (overGroundHead == true && overGroundTail == false)
            {
                StartCoroutine(ChangeStateGround(OG_TAIL_LEAVING_UP, true));
            }
        }
        else if (directionBeforeBody.y < 0)
        {
            if (overGroundHead == true && overGroundTail == true)
            {
                StartCoroutine(ChangeStateClip(OG_TAIL_DOWN));
            }
            else if (overGroundHead == false && overGroundTail == true)
            {
                StartCoroutine(ChangeStateGround(OG_TAIL_ENTERING_DOWN, false));
            }
            else if (overGroundHead == false && overGroundTail == false)
            {
                StartCoroutine(ChangeStateClip(UG_TAIL_DOWN));

            }
            else if (overGroundHead == true && overGroundTail == false)
            {
                StartCoroutine(ChangeStateGround(OG_TAIL_LEAVING_DOWN, true));
            }
        }
    }
    private void CheckHiddenHead()
    {
        if (playerHead.overGround == true)
        {
            overGroundHead = true;
        }
        else
        {
            overGroundHead = false;
        }
    }
    private void CheckTurn()
    {
        if(currentDirection != lastDirection)
        {
           turning = true;
        }
    }



    private void ChangeAnimationState(string newState)
    {
        if (currentState != newState)
        {
            StopAllCoroutines();
            animatorController.Play(newState);

            currentState = newState;
            lastDirection = currentDirection;
        }

        
    }


    enum Direction { UP, DOWN, LEFT, RIGHT }

    IEnumerator ChangeStateClip(string clip)
    {

        ChangeAnimationState(clip);
        yield return new WaitForSeconds(playerHead.velMovement);
    }

    IEnumerator TurnDirection(string clip, Direction newDirection)
    {
        
        yield return new WaitForSeconds(playerHead.velMovement);
        ChangeAnimationState(clip);
        currentDirection = newDirection;
        if (turning == false) CheckTurn();
    }

    IEnumerator ChangeStateGround(string newState, bool overground)
    {
        yield return new WaitForSeconds(playerHead.velMovement * 2);
        ChangeAnimationState(newState);
        overGroundTail = overground;
    }
}
