using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GestorDeAudios;

public class CinematicsManager : MonoBehaviour
{
    public Animator reproduciendo;
    public int numero = 0;

    public List<ListaDeAudios> AudioClipsDeCadaEscena;
    public AudioSource parlante;
    public int lista = 0;
    public int clip = 0;
    public AudioClip silencio;


    public CanvasGroup panelDeCinematicas;
    public List<RuntimeAnimatorController> cinematicass;

    private void Start()
    {
        
        reproduciendo.runtimeAnimatorController = cinematicass[numero];
        GameManager.instance.Pausar();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        { 
            parlante.Stop();
            reproduciendo.SetTrigger("next");        
        }
        
    }
    public void ReproducirSonido()
    {
        
        parlante.PlayOneShot(AudioClipsDeCadaEscena[lista].clips[clip]);
        clip++;
    }

    public void CambioDeCinematica()
    {
        lista++;
        clip = 0;
        numero++;
        reproduciendo.runtimeAnimatorController = cinematicass[numero];
        GameManager.instance.Reanudar();
        panelDeCinematicas.gameObject.SetActive(false);
    }
}
public class GestorDeAudios : MonoBehaviour
{
    [System.Serializable]
    public class ListaDeAudios
    {
        public List<AudioClip> clips;
    }    
}
