using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHolder : MonoBehaviour
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

    public int score = 8000000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
