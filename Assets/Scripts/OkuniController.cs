using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkuniController : MonoBehaviour
{
    public TableManager tableManager;
    private int x = 0;
    private int y = 0;
    public int TableNumX{
        get{ return tableManager.tableNumX; }
    }
    public int TableNumY{
        get{ return tableManager.tableNumY; }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(tableManager.firstTableVec.x,tableManager.firstTableVec.y,-2);
    }

    // Update is called once per frame
    void Update()
    {
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
        if(Input.GetKeyDown(KeyCode.Z)){
            Debug.Log(tableManager.tableStates[y][x].KamiState);
            if(tableManager.tableStates[y][x].KamiState==Table.KState.lackingOfZenzai){
                Debug.Log(tableManager.tableStates[y][x].TryToPutZenzai());
            }else if(tableManager.tableStates[y][x].KamiState==Table.KState.Ate){
                tableManager.tableStates[y][x].TryToRemoveZenzai();
            }
        }
    }
}
