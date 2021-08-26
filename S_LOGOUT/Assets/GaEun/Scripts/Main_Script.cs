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
            // splash_Panel ��Ȱ��ȭ�Ǿ������� Ȱ��ȭ �� splash() �Լ� ����
            splash_Panel.SetActive(true);
            splash();
        } else
        {
            // splash_Panel Ȱ��ȭ�Ǿ������� splash() �Լ� �ٷ� ����
            splash();
        }


        if (PlayerPrefs.HasKey("nickname"))
        {
            nickname_Text.text = PlayerPrefs.GetString("nickname");
        }
        else
        {
            nickname_Text.text = "���� ����� ���� �����ϴ�.";
        }
    }

    public void splash()
    {
        // 3�ʵ��� ��ٷȴٰ� From_Splash_To_Main_Panel�̶�� �̸��� ���� �Լ� ����
        Invoke("From_Splash_To_Main_Panel", 3f);
    }

    public void From_Splash_To_Main_Panel()
    {
        // Splash ȭ�鿡�� Main ȭ������ �Ѿ���� �ϴ� �Լ�
        splash_Panel.gameObject.SetActive(false);
        main_Panel.SetActive(true);
    }

    public void From_Main_To_Alert_Panel()
    {
        // Main ȭ�鿡�� Alert ȭ������ �Ѿ���� �ϴ� �Լ�
        // Main ȭ�鿡�� ���� ���� ��ư�� ������ �����
        main_Panel.gameObject.SetActive(false);
        alert_Panel.SetActive(true);
    }

    public void From_Main_To_Look_Around_Panel()
    {
        // Main ȭ�鿡�� Look_Around ȭ������ �Ѿ���� �ϴ� �Լ�
        // Main ȭ�鿡�� �ѷ����� ��ư�� ������ �����
        main_Panel.SetActive(false);
        look_Around_Panel.SetActive(true);
    }

    public void From_Alert_To_Input_Nickname_Panel()
    {
        // Alert ȭ�鿡�� Input_Nickname ȭ������ �Ѿ���� �ϴ� �Լ�
        // ���ǻ����� �����ְ� �������� �Ѿ�� ��ư�� ������ �г����� �Է¹޵��� �ϴ� ȭ������ ��ȯ�Ǵ� �Լ�
        alert_Panel.gameObject.SetActive(false);
        input_Nickname_Panel.SetActive(true);
    }

    public void From_Input_Nickname_To_Select_Interest_Panel()
    {
        // Input_Nickname ȭ�鿡�� Select_Interest ȭ������ �Ѿ���� �ϴ� �Լ�
        // �г����� �Է� �ް�(������ ��� Ȯ�� ��ư Ŭ�� ����), �г����� �����ϰ�, ���ɻ� ���� ȭ������ ��ȯ�Ǵ� �Լ�
        Debug.Log("Ȯ�� ��ư Ŭ��");
        if (nickNameField.text == "")
        {
            Debug.Log("Ȯ�� ��ư ����");
            return;
        }

        Save_Nickname();
        input_Nickname_Panel.gameObject.SetActive(false);
        select_Interest_Panel.SetActive(true);
    }

    public void From_Select_Interest_To_Random_Chatting_Explain_Player_Situation_Panel()
    {
        // Select_Interest ȭ�鿡�� Random_Chatting_Explain_Player_Situation ȭ������ �Ѿ���� �ϴ� �Լ�
        // ���ɻ縦 ����ä������ �����ߴٸ�, ����ä�� �÷��̾� ��Ȳ ���� ���� ������ ������ ȭ������ ��ȯ�Ǵ� �Լ�
        select_Interest_Panel.gameObject.SetActive(false);
        random_Chatting_Explain_Player_Situation_Panel.SetActive(true);
    }

    public void From_Select_Interest_To_Game_Explain_Player_Situation_Panel()
    {
        // Select_Interest ȭ�鿡�� Game_Explain_Player_Situation ȭ������ �Ѿ���� �ϴ� �Լ�
        // ���ɻ縦 �������� �����ߴٸ�, ���� �÷��̾� ��Ȳ ���� ���� ������ ������ ȭ������ ��ȯ�Ǵ� �Լ�
        select_Interest_Panel.gameObject.SetActive(false);
        game_Explain_Player_Situation_Panel.SetActive(true);
    }

    public void ChatGame_Start()
    {
        // RandomChatting ����
        SceneManager.LoadScene(chatScene_Name);
    }

    public void Game_Start()
    {
        // Game ����
        SceneManager.LoadScene(gameScene_Name);
    }

    public void Save_Nickname()
    {
        // �г����� �����ϴ� �Լ�
        // �г������� �����ϴ� �Լ�(TotalGameManager�� SetNickName())�� �����ϰ� �ٷ� nickname_Text�� ����
        // ���� �÷��̾� ��Ȳ ������ ������ �г��ӿ� ����� �� �ֵ��� ��
        Debug.Log("Ȯ�� ��ư �Է� ����");
        TotalGameManager.instance.SetNickName(nickNameField.text);

        if (PlayerPrefs.HasKey("nickname"))
        {
            nickname_Text.text = PlayerPrefs.GetString("nickname");
        }
        else
        {
            nickname_Text.text = "���� ����� ���� �����ϴ�.";
        }
    }

    public void From_Look_Around_To_Help_Detail_Panel()
    {
        // Look_Around ȭ�鿡�� Help_Detail ȭ������ �Ѿ���� �ϴ� �Լ�
        // �ѷ����� ��ư�� ���� �� ȭ�鿡�� �����û ��ư�� ������ ����
        look_Around_Panel.gameObject.SetActive(false);
        help_Detail_Panel.SetActive(true);
    }

    public void From_Look_Around_To_Show_Information_Panel()
    {
        // Look_Around ȭ�鿡�� Show_Information ȭ������ �Ѿ���� �ϴ� �Լ�
        // �ѷ����� ��ư�� ���� �� ȭ�鿡�� �������� ��ư�� ������ ����
        look_Around_Panel.gameObject.SetActive(false);
        show_Information_Panel.SetActive(true);
    }

    public void From_Look_Around_To_Show_Card_News_Panel()
    {
        // Look_Around ȭ�鿡�� Show_Card_News ȭ������ �Ѿ���� �ϴ� �Լ�
        // �ѷ����� ��ư�� ���� �� ȭ�鿡�� ī�崺�� ���� ��ư�� ������ ����
        look_Around_Panel.gameObject.SetActive(false);
        show_Card_News_Panel.SetActive(true);
    }

    public void From_Show_Card_News_To_Show_Game_Middle_Ending_Card_News_Panel()
    {
        // Show_Card_News ȭ�鿡�� Show_Game_Middle_Ending_Card_News ȭ������ �Ѿ���� �ϴ� �Լ�
        // ī�崺�� ���� ��ư�� ���� �� ȭ�鿡�� ������ �ʱ� ���� ������ ������ ����
        show_Card_News_Panel.gameObject.SetActive(false);
        show_Game_Middle_Ending_Card_News_Panel.SetActive(true);
    }

    public void From_Show_Card_News_To_Show_Game_Final_Ending_Card_News_Panel()
    {
        // Show_Card_News ȭ�鿡�� Show_Game_Final_Ending_Card_News ȭ������ �Ѿ���� �ϴ� �Լ�
        // ī�崺�� ���� ��ư�� ���� �� ȭ�鿡�� ������ ���� ���� ������ ������ ����
        show_Card_News_Panel.gameObject.SetActive(false);
        show_Game_Final_Ending_Card_News_Panel.SetActive(true);
    }

    public void From_Show_Card_News_To_Show_Random_Chatting_Middle_Ending_Card_News_Panel()
    {
        // Show_Card_News ȭ�鿡�� Show_Random_Chatting_Middle_Ending_Card_News ȭ������ �Ѿ���� �ϴ� �Լ�
        // ī�崺�� ���� ��ư�� ���� �� ȭ�鿡�� ����ä���� �ʱ� ���� ������ ������ ����
        show_Card_News_Panel.gameObject.SetActive(false);
        show_Random_Chatting_Middle_Ending_Card_News_Panel.SetActive(true);
    }

    public void From_Show_Card_News_To_Show_Random_Chatting_Final_Ending_Card_News_Panel()
    {
        // Show_Card_News ȭ�鿡�� Show_Random_Chatting_Final_Ending_Card_News ȭ������ �Ѿ���� �ϴ� �Լ�
        // ī�崺�� ���� ��ư�� ���� �� ȭ�鿡�� ����ä���� ���� ���� ������ ������ ����
        show_Card_News_Panel.gameObject.SetActive(false);
        show_Random_Chatting_Final_Ending_Card_News_Panel.SetActive(true);
    }

    public void Go_Main()
    {
        // Main ȭ������ ���ư��� �Լ�
        // Ȱ��ȭ�Ǿ����� �� �ִ� ��� ȭ���� ��Ȱ��ȭ ��Ű��, Main ȭ���� Ȱ��ȭ
        look_Around_Panel.SetActive(false);
        show_Random_Chatting_Middle_Ending_Card_News_Panel.SetActive(false);
        show_Random_Chatting_Final_Ending_Card_News_Panel.SetActive(false);
        show_Game_Final_Ending_Card_News_Panel.SetActive(false);
        show_Game_Middle_Ending_Card_News_Panel.SetActive(false);

        main_Panel.SetActive(true);
    }

    public void Go_Look_Around()
    {
        // Look_Around ȭ������ ���ư��� �Լ�
        // Ȱ��ȭ�Ǿ����� �� �ִ� ��� ȭ���� ��Ȱ��ȭ ��Ű��, �ѷ����� ȭ������ ���ư����� Look_Around ȭ���� Ȱ��ȭ
        help_Detail_Panel.SetActive(false);
        show_Card_News_Panel.SetActive(false);
        show_Information_Panel.SetActive(false);

        look_Around_Panel.SetActive(true);
    }

    public void Call_Center_01()
    { 
        // ����� ����� ��ȭ�� ���� - 01
        // Application.OpenURL("tel : 1366");
        Application.OpenURL("www.naver.com");

    }

    public void Open_Article_Site_01()
    {
        // ���������� ù��° ��ũ ����
        Application.OpenURL("https://www.bbc.com/korean/news-46148607");
    }

    public void Open_Article_Site_02()
    {
        // ���������� �ι�° ��ũ ����
        Application.OpenURL("https://news.kbs.co.kr/news/view.do?ncd=4420465");
    }

    public void Open_Article_Site_03()
    {
        // ���������� ����° ��ũ ����
        Application.OpenURL("https://www.womentimes.co.kr/news/articleView.html?idxno=50815");
    }

    public void Open_Article_Site_04()
    {
        // ���������� �׹�° ��ũ ����
        Application.OpenURL("https://www.sedaily.com/NewsVIew/22L2Y9VAGC");
    }

    public void Open_Article_Site_05()
    {
        // ���������� �ټ���° ��ũ ����
        Application.OpenURL("https://news.lawtalk.co.kr/article/OA9N3VAJUDAX");
    }

    public void Open_Article_Site_06()
    {
        // ���������� ������° ��ũ ����
        Application.OpenURL("http://www.boannews.com/media/view.asp?idx=97821");
    }
}
