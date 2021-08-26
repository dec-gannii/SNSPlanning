using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalGameManager : MonoBehaviour
{
    public static TotalGameManager instance;
    public string nickname;

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
        if (PlayerPrefs.HasKey("nickname"))
        {
            nickname = PlayerPrefs.GetString("nickname");
        }
    }

    public void SetNickName(string name)
    {
        Debug.Log("Ȯ�� ��ư �Է� �� �ѱ�");
        nickname = name;

        if (!PlayerPrefs.HasKey("nickname"))
        {
            PlayerPrefs.SetString("nickname", nickname);
            Debug.Log(nickname + "�� ����Ǿ����ϴ�.");
        }
    }
}
