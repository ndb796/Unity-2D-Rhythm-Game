using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class GameResultManager : MonoBehaviour
{
    public Text musicTitleUI;
    public Text scoreUI;
    public Text maxComboUI;
    public Image RankUI;

    public Text rank1UI;
    public Text rank2UI;
    public Text rank3UI;

    void Start()
    {
        musicTitleUI.text = PlayerInformation.musicTitle;
        scoreUI.text = "점수: " + (int) PlayerInformation.score;
        maxComboUI.text = "최대 콤보: " + PlayerInformation.maxCombo;
        // 리소스에서 비트(Beat) 텍스트 파일을 불러옵니다.
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + PlayerInformation.selectedMusic);
        StringReader reader = new StringReader(textAsset.text);
        // 첫 번째 줄과 두 번째 줄을 무시합니다.
        reader.ReadLine();
        reader.ReadLine();
        // 세 번째 줄에 적힌 비트 정보(S 랭크 점수, A 랭크 점수, B 랭크 점수)를 읽습니다.
        string beatInformation = reader.ReadLine();
        int scoreS = Convert.ToInt32(beatInformation.Split(' ')[3]);
        int scoreA = Convert.ToInt32(beatInformation.Split(' ')[4]);
        int scoreB = Convert.ToInt32(beatInformation.Split(' ')[5]);
        // 성적에 맞는 랭크 이미지를 불러옵니다.
        if (PlayerInformation.score >= scoreS)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank S");
        }
        else if(PlayerInformation.score >= scoreA)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank A");
        }
        else if (PlayerInformation.score >= scoreB)
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank B");
        }
        else
        {
            RankUI.sprite = Resources.Load<Sprite>("Sprites/Rank C");
        }
        rank1UI.text = "데이터를 불러오는 중입니다.";
        rank2UI.text = "데이터를 불러오는 중입니다.";
        rank3UI.text = "데이터를 불러오는 중입니다.";
        DatabaseReference reference;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-rhythm-game-tutori-b254d.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.GetReference("ranks")
            .Child(PlayerInformation.selectedMusic);
        // 데이터 셋의 모든 데이터를 JSON 형태로 가져옵니다.
        reference.OrderByChild("score").GetValueAsync().ContinueWith(task =>
        {
            // 성공적으로 데이터를 가져온 경우
            if (task.IsCompleted)
            {
                List<string> rankList = new List<string>();
                List<string> emailList = new List<string>();
                DataSnapshot snapshot = task.Result;
                // JSON 데이터의 각 원소에 접근합니다.
                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary rank = (IDictionary)data.Value;
                    emailList.Add(rank["email"].ToString());
                    rankList.Add(rank["score"].ToString());
                }
                // 정렬 이후 순서를 뒤집어 내림차순 정렬합니다.
                emailList.Reverse();
                rankList.Reverse();
                // 최대 상위 3명의 순위를 차례대로 화면에 출력합니다.
                rank1UI.text = "플레이 한 사용자가 없습니다.";
                rank2UI.text = "플레이 한 사용자가 없습니다.";
                rank3UI.text = "플레이 한 사용자가 없습니다.";
                List<Text> textList = new List<Text>();
                textList.Add(rank1UI);
                textList.Add(rank2UI);
                textList.Add(rank3UI);
                int count = 1;
                for(int i = 0; i < rankList.Count && i < 3; i++)
                {
                    textList[i].text = count + "위: " + emailList[i] + " (" + rankList[i] + " 점)";
                    count = count + 1;
                }
            }
        });
    }
    
    public void Replay()
    {
        SceneManager.LoadScene("SongSelectScene");
    }
}
