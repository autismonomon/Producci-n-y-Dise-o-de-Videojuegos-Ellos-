using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
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
    public static EventosManager instance;

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
        if (_primerosEnemigos == null)
        {
            _primeros = false;
            for (int i = 0; i < _barreras1.Count; i++)
            {
                _barreras1[i].SetActive(false);
            }
        }

        if (_segundosEnemigos == null) 
        {
            for (int i = 0; i < _barreras1.Count; i++)
            {
                _barreras2[i].SetActive(false);
            }

        }
    }

    public void Eventos()
    {
        if (_primeros)
        {
            for (int i = 0; i < _primerosEnemigos.Count; i++)
            {
                _primerosEnemigos[i].SetActive(true);
                _barreras1[i].SetActive(true);
                cantEnemigos++;
            }
            StartCoroutine(CDParedes());

        }
        else
        {
            for (int i = 0; i < _segundosEnemigos.Count; i++)
            {
                _primerosEnemigos[i].SetActive(true);
                
            }

            for (int i = 0; i < _barreras2.Count; i++)
            {
                _barreras1[i].SetActive(true);
            }
            StartCoroutine(CDParedes());
        }
    }

    public void CantidadEnemigos()
    {
        Debug.Log("Entre Aca");
        cantEnemigos -= 1;
    }

    private IEnumerator CDParedes()
    {
        yield return new WaitForSeconds(3);
        if(_primerosEnemigos != null) { _primerosEnemigos = null; }
        else { _segundosEnemigos = null; }
    }
}
