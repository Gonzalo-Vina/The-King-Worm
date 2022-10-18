using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllerAnimator : MonoBehaviour
{
    [SerializeField] public Animator animatorController;
    [SerializeField] PlayerController playerScript;
    bool overGround;
    public bool nextSprite = false;
    Vector3 nextPosition;
    Vector3 directionHead;




    void Start()
    {
        animatorController.SetBool("NextSprite", nextSprite);
       
    }

    
    void Update()
    {
        CheckDirection();
        CheckHiddenHead();
        if (Input.anyKeyDown && nextSprite == true) { CheckTurn(); }



        animatorController.SetFloat("Horizontal", nextPosition.x);
        animatorController.SetFloat("Vertical", nextPosition.y);
        animatorController.SetBool("OverGround", overGround);
        
    }

    
    private void CheckHiddenHead()
    {
        if (playerScript.overGround == true)
        {
            overGround = true;
        }
        else
        {
            overGround = false;
        }
    }
    private void CheckDirection()
    {
        directionHead = playerScript.transform.position - this.transform.position;
        if (directionHead.x < 0)
        {
            nextPosition = Vector3.left;
        }
        else if (directionHead.x > 0)
        {
            nextPosition = Vector3.right;
        }
        else if (directionHead.y < 0)
        {
            nextPosition = Vector3.down;
        }
        else if (directionHead.y > 0)
        {
            nextPosition = Vector3.up;
        }
    }
    private void CheckTurn()
    {
        if (directionHead == new Vector3(-1, 0, 0))
        {
            nextSprite = false;
            animatorController.SetBool("NextSprite", nextSprite);
        }else if(directionHead == new Vector3(1, 0, 0))
        {
            nextSprite = false;
            animatorController.SetBool("NextSprite", nextSprite);
        }
        else if (directionHead == new Vector3(0, -1, 0))
        {
            nextSprite = false;
            animatorController.SetBool("NextSprite", nextSprite);
        }
        else if (directionHead == new Vector3(0, 1, 0))
        {
            nextSprite = false;
            animatorController.SetBool("NextSprite", nextSprite);
        }
    }

    public void StarCoruotineSprite()
    {
        StartCoroutine(SetNextSpriteTrue());
    }

    IEnumerator SetNextSpriteTrue()
    {
        
        yield return new WaitForSeconds(playerScript.velMovement);
        nextSprite = true;
        animatorController.SetBool("NextSprite", nextSprite);
    }
    
}
