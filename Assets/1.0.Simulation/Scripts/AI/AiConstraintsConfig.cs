using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AiConstraintsConfig : MonoBehaviour
{
    public static string sceneString;

    //must be same as in the Loader script
    public enum Scene
    {
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

    public static void TakeState(Scene scene)
    {
        sceneString = scene.ToString();
    }

    public static string[] behaviorTreeType = {"Tree2", "Tree3" };
    public static string chosenBehaviorTree;


    public static float minStrenght, maxStrenght, strenght;
    public static float strengthClamped, strenghtNormalized;
    public static float minHeight, maxHeight, height = 1.7643f;
    public static float heightClamped, heightNormalized ;
    public static float speed;
    public static float mass = 1.2678f;

    public static string[] childhoodTrauma = { "None", "BeenInShooting", "SeenDeadBody", "BeenTailgated" };
    public static string chosenTrauma;

    // depends even if the situation is recognized by the supsect being watched
    public string[] Personality { get; set; }
    public static bool male = false;
    public static bool female = true;

    public static bool weapon;
    // clothes, smell, face expression, eyes color

    // Start is called before the first frame update
    void Start()
    {
        strengthClamped = Mathf.Clamp(strenght, minStrenght, maxStrenght);
        strenghtNormalized = (strengthClamped - minStrenght) / (maxStrenght - minStrenght);
        heightClamped = Mathf.Clamp(height, minHeight, maxHeight);
        heightNormalized = (heightClamped - minHeight ) / ( maxHeight - minHeight ); 
    }


}
