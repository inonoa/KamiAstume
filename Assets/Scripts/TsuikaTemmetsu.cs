using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TsuikaTemmetsu : MonoBehaviour
{
    public Timer timer;
    public GameObject tsuika;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(1100<=timer.timeF && timer.timeF<=1200){
            if(timer.timeF%20==0){
                tsuika.SetActive(true);
            }else if(timer.timeF%20==10){
                tsuika.SetActive(false);
            }
        }else if(timer.timeF==1000){
            tsuika.SetActive(false);
        }
    }
}
