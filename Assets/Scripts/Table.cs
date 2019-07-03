using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    #region 定数

    ///<summary>完食にかかるF数</summary>
    static int framesToEat = 100;

    ///<summary>ぜんざいの無い状態で着席し続けるF数</summary>
    static int framesToGetOut = 200;

    #endregion

    #region ぜんざい
    public GameObject zenzaiObj;

    //スプライト(食べてない/食べた)
    public Sprite zenzaiSpr;
    public Sprite zenzaiAteSpr;

    private int _FramesUntilAteUp = 0;
    ///<summary>あと何Fで食べきる？</summary>
    public int FramesUntilAteUp{
        get{return _FramesUntilAteUp; }
        set{
            _FramesUntilAteUp = value;
        }
    }

    
    public enum ZState{
        NoZenzai, BeingEaten, WasEaten
    }
    ///<summary>ぜんざいの状態(神の状態はKamiState)</summary>
    public ZState ZenzaiState = ZState.NoZenzai;

    #endregion

    #region 神

    private GameObject _Kami = null;
    ///<summary>神Object(?)、決まってない場合はnullになってる(今のところ)</summary>
    public GameObject Kami{
        get{
            if(_Kami==null) Debug.Log("神などいない！");

            return _Kami;
        }
    }


    public enum KState{
        NoKami, Coming, LackingOfZenzai, Eating, Ate, Leaving
    }
    ///<summary>神の状態(ぜんざいの状態はZenzaiState)</summary>
    private KState KamiState = KState.NoKami;

    private int FramesUntilGetOut = 100;

    #endregion

    #region 外部からの書き換え関数群

    ///<summary>ぜんざいを机に置く。神が着席していない/ぜんざいが既にある場合はfalseを返します</summary>
    public bool TryToPutZenzai(){

        //神が着席しているがぜんざいが無い状態の時だけ…
        if(KamiState==KState.LackingOfZenzai){

            KamiState = KState.Eating;
            ZenzaiState = ZState.BeingEaten;

            zenzaiObj.SetActive(true);
            zenzaiObj.GetComponent<SpriteRenderer>().sprite = zenzaiSpr;

            _FramesUntilAteUp = framesToEat;

            return true;

        }else{
            return false;
        }
    }

    ///<summary>完食されたぜんざいを片付ける。ぜんざいが無いもしくは食べている途中の場合はfalseを返します</summary>
    public bool TryToRemoveZenzai(){

        //ぜんざい完食済みの時だけ
        if(ZenzaiState==ZState.WasEaten){

            ZenzaiState = ZState.NoZenzai;
            zenzaiObj.SetActive(false);

            FramesUntilGetOut = framesToGetOut;

            //Ate<->lackingOfZenzai間だけはぜんざいの有無によるので…
            if(KamiState==KState.Ate) KamiState = KState.LackingOfZenzai;

            return true;

        }else{
            return false;
        }
    }

    ///<summary>神をスポーンさせます。もういる(退出中等を含む)またはお椀が残っている場合はfalseを返す。</summary>
    public bool TryToPutKami(GameObject kami){

        //神が出現してない、かつぜんざいのお椀が残ってないときだけ出る
        if(KamiState==KState.NoKami && ZenzaiState==ZState.NoZenzai){

            KamiState = KState.Coming;
            this._Kami = Instantiate(kami);
            //机の位置によってどっちからくるか決める
            if(transform.position.x>0){ this._Kami.transform.position = new Vector3(7,transform.position.y+0.7f,3);}
            else                      { this._Kami.transform.position = new Vector3(-7,transform.position.y+0.7f,3);}

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
        switch(KamiState){

            //神が来るまで何もしない
            case KState.NoKami:
                //pass
                break;

            //出現～着席までの処理
            case KState.Coming:

                //右側の机の場合右から左に移動
                if(transform.position.x>0){
                    Kami.transform.position += new Vector3(-0.05f,0,0);
                    //机の前に来たら座る
                    if(transform.position.x>Kami.transform.position.x){
                        Kami.transform.position = new Vector3(transform.position.x,transform.position.y+0.7f,3);
                        KamiState = KState.LackingOfZenzai;
                        FramesUntilGetOut = framesToGetOut;
                    }
                }
                //左側の机の場合左から右に移動
                else{
                    Kami.transform.position += new Vector3(0.05f,0,0);
                    //机の前に来たら座る
                    if(transform.position.x<Kami.transform.position.x){
                        Kami.transform.position = new Vector3(transform.position.x,transform.position.y+0.7f,3);
                        KamiState = KState.LackingOfZenzai;
                        FramesUntilGetOut = framesToGetOut;
                    }
                }

                break;

            //着席かつぜんざいがない->どんどん耐えられなくなる(スコアは増える)
            case KState.LackingOfZenzai:
                ScoreHolder.Instance.score += 1;

                FramesUntilGetOut --;
                if(FramesUntilGetOut==0){
                    KamiState = KState.Leaving;
                }

                break;

            //食べてる(一定時間で完食)
            case KState.Eating:
                FramesUntilAteUp --;
                if(FramesUntilAteUp==0){
                    //完食処理
                    KamiState = KState.Ate;
                    ZenzaiState = ZState.WasEaten;
                    FramesUntilGetOut = framesToGetOut;
                    zenzaiObj.GetComponent<SpriteRenderer>().sprite = zenzaiAteSpr;
                }
                break;

            //完食後(LackingOfZenzaiと同じ処理)
            case KState.Ate:
                ScoreHolder.Instance.score += 1;

                FramesUntilGetOut --;
                if(FramesUntilGetOut==0){
                    KamiState = KState.Leaving;
                }

                break;

            //退出、画面外に出たら消す
            case KState.Leaving:

                //右側の机の場合左から右に移動
                if(transform.position.x>0){
                    Kami.transform.position -= new Vector3(-0.05f,0,0);
                    //画面外に来たら消す
                    if(Kami.transform.position.x>8){
                        Destroy(Kami);
                        _Kami = null;
                        KamiState = KState.NoKami;
                    }
                }
                //左側の机の場合右から左に移動
                else{
                    Kami.transform.position -= new Vector3(0.05f,0,0);
                    //画面外に来たら消す
                    if(Kami.transform.position.x<-8){
                        Destroy(Kami);
                        _Kami = null;
                        KamiState = KState.NoKami;
                    }
                }
                
                break;
        }

    }
}
