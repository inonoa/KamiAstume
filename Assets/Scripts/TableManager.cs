using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public GameObject table;
    public GameObject zenzai;

    //神の候補全部乗せ(ある時点で出るのはこの中の限定された範囲内)
    public GameObject[] kamis;

    ///kamiSoejiL番目以上kamiSoejiR番目以下の神が出る
    private int kamiSoejiL = 0;
    private int kamiSoejiR = 8000000;

    public Timer timer;


    public List<List<GameObject>> tables = new List<List<GameObject>>();
    public List<List<Table>> tableStates = new List<List<Table>>();

    public float tableDistX = 1.6f;
    public float tableDistY = -2.4f;
    public int tableNumX = 7;
    public int tableNumY = 3;
    public Vector3 firstTableVec = new Vector3(-4.8f,2,1); // 書き換わってくれ
    public Vector3 firstZenzaiVec;

    // Start is called before the first frame update
    void Start()
    {
        zenzai = table.transform.Find( "Zenzai" ).gameObject;
        firstZenzaiVec = firstTableVec + new Vector3(0,0,-1);
        //テーブル生成
        for(int j=0;j<tableNumY;j++){
            tables.Add(new List<GameObject>());
            tableStates.Add(new List<Table>());
            for(int i=0;i<tableNumX;i++){
                tables[j].Add(Instantiate(table, new Vector3(i*tableDistX,j*tableDistY,0) + firstTableVec, Quaternion.identity));
                tableStates[j].Add(tables[j][i].GetComponent<Table>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.timeF%10==0){
            System.Random rd = new System.Random();
            for(int i=0;i<100;i++){
                int num = rd.Next(tableNumX*tableNumY);
                if(tableStates[num/tableNumX][num%tableNumX].KamiState==Table.KState.NoKami){
                    GameObject nxtkami = Instantiate(kamis[rd.Next(kamis.Length)]);
                    if(tableStates[num/tableNumX][num%tableNumX].TryToPutKami(nxtkami)) break;
                }
            }
        }
    }
}
