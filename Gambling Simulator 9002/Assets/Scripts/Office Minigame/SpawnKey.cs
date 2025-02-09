using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SpawnKey : MonoBehaviour
{
    private float timeRemaining = 0f;
    private float spawnTime;
    [SerializeField] private GameObject keyPrefab;
    private List<Key> keys = new List<Key>();
    [SerializeField] GameObject canvas;

    private float sleepiness = 0;

    private void Start()
    {
        spawnTime = GameManager.instance.Day * 1.25f;
        timeRemaining = spawnTime;
    }

    private void Update()
    {
        if (timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else 
        {
            timeRemaining = Random.Range(spawnTime / 2, spawnTime);
            Vector2 pos = new Vector2(Random.Range(-Screen.width / 4, Screen.width / 4), Random.Range(-Screen.height / 4, Screen.height / 4));
            Key key = Instantiate(keyPrefab, canvas.transform).GetComponent<Key>();
            key.transform.localPosition = pos;
            keys.Add(key);
        }

        if (keys.Count > 0 && Input.inputString != "" && Input.anyKeyDown)
        {
            Key keyPressed = keys.Find(k => k.KeyToPress == (KeyCode)System.Enum.Parse(typeof(KeyCode), Input.inputString[0].ToString().ToUpper()));
            if (keyPressed != null)
            {
                keys.Remove(keyPressed);
                Destroy(keyPressed.gameObject);
                sleepiness += 0.1f;
            }
        }

        if (sleepiness >= 1)
            GameManager.instance.LoadScene(2);
    }
}
