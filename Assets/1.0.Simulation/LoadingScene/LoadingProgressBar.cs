using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/***************************************************************************************
    *	Title: Scene Manager in Unity (Unity Tutorial)
    *	Author: Code Monkey
    *   Date: 5. 4. 2019
    *	Code version: 1.0
    *	Availability: https://www.youtube.com/watch?v=3I5d2rUJ0pE&ab_channel=CodeMonkey
    *
    ***************************************************************************************/

public class LoadingProgressBar : MonoBehaviour {

    private Image image;

    private void Awake() {
        image = transform.GetComponent<Image>();
    }

    private void Update() {
        image.fillAmount = Loader.GetLoadingProgress();
    }
}
