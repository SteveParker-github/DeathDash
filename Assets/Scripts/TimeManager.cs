using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private float normalTime = 1;
    private float SlowMoTime = 0.25f;
    public float currentTime;
    private float TargetTime;
    private Camera mainCam;

    private void Awake()
    {
        currentTime = normalTime;
    }
    // Start is called before the first frame update
    void Start()
    {   
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleTime(bool goingSlowMo)
    {
        TargetTime = goingSlowMo ? SlowMoTime : normalTime;

        StartCoroutine(ChangeTime());
    }

    private IEnumerator ChangeTime()
    {
        do
        {
            currentTime = Mathf.MoveTowards(currentTime, TargetTime, 5.0f * Time.deltaTime);
            //currentTime = Mathf.Lerp(currentTime, TargetTime, Time.deltaTime);
            Debug.Log(currentTime);
            //Time.timeScale = currentTime;
            yield return null;
        } while (currentTime != TargetTime);

        Debug.Log(currentTime);
        currentTime = TargetTime;
    }
}
