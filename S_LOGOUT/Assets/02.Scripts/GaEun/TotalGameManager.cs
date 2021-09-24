using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TotalGameManager : MonoBehaviour
{
    public static TotalGameManager instance;

    // nickname
    public string nickname;

    // bool type으로 다른 scene의 panel 띄우기 관리

    private bool popup_OK; // 팝업창 눌렀는지 나타내는 변수
    private bool isEnded; // 최종 엔딩인지 확인하는 변수
    private bool go_Main; // 처음으로 번튼 눌렀는지 확인하는 변수
    private bool go_Look_Around; // 둘러보기 버튼 눌렀는지 확인하는 변수

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        popup_OK = false;

        if (PlayerPrefs.HasKey("nickname"))
        {
            nickname = PlayerPrefs.GetString("nickname");
        }
    }

    public void SetNickName(string name)
    {
        nickname = name;

        if (!PlayerPrefs.HasKey("nickname"))
        {
            PlayerPrefs.SetString("nickname", nickname);
            Debug.Log(nickname + "이 없습니다.");
        }
    }

    // popup의 OK 버튼 눌렸는지 확인 (변수 popup_OK 리턴)
    public bool Is_PopUp_OK_Button_Clicked()
    {
        return popup_OK;
    }

    // 변수 popup_OK의 상태를 받은 매개변수로 설정
    public void Set_Popup_Clicked(bool clicked)
    {
        popup_OK = clicked;
    }

    // 메인 화면으로 돌아가는 버튼 (처음으로 버튼) 눌렸는지 확인 (변수 go_Main 리턴)
    public bool Is_Go_Main_Button_Clicked()
    {
        return go_Main;
    }

    // 변수 go_Main의 상태를 받은 매개변수로 설정
    public void Set_Go_Main(bool clicked)
    {
        go_Main = clicked;
    }

    // 최종 엔딩 나타나는지 확인
    public bool Is_Ended_Button_Clicked()
    {
        return isEnded;
    }

    // 변수 isEnded의 상태를 받은 매개변수로 설정
    public void Set_Is_Ended(bool clicked)
    {
        isEnded = clicked;
    }

    // 변수 go_Look_Around의 상태를 받은 매개변수로 설정
    public void Set_Go_Look_Around(bool clicked)
    {
        go_Look_Around = clicked;
    }

    // 둘러보기 버튼 클릭 됐는지 확인 
    public bool Is_Go_Look_Around_Button_Clicked()
    {
        return go_Look_Around;
    }
}
