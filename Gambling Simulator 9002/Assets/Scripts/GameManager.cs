using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private int money;
    public int Money { get => money; set => money = value; }

    [SerializeField] private int quota;
    public int Quota { get => quota; }

    [SerializeField] private int day;
    public int Day { get => day; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void EndDay()
    {
        money -= quota;
        day++;
        quota = money + 5000;
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
