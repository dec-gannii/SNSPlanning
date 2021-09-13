using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Look_Around_Script : MonoBehaviour
{
    // 배열을 다루기 위한 index 선언 및 초기화
    int index = 0;

    // Look_Around_Scene_GameObjects
    public GameObject look_Around_Panel;
    public GameObject help_Detail_Panel;
    public GameObject show_Card_News_Panel;
    public GameObject show_Game_Final_Ending_Card_News_Panel;
    public GameObject show_Game_Middle_Ending_Card_News_Panel;
    public GameObject show_Random_Chatting_Final_Ending_Card_News_Panel;
    public GameObject show_Random_Chatting_Middle_Ending_Card_News_Panel;
    public GameObject show_Information_Panel;
    public GameObject info_Detail_Panel;
    public GameObject report_Panel;

    public Image game_Final_Cardnews_Image_View;
    public Image random_Chatting_Final_Cardnews_Image_View;
    public Image middle_Cardnews01_Image_View;
    public Image middle_Cardnews02_Image_View;

    public Image Infographic_Image_View;

    Sprite Current_Cardnews;

    // Sprite 이미지 배열 (카드뉴스 이미지 배열)
    Sprite[] game_Sprites;
    Sprite[] random_Chatting_Sprites;
    Sprite[] middle_Ending01_Sprites;
    Sprite[] middle_Ending02_Sprites;

    Sprite[] Infographics;

    void Start()
    {
        // 카드뉴스 이미지들 해당하는 이미지들별로 배열에 저장
        game_Sprites = Resources.LoadAll<Sprite>("GaEun/CardNews/Game_Ending");
        random_Chatting_Sprites = Resources.LoadAll<Sprite>("GaEun/CardNews/Random_Chatting_Ending");
        middle_Ending01_Sprites = Resources.LoadAll<Sprite>("GaEun/CardNews/Middle_Ending01");
        middle_Ending02_Sprites = Resources.LoadAll<Sprite>("GaEun/CardNews/Middle_Ending02");

        Infographics = Resources.LoadAll<Sprite>("GaEun/Images/Info_Detail_Panel");

        if (TotalGameManager.instance.Is_PopUp_OK_Button_Clicked() == true)
        {
            // TotalGameManager의 isPopupOKButtonClicked 메소드에서 리턴된 팝업 클릭 여부가 참이면
            // index를 0으로 설정하고, 현재 카드뉴스를 초기엔딩 카드뉴스의 0번째 이미지로 설정
            index = 0;

            Current_Cardnews = middle_Ending02_Sprites[index];

            // 랜덤채팅에서 초기 엔딩으로 가는 경우
            show_Random_Chatting_Middle_Ending_Card_News_Panel.SetActive(true);

            // TotalGameManager의 setPopupClicked 메소드 사용해서 TotalGameManger의 popup_Clicked 변수 다시 false로 변경
            TotalGameManager.instance.Set_Popup_Clicked(false);
        }
        else if (TotalGameManager.instance.Is_Ended_Button_Clicked() == true)
        {
            // TotalGameManager의 isPopupOKButtonClicked 메소드에서 리턴된 팝업 클릭 여부가 참이면
            // index를 0으로 설정하고, 현재 카드뉴스를 초기엔딩 카드뉴스의 0번째 이미지로 설정
            index = 0;

            Current_Cardnews = random_Chatting_Sprites[index];

            // 랜덤채팅에서 최종 엔딩으로 가는 경우
            show_Random_Chatting_Final_Ending_Card_News_Panel.SetActive(true);

            // TotalGameManager의 setIsEnded 메소드 사용해서 TotalGameManger의 isEnded 변수 다시 false로 변경
            TotalGameManager.instance.Set_Is_Ended(false);
        }
    }
    // Change Panel From Look Around To Help Detail
    // Look Around Panel에서 도움요청 버튼 클릭하면 실행
    public void From_Look_Around_To_Help_Detail_Panel()
    {
        look_Around_Panel.gameObject.SetActive(false);
        help_Detail_Panel.SetActive(true);
    }

    // Change Panel From Look Around To Show Information
    // Look Around Panel에서 정보보기 버튼 클릭하면 실행
    public void From_Look_Around_To_Show_Information_Panel()
    {
        look_Around_Panel.gameObject.SetActive(false);
        show_Information_Panel.SetActive(true);
    }

    // Change Panel From Look Around To Show Cardnews Panel
    // Look Around Panel에서 카드뉴스 보기 버튼 클릭하면 실행
    public void From_Look_Around_To_Show_Card_News_Panel()
    {
        //index = 0;
        look_Around_Panel.gameObject.SetActive(false);
        show_Card_News_Panel.SetActive(true);
    }

    // Change Panel From Look Around To Report
    // Look Around Panel에서 가해자 신고하기 버튼 클릭하면 실행
    public void From_Look_Around_To_Report_Panel()
    {
        look_Around_Panel.gameObject.SetActive(false);
        report_Panel.SetActive(true);
    }

    // Change Panel From Show Cardnews To Show Game Middle Ending Cardnews
    // Show Card News Panel에서 게임 section의 초기 종료 엔딩 보기 버튼 클릭 시 실행
    public void From_Show_Card_News_To_Show_Game_Middle_Ending_Card_News_Panel()
    {
        // 카드 뉴스 보기 화면 뜨기 전 혹시 모르니 한 번 더 index 초기화
        index = 0;

        show_Card_News_Panel.gameObject.SetActive(false);
        // Current_Cardnews라는 sprite 변수에 게임 초기 종료 엔딩의 카드뉴스의 가장 첫 번째 장으로 지정
        Current_Cardnews = middle_Ending01_Sprites[index];
        // 게임의 초기 종료 엔딩의 카드뉴스 이미지가 뜨는 UI의 sprite 이미지를 Current_Cardnews 변수에 저장된 sprite 이미지로 지정
        middle_Cardnews01_Image_View.sprite = Current_Cardnews;
        show_Game_Middle_Ending_Card_News_Panel.SetActive(true);
    }

    // Change Panel From Show Cardnews To Show Game Final Ending Cardnews
    // Show Card News Panel에서 게임 section의 최종 엔딩 보기 버튼 클릭 시 실행
    public void From_Show_Card_News_To_Show_Game_Final_Ending_Card_News_Panel()
    {
        // 카드 뉴스 보기 화면 뜨기 전 혹시 모르니 한 번 더 index 초기화
        index = 0;

        show_Card_News_Panel.gameObject.SetActive(false);
        // Current_Cardnews라는 sprite 변수에 게임 최종 엔딩의 카드뉴스의 가장 첫 번째 장으로 지정
        Current_Cardnews = game_Sprites[index];
        // 게임 최종 엔딩의 카드뉴스 이미지가 뜨는 UI의 sprite 이미지를 Current_Cardnews 변수에 저장된 sprite 이미지로 지정
        game_Final_Cardnews_Image_View.sprite = Current_Cardnews;
        show_Game_Final_Ending_Card_News_Panel.SetActive(true);
    }

    // Change Panel From Show Cardnews To Show Random Chatting Middle Ending Cardnews
    // Show Card News Panel에서 랜덤채팅 section의 초기 종료 엔딩 보기 버튼 클릭 시 실행
    public void From_Show_Card_News_To_Show_Random_Chatting_Middle_Ending_Card_News_Panel()
    {
        // 카드 뉴스 보기 화면 뜨기 전 혹시 모르니 한 번 더 index 초기화
        index = 0;

        show_Card_News_Panel.gameObject.SetActive(false);
        // Current_Cardnews라는 sprite 변수에 랜덤채팅 초기 종료 엔딩의 카드뉴스의 가장 첫 번째 장으로 지정
        Current_Cardnews = middle_Ending02_Sprites[index];
        // 랜덤채팅의 초기 종료 엔딩의 카드뉴스 이미지가 뜨는 UI의 sprite 이미지를 Current_Cardnews 변수에 저장된 sprite 이미지로 지정
        middle_Cardnews02_Image_View.sprite = Current_Cardnews;
        show_Random_Chatting_Middle_Ending_Card_News_Panel.SetActive(true);
    }

    // Change Panel From Show Cardnews To Show Random Chatting Final Ending Cardnews
    // Show Card News Panel에서 랜덤채팅 section의 최종 엔딩 보기 버튼 클릭 시 실행
    public void From_Show_Card_News_To_Show_Random_Chatting_Final_Ending_Card_News_Panel()
    {
        // 카드 뉴스 보기 화면 뜨기 전 혹시 모르니 한 번 더 index 초기화
        index = 0;

        show_Card_News_Panel.gameObject.SetActive(false);
        // Current_Cardnews라는 sprite 변수에 랜덤채팅 최종 엔딩의 카드뉴스의 가장 첫 번째 장으로 지정
        Current_Cardnews = random_Chatting_Sprites[index];
        // 랜덤채팅의 최종 엔딩의 카드뉴스 이미지가 뜨는 UI의 sprite 이미지를 Current_Cardnews 변수에 저장된 sprite 이미지로 지정
        random_Chatting_Final_Cardnews_Image_View.sprite = Current_Cardnews;
        show_Random_Chatting_Final_Ending_Card_News_Panel.SetActive(true);
    }

    public void From_Show_Information_To_Info_Detail_Panel()
    {
        show_Information_Panel.SetActive(false);
        info_Detail_Panel.SetActive(true);
    }

    // Main 화면으로 돌아가기 위한 메소드
    // 활성화 상태에 문제가 될만한 Panel들을 모두 비활성화하고, Main 화면을 활성화한다.
    public void Go_Main()
    {
        TotalGameManager.instance.Set_Go_Main(true);
        SceneManager.LoadScene("Main_Scene");
        //look_Around_Panel.SetActive(false);
        //show_Random_Chatting_Middle_Ending_Card_News_Panel.SetActive(false);
        //show_Random_Chatting_Final_Ending_Card_News_Panel.SetActive(false);
        //show_Game_Final_Ending_Card_News_Panel.SetActive(false);
        //show_Game_Middle_Ending_Card_News_Panel.SetActive(false);

        //main_Panel.SetActive(true);
    }

    public void Go_Look_Around()
    {
        help_Detail_Panel.SetActive(false);
        show_Information_Panel.SetActive(false);
        show_Card_News_Panel.SetActive(false);
        report_Panel.SetActive(false);

        look_Around_Panel.SetActive(true);
    }

    public void Go_Info_Main()
    {
        info_Detail_Panel.SetActive(false);
        show_Information_Panel.SetActive(true);
    }

    public void Call_Center_01()
    {
        // 연계 기관에 전화 연결이 가능하도록 한다.
        Application.OpenURL("tel:0317561388");

    }

    public void Open_Chat_Profile_01()
    {
        // 연계 기관이 카카오톡 채널이 있는 경우 인터넷으로 카카오톡 프로필이 바로 열릴 수 있도록 한다.
        Application.OpenURL("http://pf.kakao.com/_FyKxhs");
    }

    public void Call_Center_02()
    {
        // 연계 기관에 전화 연결이 가능하도록 한다.
        Application.OpenURL("tel:0317299360");

    }

    public void Call_Center_03()
    {
        // 연계 기관에 전화 연결이 가능하도록 한다.
        Application.OpenURL("tel:0312151117");

    }

    public void Call_Center_04()
    {
        // 연계 기관에 전화 연결이 가능하도록 한다.
        Application.OpenURL("tel:027358994");

    }

    public void Call_Center_05()
    {
        // 연계 기관에 전화 연결이 가능하도록 한다.
        Application.OpenURL("tel:0317511120");

    }

    public void Open_Article_Site_01()
    {
        Infographic_Image_View.sprite = Infographics[0];
        From_Show_Information_To_Info_Detail_Panel();
    }

    public void Open_Article_Site_02()
    {
        Infographic_Image_View.sprite = Infographics[1];
        From_Show_Information_To_Info_Detail_Panel();
    }

    public void Open_Article_Site_03()
    {
        Infographic_Image_View.sprite = Infographics[2];
        From_Show_Information_To_Info_Detail_Panel();
    }

    public void Open_Article_Site_04()
    {
        Infographic_Image_View.sprite = Infographics[3];
        From_Show_Information_To_Info_Detail_Panel();
    }

    public void Open_Article_Site_05()
    {
        Infographic_Image_View.sprite = Infographics[4];
        From_Show_Information_To_Info_Detail_Panel();
    }

    public void Open_Article_Site_06()
    {
        Infographic_Image_View.sprite = Infographics[5];
        From_Show_Information_To_Info_Detail_Panel();
    }

    public void Open_Report_Site()
    {
        // 가해자 신고 기관의 사이트에 접속하도록 연결한다.
        Application.OpenURL("http://www.safe182.go.kr/index.do");
    }

    // 이전 카드뉴스 이미지를 보여주도록 한다.
    // 카드뉴스 보기에서 카드뉴스 이미지 위의 왼쪽(이전) 화살표 버튼 클릭 시 실행
    public void Show_Previous_CardNews()
    {
        // 만약 이미지가 가장 첫 장이라면 (0번째 index인 경우)
        if (index == 0)
        {
            // 클릭된 버튼 무시
            return;
        }

        // Current_Cardnews 변수에 저장되어 있는 이미지가 무슨 이미지인가에 따라서
        // index를 1만큼 감소시키고, 감소시킨 인덱스에 해당하는 이미지를 Current_Cardnews 변수에 넣어
        // Current_Cardnews 이미지를 현재 image view의 sprite 이미지로 지정한다.
        if (Current_Cardnews == random_Chatting_Sprites[index])
        {
            index--;
            Current_Cardnews = random_Chatting_Sprites[index];
            random_Chatting_Final_Cardnews_Image_View.sprite = Current_Cardnews;
        }
        else if (Current_Cardnews == game_Sprites[index])
        {
            index--;
            Current_Cardnews = game_Sprites[index];
            game_Final_Cardnews_Image_View.sprite = Current_Cardnews;
        }
        else if (Current_Cardnews == middle_Ending01_Sprites[index])
        {
            index--;
            Current_Cardnews = middle_Ending01_Sprites[index];
            middle_Cardnews01_Image_View.sprite = Current_Cardnews;
        }
        else if (Current_Cardnews == middle_Ending02_Sprites[index])
        {
            index--;
            Current_Cardnews = middle_Ending02_Sprites[index];
            middle_Cardnews02_Image_View.sprite = Current_Cardnews;
        }
    }

    // 다음 카드뉴스 이미지를 보여주도록 한다.
    // 카드뉴스 보기에서 카드뉴스 이미지 위의 오른쪽(다음) 화살표 버튼 클릭 시 실행
    public void Show_Next_CardNews()
    {
        // Current_Cardnews 변수에 저장되어 있는 이미지가 무슨 이미지인가에 따라서
        // index를 1만큼 증가시키고, 증가시킨 인덱스에 해당하는 이미지를 Current_Cardnews 변수에 넣어
        // Current_Cardnews 이미지를 현재 image view의 sprite 이미지로 지정한다.
        if (Current_Cardnews == random_Chatting_Sprites[index])
        {
            if (index == random_Chatting_Sprites.Length - 1)
            {
                // 만약 index가 Current_Cardnews에 저장된 이미지가 있는 배열의 크기 - 1 한 것과 같으면 버튼 클릭을 무시
                return;
            }
            index++;
            Current_Cardnews = random_Chatting_Sprites[index];
            random_Chatting_Final_Cardnews_Image_View.sprite = Current_Cardnews;
        }
        else if (Current_Cardnews == game_Sprites[index])
        {
            if (index == game_Sprites.Length - 1)
            {
                return;
            }
            index++;
            Current_Cardnews = game_Sprites[index];
            game_Final_Cardnews_Image_View.sprite = Current_Cardnews;
        }
        else if (Current_Cardnews == middle_Ending01_Sprites[index])
        {
            if (index == middle_Ending01_Sprites.Length - 1)
            {
                return;
            }
            index++;
            Current_Cardnews = middle_Ending01_Sprites[index];
            middle_Cardnews01_Image_View.sprite = Current_Cardnews;
        }
        else if (Current_Cardnews == middle_Ending02_Sprites[index])
        {
            if (index == middle_Ending02_Sprites.Length - 1)
            {
                return;
            }
            index++;
            Current_Cardnews = middle_Ending02_Sprites[index];
            middle_Cardnews02_Image_View.sprite = Current_Cardnews;
        }
    }

}
