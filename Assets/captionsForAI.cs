using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class captionsForAI : MonoBehaviour
{
    public GameObject captions;
    private float turnOffCaptionsTimer;
    private bool captionsActive;
    public void TurnOnCaptions()
    {
        captionsActive = true;
        captions.SetActive(true);
        //Invoke(nameof(TurnOffCaptions), 3f);
 
    }

    public void TurnOffCaptions()
    {
        captions.SetActive(false);
        captionsActive = false;
    }

}
