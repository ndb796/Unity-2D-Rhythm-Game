using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JoinManager : MonoBehaviour
{
    // 파이어베이스 인증 기능 객체
    private FirebaseAuth auth;

    // 이메일 및 패스워드 UI
    public InputField emailInputField;
    public InputField passwordInputField;

    // 회원가입 결과 UI
    public Text messageUI;

    void Start()
    {
        // 파이어베이스 인증 객체를 초기화합니다.
        auth = FirebaseAuth.DefaultInstance;
        messageUI.text = "";
    }
    
    bool InputCheck()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;
        if(email.Length < 8)
        {
            messageUI.text = "이메일은 8자 이상으로 구성되어야 합니다.";
            return false;
        }
        else if(password.Length < 8)
        {
            messageUI.text = "비밀번호는 8자 이상으로 구성되어야 합니다.";
            return false;
        }
        messageUI.text = "";
        return true;
    }

    public void Check()
    {
        InputCheck();
    }

    public void Join()
    {
        if(!InputCheck())
        {
            return;
        }
        string email = emailInputField.text;
        string password = passwordInputField.text;
        // 인증 객체를 이용해 이메일과 비밀번호로 회원가입을 수행합니다.
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(
            task =>
            {
                if(!task.IsCanceled && !task.IsFaulted)
                {
                    SceneManager.LoadScene("LoginScene");
                }
                else
                {
                    messageUI.text = "이미 사용 중이거나 형식이 바르지 않습니다.";
                }
            }
        );
    }

    public void Back()
    {
        SceneManager.LoadScene("LoginScene");
    }
}
