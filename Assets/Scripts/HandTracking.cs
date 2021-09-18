using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class HandTracking : MonoBehaviour
{
    [SerializeField] float confidenceThreshold = 0.9f;
    MLHandTracking.HandKeyPose[] gestures = new MLHandTracking.HandKeyPose[2];
    InstructionManager im;
    bool rightClosed, leftClosed;

    void Start()
    {
        im = FindObjectOfType<InstructionManager>();

        MLHandTracking.Start();

        gestures[0] = MLHandTracking.HandKeyPose.Fist;
        gestures[1] = MLHandTracking.HandKeyPose.Finger;

        MLHandTracking.KeyPoseManager.EnableKeyPoses(gestures, true, false);
    }

    void OnDestroy()
    {
        MLHandTracking.Stop();
    }

    void Update()
    {
        if (GetGesture(MLHandTracking.Left, MLHandTracking.HandKeyPose.Fist))
        {
            if (!leftClosed)
            {
                OnLeftClosed();
            }
            leftClosed = true;
        }
        else
        {
            leftClosed = false;
        }

        if (GetGesture(MLHandTracking.Right, MLHandTracking.HandKeyPose.Fist))
        {
            if (!rightClosed)
            {
                OnRightClosed();
            }
            rightClosed = true;
        }
        else
        {
            rightClosed = false;
        }
    }

    bool GetGesture(MLHandTracking.Hand hand, MLHandTracking.HandKeyPose type)
    {
        if (hand != null)
        {
            if (hand.KeyPose == type && hand.HandKeyPoseConfidence > confidenceThreshold)
            {
                return true;
            }
        }
        return false;
    }

    public void OnRightClosed()
    {
        Instruction instruction = im.GetInstruction();
        instruction?.AnimationForward();

        Debug.Log("Right closed");
    }

    public void OnLeftClosed()
    {
        Instruction instruction = im.GetInstruction();
        instruction?.AnimationBackward();

        Debug.Log("Left closed");
    }
}
