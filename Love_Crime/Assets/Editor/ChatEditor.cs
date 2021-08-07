using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // 에디터 사용을 위해서 사용


[CustomEditor(typeof(ChatManager))] // 대상
public class ChatEditor : Editor
{
    ChatManager chatManager;
    string text;

    // 스크립트가 선택 시 실행되는 함수
    private void OnEnable()
    {
        chatManager = target as ChatManager; // 오브젝트 형식의 ChatManager를 대입
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();

        text = EditorGUILayout.TextArea(text); // Text 입력 받고

        // 버튼 형식이고 text가 비어있지 않을 때만 작동되도록 한다
        if (GUILayout.Button("보내기", GUILayout.Width(60)) && text.Trim() != "")
        {
            // 내가 보냈기 때문에 처음 값이 true
            chatManager.Chat(true, text, "나", null);
            text = "";
            GUI.FocusControl(null); // Text가 남아있는 버그를 없애기 위해서
        }

        if (GUILayout.Button("받기", GUILayout.Width(60)) && text.Trim() != "")
        {

            chatManager.Chat(false, text, "타인", null);

            // 프로필 사진 바꾸기 위해서
            // chatManager.Chat(false, text, "나쁜사람", Resources.Load<Texture2D>("ETC/suspect"));
            text = "";
            GUI.FocusControl(null);
        }

        EditorGUILayout.EndHorizontal();
    }
}
