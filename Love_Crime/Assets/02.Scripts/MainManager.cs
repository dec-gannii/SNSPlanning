using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{

    public GameObject main_Panel;
    public GameObject situation_Panel;
    public GameObject caution_Panel;
    public GameObject input_Name_Panel;

    public string gameScene_Name;
    public string chatScene_Name;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void From_Caution_To_InputName_Panel()
    {
        caution_Panel.gameObject.SetActive(false);
        input_Name_Panel.SetActive(true);
    }

    public void From_InputName_To_Main_Panel()
    {
        input_Name_Panel.gameObject.SetActive(false);
        main_Panel.SetActive(true);
    }

    public void From_Main_To_Situation_Panel()
    {
        main_Panel.gameObject.SetActive(false);
        situation_Panel.SetActive(true);
    }

    public void ChatGame_Start()
    {
        SceneManager.LoadScene(chatScene_Name);
    }

    public void Game_Start()
    {
        SceneManager.LoadScene(gameScene_Name);
    }

}
