using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/***************************************************************************************
    *	Title: Scene Manager in Unity (Unity Tutorial)
    *	Author: Code Monkey
    *   Date: 5. 4. 2019
    *	Code version: 1.0
    *	Availability: https://www.youtube.com/watch?v=3I5d2rUJ0pE&ab_channel=CodeMonkey
    *
    ***************************************************************************************/

public static class Loader {

    private class LoadingMonoBehaviour : MonoBehaviour { }
    
    public enum Scene {
        GameScene,
        Loading,
        MainMenu,
        BombScene,
        BushScene,
        ElevatorScene,
        ConfrontationScene,
        TailGating,
        AiConstraints,
        DeadBodyScene,
        BasicGameScene,
    }

    private static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;

    public static void Load(Scene scene) {
        // Set the loader callback action to load the target scene
        onLoaderCallback = () => {
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
        };

        // Load the loading scene
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    private static IEnumerator LoadSceneAsync(Scene scene) {
        yield return null;

        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        while (!loadingAsyncOperation.isDone) {
            yield return null;
        }
    }

    public static float GetLoadingProgress() {
        if (loadingAsyncOperation != null) {
            return loadingAsyncOperation.progress;
        } else {
            return 1f;
        }
    }

    public static void LoaderCallback() {
        // Triggered after the first Update which lets the screen refresh
        // Execute the loader callback action which will load the target scene
        if (onLoaderCallback != null) {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
