using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    static int framesToEat = 300;
    static int framesToGetOut = 100;

    #region ぜんざい
    public GameObject zenzaiObj; //これはなに
    public Sprite zenzaiSpr;
    public Sprite zenzaiAteSpr;
    private int _FramesUntilAteUp = 0;
    public int FramesUntilAteUp{
        get{return _FramesUntilAteUp; }
        set{
            _FramesUntilAteUp = value;
        }
    }
    #endregion

    #region 神
    private GameObject _Kami = null;
    public GameObject Kami{
        get{
            if(_Kami==null){
                Debug.Log("神などいない！");
            }
            return _Kami;
        }
    }
    public enum KState{
        NoKami, Coming, lackingOfZenzai, Eating, Ate, GettingOut
    }
    private KState _KamiState = KState.NoKami;
    public KState KamiState{
        get{ return _KamiState; }
    }
    public bool IsSatByKami{
        get{ return _KamiState!=KState.NoKami; }
    }

    private int _FramesUntilGetOut = 100;
    public int FramesUntilGetOut{
        get{ return _FramesUntilGetOut;}
    }
    #endregion

    #region Setter
    public bool TryToPutZenzai(){
        if(KamiState==KState.lackingOfZenzai){
            _KamiState = KState.Eating;
            zenzaiObj.SetActive(true);
            zenzaiObj.GetComponent<SpriteRenderer>().sprite = zenzaiSpr;
            _FramesUntilAteUp = framesToEat;
            return true;
        }else{
            return false;
        }
    }
    public bool TryToRemoveZenzai(){
        if(KamiState==KState.Ate){
            _KamiState = KState.lackingOfZenzai;
            zenzaiObj.SetActive(false);
            return true;
        }else{
            return false;
        }
    }
    public bool TryToPutKami(GameObject kami){
        if(KamiState==KState.NoKami){
            _KamiState = KState.Coming;
            this._Kami = kami;
            if(transform.position.x>0){
                kami.transform.position = new Vector3(5,transform.position.y+0.5f,3);
            }else{
                kami.transform.position = new Vector3(-5,transform.position.y+0.5f,3);
            }
            return true;
        }else{
            return false;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //食べるとこ
        if(KamiState==KState.Eating){
            FramesUntilAteUp --;
            if(FramesUntilAteUp==0){
                _KamiState = KState.Ate;
                _FramesUntilGetOut = framesToGetOut;
                zenzaiObj.GetComponent<SpriteRenderer>().sprite = zenzaiAteSpr;
            }
        }

        //食べてないとき
        if(KamiState==KState.Ate || KamiState==KState.lackingOfZenzai){
            _FramesUntilGetOut --;
            if(FramesUntilGetOut==0){
                _KamiState = KState.GettingOut;
            }
        }

        //今は罷らむ
        if(KamiState==KState.GettingOut){
            if(transform.position.x>0){
                Kami.transform.position -= new Vector3(-0.05f,0,0);
                if(Kami.transform.position.x>8){
                    Destroy(Kami);
                    _Kami = null;
                    _KamiState = KState.NoKami;
                }
            }
            else{
                Kami.transform.position -= new Vector3(0.05f,0,0);
                if(Kami.transform.position.x<-8){
                    Destroy(Kami);
                    _Kami = null;
                    _KamiState = KState.NoKami;
                }
            }
        }

        //神がやってくるとこ
        if(KamiState==KState.Coming){
            if(transform.position.x>0){
                Kami.transform.position += new Vector3(-0.05f,0,0);
                if(Kami.transform.position.x<transform.position.x){
                    //座る
                    Kami.transform.position = new Vector3(transform.position.x,transform.position.y+0.5f,3);
                    _KamiState = KState.lackingOfZenzai;
                    _FramesUntilGetOut = framesToGetOut;
                }
            }
            else{
                Kami.transform.position += new Vector3(0.05f,0,0);
                if(Kami.transform.position.x>transform.position.x){
                    //座る
                    Kami.transform.position = new Vector3(transform.position.x,transform.position.y+0.5f,3);
                    _KamiState = KState.lackingOfZenzai;
                    _FramesUntilGetOut = framesToGetOut;
                }
            }
        }

    }
}
