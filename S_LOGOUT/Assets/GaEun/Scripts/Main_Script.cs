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


    // Look_Around_Scene_GameObjects
    public GameObject look_Around_Panel;
    public GameObject help_Detail_Panel;
    public GameObject show_Card_News_Panel;
    public GameObject show_Game_Final_Ending_Card_News_Panel;
    public GameObject show_Game_Middle_Ending_Card_News_Panel;
    public GameObject show_Random_Chatting_Final_Ending_Card_News_Panel;
    public GameObject show_Random_Chatting_Middle_Ending_Card_News_Panel;
    public GameObject show_Information_Panel;

    // Nickname strings
    public string gameScene_Name;
    public string chatScene_Name;

    public TMP_InputField nickNameField;
    public Text nickname_Text;

    // Start is called before the first frame update
    void Start()
    {
        if (splash_Panel.activeSelf == false)
        {
            // splash_Panel 비활성화되어있으면 활성화 후 splash() 함수 실행
            splash_Panel.SetActive(true);
            splash();
        } else
        {
            // splash_Panel 활성화되어있으면 splash() 함수 바로 실행
            splash();
        }


        if (PlayerPrefs.HasKey("nickname"))
        {
            nickname_Text.text = PlayerPrefs.GetString("nickname");
        }
        else
        {
            nickname_Text.text = "아직 저장된 값이 없습니다.";
        }
    }

    public void splash()
    {
        // 3초동안 기다렸다가 From_Splash_To_Main_Panel이라는 이름을 가진 함수 실행
        Invoke("From_Splash_To_Main_Panel", 3f);
    }

    public void From_Splash_To_Main_Panel()
    {
        // Splash 화면에서 Main 화면으로 넘어가도록 하는 함수
        splash_Panel.gameObject.SetActive(false);
        main_Panel.SetActive(true);
    }

    public void From_Main_To_Alert_Panel()
    {
        // Main 화면에서 Alert 화면으로 넘어가도록 하는 함수
        // Main 화면에서 게임 시작 버튼을 누르면 실행됨
        main_Panel.gameObject.SetActive(false);
        alert_Panel.SetActive(true);
    }

    public void From_Main_To_Look_Around_Panel()
    {
        // Main 화면에서 Look_Around 화면으로 넘어가도록 하는 함수
        // Main 화면에서 둘러보기 버튼을 누르면 실행됨
        main_Panel.SetActive(false);
        look_Around_Panel.SetActive(true);
    }

    public void From_Alert_To_Input_Nickname_Panel()
    {
        // Alert 화면에서 Input_Nickname 화면으로 넘어가도록 하는 함수
        // 주의사항을 보여주고 다음으로 넘어가기 버튼을 누르면 닉네임을 입력받도록 하는 화면으로 전환되는 함수
        alert_Panel.gameObject.SetActive(false);
        input_Nickname_Panel.SetActive(true);
    }

    public void From_Input_Nickname_To_Select_Interest_Panel()
    {
        // Input_Nickname 화면에서 Select_Interest 화면으로 넘어가도록 하는 함수
        // 닉네임을 입력 받고(공백인 경우 확인 버튼 클릭 무시), 닉네임을 저장하고, 관심사 선택 화면으로 전환되는 함수
        Debug.Log("확인 버튼 클릭");
        if (nickNameField.text == "")
        {
            Debug.Log("확인 버튼 무시");
            return;
        }

        Save_Nickname();
        input_Nickname_Panel.gameObject.SetActive(false);
        select_Interest_Panel.SetActive(true);
    }

    public void From_Select_Interest_To_Random_Chatting_Explain_Player_Situation_Panel()
    {
        // Select_Interest 화면에서 Random_Chatting_Explain_Player_Situation 화면으로 넘어가도록 하는 함수
        // 관심사를 랜덤채팅으로 선택했다면, 랜덤채팅 플레이어 상황 설명에 대한 설명이 나오는 화면으로 전환되는 함수
        select_Interest_Panel.gameObject.SetActive(false);
        random_Chatting_Explain_Player_Situation_Panel.SetActive(true);
    }

    public void From_Select_Interest_To_Game_Explain_Player_Situation_Panel()
    {
        // Select_Interest 화면에서 Game_Explain_Player_Situation 화면으로 넘어가도록 하는 함수
        // 관심사를 게임으로 선택했다면, 게임 플레이어 상황 설명에 대한 설명이 나오는 화면으로 전환되는 함수
        select_Interest_Panel.gameObject.SetActive(false);
        game_Explain_Player_Situation_Panel.SetActive(true);
    }

    public void ChatGame_Start()
    {
        // RandomChatting 시작
        SceneManager.LoadScene(chatScene_Name);
    }

    public void Game_Start()
    {
        // Game 시작
        SceneManager.LoadScene(gameScene_Name);
    }

    public void Save_Nickname()
    {
        // 닉네임을 저장하는 함수
        // 닉네임으로 설정하는 함수(TotalGameManager의 SetNickName())를 실행하고 바로 nickname_Text로 설정
        // 게임 플레이어 상황 설명에서 나오는 닉네임에 사용할 수 있도록 함
        Debug.Log("확인 버튼 입력 시작");
        TotalGameManager.instance.SetNickName(nickNameField.text);

        if (PlayerPrefs.HasKey("nickname"))
        {
            nickname_Text.text = PlayerPrefs.GetString("nickname");
        }
        else
        {
            nickname_Text.text = "아직 저장된 값이 없습니다.";
        }
    }

    public void From_Look_Around_To_Help_Detail_Panel()
    {
        // Look_Around 화면에서 Help_Detail 화면으로 넘어가도록 하는 함수
        // 둘러보기 버튼을 눌러 들어간 화면에서 도움요청 버튼을 누르면 실행
        look_Around_Panel.gameObject.SetActive(false);
        help_Detail_Panel.SetActive(true);
    }

    public void From_Look_Around_To_Show_Information_Panel()
    {
        // Look_Around 화면에서 Show_Information 화면으로 넘어가도록 하는 함수
        // 둘러보기 버튼을 눌러 들어간 화면에서 정보보기 버튼을 누르면 실행
        look_Around_Panel.gameObject.SetActive(false);
        show_Information_Panel.SetActive(true);
    }

    public void From_Look_Around_To_Show_Card_News_Panel()
    {
        // Look_Around 화면에서 Show_Card_News 화면으로 넘어가도록 하는 함수
        // 둘러보기 버튼을 눌러 들어간 화면에서 카드뉴스 보기 버튼을 누르면 실행
        look_Around_Panel.gameObject.SetActive(false);
        show_Card_News_Panel.SetActive(true);
    }

    public void From_Show_Card_News_To_Show_Game_Middle_Ending_Card_News_Panel()
    {
        // Show_Card_News 화면에서 Show_Game_Middle_Ending_Card_News 화면으로 넘어가도록 하는 함수
        // 카드뉴스 보기 버튼을 눌러 들어간 화면에서 게임의 초기 종료 엔딩을 누르면 실행
        show_Card_News_Panel.gameObject.SetActive(false);
        show_Game_Middle_Ending_Card_News_Panel.SetActive(true);
    }

    public void From_Show_Card_News_To_Show_Game_Final_Ending_Card_News_Panel()
    {
        // Show_Card_News 화면에서 Show_Game_Final_Ending_Card_News 화면으로 넘어가도록 하는 함수
        // 카드뉴스 보기 버튼을 눌러 들어간 화면에서 게임의 최종 종료 엔딩을 누르면 실행
        show_Card_News_Panel.gameObject.SetActive(false);
        show_Game_Final_Ending_Card_News_Panel.SetActive(true);
    }

    public void From_Show_Card_News_To_Show_Random_Chatting_Middle_Ending_Card_News_Panel()
    {
        // Show_Card_News 화면에서 Show_Random_Chatting_Middle_Ending_Card_News 화면으로 넘어가도록 하는 함수
        // 카드뉴스 보기 버튼을 눌러 들어간 화면에서 랜덤채팅의 초기 종료 엔딩을 누르면 실행
        show_Card_News_Panel.gameObject.SetActive(false);
        show_Random_Chatting_Middle_Ending_Card_News_Panel.SetActive(true);
    }

    public void From_Show_Card_News_To_Show_Random_Chatting_Final_Ending_Card_News_Panel()
    {
        // Show_Card_News 화면에서 Show_Random_Chatting_Final_Ending_Card_News 화면으로 넘어가도록 하는 함수
        // 카드뉴스 보기 버튼을 눌러 들어간 화면에서 랜덤채팅의 최종 종료 엔딩을 누르면 실행
        show_Card_News_Panel.gameObject.SetActive(false);
        show_Random_Chatting_Final_Ending_Card_News_Panel.SetActive(true);
    }

    public void Go_Main()
    {
        // Main 화면으로 돌아가는 함수
        // 활성화되어있을 수 있는 모든 화면을 비활성화 시키고, Main 화면을 활성화
        look_Around_Panel.SetActive(false);
        show_Random_Chatting_Middle_Ending_Card_News_Panel.SetActive(false);
        show_Random_Chatting_Final_Ending_Card_News_Panel.SetActive(false);
        show_Game_Final_Ending_Card_News_Panel.SetActive(false);
        show_Game_Middle_Ending_Card_News_Panel.SetActive(false);

        main_Panel.SetActive(true);
    }

    public void Go_Look_Around()
    {
        // Look_Around 화면으로 돌아가는 함수
        // 활성화되어있을 수 있는 모든 화면을 비활성화 시키고, 둘러보기 화면으로 돌아가도록 Look_Around 화면을 활성화
        help_Detail_Panel.SetActive(false);
        show_Card_News_Panel.SetActive(false);
        show_Information_Panel.SetActive(false);

        look_Around_Panel.SetActive(true);
    }

    public void Call_Center_01()
    { 
        // 연계된 기관의 전화로 연결 - 01
        // Application.OpenURL("tel : 1366");
        Application.OpenURL("www.naver.com");

    }

    public void Open_Article_Site_01()
    {
        // 정보보기의 첫번째 링크 연결
        Application.OpenURL("https://www.bbc.com/korean/news-46148607");
    }

    public void Open_Article_Site_02()
    {
        // 정보보기의 두번째 링크 연결
        Application.OpenURL("https://news.kbs.co.kr/news/view.do?ncd=4420465");
    }

    public void Open_Article_Site_03()
    {
        // 정보보기의 세번째 링크 연결
        Application.OpenURL("https://www.womentimes.co.kr/news/articleView.html?idxno=50815");
    }

    public void Open_Article_Site_04()
    {
        // 정보보기의 네번째 링크 연결
        Application.OpenURL("https://www.sedaily.com/NewsVIew/22L2Y9VAGC");
    }

    public void Open_Article_Site_05()
    {
        // 정보보기의 다섯번째 링크 연결
        Application.OpenURL("https://news.lawtalk.co.kr/article/OA9N3VAJUDAX");
    }

    public void Open_Article_Site_06()
    {
        // 정보보기의 여섯번째 링크 연결
        Application.OpenURL("http://www.boannews.com/media/view.asp?idx=97821");
    }
}
