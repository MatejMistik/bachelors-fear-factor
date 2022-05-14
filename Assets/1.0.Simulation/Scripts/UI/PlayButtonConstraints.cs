using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonConstraints : MonoBehaviour
{
	public Button playButton;

	void Start()
	{
		Button btn = playButton.GetComponent<Button>();
		btn.onClick.AddListener(delegate { TaskOnClick(); });
	}

	void TaskOnClick()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Debug.Log(AiConstraintsConfig.sceneString);
		if (AiConstraintsConfig.sceneString == null)
		{
			Loader.Load(Loader.Scene.MainMenu);
			return;
		}
		Loader.Scene enumValue = (Loader.Scene)System.Enum.Parse(typeof(Loader.Scene), AiConstraintsConfig.sceneString);
		Loader.Load(enumValue);
		Debug.Log("You have clicked the button!");
	}
          
     
}
