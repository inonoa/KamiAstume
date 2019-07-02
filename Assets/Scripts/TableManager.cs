using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public enum TableState{
        Empty, Zenzai
    }
    public GameObject table;
    public GameObject zenzai;
    public List<GameObject> kamis;
    public List<List<GameObject>> tables = new List<List<GameObject>>();
    public List<List<TableState>> tableStates = new List<List<TableState>>();
    public List<List<GameObject>> zenzais = new List<List<GameObject>>();

    public float tableDistX = 1.6f;
    public float tableDistY = -2.4f;
    public int tableNumX = 7;
    public int tableNumY = 2;
    public Vector3 firstTableVec = new Vector3(-4.8f,2,1); // 書き換わってくれ
    public Vector3 firstZenzaiVec;

    // Start is called before the first frame update
    void Start()
    {
        firstZenzaiVec = firstTableVec + new Vector3(0,0,-1);
        //テーブル生成
        for(int j=0;j<tableNumY;j++){
            tables.Add(new List<GameObject>());
            zenzais.Add(new List<GameObject>());
            tableStates.Add(new List<TableState>());
            for(int i=0;i<tableNumX;i++){
                tables[j].Add(Instantiate(table, new Vector3(i*tableDistX,j*tableDistY,0) + firstTableVec, Quaternion.identity));
                zenzais[j].Add(Instantiate(zenzai, new Vector3(i*tableDistX,j*tableDistY+0.5f,0) + firstZenzaiVec, Quaternion.identity));
                zenzais[j][i].SetActive(false);
                tableStates[j].Add(TableState.Empty);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
