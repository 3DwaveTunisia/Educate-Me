using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

public class EventsHandler : MonoBehaviour, ITrackableEventHandler
{

    public UnityAction OnTrackingFound;
    public UnityAction OnTrackingLoss;

    private TrackableBehaviour mTrackableBehaviour = null;

    public UnityEvent onDetect;
    public UnityEvent onLoss;

    private readonly List<TrackableBehaviour.Status> mTrackingFound = new List<TrackableBehaviour.Status>
    {
        TrackableBehaviour.Status.DETECTED,
        TrackableBehaviour.Status.TRACKED,
        TrackableBehaviour.Status.EXTENDED_TRACKED
    };

    private readonly List<TrackableBehaviour.Status> mTrackingLoss = new List<TrackableBehaviour.Status>
    {
        TrackableBehaviour.Status.TRACKED,
        TrackableBehaviour.Status.NO_POSE
    };

    void Awake()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        mTrackableBehaviour.RegisterTrackableEventHandler(this);

        if (onDetect == null)
            onDetect = new UnityEvent();

        if (onLoss == null)
            onLoss = new UnityEvent();

    }

    private void OnDestroy()
    {
        mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }
    
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        // if Tracking Found
        foreach(TrackableBehaviour.Status trackedStatus in mTrackingFound)
        {
            if (newStatus == trackedStatus)
            {
                if (OnTrackingFound != null)
                    OnTrackingFound();

                //print("Tracking Found!");
                onDetect.Invoke();

                return;
            }

        }

        // if Tracking Lost
        foreach(TrackableBehaviour.Status trackedStatus in mTrackingLoss)
        {
            if (newStatus == trackedStatus)
            {
                if (OnTrackingLoss != null)
                    OnTrackingLoss();

                //print("Tracking Loss!");
                onLoss.Invoke();

                return;
            }

        }

    }

}
