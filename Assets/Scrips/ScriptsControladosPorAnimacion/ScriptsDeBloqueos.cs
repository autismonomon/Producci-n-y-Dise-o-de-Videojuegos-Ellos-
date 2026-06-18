using System;
using UnityEngine;

public class ScriptsDeBloqueos : MonoBehaviour
{
    public BoxCollider2D padre;

    public void Destruirme()
    {
        //padre = GetComponentInParent<GameObject>();
        padre = GetComponentInParent<BoxCollider2D>();
        padre.gameObject.SetActive(false);
    }
}

   
