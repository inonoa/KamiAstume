﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            //naichilab.UnityRoomTweet.Tweet ("kamiatsume", "テスト", "unityroom");

            // Type == Number の場合
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking (101);
        }
    }
}
