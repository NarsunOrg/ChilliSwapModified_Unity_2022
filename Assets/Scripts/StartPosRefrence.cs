﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartPosRefrence : MonoBehaviour
{
    public GameObject NextPosition;
  
    void Start()
    {
        
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.tag == "Player")
    //    {
    //        other.transform.gameObject.GetComponent<PlayerController>().isonturn = true;
    //        if (other.transform.gameObject.GetComponent<PlayerController>().SuperSpeedBool)
    //        {
    //            other.transform.gameObject.GetComponent<PlayerController>().SuperSpeedTurn(NextPosition,this.gameObject.tag);
    //        }
    //        else
    //        {
    //            other.transform.gameObject.GetComponent<PlayerController>().EnteredPlatformTrigger(NextPosition,this.gameObject.tag);
    //        }
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.transform.tag == "Player")
    //    {
    //        other.transform.gameObject.GetComponent<PlayerController>().isonturn = false;
    //    }
    //}

}
