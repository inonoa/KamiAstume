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
    public List<List<GameObject>> tables = new List<List<GameObject>>();
    public List<List<TableState>> tableStates = new List<List<TableState>>();
    public List<List<GameObject>> zenzais = new List<List<GameObject>>();

    [HideInInspector]
    public float tableDistX = 1.6f;
    [HideInInspector]
    public float tableDistY = -2.4f;
    [HideInInspector]
    public int tableNumX = 7;
    [HideInInspector]
    public int tableNumY = 3;
    [HideInInspector]
    public Vector3 firstTableVec = new Vector3(-4.8f,2); // 書き換わってくれ

    // Start is called before the first frame update
    void Start()
    {
        //テーブル生成
        for(int j=0;j<tableNumY;j++){
            tables.Add(new List<GameObject>());
            zenzais.Add(new List<GameObject>());
            tableStates.Add(new List<TableState>());
            for(int i=0;i<tableNumX;i++){
                tables[j].Add(Instantiate(table, new Vector3(i*tableDistX,j*tableDistY,0) + firstTableVec, Quaternion.identity));
                zenzais[j].Add(Instantiate(zenzai, new Vector3(i*tableDistX,j*tableDistY+0.5f,0) + firstTableVec, Quaternion.identity));
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
