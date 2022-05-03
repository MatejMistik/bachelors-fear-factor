using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class captionsForAI : MonoBehaviour
{
    public GameObject captions;
    public float turnOffCaptionsTimer;
    private bool captionsActive;
    public void TurnOnCaptions()
    {
        captionsActive = true;
        captions.SetActive(true);
        Invoke(nameof(TurnOffCaptions), turnOffCaptionsTimer);
 
    }

    public void TurnOffCaptions()
    {
        captions.SetActive(false);
        captionsActive = false;
    }

}
