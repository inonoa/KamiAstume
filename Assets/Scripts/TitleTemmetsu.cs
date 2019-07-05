using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTemmetsu : MonoBehaviour
{
    int frames = 0;

    public Sprite z_ari;
    public Sprite z_nashi;

    public Image kore;

    // Start is called before the first frame update
    void Start()
    {
        kore = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(frames==40){
            kore.sprite = z_nashi;
        }else if(frames==80){
            kore.sprite = z_ari;
            frames = 0;
        }
        frames ++;
    }
}
