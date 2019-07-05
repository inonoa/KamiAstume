using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Renkeier : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Twitter
        if(Input.GetKeyDown(KeyCode.T)){
            naichilab.UnityRoomTweet.Tweet ("kamiatsume",
            "(test)神集めで" + Kanjinizer.Kanjinize(ScoreHolder.Instance.score) + "点を獲得しました！",
             "unity1week","kamiatsume");
        }
        //オンラインランキング
        if(Input.GetKeyDown(KeyCode.R)){
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking (ScoreHolder.Instance.score);
        }
        //タイトル画面へ
        if(Input.GetKeyDown(KeyCode.X)){
            SceneManager.LoadScene("Title2Intro");
        }
        //リトライ
        if(Input.GetKeyDown(KeyCode.Z)){
            SceneManager.LoadScene("SampleScene");
        }
    }
}
