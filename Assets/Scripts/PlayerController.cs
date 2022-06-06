using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour
{
    private delegate void voidCallBack();
    public Material Normal, Transparent;
    public GameObject rendererRef;
    public Animator PlayerAnim;
    public Rigidbody rb;
    public bool isGrounded, isMoved;
    private float GroundedTime;
    Vector2 initialTouchPosition, endTouchPosition;
    float xDifference, yDifference;
    public GameObject SlidingCollider;
    public GameObject Parent;
    public float speed, IncreasedSpeed; //CurrentSpeed
    float Jumpforce;
  public  int Line = 0;
    bool changingline = false;
    public bool ChangingPlatform = false;
    GameObject nextTransformPosition;
    public bool dead = false;
    public string CurrentRespectiveState;
    bool hasSwiped;
    public bool InvisibilityBool;
    public bool RespawnInvisibilityBool;
    public bool SuperSpeedBool;
    public GameObject LaserToUse;
    public float speedTimer;
    bool isCollidingEnter = false;
    bool isCollidingExit = false;
    public bool powerUpInUse = false;
    public bool AlreadyHit = false;
    public GameObject Monster;
    bool slide = false;
    public GameObject Portal;
    public PUButtonsHandler PUButtonhandlerRef;
    public Animator MonsterAnim;

    string TurnTag;

    public GameObject Boy, Girl;
    public Avatar BoyAvatar, GirlAvatar;

    public GameObject LaserEffect, GravityEffect, motionEffect;
    public Transform PlayerRespawnTransform;

    //public int TotalTimeSpend = 0;
    public Vector3 PlayerLastStoredTransform;

    Coroutine ResetRef;
    public bool PortalUse = false;
    public float portalDistanceMultiplier = 0.5f;
    public int portaldistance = 0;
    public int SuperSpeedDistance = 0;


    public void SettingBoy()
    {
        
        Boy.SetActive(true);
        Girl.SetActive(false);
        PlayerAnim.avatar = BoyAvatar;
        Boy.GetComponent<CharacterCustomizer>().ChangeData(GameConstants.SelectedPlayerForGame);
    }
    public void SettingGirl()
    {
        Girl.SetActive(true);
        Boy.SetActive(false);
        PlayerAnim.avatar = GirlAvatar;
        Girl.GetComponent<CharacterCustomizer>().ChangeData(GameConstants.SelectedPlayerForGame);

    }

    void Start()
    {
        SoundManager.instance.ASBg.Play();
        motionEffect = Camera.main.gameObject.transform.GetChild(0).gameObject;
        AlreadyHit = false;
        powerUpInUse = false;
        InvisibilityBool = false;
        SuperSpeedBool = false;
        
        Jumpforce = 500;
        GroundedTime = 0.8f;
        speed = 10;
        IncreasedSpeed = speed;
        isGrounded = true;
        rb = gameObject.GetComponent<Rigidbody>();
        InvokeRepeating("TotalTimeCount", 1, 1);
        //InvokeRepeating("StoreLastPlayerPosition", 5, 1);
    }

    //public void StoreLastPlayerPosition()
    //{
    //    PlayerLastStoredTransform = Parent.transform.localPosition;
    //}

    public void TotalTimeCount()
    {
        GameManager.instance.TotalTimeSpend = GameManager.instance.TotalTimeSpend + 1;
    }

    public void CancelFunctionsInvoke()
    {
        CancelInvoke("TotalTimeCount");
        //CancelInvoke("StoreLastPlayerPosition");
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
            speed += speed * 0.1f;
            IncreasedSpeed = speed;
            speedTimer = 0;
        }
        if (!dead && GameConstants.IsPaused == false)
        {
            GameManager.instance.TotalDIstanceCovered += ((int)speed * GameManager.instance.TotalTimeSpend);
        }
        #endregion

        #region Keyboard Input
        //Keyboard Input
        if (Input.GetKeyDown(KeyCode.DownArrow)  && !ChangingPlatform && !slide && !dead)
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
        
        if (Input.GetKey(KeyCode.LeftArrow) && ChangingPlatform && !dead  && TurnTag == "Left")
        {
            ChangeState("Left");
        }
        if (Input.GetKey(KeyCode.RightArrow) && ChangingPlatform && !dead && TurnTag == "Right")
        {
            ChangeState("Right");
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
            hasSwiped = true;
        }
        if (hasSwiped)
        {
            hasSwiped = false;
            endTouchPosition = Input.GetTouch(0).position;
            xDifference = initialTouchPosition.x - endTouchPosition.x;
            yDifference = initialTouchPosition.y - endTouchPosition.y;
            if (Mathf.Abs(xDifference) > 8 || Mathf.Abs(yDifference) > 8)
            {
                isMoved = true;
                if (Mathf.Abs(xDifference) >= Mathf.Abs(yDifference))
                {
                    //move left or right
                    if (xDifference > 0 && ChangingPlatform && !dead)
                    {
                        ChangeState("Left");
                    }
                    else if (xDifference < 0 && ChangingPlatform && !dead)
                    {
                        ChangeState("Right");
                    }
                }
                else
                {
                    //jump or slide
                    if (yDifference > 0)
                    {
                        if(!slide && !dead && isGrounded)
                        {
                            Down();
                        }
                    }
                    else
                    {
                        if(isGrounded && !ChangingPlatform && !dead)
                        {
                            Up();
                        }
                    }
                }

            }
        }
        #endregion

        #region Parent Movement
        float x = Parent.transform.position.x;
        float z = Parent.transform.position.z;
       
        if (!dead)
        {
            Parent.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime), Space.Self);
        }
        if ((gameObject.transform.localPosition.z > 0.0001 || gameObject.transform.localPosition.z < -0.0001) && !dead)
        {
            gameObject.transform.DOLocalMoveZ(0, 0.01f);
        }

        #endregion

        #region Gyro Movement
        if (Input.acceleration.x != 0 && !dead)
        {
            if (Mathf.Abs(Input.acceleration.x) > 0.07)
            {
                transform.DOLocalMoveX(Mathf.Clamp((Input.acceleration.x) * Time.deltaTime * 120, -1.2f, 1.2f), 1.0f);
                if (!InvisibilityBool)
                {
                    Monster.transform.DOLocalMoveX(Mathf.Clamp((Input.acceleration.x) * Time.deltaTime * 120, -1.2f, 1.2f), 1.0f);
                }
            }
        }
        #endregion


    }

    public void RespwanPlayer()
    {
        dead = false;
        RespawnInvisibility();
        DisablePowerUps();
        ChangingPlatform = false;
        //changingline = false;
        //Line = 0;
        AlreadyHit = false;
        powerUpInUse = false;
        InvisibilityBool = false;
        SuperSpeedBool = false;
        Jumpforce = 500;
        GroundedTime = 0.8f;
        speed = IncreasedSpeed;
        isGrounded = true;
        PlayerAnim.SetBool("Running", true);
        PlayerAnim.SetBool("Death", false);
        AlreadyHit = false;
        // Parent.transform.localPosition = new Vector3(Parent.transform.localPosition.x, Parent.transform.localPosition.y, Parent.transform.localPosition.z + 10);
        Parent.transform.localPosition = (Parent.transform.forward * -20f) + Parent.transform.localPosition;
        
        //transform.DOLocalMoveX(0, 0.1f);
        //Monster.transform.DOLocalMoveX(0, 0.1f);
        FollowPlayer.lookatspeed = 1;
        
        MonsterAnim.SetBool("Attack", false);
        InvokeRepeating("TotalTimeCount", 1, 1);
        if (ResetRef != null)
        {
            StopCoroutine(ResetRef);

        }
        // InvokeRepeating("StoreLastPlayerPosition", 5, 1);
    }
    public void MonsterAttackAnim()
    {
        MonsterAnim.SetBool("Attack", true);
    }
    //Player dummy collider to check if its on ground
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.transform.tag)
        {  
            case "Wall":
                if (dead == false && PortalUse == false)
                {
                    PlayerRespawnTransform.SetParent(null);
                    dead = true;
                    if (GameConstants.CharacterType == "boy")
                    {
                        SoundManager.instance.ASPlayer.clip = SoundManager.instance.BoyDeathClip;
                        SoundManager.instance.ASPlayer.Play();
                    }
                    else
                    {
                        SoundManager.instance.ASPlayer.clip = SoundManager.instance.GirlDeathClip;
                        SoundManager.instance.ASPlayer.Play();
                    }
                    MonsterAnim.SetBool("Attack", true);
                    PlayerAnim.SetBool("Running", false);
                    PlayerAnim.SetBool("Death", true);
                    MonsterMovement(2);
                    GameManager.instance.CurrentLives -= 1;
                    if (GameManager.instance.CurrentLives < 1)
                    {
                        if (GameConstants.GameType == "Tournament")
                        {
                            APIManager.instance.PostTournamentResultApi(GameConstants.JoinedTournamentId, (GameManager.instance.TotalDIstanceCovered / 10000).ToString(), GameManager.instance.TotalTimeSpend.ToString(), GameManager.instance.CollectedChillis.ToString());
                            Debug.Log(GameConstants.JoinedTournamentId + GameManager.instance.TotalDIstanceCovered + GameManager.instance.TotalTimeSpend + GameManager.instance.CollectedChillis);
                        }
                        Invoke("LoadSceneDelayCall", 3f);
                    }
                    else
                    {
                        DisablePowerUps();
                        Invoke("RespwanPlayer", 3f);
                        StartCoroutine("StumbleWaitWall");
                    }
                    
                }
                break;
        }
       
    }

    public void LoadSceneDelayCall()
    {
        CancelFunctionsInvoke();

        UIManager.instance.GameOverPanelChilliCountText.text = GameManager.instance.CollectedChillis.ToString();
        UIManager.instance.GameOverPanelTimeHourText.text = (TimeSpan.FromSeconds(GameManager.instance.TotalTimeSpend).Hours).ToString("00");
        UIManager.instance.GameOverPanelTimeMinuteText.text = (TimeSpan.FromSeconds(GameManager.instance.TotalTimeSpend).Minutes).ToString("00");
        UIManager.instance.GameOverPanelTimeSecondsText.text = (TimeSpan.FromSeconds(GameManager.instance.TotalTimeSpend).Seconds).ToString("00");
        UIManager.instance.GameOverPanelDistanceCoveredText.text = (GameManager.instance.TotalDIstanceCovered / 10000).ToString();
        UIManager.instance.GameOverPanel.SetActive(true);
        if (GameConstants.GameType == "Tournament")
        {
            UIManager.instance.HomeButton.SetActive(false);
            UIManager.instance.RestartButton.SetActive(false);
            UIManager.instance.GameOverHomeButton.SetActive(true);
        }
        else
        {
            UIManager.instance.HomeButton.SetActive(true);
            UIManager.instance.RestartButton.SetActive(true);
            UIManager.instance.GameOverHomeButton.SetActive(false);
        }
            //SceneManager.LoadScene(0);
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
                if(!InvisibilityBool)
                {
                    Monster.transform.DOLocalMoveX(gameObject.transform.localPosition.x - 1, 0.1f);
                    PlayerRespawnTransform.transform.DOLocalMoveX(gameObject.transform.localPosition.x - 1, 0.1f);
                }
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
                if (!InvisibilityBool)
                {
                    Monster.transform.DOLocalMoveX(gameObject.transform.localPosition.x - 1, 0.1f);
                    PlayerRespawnTransform.transform.DOLocalMoveX(gameObject.transform.localPosition.x - 1, 0.1f);
                }
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
                if (!InvisibilityBool)
                {
                    Monster.transform.DOLocalMoveX(gameObject.transform.localPosition.x + 1, 0.1f);
                    PlayerRespawnTransform.transform.DOLocalMoveX(gameObject.transform.localPosition.x + 1, 0.1f);
                }
                changingline = true;
                Invoke("LineChnaged", 0.1f);
                Line = 1;
                break;
            case -1:
                if (Line == 0)
                    break;
                transform.DOLocalMoveX(gameObject.transform.localPosition.x + 1, 0.1f);
                if (!InvisibilityBool)
                {
                    Monster.transform.DOLocalMoveX(gameObject.transform.localPosition.x + 1, 0.1f);
                    PlayerRespawnTransform.transform.DOLocalMoveX(gameObject.transform.localPosition.x + 1, 0.1f);
                }
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
        if (Jumpforce == 650)
        {
            GravityEffect.SetActive(true);
            SlideDelayCall();
            //StartCoroutine("gravityFunction");
            Invoke("gravityFunction", 0.5f);
        }
        SlidingCollider.transform.localPosition = new Vector3(0, 0.9f, 0);
        isGrounded = false;
        PlayerAnim.SetBool("Jump", true);
        if(GameConstants.CharacterType == "boy")
        {
            SoundManager.instance.ASPlayer.clip = SoundManager.instance.BoyJumpClip;
            SoundManager.instance.ASPlayer.Play();
        }
        else
        {
            SoundManager.instance.ASPlayer.clip = SoundManager.instance.GirlJumpClip;
            SoundManager.instance.ASPlayer.Play();
        }
        PlayerAnim.SetBool("Running", false);
        PlayerAnim.SetBool("Sliding", false);
        rb.AddForce((new Vector3(0f, 2f, 0f)) * Jumpforce);
        Invoke("DelayCall", GroundedTime);
    }
    public void DelayCall()
    {
        PlayerAnim.SetBool("Jump", false);
        PlayerAnim.SetBool("Running", true);
        isGrounded = true;
        if (Jumpforce == 650)
        {
            Invoke("gravityDownFunction", 0.3f);
        }
    }
    public void gravityFunction()
    {
        GravityEffect.SetActive(false);
    }
    public void gravityDownFunction()
    {
        GravityEffect.SetActive(true);
        Invoke("gravityFunction", 0.5f);
    }

    public void Down()
    {
        slide = true;
        SlidingCollider.transform.DOLocalMove(new Vector3(0, 0.1f, 0), 1);
        transform.DOLocalMoveY(0, 0.05f);
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
        if (str == "Left" && TurnTag == "Left")
        {
            Parent.transform.DORotate(new Vector3(0f, -90f, 0f), 0.10f).SetRelative();
            Parent.transform.DOMove(nextTransformPosition.transform.position, 0.10f);
        }
        else if (str == "Right" && TurnTag == "Right")
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
    public void EnteredPlatformTrigger(GameObject obj,string tag)
    {
        nextTransformPosition = obj;
        TurnTag = tag;
        ChangingPlatform = true;
        FollowPlayer.lookatspeed = 0.2f;
    }
    #region PowerUps

    public void RespawnInvisibility()
    {
        RespawnInvisibilityBool = true;
        if (GameConstants.CharacterType == "boy")
        {
            StartCoroutine(OnRespawnInvisibilityofBoy());
        }
        else
        {
            StartCoroutine(OnRespawnInvisibilityofGirl());
        }
        Invoke("RespawnInvisibilityDelayCall", 0.5f);
    }

    public void RespawnInvisibilityDelayCall()
    {
        if (GameConstants.CharacterType == "boy")
        {
            StopCoroutine(OnRespawnInvisibilityofBoy());
        }
        else
        {
            StopCoroutine(OnRespawnInvisibilityofGirl());
        }
        RespawnInvisibilityBool = false;


    }

    IEnumerator OnRespawnInvisibilityofBoy()
    {
        float re = 0;

        while (re < 0.5 && !dead)
        {
            Boy.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            Boy.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            re += 0.1f;
        }

    }

    IEnumerator OnRespawnInvisibilityofGirl()
    {
        float re = 0;
        
        while (re < 0.5 && !dead)
        {
            Girl.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            Girl.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            re += 0.1f;
        }

    }

    public void Invisibility()
    {
        if (!powerUpInUse)
        {
            if (!dead)
            {
                InvisibilityBool = true;
                rendererRef.GetComponent<SkinnedMeshRenderer>().material = Transparent;
                rendererRef.GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                ResetRef = StartCoroutine(ResetPowerUp(Invisibility));
            }
        }
        else
        {
            powerUpInUse = false;
            InvisibilityBool = false;
            rendererRef.GetComponent<SkinnedMeshRenderer>().material = Normal;
            rendererRef.GetComponent<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }
    public void SuperJump()
    {
        if (!powerUpInUse)
        {
            if (!dead)
            {
                Jumpforce = 650;
                GroundedTime = 0.89f;
                ResetRef = StartCoroutine(ResetPowerUp(SuperJump));
            }
        }
        else
        {
            GroundedTime = 0.8f;
            powerUpInUse = false;
            Jumpforce = 500;
        }

    }
    public void SuperSpeed()
    {
        if (!powerUpInUse)
        {
            if (!dead)
            {
                MonsterMovement(0);
                motionEffect.SetActive(true);
                PlayerAnim.SetFloat("RunningSpeed", 2);
                //CurrentSpeed = speed;
                speed = speed * 4;
                SuperSpeedBool = true;
                ResetRef = StartCoroutine(ResetPowerUp(SuperSpeed));
            }
        }
        else
        {
            motionEffect.SetActive(false);
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
            if (!dead)
            {
                //CurrentSpeed = speed;
                speed = speed / 2;
                PlayerAnim.SetFloat("RunningSpeed", 0.7f);
                ResetRef = StartCoroutine(ResetPowerUp(SlowingDown));
            }
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
        if(!powerUpInUse)
        {
            if (!dead)
            {
                PortalUse = true;
                if (SpawnManager.instance.os != null)
                {
                    SpawnManager.instance.os.DestroyChillies();
                    SpawnManager.instance.os.DestroyHurdles();
                }
                Portal.transform.GetChild(0).gameObject.SetActive(true);
                Portal.transform.SetParent(null);
                ChangingPlatform = false;
            }
            
            //GameObject teleportPortal = Instantiate(Portal, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z - 15f), this.gameObject.transform.rotation);
            //PlayerAnim.SetFloat("RunningSpeed", 3);
            //CurrentSpeed = speed;
            //speed = speed * 20;
            //SuperSpeedBool = true;
            //StartCoroutine("TeleportReset");
        }
        else
        {
            PlayerAnim.SetFloat("RunningSpeed", 1.2f);
            powerUpInUse = false;
            speed = speed / 20;
            SuperSpeedBool = false;
        }
    }
    public void LaserGoggles()
    {
        if (!powerUpInUse)
        {
            if (!dead)
            {
                LaserToUse.SetActive(true);
                LaserEffect.SetActive(true);
                ResetRef = StartCoroutine(ResetPowerUp(LaserGoggles));
            }
        }
        else
        {
            powerUpInUse = false;
            LaserToUse.SetActive(false);
            LaserEffect.SetActive(false);
        }
    }
    #endregion
    public void SuperSpeedTurn(GameObject NextPosition, string currentTag)
    {
        nextTransformPosition = NextPosition;
        FollowPlayer.lookatspeed = 0.2f;
        ChangingPlatform = true;
        TurnTag = currentTag;
        ChangeState(currentTag);
    }
    public void DisablePowerUps()
    {
        InvisibilityBool = false;
        rendererRef.GetComponent<SkinnedMeshRenderer>().material = Normal;
        Jumpforce = 500;
        GroundedTime = 0.8f;
        PlayerAnim.SetFloat("RunningSpeed", 1.2f);
        SuperSpeedBool = false;
        PlayerAnim.SetFloat("RunningSpeed", 1.2f);
        LaserToUse.SetActive(false);
        motionEffect.SetActive(false);
        LaserEffect.SetActive(false);
        GravityEffect.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enter")
        {
            other.gameObject.transform.GetComponentInParent<ObjectSpawner>().SpawnNextPatchObjects();
        }

        if (other.gameObject.tag == "portal")
        {
            portalDistanceMultiplier = 2;
            FollowPlayer.lookatspeed = 0;
            int randompoint = UnityEngine.Random.Range(0, GameManager.instance._playerSpawnPoints.Length);
            Parent.transform.position = GameManager.instance._playerSpawnPoints[randompoint].position;
            Parent.transform.rotation = GameManager.instance._playerSpawnPoints[randompoint].rotation;
            Portal.transform.SetParent(Parent.transform);
            Portal.transform.localPosition = new Vector3(0f, 0f, 0f);
            Portal.transform.localRotation = Quaternion.EulerAngles(0, 0, 0);
            Portal.transform.GetChild(0).gameObject.SetActive(false);
            Portal.transform.GetChild(1).gameObject.SetActive(true);
            Portal.transform.SetParent(null);
            Invoke("DelayPortalCall", 1f);
        }
        switch (other.gameObject.tag)
        {
            case "GreenChilli":
                SoundManager.instance.ASChillies.clip = SoundManager.instance.GreenChilliClip;
                SoundManager.instance.ASChillies.Play();
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                Destroy(other.gameObject);
                GameManager.instance.CollectedChillis = GameManager.instance.CollectedChillis + GameConstants.GreenChilliCount;
                break;
            case "RedChilli":
                SoundManager.instance.ASChillies.clip = SoundManager.instance.RedChilliClip;
                SoundManager.instance.ASChillies.Play();
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                Destroy(other.gameObject);
                GameManager.instance.CollectedChillis = GameManager.instance.CollectedChillis + GameConstants.RedChilliCount;
                if (GameManager.instance.CollectedChillis < 0)
                {
                    GameManager.instance.CollectedChillis = 0;
                }
                break;
            case "GoldenChilli":
                SoundManager.instance.ASChillies.clip = SoundManager.instance.GoldenChilliClip;
                SoundManager.instance.ASChillies.Play();
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                Destroy(other.gameObject);
                GameManager.instance.CollectedChillis = GameManager.instance.CollectedChillis + GameConstants.GoldenChilliCount;
                break;
            case "BlueChilli":
                SoundManager.instance.ASChillies.clip = SoundManager.instance.BlueChilliClip;
                SoundManager.instance.ASChillies.Play();
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                Destroy(other.gameObject);
                if (GameManager.instance.CurrentLives < GameConstants.PlayerLives)
                {
                    GameManager.instance.CurrentLives = GameManager.instance.CurrentLives + GameConstants.BlueChilliCount;
                }
                break;
            case "invisibility":
                if (!powerUpInUse)
                {
                    SoundManager.instance.ASChillies.clip = SoundManager.instance.PowerUpClip;
                    SoundManager.instance.ASChillies.Play();
                    SpawnManager.instance.IsInvisible = true;
                    other.gameObject.GetComponent<BoxCollider>().enabled = false;
                    Destroy(other.gameObject);
                    //Invisibility();
                    PUButtonhandlerRef.setPowerUpInvisibility(0);
                }
                break;

        }
    }

    public void DelayPortalCall()
    {
        portalDistanceMultiplier = 1;
        Portal.transform.SetParent(Parent.transform);
        Portal.transform.localPosition = new Vector3(0f, 0f, 0f);
        Portal.transform.localRotation = Quaternion.EulerAngles(0, 0, 0);
        Portal.transform.GetChild(0).gameObject.SetActive(false);
        Portal.transform.GetChild(1).gameObject.SetActive(false);
        FollowPlayer.lookatspeed = 1f;
        PortalUse = false;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Exit")
        {
            other.gameObject.transform.GetComponentInParent<ObjectSpawner>().DestroyChillies();
            other.gameObject.transform.GetComponentInParent<ObjectSpawner>().DestroyHurdles();
        }
    }

    IEnumerator ResetPowerUp(voidCallBack callBackMethod)
    {
        powerUpInUse = true;
        yield return new WaitForSeconds(10);
        callBackMethod();
        yield return new WaitForSeconds(20);
        SpawnManager.instance.IsInvisible = false;
    }
    IEnumerator StumbleWait()
    {
        yield return new WaitForSeconds(2);
        AlreadyHit = true;
        yield return new WaitForSeconds(7);
        AlreadyHit = false;
    }

    public void MonsterMovement(int i)
    {
        if (i == 0)
        {
            Monster.transform.DOLocalMoveZ(-10, 0.5f);
        }
        else if (i == 1)
        {
            Monster.transform.DOLocalMoveZ(-2.5f, 0.5f);
        }
        else
        {
            Monster.transform.DOLocalMoveZ(-2.5f, 0.5f);
        }
    }
    IEnumerator TeleportReset()
    {
        powerUpInUse = true;
        yield return new WaitForSeconds(3);
        Teleportation();
    }
    IEnumerator StumbleWaitWall()
    {
        yield return new WaitForSeconds(3);
        MonsterMovement(1);
        yield return new WaitForSeconds(3);
        MonsterMovement(0);
    }
}
