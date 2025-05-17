using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private PlayerControls playerControls;
    private TimeManager timeManager;
    private bool slowMoActive = false;
    private float timeRemaining;
    private float maxTime = 10;
    private float minTimeAllow = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerControls = GetComponent<PlayerControls>();
        timeManager = FindAnyObjectByType<TimeManager>();
        timeRemaining = maxTime;

        if (timeManager == null)
        {
            Debug.Log("Can't find TimeManager!");
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (playerControls.IsTimeToggled) 
       {
            playerControls.IsTimeToggled = false;

            if (timeRemaining < minTimeAllow && !slowMoActive)
            {
                return;
            }

            slowMoActive = !slowMoActive;
            timeManager.ToggleTime(slowMoActive);
       }

       if (slowMoActive)
       {
            if (timeRemaining < 0.0f)
            {
                slowMoActive = !slowMoActive;
                timeManager.ToggleTime(slowMoActive);
                return;
            }

            timeRemaining -= Time.deltaTime;
       }
    }
}
