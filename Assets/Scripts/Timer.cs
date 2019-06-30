using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text ui;
    private int timeF = 60 * 60;
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
    }
}
