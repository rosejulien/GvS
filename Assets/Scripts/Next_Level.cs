using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.Port;

public class Next_Level : MonoBehaviour
{
    public GameObject text;
    private Text opacity;

    private float startTime;
    public float delay;
    private bool next = false;

    private void Start()
    {
        startTime = Time.time;
        opacity = text.GetComponent<Text>();
    }

    private void Update()
    {
        if (next == false)
        {
            float elapsedTime1 = Time.time - startTime;

            if (elapsedTime1 >= 1 && elapsedTime1 <= 4)
            {
                float t1 = Mathf.Clamp01((elapsedTime1 - 1f) / 3f);

                Color textColor = opacity.color;
                textColor.a = t1;
                opacity.color = textColor;
            }
            else if (elapsedTime1 >= 13 && elapsedTime1 <= 16)
            {
                float t2 = 1 - Mathf.Clamp01((elapsedTime1 - 13f) / 3f);

                Color textColor = opacity.color;
                textColor.a = t2;
                opacity.color = textColor;

            }
            else if (elapsedTime1 >= 17)
            {
                next = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
