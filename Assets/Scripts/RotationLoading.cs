using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLoading : MonoBehaviour
{
    public RectTransform MainIcon;
    public float TimeStep;
    public float OneStepAngle;
    float StartTime;
    
    void Start()
    {
        StartTime = Time.time;
    }

    
    void Update()
    {
        if (Time.time - StartTime >= TimeStep)
        {
            Vector3 IconAngle = MainIcon.localEulerAngles;
            IconAngle.z += OneStepAngle;
            MainIcon.localEulerAngles = IconAngle;
            StartTime = Time.time;
        }
    }
}
