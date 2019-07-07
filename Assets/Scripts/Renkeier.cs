using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Renkeier : MonoBehaviour
{

    public bool inRanking = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!inRanking){
        //Twitter
        if(Input.GetKeyDown(KeyCode.T)){
            naichilab.UnityRoomTweet.Tweet ("kamiatsume",
            "神集めで" + Kanjinizer.Kanjinize(ScoreHolder.Instance.score) + "点を獲得しました！" + new string[4]{"あなたが神か…!?\n","プロフェッショナル神集めイスト!!!\n","神\n","もはや神の域へ達している…！\n"}[new System.Random().Next(4)],
            "unity1week","kamiatsume");
            Debug.Log("神集めで" + Kanjinizer.Kanjinize(ScoreHolder.Instance.score) + "点を獲得しました！" + new string[4]{"あなたが神か…!?\n","プロフェッショナル神集めイスト!!!\n","神\n","もはや神の域へ達している…！\n"}[new System.Random().Next(4)] + "とツイートしたい");
        }
        //オンラインランキング
        if(Input.GetKeyDown(KeyCode.R)){
            inRanking = true;
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking (ScoreHolder.Instance.score);
        }
        //タイトル画面へ
        if(Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Backspace)){
            ScoreHolder.Instance.score = 0;
            SceneManager.LoadScene("Title2Intro");
            Destroy(GameObject.Find("BGMer"));
        }
        //リトライ
        if(Input.GetKeyDown(KeyCode.C)){
            ScoreHolder.Instance.score = 0;
            SceneManager.LoadScene("SampleScene");
        }
        }
    }
}
