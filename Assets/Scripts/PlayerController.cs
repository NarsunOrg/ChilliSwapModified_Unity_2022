﻿using System.Collections;
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
    float Jumpforce;
    int Line = 0;
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
    public GameObject Monster;
    bool slide = false;

    // Start is called before the first frame update
    void Start()
    {
        AlreadyHit = false;
        powerUpInUse = false;
        InvisibilityBool = false;
        SuperSpeedBool = false;
        Jumpforce = 250;
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
        if (speedTimer < 20)
        {
            speedTimer += Time.deltaTime;
        }
        else
        {
            speed += speed * 0.05f;
            speedTimer = 0;
        }
        #endregion

        #region Keyboard Input
        //Keyboard Input
        if (Input.GetKeyDown(KeyCode.DownArrow) && isGrounded && !ChangingPlatform && !slide && !dead)
        {
            Down();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded && !ChangingPlatform && !dead)
        {
            Up();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && changingline == false && !ChangingPlatform && !dead)
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && changingline == false && !ChangingPlatform && !dead)
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
        if (Input.GetKey(KeyCode.LeftArrow) && ChangingPlatform && !dead)
        {
            ChangeState("Left");
        }
        if (Input.GetKey(KeyCode.RightArrow) && ChangingPlatform && !dead)
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
        if (hasSwiped)
        {
            hasSwiped = false;
            endTouchPosition = Input.GetTouch(0).position;
            xDifference = initialTouchPosition.x - endTouchPosition.x;
            yDifference = initialTouchPosition.y - endTouchPosition.y;
            if (Mathf.Abs(xDifference) > 10 || Mathf.Abs(yDifference) > 10)
            {
                isMoved = true;
                if (Mathf.Abs(xDifference) >= Mathf.Abs(yDifference))
                {
                    //move left or right
                    if (xDifference > 0 && ChangingPlatform)
                    {
                        ChangeState("Left");
                    }
                    else if (xDifference < 0 && ChangingPlatform)
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
        if (!dead)
        {
            Parent.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime), Space.Self);
           
        }

        #endregion

        #region Gyro Movement
        if (Input.acceleration.x != 0)
        {
            if (Mathf.Abs(Input.acceleration.x) > 0.05)
            {
                transform.DOLocalMoveX(Mathf.Clamp(Input.acceleration.x * Time.deltaTime * 120, -1, 1), 0.35f);
            }
        }
        #endregion
    }

    public void RespwanPlayer()
    {
        ChangingPlatform = false;
        changingline = false;
        Line = 0;
        State = "Front";
        middle = true;
        left = false;
        right = false;
        isGrounded = true;
        PlayerAnim.SetBool("Running", true);
        PlayerAnim.SetBool("Death", false);

        AlreadyHit = false;
        int randompoint = Random.Range(0, GameManager.instance._playerSpawnPoints.Length);
        Parent.transform.position = GameManager.instance._playerSpawnPoints[randompoint].position;
        Parent.transform.rotation = GameManager.instance._playerSpawnPoints[randompoint].rotation;
        transform.DOLocalMoveX(0, 0.1f);
        FollowPlayer.lookatspeed = 1;
        dead = false;
    }
    //Player dummy collider to check if its on ground
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collideddd");
        switch (collision.transform.tag)
        {
            case "Floor":
                //isGrounded = true;
                break;
            case "Wall":
                if (dead == false)
                {
                    dead = true;
                    PlayerAnim.SetBool("Running", false);
                    PlayerAnim.SetBool("Death", true);
                    GameManager.instance.CurrentLives -= 1;
                    if (GameManager.instance.CurrentLives < 1)
                    {
                        SceneManager.LoadScene(0);
                    }
                    else
                    {
                        Invoke("RespwanPlayer", 3f);
                    }
                    //Invoke("PanelDelayCall", 2f);
                }
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
        switch (Line)
        {

            case 0:
                if (Line == -1)
                    break;
                transform.DOLocalMoveX(gameObject.transform.localPosition.x - 1, 0.1f);
                changingline = true;
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
        PlayerAnim.SetBool("Jump", true);
        PlayerAnim.SetBool("Running", false);
        PlayerAnim.SetBool("Sliding", false);
        rb.AddForce((new Vector3(0f, 2f, 0f)) * Jumpforce);
        Invoke("DelayCall", 0.8f);
    }
    public void DelayCall()
    {
        PlayerAnim.SetBool("Jump", false);
        PlayerAnim.SetBool("Running", true);
        isGrounded = true;
    }

    public void Down()
    {
        slide = true;
        SlidingCollider.transform.DOLocalMove(new Vector3(0, 0.1f, 0), 1);
        transform.DOLocalMoveY(0, 0.25f);
        PlayerAnim.SetBool("Sliding", true);
        PlayerAnim.SetBool("Jump", false);
        PlayerAnim.SetBool("Running", false);
        Invoke("SlideDelayCall", 0.9f);
        
    }

    public void SlideDelayCall()
    {
        SlidingCollider.transform.DOLocalMove(new Vector3(0, 0.9f, 0), 1);
        PlayerAnim.SetBool("Sliding", false);
        PlayerAnim.SetBool("Running", true);
        slide = false;
    }
   
    public void ChangeState(string str)
    {
        //State = nextTransformPosition.transform.tag;
        if (str == "Left")
        {
            Parent.transform.DORotate(new Vector3(0f, -90f, 0f), 0.10f).SetRelative();
            Parent.transform.DOMove(nextTransformPosition.transform.position, 0.10f);
        }
        else if (str == "Right")
        {
            Parent.transform.DORotate(new Vector3(0f, 90f, 0f), 0.10f).SetRelative();
            Parent.transform.DOMove(nextTransformPosition.transform.position, 0.10f);
        }
        ChangingPlatform = false;

        Invoke("resetfollowspeed", 1.5f);
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
    }
    #region PowerUps

    public void Invisibility()
    {
        if (!powerUpInUse)
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
            Jumpforce = 300;
            StartCoroutine(ResetPowerUp(SuperJump));
        }
        else
        {
            powerUpInUse = false;
            Jumpforce = 250;
        }

    }
    public void SuperSpeed()
    {
        if (!powerUpInUse)
        {
            PlayerAnim.SetFloat("RunningSpeed", 2);
            speed = speed * 4;
            SuperSpeedBool = true;
            StartCoroutine(ResetPowerUp(SuperSpeed));
        }
        else
        {
            PlayerAnim.SetFloat("RunningSpeed", 1.2f);
            powerUpInUse = false;
            speed = speed / 4;
            SuperSpeedBool = false;
        }
    }
    public void SlowingDown()
    {
        if (!powerUpInUse)
        {
            speed = speed / 2;
            PlayerAnim.SetFloat("RunningSpeed", 0.7f);
            StartCoroutine(ResetPowerUp(SlowingDown));
        }
        else
        {
            powerUpInUse = false;
            speed = speed * 2;
            PlayerAnim.SetFloat("RunningSpeed", 1.2f);
        }
    }
    public void Teleportation()
    {

    }
    public void LaserGoggles()
    {
        if (!powerUpInUse)
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
    #endregion
    public void SuperSpeedTurn(GameObject NextPosition, GameObject currentTag)
    {
        //gameObject.GetComponent<PlayerController>().State = NextPosition.transform.tag;
        nextTransformPosition = NextPosition;
        FollowPlayer.lookatspeed = 0.001f;
        ChangingPlatform = true;
        ChangeState(currentTag.transform.tag);
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
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                Destroy(other.gameObject);
                GameManager.instance.CollectedChillis = GameManager.instance.CollectedChillis + GameConstants.GreenChilliCount;
                break;
            case "RedChilli":
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                Destroy(other.gameObject);
                GameManager.instance.CollectedChillis = GameManager.instance.CollectedChillis + GameConstants.RedChilliCount;
                if (GameManager.instance.CollectedChillis < 0)
                {
                    GameManager.instance.CollectedChillis = 0;
                }
                break;
            case "GoldenChilli":
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                Destroy(other.gameObject);
                GameManager.instance.CollectedChillis = GameManager.instance.CollectedChillis + GameConstants.GoldenChilliCount;
                break;
            case "BlueChilli":
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                Debug.Log("blue chilli call");
                Destroy(other.gameObject);
                if (GameManager.instance.CurrentLives < GameConstants.PlayerLives)
                {
                    GameManager.instance.CurrentLives = GameManager.instance.CurrentLives + GameConstants.BlueChilliCount;
                }
                break;
        }
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

    public void DisablePowerUps()
    {
        InvisibilityBool = false;
        rendererRef.GetComponent<SkinnedMeshRenderer>().material = Normal;
        Jumpforce = 250;
        PlayerAnim.SetFloat("RunningSpeed", 1.2f);
        speed = speed / 4;
        SuperSpeedBool = false;
        speed = speed * 2;
        PlayerAnim.SetFloat("RunningSpeed", 1.2f);
        LaserToUse.SetActive(false);
    }
    public void MonsterMovement(int i)
    {
        if (i == 0)
        {
            Monster.transform.DOLocalMoveZ(-5, 0.5f);
        }
        else if (i == 1)
        {
            Monster.transform.DOLocalMoveZ(-2, 0.5f);
        }
        else
        {
            Monster.transform.DOLocalMoveZ(-1, 0.5f);
        }
    }
}
