using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiConstraintsConfig : MonoBehaviour
{

    public float strength;
    public float height;

    
    public string[] childhoodTrauma { get; set; }
    
    // depends even if the situation is recognized by the supsect being watched
    public string[] Personality { get; set; }
    public string[] sex;

    public bool weapon;
    // clothes, smell, face expression, eyes color

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
