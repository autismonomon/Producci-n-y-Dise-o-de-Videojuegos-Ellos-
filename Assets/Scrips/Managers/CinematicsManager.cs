using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CinematicsManager : MonoBehaviour
{
    public Animator reproduciendo;
    public int numero = 0;

    public CanvasGroup panelDeCinematicas;
    //public List<Animator> cinematicas;
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
            reproduciendo.SetTrigger("next");        
        }
        
    }

    public void CambioDeCinematica()
    {
        numero++;
        reproduciendo.runtimeAnimatorController = cinematicass[numero];
        GameManager.instance.Reanudar();
        panelDeCinematicas.gameObject.SetActive(false);
    }
    //public void InicioDeCinematica()
    //{
    //    GameManager.instance.Pausar();
    //    panelDeCinematicas.gameObject.SetActive(true);
    //}




}
