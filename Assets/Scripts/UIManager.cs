using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    float score;
    public TextMeshProUGUI scoreTMPro, highScoreTMPro;



    private void Start()
    {
        highScoreTMPro.text = "HI: " + PlayerPrefs.GetFloat("HighScore", 0f).ToString("F2");
    }
    void Update()
    {
        score += Time.deltaTime;
        scoreTMPro.text = score.ToString("F2");
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("HighScore", score);
    }
}
