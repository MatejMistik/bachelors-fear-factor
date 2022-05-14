using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BombTimer : MonoBehaviour
{
    public TextMeshProUGUI bombTimerText;
    private float timeLeft;
    private bool bombActivated;
    private float colorGreen = 80;
    private float colorGreenTimer = 0.0f;
    private float colorBlueTimer = 0.0f;
    private float colorBlue = 110;

    // Update is called once per frame
    void Update()
    {
        if (bombActivated)
            timeLeft -= Time.deltaTime;
        bombTimerText.SetText(string.Format("{0:N2}", timeLeft));
        colorGreenTimer += Time.deltaTime;
        if (colorGreenTimer >= 0.006)
        {
            colorGreen++;
            colorGreenTimer = 0f;
            ChangeColor();
        }
        if(colorGreen >= 240)
        {
            colorGreen = 80;
        }
        colorBlueTimer += Time.deltaTime;
        if (colorBlueTimer >= 0.012)
        {
            colorBlue++;
            colorBlueTimer = 0f;
            ChangeColor();
        }
        if (colorBlue >= 150)
        {
            colorBlue = 110;
        }

        Debug.Log(bombTimerText.color);

        
    }

    public void StartTimer(float bombTime)
    {
        timeLeft = bombTime;
        bombActivated = true;
        Destroy(gameObject, bombTime);
    }

    public void ChangeColor()
    {
        bombTimerText.color = new Color(1, colorGreen/255, colorBlue/255, 1);
    }

}
