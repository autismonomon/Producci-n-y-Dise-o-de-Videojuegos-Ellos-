using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CinematicsManager : MonoBehaviour
{
    public Animator cinematica;
    private int numero = 0;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        { 
            cinematica.SetTrigger("next");
            numero++;  
        }
        if (numero == 6)
        {
            SceneManager.LoadScene(2);
        }
    }
}
