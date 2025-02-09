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
    private Key key;
    [SerializeField] private float vignetteIntensity = 0f;
    private Vignette vignette;


    private void Start()
    {
        Volume volume = FindObjectOfType<Volume>();
            
        if (volume != null && volume.profile.TryGet(out Vignette vignette))
        {
            this.vignette = vignette;
        }
    }

    private void Update()
    {
        if (timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = spawnTime;
            Vector2 vector = new Vector2(UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(-2, 2));
            key = Instantiate(keyPrefab, vector, Quaternion.identity).GetComponent<Key>();
            
        }

        if (Input.GetKeyDown(key.KeyToPress) && key.gameObject != null)
        {
            Destroy(key.gameObject);
            vignetteIntensity += 0.1f;
            this.vignette.intensity.value += vignetteIntensity;
        }
    }
}
