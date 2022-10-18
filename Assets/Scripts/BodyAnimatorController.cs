using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyAnimatorController : MonoBehaviour
{
    [SerializeField] public Animator animatorController;
    [SerializeField] PlayerController playerHead;

    Vector3 directionHead;
    public Vector3 nextPosition;

    

    [SerializeField] public Direction currentDirection;
    [SerializeField] Direction lastDirection;

    string currentState;
  

    public bool overGroundHead;
    public bool overGroundBody;
    public bool turning;

    

    #region Clips de Animator Body
    const string OG_BODY_UP = "OG-Body-Up";
    const string OG_BODY_DOWN = "OG-Body-Down";
    const string OG_BODY_LEFT = "OG-Body-Left";
    const string OG_BODY_RIGHT = "OG-Body-Right";
    const string OG_BODY_TURNING_UPLEFT = "OG-Body-Turning-UpLeft";
    const string OG_BODY_TURNING_UPRIGHT = "OG-Body-Turning-UpRight";
    const string OG_BODY_TURNING_DOWNLEFT = "OG-Body-Turning-DownLeft";
    const string OG_BODY_TURNING_DOWNRIGHT = "OG-Body-Turning-DownRight";
    const string OG_BODY_TURNING_LEFTUP = "OG-Body-Turning-LeftUp";
    const string OG_BODY_TURNING_LEFTDOWN = "OG-Body-Turning-LeftDown";
    const string OG_BODY_TURNING_RIGHTUP = "OG-Body-Turning-RightUp";
    const string OG_BODY_TURNING_RIGHTDOWN = "OG-Body-Turning-RightDown";
    const string OG_BODY_ENTERING_UP = "OG-Body-Entering-Up";
    const string OG_BODY_ENTERING_DOWN = "OG-Body-Entering-Down";
    const string OG_BODY_ENTERING_LEFT = "OG-Body-Entering-Left";
    const string OG_BODY_ENTERING_RIGHT = "OG-Body-Entering-Right";
    const string OG_BODY_LEAVING_UP = "OG-Body-Leaving-Up";
    const string OG_BODY_LEAVING_DOWN = "OG-Body-Leaving-Down";
    const string OG_BODY_LEAVING_LEFT = "OG-Body-Leaving-Left";
    const string OG_BODY_LEAVING_RIGHT = "OG-Body-Leaving-Right";
    const string UG_BODY_UP = "UG-Body-Up";
    const string UG_BODY_DOWN = "UG-Body-Down";
    const string UG_BODY_LEFT = "UG-Body-Left";
    const string UG_BODY_RIGHT = "UG-Body-Right";
    const string UG_BODY_TURNING_UPLEFT = "UG-Body-Turning-UpLeft";
    const string UG_BODY_TURNING_UPRIGHT = "UG-Body-Turning-UpRight";
    const string UG_BODY_TURNING_DOWNLEFT = "UG-Body-Turning-DownLeft";
    const string UG_BODY_TURNING_DOWNRIGHT = "UG-Body-Turning-DownRight";
    const string UG_BODY_TURNING_LEFTUP = "UG-Body-Turning-LeftUp";
    const string UG_BODY_TURNING_LEFTDOWN = "UG-Body-Turning-LeftDown";
    const string UG_BODY_TURNING_RIGHTUP = "UG-Body-Turning-RightUp";
    const string UG_BODY_TURNING_RIGHTDOWN = "UG-Body-Turning-RightDown";
    #endregion

    void Start()
    {
        currentDirection = Direction.UP;
        ChangeAnimationState(OG_BODY_UP);
        turning = true;
        overGroundBody = true;
        overGroundHead = true;
    }

    
    void Update()
    {
        CheckHiddenHead();
        CheckDirection();
        


        nextPosition = playerHead.lastPositionHead;
        
    }

    private void CheckDirection()
    {
        directionHead = playerHead.transform.position - this.transform.position;

        if (directionHead.x < 0)
        {
            if (overGroundHead == true && overGroundBody == true)
            {
                if (currentDirection == Direction.UP)
                {
                    if (turning == true) ChangeAnimationStateTurn(OG_BODY_TURNING_UPLEFT);
                    if (turning == false) StartCoroutine(TurnDirection(OG_BODY_LEFT, Direction.LEFT));
                    
                    
                } else if (currentDirection == Direction.DOWN)
                {
                    if (turning == true) ChangeAnimationStateTurn(OG_BODY_TURNING_DOWNLEFT);
                    if (turning == false) StartCoroutine(TurnDirection(OG_BODY_LEFT, Direction.LEFT));
                }
                 
            }
            else if (overGroundHead == false && overGroundBody == true)
            {
                StartCoroutine(ChangeStateGround(OG_BODY_ENTERING_LEFT, false));
            } 
            else if (overGroundHead == false && overGroundBody == false)
            {
                if (currentDirection == Direction.UP)
                {
                    if (overGroundBody == false && turning == true) ChangeAnimationStateTurn(UG_BODY_TURNING_UPLEFT);
                    if (overGroundBody == false && turning == false) StartCoroutine(TurnDirection(UG_BODY_LEFT, Direction.LEFT));
                }
                else if (currentDirection == Direction.DOWN)
                {
                    if (overGroundBody == false && turning == true) ChangeAnimationStateTurn(UG_BODY_TURNING_DOWNLEFT);
                    if (overGroundBody == false && turning == false) StartCoroutine(TurnDirection(UG_BODY_LEFT, Direction.LEFT));
                }
            } 
            else if (overGroundHead == true && overGroundBody == false)
            {
                StartCoroutine(ChangeStateGround(OG_BODY_LEAVING_LEFT, true));
            }

        }
        else if (directionHead.x > 0)
        {
            if (overGroundHead == true && overGroundBody == true)
            {
                if (currentDirection == Direction.UP)
                {
                    if (turning == true) ChangeAnimationStateTurn(OG_BODY_TURNING_UPRIGHT);
                    if (turning == false) StartCoroutine(TurnDirection(OG_BODY_RIGHT, Direction.RIGHT));
                }
                else if (currentDirection == Direction.DOWN)
                {
                    if (turning == true) ChangeAnimationStateTurn(OG_BODY_TURNING_DOWNRIGHT);
                    if (turning == false) StartCoroutine(TurnDirection(OG_BODY_RIGHT, Direction.RIGHT));
                }

            }
            else if (overGroundHead == false && overGroundBody == true)
            {
                StartCoroutine(ChangeStateGround(OG_BODY_ENTERING_RIGHT,false));
            }
            else if (overGroundHead == false && overGroundBody == false)
            {
                if (currentDirection == Direction.UP)
                {
                    if (overGroundBody == false && turning == true) ChangeAnimationStateTurn(UG_BODY_TURNING_UPRIGHT);
                    if (overGroundBody == false && turning == false) StartCoroutine(TurnDirection(UG_BODY_RIGHT, Direction.RIGHT));
                }
                else if (currentDirection == Direction.DOWN)
                {
                    if (overGroundBody == false && turning == true) ChangeAnimationStateTurn(UG_BODY_TURNING_DOWNRIGHT);
                    if (overGroundBody == false && turning == false) StartCoroutine(TurnDirection(UG_BODY_RIGHT, Direction.RIGHT));
                }
            }
            else if (overGroundHead == true && overGroundBody == false)
            {
                StartCoroutine(ChangeStateGround(OG_BODY_LEAVING_RIGHT, true));
            }
        }
        else if (directionHead.y > 0)
        {
            if (overGroundHead == true && overGroundBody == true)
            {
                if (currentDirection == Direction.LEFT)
                {
                    if (turning == true) ChangeAnimationStateTurn(OG_BODY_TURNING_LEFTUP);
                    if (turning == false) StartCoroutine(TurnDirection(OG_BODY_UP, Direction.UP));
                }
                else if (currentDirection == Direction.RIGHT)
                {
                    if (turning == true) ChangeAnimationStateTurn(OG_BODY_TURNING_RIGHTUP);
                    if (turning == false) StartCoroutine(TurnDirection(OG_BODY_UP, Direction.UP));
                }
            }
            else if (overGroundHead == false && overGroundBody == true)
            {
                StartCoroutine(ChangeStateGround(OG_BODY_ENTERING_UP, false));
            }
            else if (overGroundHead == false && overGroundBody == false)
            {
                if (currentDirection == Direction.LEFT)
                {
                    if (overGroundBody == false && turning == true) ChangeAnimationStateTurn(UG_BODY_TURNING_LEFTUP);
                    if (overGroundBody == false && turning == false) StartCoroutine(TurnDirection(UG_BODY_UP, Direction.UP));
                }
                else if (currentDirection == Direction.RIGHT)
                {
                    if (overGroundBody == false && turning == true) ChangeAnimationStateTurn(UG_BODY_TURNING_RIGHTUP);
                    if (overGroundBody == false && turning == false) StartCoroutine(TurnDirection(UG_BODY_UP, Direction.UP));
                }
            }
            else if (overGroundHead == true && overGroundBody == false)
            {
                StartCoroutine(ChangeStateGround(OG_BODY_LEAVING_UP, true));
            }
        }
        else if (directionHead.y < 0)
        {
            if (overGroundHead == true && overGroundBody == true)
            {
                if (currentDirection == Direction.LEFT)
                {
                    if (turning == true) ChangeAnimationStateTurn(OG_BODY_TURNING_LEFTDOWN);
                    if (turning == false) StartCoroutine(TurnDirection(OG_BODY_DOWN, Direction.DOWN));
                    
                }
                else if (currentDirection == Direction.RIGHT)
                {
                    if (turning == true) ChangeAnimationStateTurn(OG_BODY_TURNING_RIGHTDOWN);
                    if (turning == false) StartCoroutine(TurnDirection(OG_BODY_DOWN, Direction.DOWN));
                }
            }
            else if (overGroundHead == false && overGroundBody == true)
            {
                StartCoroutine(ChangeStateGround(OG_BODY_ENTERING_DOWN, false));
            }
            else if (overGroundHead == false && overGroundBody == false)
            {
                if (currentDirection == Direction.LEFT)
                {
                    if (overGroundBody == false && turning == true) ChangeAnimationStateTurn(UG_BODY_TURNING_LEFTDOWN);
                    if (overGroundBody == false && turning == false) StartCoroutine(TurnDirection(UG_BODY_DOWN, Direction.DOWN));

                }
                else if (currentDirection == Direction.RIGHT)
                {
                    if (overGroundBody == false && turning == true) ChangeAnimationStateTurn(UG_BODY_TURNING_RIGHTDOWN);
                    if (overGroundBody == false && turning == false) StartCoroutine(TurnDirection(UG_BODY_DOWN, Direction.DOWN));
                }
            }
            else if (overGroundHead == true && overGroundBody == false)
            {
                StartCoroutine(ChangeStateGround(OG_BODY_LEAVING_DOWN, true));
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
    private void ChangeAnimationStateTurn(string newState)
    {
        if (currentState != newState)
        {

            StopAllCoroutines();
            turning = false;
            animatorController.Play(newState);
            currentState = newState;
            lastDirection = currentDirection;
        }


    }

    public enum Direction { UP, DOWN, LEFT, RIGHT }

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
        overGroundBody = overground;
    }
}
