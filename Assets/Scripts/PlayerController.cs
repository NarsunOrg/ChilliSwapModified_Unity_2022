using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Animator PlayerAnim;
    public bool middle, left, right;
    public Rigidbody rb;
    public bool isGrounded, isMoved;
    Vector2 initialTouchPosition, endTouchPosition;
    float xDifference, yDifference;
    public GameObject SlidingCollider;
    public GameObject Parent;
    public string State;
    int speed;
    int JumpForce;
    int Line= 0;
    bool changingline = false;
    public bool ChangingPlatform = false;
    GameObject nextTransformPosition;
    bool dead = false;
    public string CurrentRespectiveState;
    bool hasSwiped;
    public bool InvisibilityBool;
    public bool SuperSpeedBool;
    public GameObject LaserToUse;

    // Start is called before the first frame update
    void Start()
    {
        InvisibilityBool = false;
        SuperSpeedBool = false;
        JumpForce = 250;
        speed = 20;
        State = "Front";
        middle = true;
        left = false;
        right = false;
        isGrounded = true;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Keyboard Input
        //Keyboard Input
        if (Input.GetKeyDown(KeyCode.DownArrow) && !ChangingPlatform)
        {
            Down();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded && !ChangingPlatform)
        {
            Up();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)&&changingline == false && !ChangingPlatform)
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)&&changingline==false && !ChangingPlatform)
        {
            MoveRight();
        }
        //if (Input.GetKeyDown(KeyCode.LeftArrow) && ChangingPlatform && CurrentRespectiveState == "Left")
        //{
        //    ChangeState();
        //}
        //if (Input.GetKeyDown(KeyCode.RightArrow) && ChangingPlatform && CurrentRespectiveState == "Right")
        //{
        //    ChangeState();
        //}
        if (Input.GetKeyDown(KeyCode.LeftArrow) && ChangingPlatform)
        {
            ChangeState("Left");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && ChangingPlatform)
        {
            ChangeState("Right");
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Invisibility();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SuperJump();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            SuperSpeed();
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            SlowingDown();
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            Teleportation();
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            LaserGoggles();
        }
        #endregion

        #region Touch Input
        //Touch Input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            isMoved = false;
            initialTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && !isMoved)
        {
            //if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            //{
            hasSwiped = true;
            //}
        }
        if(hasSwiped)
        {
            hasSwiped = false;
            endTouchPosition = Input.GetTouch(0).position;
            xDifference = initialTouchPosition.x - endTouchPosition.x;
            yDifference = initialTouchPosition.y - endTouchPosition.y;
            if (Mathf.Abs(xDifference) > 3 || Mathf.Abs(yDifference) > 3)
            {
                isMoved = true;
                if (Mathf.Abs(xDifference) >= Mathf.Abs(yDifference))
                {
                    //move left or right
                    if (xDifference > 0 && ChangingPlatform && CurrentRespectiveState == "Left")
                    {
                        ChangeState("Left");
                    }
                    else if (xDifference < 0 && ChangingPlatform && CurrentRespectiveState == "Right")
                    {
                        ChangeState("Right");
                    }
                }
                else
                {
                    //jump or slide
                    if (yDifference > 0)
                    {
                        Down();
                    }
                    else
                    {
                        Up();
                    }
                }

            }
        }
        #endregion

        #region Parent Movement
        float x = Parent.transform.position.x;
        float z = Parent.transform.position.z;
        //CalculateParentMovement(z, x);
        if(!dead)
        {
            Parent.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime), Space.Self);
        }
        
        #endregion

        #region Gyro Movement
        if(Input.acceleration.x != 0)
        {
            transform.DOLocalMoveX(Input.acceleration.x * Time.deltaTime * 200, 1);
        }
        #endregion
    }
    //Player dummy collider to check if its on ground
    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.transform.tag)
        {
            case "Floor":
                isGrounded = true;
                break;
            case "Wall":
                dead = true;
                PlayerAnim.SetTrigger("Death");
                break;
            case "Hurdle":
                if (!InvisibilityBool && !SuperSpeedBool)
                {
                    dead = true;
                    PlayerAnim.SetTrigger("Death");
                }
                break;
            case "PowerUpInvisibility":
                Invisibility();
                break;
            case "PowerUpJump":
                SuperJump();
                break;
            case "PowerUpSpeed":
                SuperSpeed();
                break;
            case "PowerUpSlow":
                SlowingDown();
                break;
            case "PowerUpTeleportation":
                Teleportation();
                break;
            case "PowerUpLaser":
                LaserGoggles();
                break;
        }
        #region commented code
        //if(collision.transform.tag == "Floor")
        //{
        //    isGrounded = true;
        //}
        //if (collision.transform.tag == "Wall")
        //{
        //    dead = true;
        //    PlayerAnim.SetTrigger("Death");
        //}
        //if (collision.transform.tag == "Hurdle" && !InvisibilityBool && !SuperSpeedBool)
        //{
        //    dead = true;
        //    PlayerAnim.SetTrigger("Death");
        //}
        //if (collision.transform.tag == "PowerUpInvisibility")
        //{
        //    Invisibility();
        //}
        //if (collision.transform.tag == "PowerUpJump")
        //{
        //    SuperJump();
        //}
        //if (collision.transform.tag == "PowerUpSpeed")
        //{
        //    SuperSpeed();
        //}
        //if (collision.transform.tag == "PowerUpSlow")
        //{
        //    SlowingDown();
        //}
        //if (collision.transform.tag == "PowerUpTeleportation")
        //{
        //    Teleportation();
        //}
        //if (collision.transform.tag == "PowerUpLaser")
        //{
        //    LaserGoggles();
        //}
        #endregion
    }
    public void MoveLeft()
    {
        //if (!left)
        //{
        //    if (middle)
        //    {
        //        transform.DOLocalMoveX(gameObject.transform.localPosition.x - 2, 0.25f);
        //        middle = false;
        //        left = true;
        //    }
        //    else
        //    {
        //        transform.DOLocalMoveX(gameObject.transform.localPosition.x - 2, 0.25f);
        //        right = false;
        //        middle = true;
        //    }
        //}
        switch (Line)
        {

            case 0:
                if (Line == -1)
                    break;
                transform.DOLocalMoveX(gameObject.transform.localPosition.x - 1, 0.25f);
                changingline=true;
                Invoke("LineChnaged", 0.25f);
                Line = -1;
                break;
            case -1:
                break;
            case 1:
                if (Line == 0)
                    break;
                transform.DOLocalMoveX(gameObject.transform.localPosition.x - 1, 0.25f);
                changingline = true;
                Invoke("LineChnaged", 0.25f);
                Line = 0;
                break;
        }
    }
    public void MoveRight()
    {
        //if (!right)
        //{
        //    if (middle)
        //    {
        //        transform.DOLocalMoveX(gameObject.transform.localPosition.x + 2, 0.25f);
        //        middle = false;
        //        right = true;
        //    }
        //    else
        //    {
        //        transform.DOLocalMoveX(gameObject.transform.localPosition.x + 2, 0.25f);
        //        left = false;
        //        middle = true;
        //    }
        //}
        switch (Line)
        {

            case 0:
                if (Line == 1)
                    break;
                transform.DOLocalMoveX(gameObject.transform.localPosition.x + 1, 0.25f);
                changingline = true;
                Invoke("LineChnaged", 0.25f);
                Line = 1;
                break;
            case -1:
                if (Line == 0)
                    break;
                transform.DOLocalMoveX(gameObject.transform.localPosition.x + 1, 0.25f);
                changingline = true;
                Invoke("LineChnaged", 0.25f);
                Line = 0;
                break;
            case 1:
               
                break;
        }
    }

    public void LineChnaged()
    {
        changingline = false;
    }
    public void Up()
    {
        SlidingCollider.transform.localPosition = new Vector3(0, 0.9f, 0);
        isGrounded = false;
        PlayerAnim.SetTrigger("Jump 0");
        PlayerAnim.SetBool("Jump", true);
        rb.AddForce(transform.up * JumpForce);
        //PlayerAnim.SetBool("Jump", false);
    }
    public void Down()
    {
        SlidingCollider.transform.DOLocalMove(new Vector3(0, 0.1f, 0),1);
        transform.DOLocalMoveY(0, 0.25f);
        PlayerAnim.SetTrigger("Sliding 0");
        PlayerAnim.SetBool("Sliding", true);
        StartCoroutine("DelatPos");
        //PlayerAnim.SetBool("Sliding", false);
    }
    IEnumerator DelatPos()
    {
        yield return new WaitForSeconds(1.10f);
        SlidingCollider.transform.DOLocalMove(new Vector3(0, 0.9f, 0),1);
    }
    public void CalculateParentMovement(float frontback,float leftright)
    {
        if(State== "Front" && !dead)
        {
            frontback = frontback + Time.deltaTime * speed;
            Parent.transform.DOLocalMoveZ(frontback,1f);
            Parent.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
        }
        else if (State == "Back" && !dead)
        {
            frontback = frontback - Time.deltaTime * speed;
            Parent.transform.DOLocalMoveZ(frontback, 1f);
            Parent.transform.DOLocalRotate(new Vector3(0, -180, 0), 1);
        }
        else if (State == "Left" && !dead)
        {
            leftright = leftright - Time.deltaTime * speed;
            Parent.transform.DOLocalMoveX(leftright, 1f);
            Parent.transform.DOLocalRotate(new Vector3(0, -90, 0), 1);
        }
        else if(State == "Right" && !dead)
        {
            leftright = leftright + Time.deltaTime * speed;
            Parent.transform.DOLocalMoveX(leftright, 1f);
            Parent.transform.DOLocalRotate(new Vector3(0, 90, 0), 1);
        }
    }
    public void ChangeState(string str)
    {
        //State = nextTransformPosition.transform.tag;
        if (str == "Left")
        {
            Parent.transform.DORotate(new Vector3(0f, -90f, 0f), 0.5f).SetRelative();
            Parent.transform.DOMove(nextTransformPosition.transform.position, 0.35f);
        }
        else if(str == "Right")
        {
            Parent.transform.DORotate(new Vector3(0f, 90f, 0f), 0.5f).SetRelative();
            Parent.transform.DOMove(nextTransformPosition.transform.position, 0.35f);
        }
        ChangingPlatform = false;
        
    }
    public void EnteredPlatformTrigger(GameObject obj)
    {
        nextTransformPosition = obj;
        ChangingPlatform = true;
        //switch(State)
        //{
        //    case "Front":
        //        if (nextTransformPosition.transform.tag == "Left")
        //        {
        //            CurrentRespectiveState = "Left";
        //        }
        //        if (nextTransformPosition.transform.tag == "Right")
        //        {
        //            CurrentRespectiveState = "Right";
        //        }
        //        break;
        //    case "Back":
        //        if (nextTransformPosition.transform.tag == "Left")
        //        {
        //            CurrentRespectiveState = "Right";
        //        }
        //        if (nextTransformPosition.transform.tag == "Right")
        //        {
        //            CurrentRespectiveState = "Left";
        //        }
        //        break;
        //    case "Left":
        //        if (nextTransformPosition.transform.tag == "Front")
        //        {
        //            CurrentRespectiveState = "Right";
        //        }
        //        if (nextTransformPosition.transform.tag == "Back")
        //        {
        //            CurrentRespectiveState = "Left";
        //        }
        //        break;
        //    case "Right":
        //        if (nextTransformPosition.transform.tag == "Front")
        //        {
        //            CurrentRespectiveState = "Left";
        //        }
        //        if (nextTransformPosition.transform.tag == "Back")
        //        {
        //            CurrentRespectiveState = "Right";
        //        }
        //        break;
        //}
    }
    public void Invisibility()
    {
        InvisibilityBool = true;
    }
    public void SuperJump()
    {
        JumpForce = 350;
    }
    public void SuperSpeed()
    {
        speed = 50;
        SuperSpeedBool = true;
    }
    public void SlowingDown()
    {
        speed = 10;
    }
    public void Teleportation()
    {

    }
    public void LaserGoggles()
    {
        LaserToUse.SetActive(true);
    }
    public void SuperSpeedTurn(GameObject NextPosition)
    {
        //gameObject.GetComponent<PlayerController>().State = NextPosition.transform.tag;
        Parent.transform.DOLocalMove(NextPosition.transform.position, 1);
    }
}
