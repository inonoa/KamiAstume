using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score2Result : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "得点: " + Kanjinizer.Kanjinize(ScoreHolder.Instance.score,6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
