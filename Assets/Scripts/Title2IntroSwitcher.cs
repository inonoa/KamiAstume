using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title2IntroSwitcher : MonoBehaviour
{
    public GameObject TitleObj;
    public List<GameObject> intros = new List<GameObject>();
    private int introPageNum;
    private int page = 0;
    // Start is called before the first frame update
    void Start()
    {
        introPageNum = intros.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return)){
            if(page==0){
                TitleObj.SetActive(false);
                intros[0].SetActive(true);
                page ++;
            }else if(page<introPageNum){
                intros[page-1].SetActive(false);
                intros[page].SetActive(true);
                page ++;
            }else{
                SceneManager.LoadScene("SampleScene");
            }
        }
        if(Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Backspace)){
            if(page>1){
                intros[page-1].SetActive(false);
                intros[page-2].SetActive(true);
                page --;
            }else if(page==1){
                intros[0].SetActive(false);
                TitleObj.SetActive(true);
                page --;
            }
        }
    }
}
