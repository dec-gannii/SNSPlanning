using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TotalGameManager : MonoBehaviour
{
    public static TotalGameManager instance;
    public string nickname;
    public bool popup_OK;

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
        Debug.Log("???? ???? ???? ?? ????");
        nickname = name;

        if (!PlayerPrefs.HasKey("nickname"))
        {
            PlayerPrefs.SetString("nickname", nickname);
            Debug.Log(nickname + "?? ??????????????.");
        }
    }

    public bool Is_PopUp_OK_Button_Clicked()
    {
        return popup_OK;
    }
}
