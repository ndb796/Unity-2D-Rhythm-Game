using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    // 파이어베이스 인증 기능 객체
    private FirebaseAuth auth;

    // 이메일 및 패스워드 UI
    public InputField emailInputField;
    public InputField passwordInputField;

    // 로그인 결과 UI
    public Text messageUI;

    void Start()
    {
        Screen.SetResolution(1920, 1200, true);
        // 파이어베이스 인증 객체를 초기화합니다.
        auth = FirebaseAuth.DefaultInstance;
        messageUI.text = "";
    }

    public void Login()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;
        // 인증 객체를 이용해 이메일과 비밀번호로 로그인을 수행합니다.
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(
            task =>
            {
                if(task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
                {
                    PlayerInformation.auth = auth;
                    SceneManager.LoadScene("SongSelectScene");
                }
                else
                {
                    messageUI.text = "계정을 다시 확인해주세요.";
                }
            }
        );
    }

    public void GoToJoin()
    {
        SceneManager.LoadScene("JoinScene");
    }
}
