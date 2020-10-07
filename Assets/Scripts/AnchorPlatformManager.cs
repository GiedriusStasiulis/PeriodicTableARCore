using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;
using TMPro;

public class AnchorPlatformManager : MonoBehaviour
{
    [SerializeField]
    private GameObject anchorPlatformObj;

    [SerializeField]
    private TextMeshProUGUI feedbackText;

    private List<DetectedPlane> detectedPlanes = new List<DetectedPlane>();

    private bool platformSpawned;

    private void Awake()
    {
        feedbackText.text = "Scanning ... ";
    }

    // Update is called once per frame
    void Update()
    {
        Session.GetTrackables<DetectedPlane>(detectedPlanes, TrackableQueryFilter.New);

        if(detectedPlanes.Count == 1)
        {
            feedbackText.text = "Place the platform";
        }


        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.Default;

            if(touch.phase == TouchPhase.Began)
            {
                if(Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
                {
                    var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                    if(!platformSpawned)
                    {
                        Instantiate(anchorPlatformObj, anchor.transform.position, anchor.transform.rotation, anchor.transform);
                        feedbackText.text = "";
                        platformSpawned = true;
                    }                   
                }
            }
        }
    }
}
