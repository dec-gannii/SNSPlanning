using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Main_Script : MonoBehaviour
{
    // Main_Scene_GameObjects
    public GameObject splash_Panel;
    public GameObject main_Panel;
    public GameObject alert_Panel;
    public GameObject input_Nickname_Panel;
    public GameObject select_Interest_Panel;
    public GameObject random_Chatting_Explain_Player_Situation_Panel;
    public GameObject game_Explain_Player_Situation_Panel;

    public GameObject credit_Panel;

    // For manage Nickname String
    public TMP_InputField nickNameField;
    public Text nickname_Text;

    // Start is called before the first frame update
    void Start()
    {
        if (TotalGameManager.instance.Is_Go_Main_Button_Clicked() == true)
        {
            main_Panel.SetActive(true);
            TotalGameManager.instance.Set_Go_Main(false);
        }
        if (splash_Panel.activeSelf == false)
        {
            // splash_Panel 비활성화 되어있는 경우에 활성화한 후 splash() 메소드 실행
            splash_Panel.SetActive(true);
            splash();
        }
        else
        {
            // splash_Panel 활성화 되어있는 경우 splash() 메소드 그냥 실행
            splash();
        }

        // 이미 nickname이라는 키 있으면 nickname_text를 nickname이라는 키에 저장된 문자열로 지정
        if (PlayerPrefs.HasKey("nickname"))
        {
            nickname_Text.text = PlayerPrefs.GetString("nickname");
        }
        else
        {
            nickname_Text.text = "저장된 닉네임이 없습니다.";
        }
    }

    // 3초 대기 했다가 메소드 이름이 From_Splash_To_Main_Panel인 메소드 실행
    public void splash()
    {
        Invoke("From_Splash_To_Main_Panel", 3f);
    }

    // Chagne Panel From Splash To Main
    public void From_Splash_To_Main_Panel()
    {
        //index = 0;
        splash_Panel.gameObject.SetActive(false);
        main_Panel.SetActive(true);
        Sound_Manager.instance.Title_BGM_Play();
    }

    // Chagne Panel From Main To Alert
    // Main Panel에서 시작 버튼 클릭하면 실행
    public void From_Main_To_Alert_Panel()
    {
        main_Panel.gameObject.SetActive(false);
        alert_Panel.SetActive(true);
    }

    // Change Panel From Main To Look Around
    // Main Panel에서 둘러보기 버튼 클릭하면 실행
    public void From_Main_To_Look_Around_Panel()
    {
        SceneManager.LoadScene("Look_Around_Scene");
    }

    // Change Panel From Alert To Input Nickname
    // Alert Panel에서 다음으로 넘어가기 버튼 클릭하면 실행
    public void From_Alert_To_Input_Nickname_Panel()
    {
        if (PlayerPrefs.HasKey("nickname"))
        {
            alert_Panel.gameObject.SetActive(false);
            select_Interest_Panel.SetActive(true);
        }
        else
        {
            alert_Panel.gameObject.SetActive(false);
            input_Nickname_Panel.SetActive(true);
        }
    }

    // Change Panel From Input Nickname To Select Interest
    // Input Nickname Panel에서 닉네임 입력 후 확인 버튼 클릭하면 실행
    public void From_Input_Nickname_To_Select_Interest_Panel()
    {
        // 아무것도 입력하지 않으면 확인 버튼 무시
        if (nickNameField.text == "") return;

        // 닉네임 저장 함수 실행
        Save_Nickname();
        input_Nickname_Panel.gameObject.SetActive(false);
        select_Interest_Panel.SetActive(true);
    }

    // Change Panel From Select Interest To Random Chatting Explain Player Situation
    // Select Interest Panel에서 랜덤채팅 버튼 클릭하면 실행
    public void From_Select_Interest_To_Random_Chatting_Explain_Player_Situation_Panel()
    {
        select_Interest_Panel.gameObject.SetActive(false);
        random_Chatting_Explain_Player_Situation_Panel.SetActive(true);
    }

    // Change Panel From Select Interest To Game Explain Player Situation
    // Select Interest Panel에서 게임 버튼 클릭하면 실행
    public void From_Select_Interest_To_Game_Explain_Player_Situation_Panel()
    {
        select_Interest_Panel.gameObject.SetActive(false);
        game_Explain_Player_Situation_Panel.SetActive(true);
    }

    // 닉네임 저장 메소드
    public void Save_Nickname()
    {
        // TotalGameManager의 메소드 중 SetNickName에 매개변수로 nickNameField에 작성한 text 전달
        TotalGameManager.instance.SetNickName(nickNameField.text);

        if (PlayerPrefs.HasKey("nickname"))
        {
            // 만약 이미 키 'nickname'이 존재하면 nickname_Text의 text를 해당 키에서 가져온 값으로 저장
            nickname_Text.text = PlayerPrefs.GetString("nickname");
        }
        else
        {
            nickname_Text.text = "저장된 닉네임이 없습니다.";
        }
    }

    // Change Scene To 'rChatting'
    // random_Chatting_Explain_Player_Situation_Panel에서 '낯선 사람과 랜덤채팅하러 가기' 버튼 클릭 시 실행
    public void From_Explain_Player_Situation_To_Random_Chatting_Start()
    {
        Sound_Manager.instance.Title_BGM_Stop();
        TotalGameManager.instance.Set_Is_It_Game(false);
        random_Chatting_Explain_Player_Situation_Panel.SetActive(false);
        SceneManager.LoadScene("rChatting");
    }

    public void From_Explain_Player_Situation_To_Game_Chatting_Start()
    {
        Sound_Manager.instance.Title_BGM_Stop();
        TotalGameManager.instance.Set_Is_It_Game(true);
        random_Chatting_Explain_Player_Situation_Panel.SetActive(false);
        SceneManager.LoadScene("gChatting");
    }

    public void From_Main_To_Credit_Panel_On()
    {
        credit_Panel.SetActive(true);
    }

    public void From_Main_To_Credit_Panel_Off()
    {
        credit_Panel.SetActive(false);
    }
}
