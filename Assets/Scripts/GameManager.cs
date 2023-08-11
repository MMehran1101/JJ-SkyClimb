using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _score = 0;
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.SetTextScore(0);
    }


    public void UpdateScore(int score)
    {
        if (score > _score)
        {
            _score = score;
            UIManager.Instance.SetTextScore(_score);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        Debug.Log("Game overrrr");
    }
}