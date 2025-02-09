using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    [SerializeField] private int type;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKeyUp(KeyCode.E))
        {
            if (type == 0)
            {
                getInside();
            }
            else if (type == 1)
            {
                Gamble();
            }
        }
    }

    public void getInside()
    {

    }

    public void Gamble()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
