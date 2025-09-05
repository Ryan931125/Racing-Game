using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Collider startLineCollider;
    [SerializeField]
    private GameObject[] checkPointList;
    private int _gameState = -1;
    private int _checkPointNum;

    private float _startTime;
    private bool _isPlaying;
    [SerializeField]
    private TMP_Text timerText;

    // Coins
    public GameObject coin;
    private int coinNum;
    [SerializeField]
    private List<GameObject> coinList = new List<GameObject>();
    [SerializeField]
    private List<Vector3> coinPosList = new List<Vector3>();

    void Awake()
    {   
        // Group 1
        for(int i = 0; i < 3; i++)
            coinPosList.Add(new Vector3(39f, 0.5f, 6f - i * 6f));
        // Group 2
        for(int i = 1; i <= 6; i++)
            coinPosList.Add(new Vector3(14f - 3f * i, 0.5f, -53f));
        // Group 3
        for(int i = 0; i < 5; i++)
            coinPosList.Add(new Vector3(-35f + i * 5f, 4.5f, -2f));

        coinNum = coinPosList.Count;
        foreach(Vector3 pos in coinPosList){
            coinList.Add(Instantiate(coin, pos, Quaternion.identity));
        }

        _checkPointNum = checkPointList.Length;
        foreach (GameObject checkPoint in checkPointList)
            checkPoint.SetActive(false);
    }

    private void Update(){
        if(_isPlaying){
            UpdateTimer();
        }
    }

    public void LoadCheckPoint(){
        if(_gameState == _checkPointNum){
            Debug.Log("Finish!");
            startLineCollider.enabled = false;
            _isPlaying = false;
            return;
        }

        Debug.Log($"Game State: {_gameState}");
        if(_gameState == -1){
            startLineCollider.enabled = false;
            _startTime = Time.time;
            _isPlaying = true;    
        }
        else
            checkPointList[_gameState].SetActive(false);
        
        _gameState++;

        if(_gameState == _checkPointNum)
            startLineCollider.enabled = true;
        else
            checkPointList[_gameState].SetActive(true);

    }

    void UpdateTimer(){
        float currentTime = Time.time - _startTime;
        currentTime = Mathf.Max(0, currentTime);
        int minute = (int)(currentTime / 60);
        float second = currentTime % 60;
        timerText.text = $"{minute}:{second:00.00}";
    }

    public void reduceTime(float time){
        _startTime += time;
        if(_startTime < 0)
            _startTime = 0;
    }
}
