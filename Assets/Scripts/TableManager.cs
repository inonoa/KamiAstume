using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    #region 生成元オブジェクト
    public GameObject table;
    public GameObject zenzai;
    public Timer timer;

    //神の候補全部乗せ(ある時点で出るのはこの中の限定された範囲内)
    public GameObject[] kamis;

    #endregion

    #region 途中で変わりそうなパラメータ

    //kamiSoejiL番目以上kamiSoejiR番目以下の神が出る
    private int kamiSoejiL = 0;
    private int kamiSoejiR = 8000000;

    //神の出現頻度(の逆数)
    private int framesToSpawn = 30;

    public int tableNumX = 7;
    public int tableNumY = 3;
    public List<List<GameObject>> tables = new List<List<GameObject>>();
    public List<List<Table>> tableStates = new List<List<Table>>();

    #endregion

    #region 定数
    public float tableDistX = 1.6f;
    public float tableDistY = -2.4f;
    public static readonly Vector3 firstTableVec = new Vector3(-4.8f,2,1);
    public static readonly Vector3 firstZenzaiVec = firstTableVec + new Vector3(0,0,-1);

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        zenzai = table.transform.Find( "Zenzai" ).gameObject;

        //机生成
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
        //一定時間ごとに神を出現
        if(timer.timeF%framesToSpawn==0){
            System.Random rd = new System.Random();

            //100回ランダムな場所にトライして空きが無かったら諦める
            for(int i=0;i<100;i++){
                //机の番号と神をランダム生成
                int num = rd.Next(tableNumX*tableNumY);
                GameObject nxtkami = Instantiate(kamis[rd.Next(kamis.Length)]);


                if(tableStates[num/tableNumX][num%tableNumX].TryToPutKami(nxtkami)) break;
                else Destroy(nxtkami);
            }
        }
    }
}
