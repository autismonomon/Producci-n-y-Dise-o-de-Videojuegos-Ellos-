using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventosManager : MonoBehaviour
{
    public int cantEnemigos;
    private bool _primeros;
    [SerializeField]
    private List<GameObject> _primerosEnemigos;
    [SerializeField]
    private List<GameObject> _segundosEnemigos;
    [SerializeField]
    private List<GameObject> _barreras1;
    [SerializeField]
    private List<GameObject> _barreras2;

    public List<GameObject> enemigosVivos;
    public static EventosManager instance;

    public CinematicsManager ControladorDeCinematicas;

    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _primeros = true;
    }

    // Update is called once per frame
    void Update()
    {

       
    }

    public void Eventos()
    {
        if (_primeros)
        {
            for (int i = 0; i < _primerosEnemigos.Count; i++)
            {
                enemigosVivos.Add(_primerosEnemigos[i]);
                _primerosEnemigos[i].SetActive(true);
                _barreras1[i].SetActive(true);               
                cantEnemigos++;
            }
        }
        else
        {
            for (int i = 0; i < _segundosEnemigos.Count; i++)
            {
                enemigosVivos.Add(_segundosEnemigos[i]);
                _segundosEnemigos[i].SetActive(true);
                
            }

            for (int i = 0; i < _barreras2.Count; i++)
            {
                _barreras2[i].SetActive(true);
            }
        }
    }

    public void DesaparecenParedes()
    {
        if (!enemigosVivos.Any())
        {
            if (_primeros)
            {
                for (int i = 0; i < _barreras1.Count; i++)
                {
                    _barreras1[i].SetActive(false);
                }
                _primeros = false;
            }
            else
            {
                for (int i = 0; i < _barreras1.Count; i++)
                {
                    _barreras2[i].SetActive(false);
                }
            }
        }
    }

    public void CantidadEnemigos()
    {
        Debug.Log("Entre Aca");
        cantEnemigos -= 1;
    }
}
