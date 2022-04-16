using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHittingHurdle : MonoBehaviour
{
    public PlayerController PC;
    public GameObject restartPanel;

    public void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Hurdle")
        {
            if (!PC.InvisibilityBool && !PC.SuperSpeedBool)
            {

                if (PC.AlreadyHit)
                {
                    PC.dead = true;
                    PC.PlayerAnim.SetTrigger("Death");
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
                    // Destroy(other.gameObject.transform.parent.gameObject);
                }
                if (!PC.AlreadyHit)
                {
                    PC.PlayerAnim.SetTrigger("Stumble");
                    StartCoroutine("StumbleWait");
                    //   Destroy(other.gameObject.transform.parent.gameObject);
                }

            }
        }

        if (other.gameObject.tag == "HurdleDie")
        {
            if (!PC.InvisibilityBool && !PC.SuperSpeedBool)
            {
                PC.dead = true;
                PC.PlayerAnim.SetTrigger("Death");
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
            }
        }

    }

    IEnumerator StumbleWait()
    {
        yield return new WaitForSeconds(2);
        PC.AlreadyHit = true;
        yield return new WaitForSeconds(7);
        PC.AlreadyHit = false;
    }

    public void PanelDelayCall()
    {
        restartPanel.SetActive(true);
    }
}
