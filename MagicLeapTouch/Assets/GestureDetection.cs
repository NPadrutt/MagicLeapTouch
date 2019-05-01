using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class GestureDetection : MonoBehaviour
{
    //game object representing the finger position
    public Transform Finger;
    // Use this for initialization
    void Start()
    {
        if (!MLHands.Start().IsOk)
        {
            Debug.Log("MLHands didn't start...bail.");
            return;
        }
        //set gestures to track
        setGesturesToTrack();
    }

    // Update is called once per frame
    void Update()
    {
        //if MLHands is running start tracking gestures
        if (MLHands.IsStarted)
            gestureTracker();
    }

    //set the gestures to track
    void setGesturesToTrack()
    {
        List<MLHandKeyPose> gestures = new List<MLHandKeyPose>();
        //add the gestures we want to track
        gestures.Add(MLHandKeyPose.Finger);
        gestures.Add(MLHandKeyPose.L);
        //add the gestures to the gesture manager
        MLHands.KeyPoseManager.EnableKeyPoses(gestures.ToArray(), true, true);
    }

    //track the gestures
    void gestureTracker()
    {
        //check for the l or finger gesture
        //and that we've got some keypoints
        if ((MLHands.Left.KeyPose == MLHandKeyPose.Finger ||
             MLHands.Left.KeyPose == MLHandKeyPose.L) &&
            MLHands.Left.KeyPoseConfidence >= 0.8f)
        {
            positionHand(MLHands.Left);
        }

        //check for the l or finger gesture
        //and that we've got some keypoints
        if ((MLHands.Right.KeyPose == MLHandKeyPose.Finger ||
             MLHands.Right.KeyPose == MLHandKeyPose.L) &&
            MLHands.Right.KeyPoseConfidence >= 0.8f)
        {
            positionHand(MLHands.Right);
        }
    }

    //position the game object to the tracked finger
    void positionHand(MLHand hand) 
    {
        Finger.position = hand.Index.Tip.Position;
    }

    private void OnDestroy()
    {
        MLHands.Stop();
    }
}
