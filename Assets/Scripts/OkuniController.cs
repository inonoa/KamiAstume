using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkuniController : MonoBehaviour
{

    //机間の移動/ぜんざいまわりに使う
    public TableManager tableManager;

    //座標(何番目の机前にいるか？)
    private int x = 0;
    private int y = 0;

    //動ける範囲取ってくるだけ
    public int TableNumX{
        get{ return tableManager.tableNumX; }
    }
    public int TableNumY{
        get{ return tableManager.tableNumY; }
    }


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(TableManager.firstTableVec.x,TableManager.firstTableVec.y,-2);
    }

    // Update is called once per frame
    void Update()
    {
        #region 移動
        Vector3 move = new Vector3();
        if(Input.GetKeyDown(KeyCode.RightArrow) && x < TableNumX-1){
            x ++;
            move += new Vector3(tableManager.tableDistX,0);
        }if(Input.GetKeyDown(KeyCode.LeftArrow) && x > 0){
            x --;
            move -= new Vector3(tableManager.tableDistX,0);
        }if(Input.GetKeyDown(KeyCode.DownArrow) && y < TableNumY-1){
            y ++;
            move += new Vector3(0,tableManager.tableDistY);
        }if(Input.GetKeyDown(KeyCode.UpArrow) && y > 0){
            y --;
            move -= new Vector3(0,tableManager.tableDistY);
        }
        transform.position += move;
        #endregion

        #region ぜんざいまわり

        // ZはZENZAIのZ
        if(Input.GetKeyDown(KeyCode.Z)){

            // ぜんざいが無ければぜんざいを置く、そうでなければぜんざいを片付ける
            if(tableManager.tableStates[y][x].ZenzaiState==Table.ZState.NoZenzai)
            {
                tableManager.tableStates[y][x].TryToPutZenzai();
            }else
            {
                tableManager.tableStates[y][x].TryToRemoveZenzai();
            }

        }
        #endregion

    }
}
