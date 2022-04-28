using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class MainMenu : MonoBehaviour {

    private void Awake() {
        transform.Find("TailGating").GetComponent<Button_UI>().ClickFunc = () => {
            Debug.Log("Click Play");
            Loader.Load(Loader.Scene.TailGating);
        };
        transform.Find("BombScene").GetComponent<Button_UI>().ClickFunc = () => {
            Debug.Log("Click Play");
            Loader.Load(Loader.Scene.BombScene);
        };
        transform.Find("ElevatorScene").GetComponent<Button_UI>().ClickFunc = () => {
            Debug.Log("Click Play");
            Loader.Load(Loader.Scene.ElevatorScene);
        };
        transform.Find("PhoneScene").GetComponent<Button_UI>().ClickFunc = () => {
            Debug.Log("Click Play");
            Loader.Load(Loader.Scene.PhoneScene);
        };

    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
