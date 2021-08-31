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
    private bool popup_OK;
    private bool go_Main;

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
}
