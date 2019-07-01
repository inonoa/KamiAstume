using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private Text ui;
    public int timeF = 60 * 60;

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
        ui.text = "会議終了まで " + (timeF/60).ToString("00") + ":" + (timeF%60).ToString("00");
        timeF --;
        if(timeF==0){
            TimeUp();
        }
    }
}
