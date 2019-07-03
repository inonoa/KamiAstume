using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHolder
{
    private ScoreHolder(){}
    private static ScoreHolder _Instance;
    public static ScoreHolder Instance{
        get{
            if(_Instance==null){
                _Instance = new ScoreHolder();
            }
            return _Instance;
        }
    }

    public int score = 0;
}
