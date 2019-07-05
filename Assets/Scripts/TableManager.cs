using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    #region 生成元オブジェクト
    public GameObject table;
    public GameObject zenzai;
    public Timer timer;
    public Fukidashi fkds;

    //神の候補全部乗せ(ある時点で出るのはこの中の限定された範囲内)
    public GameObject[] kamis;

    #endregion

    public OkuniController okuni;

    #region 途中で変わりそうなパラメータ

    //kamiSoejiL番目以上kamiSoejiR番目未満の神が出る
    public int kamiSoejiL = -5;
    public int kamiSoejiR = 5;

    //神の出現頻度(の逆数)
    public int framesToSpawn = 105;

    public int tableNumX = 7;
    public int tableNumY = 3;
    public List<List<GameObject>> tables = new List<List<GameObject>>();
    public List<List<Table>> tableStates = new List<List<Table>>();

    #endregion

    #region 定数
    public float tableDistX = 1.6f;
    public float tableDistY = -1.8f;
    public Vector3 firstTableVec = new Vector3(-4.8f,1.5f,1);
    public Vector3 firstZenzaiVec = new Vector3(-4.8f,1.5f,0);

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        zenzai = table.transform.Find( "Zenzai" ).gameObject;

        System.Random rd = new System.Random();

        //机生成
        for(int j=0;j<tableNumY;j++){
            tables.Add(new List<GameObject>());
            tableStates.Add(new List<Table>());

            for(int i=0;i<tableNumX;i++){
                tables[j].Add(Instantiate(table, new Vector3(i*tableDistX,j*tableDistY,0) + firstTableVec, Quaternion.identity));
                tableStates[j].Add(tables[j][i].GetComponent<Table>());

                for(int k=0;k<3;k++){
                    //フキダシ生成
                    tableStates[j][i].fukidashis[k] = Instantiate(
                        fkds[rd.Next(fkds.fkds.Length)],
                        new Vector3((k-1)*0.7f, (-(System.Math.Abs(k-1))+3)*0.4f, -2) + tableStates[j][i].transform.position,
                        Quaternion.identity,
                        tableStates[j][i].transform);
                    
                    tableStates[j][i].fukidashis[k].GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);

                    tableStates[j][i].fukidashis[k].SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //一定時間ごとに神を出現
        if(timer.timeF%framesToSpawn==0){
            System.Random rd = new System.Random();

            //机の番号と神をランダム生成
            int num = rd.Next(tableNumX*tableNumY);
            GameObject nxtkami = kamis[rd.Next(System.Math.Max(kamiSoejiL,0),kamiSoejiR)];
            tableStates[num/tableNumX][num%tableNumX].TryToPutKami(nxtkami);
        }

        //出現頻度増大
        if(timer.timeF%100==0){
            if(timer.timeF>300)framesToSpawn -= 3;
            else framesToSpawn --;
        }

        if(timer.timeF%120==0){
            kamiSoejiL += 1;
            if(kamiSoejiR<kamis.Length)kamiSoejiR += 1;
        }

        #region 残り20秒で机増加
        if(timer.timeF==1200){
            framesToSpawn -= 5;

            //帳尻合わせ
            tableNumY += 2;
            firstTableVec -= new Vector3(0,tableDistY,0);
            firstZenzaiVec -= new Vector3(0,tableDistY,0);
            okuni.y ++;

            System.Random rd = new System.Random();

            tables.Add(new List<GameObject>());
            tableStates.Add(new List<Table>());
            //後ろに一つずつずらすことで先頭に空きを作ってそこに差し込む
            for(int i=tableNumY-3;i>-1;i--){
                tables[i+1] = tables[i];
                tableStates[i+1] = tableStates[i];
            }
            tables[0] = new List<GameObject>();
            tableStates[0] = new List<Table>();
            for(int i=0;i<tableNumX;i++){
                tables[0].Add(Instantiate(table, new Vector3(i*tableDistX+1.75f,0,0) + firstTableVec, Quaternion.identity));
                tables[0][i].GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
                tableStates[0].Add(tables[0][i].GetComponent<Table>());

                for(int k=0;k<3;k++){
                    //フキダシ生成
                    tableStates[0][i].fukidashis[k] = Instantiate(
                        fkds[rd.Next(fkds.fkds.Length)],
                        new Vector3((k-1)*0.7f, (-(System.Math.Abs(k-1))+3)*0.4f, -2) + tableStates[0][i].transform.position,
                        Quaternion.identity,
                        tableStates[0][i].transform);

                    tableStates[0][i].fukidashis[k].GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);

                    tableStates[0][i].fukidashis[k].SetActive(false);
                }
            }
            

            //こっちは後ろに足せばいい
            tables.Add(new List<GameObject>());
            tableStates.Add(new List<Table>());
            for(int i=0;i<tableNumX;i++){
                tables[tableNumY-1].Add(Instantiate
                    (table, new Vector3(i*tableDistX+1.75f,(tableNumY-1)*tableDistY,0) + firstTableVec, Quaternion.identity));
                tables[tableNumY-1][i].GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
                tableStates[tableNumY-1].Add(tables[tableNumY-1][i].GetComponent<Table>());

                for(int k=0;k<3;k++){
                    //フキダシ生成
                    tableStates[tableNumY-1][i].fukidashis[k] = Instantiate(
                        fkds[rd.Next(fkds.fkds.Length)],
                        new Vector3((k-1)*0.7f, (-(System.Math.Abs(k-1))+3)*0.4f, -2) + tableStates[tableNumY-1][i].transform.position,
                        Quaternion.identity,
                        tableStates[tableNumY-1][i].transform);
                    
                    tableStates[tableNumY-1][i].fukidashis[k].GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);

                    tableStates[tableNumY-1][i].fukidashis[k].SetActive(false);
                }
            }
        }
        #endregion

        if(timer.timeF<1200 && timer.timeF>1149){
            for(int i=0;i<tableNumX;i++){
                tables[tableNumY-1][i].GetComponent<SpriteRenderer>().color += new Color(0,0,0,0.02f);
                tables[0][i].GetComponent<SpriteRenderer>().color += new Color(0,0,0,0.02f);
                tables[tableNumY-1][i].transform.position += new Vector3((1149-timer.timeF)*0.00133f,0,0);
                tables[0][i].transform.position += new Vector3((1149-timer.timeF)*0.00133f,0,0);
            }
        }
    }
}
