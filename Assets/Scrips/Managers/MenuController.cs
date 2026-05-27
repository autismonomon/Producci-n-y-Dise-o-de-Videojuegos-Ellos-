using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public CanvasGroup menuSelector;
    private bool cambio = false;


    public void GoToScene(string name)
    {
        SceneManager.LoadScene(name);  
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MostrarSelector()
    {
        menuSelector.gameObject.SetActive(!cambio);
    }
}
