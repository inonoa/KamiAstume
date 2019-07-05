using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private Text ui;
    public int timeF = fullTime;
    public static readonly int fullTime = 60 * 60;

    void TimeUp(){
        SceneManager.LoadScene("ResultScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        ui = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        ui.text = "時間: " + Kanjinizer.Kanjinize(timeF/60,2) + ":" + Kanjinizer.Kanjinize(timeF%60,2);
        timeF --;
        if(timeF==0){
            TimeUp();
        }
    }
}
