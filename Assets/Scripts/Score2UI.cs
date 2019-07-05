using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score2UI : MonoBehaviour
{
    Text jibun;

    // Start is called before the first frame update
    void Start()
    {
        jibun = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        jibun.text = "得点: " + Kanjinizer.Kanjinize(ScoreHolder.Instance.score,6);
        Debug.Log(Kanjinizer.Kanjinize(ScoreHolder.Instance.score,6));
    }
}
