using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoBackButton : MonoBehaviour
{
	public Button goBackButton;

	void Start()
	{
		Button btn = goBackButton.GetComponent<Button>();
		btn.onClick.AddListener(delegate { TaskOnClick(); });
	}

	void TaskOnClick()
	{
		Loader.Load(Loader.Scene.MainMenu);
	}
}
