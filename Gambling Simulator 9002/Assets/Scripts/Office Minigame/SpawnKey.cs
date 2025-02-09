using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SpawnKey : MonoBehaviour
{
    [SerializeField] private float timeRemaining = 0f;
    [SerializeField] private float spawnTime = 5f;
    [SerializeField] private GameObject keyPrefab;
    private List<Key> keys = new List<Key>();

    private float sleepiness = 0;

    private void Update()
    {
        if (timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else 
        {
            timeRemaining = spawnTime;
            Vector2 vector = new Vector2(Random.Range(-2, 2), UnityEngine.Random.Range(-2, 2));
            Key key = Instantiate(keyPrefab, vector, Quaternion.identity).GetComponent<Key>();
            keys.Add(key);
            print(keys.Count);
        }

        if (keys.Count > 0 && Input.inputString != "" && Input.anyKeyDown)
        {
            Key keyPressed = keys.Find(k => k.KeyToPress == (KeyCode)System.Enum.Parse(typeof(KeyCode), Input.inputString[0].ToString().ToUpper()));
            if (keyPressed != null)
            {
                Destroy(keyPressed.gameObject);
                keys.RemoveAll(k => k.gameObject == null);
                sleepiness += 0.1f;
            }
        }

        if (sleepiness >= 1)
            GameManager.instance.LoadScene(2);
    }
}
