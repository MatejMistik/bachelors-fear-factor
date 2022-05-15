using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class MainMenu : MonoBehaviour {

    /***************************************************************************************
    *	Title: Scene Manager in Unity (Unity Tutorial)
    *	Author: Code Monkey
    *   Date: 5. 4. 2019
    *	Code version: 1.0
    *	Availability: https://www.youtube.com/watch?v=3I5d2rUJ0pE&ab_channel=CodeMonkey
    *
    ***************************************************************************************/

    private void Awake() {

        transform.Find("TailGating").GetComponent<Button_UI>().ClickFunc = () => {
            Debug.Log("Click Play");
            AiConstraintsConfig.TakeState(AiConstraintsConfig.Scene.TailGating);
            Loader.Load(Loader.Scene.AiConstraints);
        };
        transform.Find("BasicGameScene").GetComponent<Button_UI>().ClickFunc = () => {
            Debug.Log("Click Play");
            AiConstraintsConfig.TakeState(AiConstraintsConfig.Scene.BasicGameScene);
            Loader.Load(Loader.Scene.AiConstraints);
        };
        transform.Find("ElevatorScene").GetComponent<Button_UI>().ClickFunc = () => {
            Debug.Log("Click Play");
            AiConstraintsConfig.TakeState(AiConstraintsConfig.Scene.ElevatorScene);
            Loader.Load(Loader.Scene.AiConstraints);
        };
        transform.Find("ConfrontationScene").GetComponent<Button_UI>().ClickFunc = () => {
            Debug.Log("Click Play");
            AiConstraintsConfig.TakeState(AiConstraintsConfig.Scene.ConfrontationScene);
            Loader.Load(Loader.Scene.AiConstraints);
        };
        transform.Find("QuitBtn").GetComponent<Button_UI>().ClickFunc = () => {
            Debug.Log("Click Quit");
            Application.Quit();
        };
        transform.Find("ShotgunScene").GetComponent<Button_UI>().ClickFunc = () => {
            Debug.Log("Click Quit");
            AiConstraintsConfig.TakeState(AiConstraintsConfig.Scene.DeadBodyScene);
            Loader.Load(Loader.Scene.AiConstraints);
        };


    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
