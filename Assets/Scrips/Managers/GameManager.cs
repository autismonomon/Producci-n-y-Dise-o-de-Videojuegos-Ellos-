using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public Player player;
    [SerializeField]
    private GameObject _derrota;
    [SerializeField] 
    private GameObject _ganaste;
    [SerializeField]
    private List<GameObject> _cajas;
    [SerializeField]
    private GameObject _oscuridad;

    bool _gane;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DOTween.Init();

        if(_cajas == null) { _oscuridad.SetActive(false); }
        _gane = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.mori)
        { 
            Time.timeScale = 0;
            _derrota.SetActive(true);
        }
        else if (!_gane) {Time.timeScale = 1; }

        _cajas.RemoveAll(item => item == null);

        if (_cajas.Count == 0) { _oscuridad.SetActive(false); }
    }

    public void Victory()
    {
        _gane = true;
        Time.timeScale = 0;
        _ganaste.SetActive(true);
    }    

    public void Reiniciar(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
