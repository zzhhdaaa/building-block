using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using UnityEngine.Events;

public class FeedbacksListener : MonoBehaviour
{
    private bool feedbackOn;

    public MMFeedbacks giveItLiveFeedback;
    public MMFeedbacks buildFeedback;
    public MMFeedbacks breakFeedback;
    public MMFeedbacks switchFeedback;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            giveItLiveFeedback?.PlayFeedbacks();
        }
        if (Input.GetMouseButtonDown(0))
        {
            buildFeedback?.PlayFeedbacks();
        }
        if (Input.GetMouseButtonDown(1))
        {
            breakFeedback?.PlayFeedbacks();
        }
        if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.Backspace))
        {
            switchFeedback?.PlayFeedbacks();
        }
    }
}
