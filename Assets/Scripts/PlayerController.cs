using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Atributos del Player
    float inputX, inputY;
    int pointAccumulated;
    [HideInInspector] public float velMovement;
    [HideInInspector] public Vector3 nextPosition = Vector3.up;
    [HideInInspector] public Vector3 lastPositionBody;
    [HideInInspector] public Vector3 lastPositionHead;
    bool bodyPending = false;
    bool moveTail = true;
    [HideInInspector] public bool overGround = true;
    bool changeDirection = true;

    [SerializeField] FoodManager food;
    [SerializeField] GameController gameController;

    [HideInInspector] public Direction currentDirectionHead;

    

    [Header("Parts")]
    [SerializeField] public List<Transform> body = new List<Transform>();
    [SerializeField] Transform tail;

    [Header("Prefab to add")]
    [SerializeField] GameObject bodyPrefab;
    [Space]

    #endregion


    #region Atributos del Animator
    [Header("Animator")]
    [SerializeField] Animator headAnimator;

    #endregion

    void Awake()
    {
        Time.timeScale = 1;
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        velMovement = 0.33f;
        StartCoroutine(MoveCoroutine());
        body[0].position = transform.position + Vector3.down;
        tail.position = transform.position + (Vector3.down*2);
        food.CreateFood();
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");


        SelectDirection();
        SetVelMovement();
        HidePlayer();
        
        if(Time.timeScale == 0 && Input.GetKey(KeyCode.Space))
        {
            ResetScene();
        }



        #region Animator
        headAnimator.SetFloat("Horizontal", nextPosition.x);
        headAnimator.SetFloat("Vertical", nextPosition.y);
        headAnimator.SetBool("OverGround", overGround);
        headAnimator.SetBool("NextSprite", false);
        #endregion
    }

    #region Player Controllers
    private void SelectDirection()
    {
        if (inputX < 0 && nextPosition.x <= 0 && changeDirection == true)
        {
            nextPosition = Vector3.left;
            changeDirection = false;
            currentDirectionHead = Direction.LEFT;
        }
        else if (inputX > 0 && nextPosition.x >= 0 && changeDirection == true)
        {
            nextPosition = Vector3.right;
            changeDirection = false;
            currentDirectionHead = Direction.RIGHT;
        }
        else if (inputY < 0 && nextPosition.y <= 0 && changeDirection == true)
        {
            nextPosition = Vector3.down;
            changeDirection = false;
            currentDirectionHead = Direction.DOWN;
        }
        else if (inputY > 0 && nextPosition.y >= 0 && changeDirection == true)
        {
            nextPosition = Vector3.up;
            changeDirection = false;
            currentDirectionHead = Direction.UP;
        }
    }
    private void SetVelMovement()
    {
        if (body.Count >= 3 && body.Count < 5)
        {
            velMovement = 0.32f;
        }
        else if (body.Count >= 5 && body.Count < 7)
        {
            velMovement = 0.31f;
        }
        else if (body.Count >= 7 && body.Count < 9)
        {
            velMovement = 0.29f;
        }
        else if (body.Count >= 9 && body.Count < 11)
        {
            velMovement = 0.27f;
        }
        else if (body.Count >= 11 && body.Count < 13)
        {
            velMovement = 0.25f;
        }
        else if (body.Count >= 13 && body.Count < 15)
        {
            velMovement = 0.23f;
        }
        else if (body.Count >= 15 && body.Count < 17)
        {
            velMovement = 0.21f;
        }
        else if (body.Count >= 17 && body.Count < 19)
        {
            velMovement = 0.19f;
        }
        else if (body.Count >= 19 && body.Count < 21)
        {
            velMovement = 0.17f;
        }
        else if (body.Count >= 21)
        {
            velMovement = 0.15f;
        }
    }
    private void HidePlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
                overGround = false;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            overGround = true;
        }
    }
    private void MoveBody()
    {
        for (int i = 0; i < body.Count; i++)
        {
            Vector3 temp = body[i].position;
            body[i].position = lastPositionBody;
            lastPositionBody = temp;
        }
    }
    private void AddBody()
    {
        GameObject newBody = Instantiate(bodyPrefab, lastPositionBody, Quaternion.identity);
        newBody.name = "Body_" + body.Count;
        body.Add(newBody.transform);
        bodyPending = false;
    }
    private void MoveTail()
    {
        tail.position = lastPositionBody;
    }
    
    private void MoveHead()
    {
        if(nextPosition != lastPositionBody)
        {
            lastPositionHead = transform.position;
            transform.position += nextPosition;
        }
        headAnimator.SetBool("NextSprite", true);
        
    }

    private void ResetScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public enum Direction { UP, DOWN, LEFT, RIGHT }

    IEnumerator MoveCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(velMovement);
            lastPositionBody = transform.position;
            MoveHead();
            MoveBody();
            if (bodyPending) AddBody();
            if (moveTail) MoveTail();
            changeDirection = true;
        }
        
    }
    IEnumerator WaitMoveTail()
    {
        yield return new WaitForSeconds(velMovement);
        moveTail = true;
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food" && overGround == true)
        {
            bodyPending = true;
            moveTail = false;
            StartCoroutine(WaitMoveTail());
            Destroy(collision.gameObject);
            food.CreateFood();
            pointAccumulated += 10;
        }
        else if (collision.gameObject.tag == "Limit")
        {

            gameController.PauseGame();
            
        }
        else if (collision.gameObject.tag == "Body" && overGround == true)
        {
            gameController.PauseGame();
        }
    }
    #endregion

}
