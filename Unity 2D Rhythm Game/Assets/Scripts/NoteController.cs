using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteController : MonoBehaviour
{
    // 하나의 노트에 대한 정보를 담는 노트(Note) 클래스를 정의
    class Note
    {
        public int noteType { get; set; }
        public int order { get; set; }
        public Note(int noteType, int order)
        {
            this.noteType = noteType;
            this.order = order;
        }
    }

    public GameObject[] Notes;

    private ObjectPooler noteObjectPooler;
    private List<Note> notes = new List<Note>();
    private float x, z, startY = 8.0f;

    void MakeNote(Note note)
    {
        GameObject obj = noteObjectPooler.getObject(note.noteType);
        // 설정된 시작 라인으로 노트를 이동시킵니다.
        x = obj.transform.position.x;
        z = obj.transform.position.z;
        obj.transform.position = new Vector3(x, startY, z);
        obj.GetComponent<NoteBehavior>().Initialize();
        obj.SetActive(true);
    }

    private string musicTitle;
    private string musicArtist;
    private int bpm;
    private int divider;
    private float startingPoint;
    private float beatCount;
    private float beatInterval;

    IEnumerator AwaitMakeNote(Note note)
    {
        int noteType = note.noteType;
        int order = note.order;
        yield return new WaitForSeconds(startingPoint + order * beatInterval);
        MakeNote(note);
    }

    void Start()
    {
        noteObjectPooler = gameObject.GetComponent<ObjectPooler>();
        // 리소스에서 비트(Beat) 텍스트 파일을 불러옵니다.
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + GameManager.instance.music);
        StringReader reader = new StringReader(textAsset.text);
        // 첫 번째 줄에 적힌 곡 이름을 읽습니다.
        musicTitle = reader.ReadLine();
        // 두 번째 줄에 적힌 아티스트 이름을 읽습니다.
        musicArtist = reader.ReadLine();
        // 세 번째 줄에 적힌 비트 정보(BPM, Divider, 시작 시간)를 읽습니다.
        string beatInformation = reader.ReadLine();
        bpm = Convert.ToInt32(beatInformation.Split(' ')[0]);
        divider = Convert.ToInt32(beatInformation.Split(' ')[1]);
        startingPoint = (float) Convert.ToDouble(beatInformation.Split(' ')[2]);
        // 1초마다 떨어지는 비트 개수
        beatCount = (float)bpm / divider;
        // 비트가 떨어지는 간격 시간
        beatInterval = 1 / beatCount;
        // 각 비트들이 떨어지는 위치 및 시간 정보를 읽습니다.
        string line;
        while((line = reader.ReadLine()) != null)
        {
            Note note = new Note(
                Convert.ToInt32(line.Split(' ')[0]) + 1,
                Convert.ToInt32(line.Split(' ')[1])
            );
            notes.Add(note);
        }
        // 모든 노트를 정해진 시간에 출발하도록 설정
        for (int i = 0; i < notes.Count; i++)
        {
            StartCoroutine(AwaitMakeNote(notes[i]));
        }
        // 마지막 노트를 기준으로 게임 종료 함수를 불러옵니다.
        StartCoroutine(AwaitGameResult(notes[notes.Count - 1].order));
    }

    IEnumerator AwaitGameResult(int order)
    {
        yield return new WaitForSeconds(startingPoint + order * beatInterval + 8.0f);
        GameResult();
    }

    void GameResult()
    {
        PlayerInformation.maxCombo = GameManager.instance.maxCombo;
        PlayerInformation.score = GameManager.instance.score;
        PlayerInformation.musicTitle = musicTitle;
        PlayerInformation.musicArtist = musicArtist;
        SceneManager.LoadScene("GameResultScene");
    }
    
    void Update()
    {
        
    }
}
