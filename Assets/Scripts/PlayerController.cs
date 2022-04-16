using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private delegate void voidCallBack();
    public Material Normal, Transparent;
    public GameObject rendererRef;
    public Animator PlayerAnim;
    public bool middle, left, right;
    public Rigidbody rb;
    public bool isGrounded, isMoved;
    Vector2 initialTouchPosition, endTouchPosition;
    float xDifference, yDifference;
    public GameObject SlidingCollider;
    public GameObject Parent;
    public string State;
    float speed;
    float JumpPos;
    int Line= 0;
    bool changingline = false;
    public bool ChangingPlatform = false;
    GameObject nextTransformPosition;
    public bool dead = false;
    public string CurrentRespectiveState;
    bool hasSwiped;
    public bool InvisibilityBool;
    public bool SuperSpeedBool;
    public GameObject LaserToUse;
    public float speedTimer;
    bool isCollidingEnter = false;
    bool isCollidingExit = false;
    bool powerUpInUse;
    public bool AlreadyHit = false;
   
    // Start is called before the first frame update
    void Start()
    {
        AlreadyHit = false;
        powerUpInUse = false;
        InvisibilityBool = false;
        SuperSpeedBool = false;
        JumpPos = 1.3f;
        speed = 10;
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
        #region speed
        if(speedTimer < 20)
        {
            speedTimer += Time.deltaTime;
        }
        else
        {
            speed += speed * 0.025f;
            speedTimer = 0;
        }
        #endregion

        #region Keyboard Input
        //Keyboard Input
        if (Input.GetKeyDown(KeyCode.DownArrow) && isGrounded && !ChangingPlatform)
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
        if (Input.GetKey(KeyCode.LeftArrow) && ChangingPlatform)
        {
            ChangeState("Left");
        }
        if (Input.GetKey(KeyCode.RightArrow) && ChangingPlatform)
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
        Debug.Log("collideddd");
        switch(collision.transform.tag)
        {
            case "Floor":
                //isGrounded = true;
                break;
            case "Wall":
                dead = true;
                PlayerAnim.SetTrigger("Death");
                GameManager.instance.CurrentLives -= 1;
                if (GameManager.instance.CurrentLives < 1)
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    //respawn player
                }
                Invoke("PanelDelayCall", 2f);
                break;
           
            //case "Hurdle":
            //    if (!InvisibilityBool && !SuperSpeedBool)
            //    {
            //        if(!AlreadyHit)
            //        {
            //            AlreadyHit = true;
            //            PlayerAnim.SetTrigger("Stumble");
            //            StartCoroutine("StumbleWait");
            //        }
            //        else if(AlreadyHit)
            //        {
            //            dead = true;
            //            PlayerAnim.SetTrigger("Death");
            //        }
            //    }
            //    break;
            case "PowerUpInvisibility":
                if (!powerUpInUse)
                {
                    Invisibility();
                }
                break;
            case "PowerUpJump":
                if (!powerUpInUse)
                {
                    SuperJump();
                }
                break;
            case "PowerUpSpeed":
                if (!powerUpInUse)
                {
                    SuperSpeed();
                }
                break;
            case "PowerUpSlow":
                if (!powerUpInUse)
                {
                    SlowingDown();
                }
                break;
            case "PowerUpTeleportation":
                if (!powerUpInUse)
                {
                    Teleportation();
                }
                break;
            case "PowerUpLaser":
                if (!powerUpInUse)
                {
                    LaserGoggles();
                }
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

    public void PanelDelayCall()
    {
        this.gameObject.GetComponentInChildren<PlayerHittingHurdle>().restartPanel.SetActive(true);
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
                transform.DOLocalMoveX(gameObject.transform.localPosition.x - 1, 0.1f);
                changingline=true;
                Invoke("LineChnaged", 0.1f);
                Line = -1;
                break;
            case -1:
                break;
            case 1:
                if (Line == 0)
                    break;
                transform.DOLocalMoveX(gameObject.transform.localPosition.x - 1, 0.1f);
                changingline = true;
                Invoke("LineChnaged", 0.1f);
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
                transform.DOLocalMoveX(gameObject.transform.localPosition.x + 1, 0.1f);
                changingline = true;
                Invoke("LineChnaged", 0.1f);
                Line = 1;
                break;
            case -1:
                if (Line == 0)
                    break;
                transform.DOLocalMoveX(gameObject.transform.localPosition.x + 1, 0.1f);
                changingline = true;
                Invoke("LineChnaged", 0.1f);
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
         //PlayerAnim.SetTrigger("Jump 0");
         PlayerAnim.SetBool("Jump", true);
         PlayerAnim.SetBool("Running", false);
         PlayerAnim.SetBool("Sliding", false);
        //StartCoroutine("JumpRoutine");

         rb.AddForce((new Vector3(0f, 2f, 0f)) * 250);
         Invoke("DelayCall", 0.9f);
            
         //PlayerAnim.SetBool("Jump", false);
    }
    public void DelayCall()
    {
        PlayerAnim.SetBool("Jump", false);
        PlayerAnim.SetBool("Running", true);
        isGrounded = true;
    }

    public void Down()
    {
        SlidingCollider.transform.DOLocalMove(new Vector3(0, 0.1f, 0),1);
        transform.DOLocalMoveY(0, 0.25f);
        //PlayerAnim.SetTrigger("Sliding 0");
        PlayerAnim.SetBool("Sliding", true);
        PlayerAnim.SetBool("Running", false);
        StartCoroutine("DelatPos");
        //PlayerAnim.SetBool("Sliding", false);
    }
    IEnumerator DelatPos()
    {
        yield return new WaitForSeconds(1.05f);
        SlidingCollider.transform.DOLocalMove(new Vector3(0, 0.9f, 0),1);
        PlayerAnim.SetBool("Sliding", false);
        PlayerAnim.SetBool("Running", true);
    }
    public void CalculateParentMovement(float frontback,float leftright)
    {
        //if(State== "Front" && !dead)
        //{
        //    frontback = frontback + Time.deltaTime * speed;
        //    Parent.transform.DOLocalMoveZ(frontback,1f);
        //    Parent.transform.DOLocalRotate(new Vector3(0, 0, 0), 1);
        //}
        //else if (State == "Back" && !dead)
        //{
        //    frontback = frontback - Time.deltaTime * speed;
        //    Parent.transform.DOLocalMoveZ(frontback, 1f);
        //    Parent.transform.DOLocalRotate(new Vector3(0, -180, 0), 1);
        //}
        //else if (State == "Left" && !dead)
        //{
        //    leftright = leftright - Time.deltaTime * speed;
        //    Parent.transform.DOLocalMoveX(leftright, 1f);
        //    Parent.transform.DOLocalRotate(new Vector3(0, -90, 0), 1);
        //}
        //else if(State == "Right" && !dead)
        //{
        //    leftright = leftright + Time.deltaTime * speed;
        //    Parent.transform.DOLocalMoveX(leftright, 1f);
        //    Parent.transform.DOLocalRotate(new Vector3(0, 90, 0), 1);
        //}
    }
    public void ChangeState(string str)
    {
        //State = nextTransformPosition.transform.tag;
        if (str == "Left")
        {
            Parent.transform.DORotate(new Vector3(0f, -90f, 0f), 0.10f).SetRelative();
            Parent.transform.DOMove(nextTransformPosition.transform.position, 0.10f);
        }
        else if(str == "Right")
        {
            Parent.transform.DORotate(new Vector3(0f, 90f, 0f), 0.10f).SetRelative();
            Parent.transform.DOMove(nextTransformPosition.transform.position, 0.10f);
        }
        ChangingPlatform = false;

        Invoke("resetfollowspeed",1.5f);
    }


    public void resetfollowspeed()
    {
        FollowPlayer.lookatspeed = 1f;
    }
    public void EnteredPlatformTrigger(GameObject obj)
    {
        nextTransformPosition = obj;
        ChangingPlatform = true;
        FollowPlayer.lookatspeed = 0.001f;
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
        if(!powerUpInUse)
        {
            InvisibilityBool = true;
            rendererRef.GetComponent<SkinnedMeshRenderer>().material = Transparent;
            StartCoroutine(ResetPowerUp(Invisibility));
        }
        else
        {
            powerUpInUse = false;
            InvisibilityBool = false;
            rendererRef.GetComponent<SkinnedMeshRenderer>().material = Normal;
        }
    }
    public void SuperJump()
    {
        if (!powerUpInUse)
        {
            JumpPos = 2;
            StartCoroutine(ResetPowerUp(SuperJump));
        }
        else
        {
            powerUpInUse = false;
            JumpPos = 1.3f;
        }
        
    }
    public void SuperSpeed()
    {
        if (!powerUpInUse)
        {
            speed = speed * 2;
            SuperSpeedBool = true;
            StartCoroutine(ResetPowerUp(SuperSpeed));
        }
        else
        {
            powerUpInUse = false;
            speed = speed / 2;
            SuperSpeedBool = false;
        }
    }
    public void SlowingDown()
    {
        if(!powerUpInUse)
        {
            speed = speed / 2;
            PlayerAnim.SetFloat("RunningSpeed", 0.7f);
            StartCoroutine(ResetPowerUp(SlowingDown));
        }
        else
        {
            powerUpInUse = false;
            speed = speed * 2;
            PlayerAnim.SetFloat("RunningSpeed", 1.5f);
        }
    }
    public void Teleportation()
    {

    }
    public void LaserGoggles()
    {
        if(!powerUpInUse)
        {
            LaserToUse.SetActive(true);
            StartCoroutine(ResetPowerUp(LaserGoggles));
        }
        else
        {
            powerUpInUse = false;
            LaserToUse.SetActive(false);
        }
    }
    public void SuperSpeedTurn(GameObject NextPosition,GameObject currentTag)
    {
        //gameObject.GetComponent<PlayerController>().State = NextPosition.transform.tag;
        nextTransformPosition = NextPosition;
        FollowPlayer.lookatspeed = 0.001f;
        ChangingPlatform = true;
        ChangeState(currentTag.transform.tag);
    }
    IEnumerator JumpRoutine()
    {
        transform.DOLocalMoveY(JumpPos,0.65f);
        yield return new WaitForSeconds(0.65f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enter")
        {
            other.gameObject.transform.GetComponentInParent<ObjectSpawner>().SpawnNextPatchObjects();
            Debug.Log("enter");
        }
        switch (other.gameObject.tag)
        {
            case "GreenChilli":
                
                Destroy(other.gameObject);
                GameManager.instance.CollectedChillis += GameConstants.GreenChilliCount;
                break;
            case "RedChilli":
               
                Destroy(other.gameObject);
                GameManager.instance.CollectedChillis += GameConstants.RedChilliCount;
                if (GameManager.instance.CollectedChillis < 0)
                {
                    GameManager.instance.CollectedChillis = 0;
                }
                break;
            case "GoldenChilli":
               
                Destroy(other.gameObject);
                GameManager.instance.CollectedChillis += GameConstants.GoldenChilliCount;
                break;
            case "BlueChilli":
               
                Destroy(other.gameObject);
                if (GameManager.instance.CurrentLives < GameConstants.PlayerLives)
                {
                    GameManager.instance.CurrentLives += GameConstants.BlueChilliCount;
                }
                break;
        }

        //if (other.gameObject.tag == "Hurdle")
        //{
        //    if (!InvisibilityBool && !SuperSpeedBool)
        //    {

        //        if (AlreadyHit)
        //        {
        //            dead = true;
        //            PlayerAnim.SetTrigger("Death");
        //           // Destroy(other.gameObject.transform.parent.gameObject);
        //        }
        //        if (!AlreadyHit)
        //        {

        //            PlayerAnim.SetTrigger("Stumble");
        //            StartCoroutine("StumbleWait");
        //         //   Destroy(other.gameObject.transform.parent.gameObject);
        //        }

        //    }
        //}

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Exit")
        {
            other.gameObject.transform.GetComponentInParent<ObjectSpawner>().DestroyObjects();
            Debug.Log("exit");
        }
    }

    IEnumerator ResetPowerUp(voidCallBack callBackMethod)
    {
        powerUpInUse = true;
        yield return new WaitForSeconds(10);
        callBackMethod();
    }
    IEnumerator StumbleWait()
    {
        yield return new WaitForSeconds(2);
        AlreadyHit = true;
        yield return new WaitForSeconds(7);
        AlreadyHit = false;
    }

}
