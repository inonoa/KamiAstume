using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public GameObject table;
    public List<GameObject> tables = new List<GameObject>();
    private float tableDistX = 1.6f;
    private float tableDistY = -2.4f;
    private int tableNumX = 7;
    private int tableNumY = 3;
    private Vector3 firstTableVec = new Vector3(-4.8f,2); // 書き換わってくれ

    // Start is called before the first frame update
    void Start()
    {
        //テーブル生成
        for(int j=0;j<tableNumY;j++){
            for(int i=0;i<tableNumX;i++){
                tables.Add(Instantiate(table, new Vector3(i*tableDistX,j*tableDistY,0) + firstTableVec, Quaternion.identity));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.X)){
            Instantiate(table, new Vector3(0,0,0), Quaternion.identity);
        }
        */
    }
}
