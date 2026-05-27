using System.Collections;
using UnityEngine;

public class Cajas : MonoBehaviour
{
    [SerializeField]
    private float _vida;
    [SerializeField]
    private bool _explosiva = false;
    SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Sprite _sprite;

    private bool _vivo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _vivo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_vida <= 0 && !_explosiva) { Destroy(gameObject); }
        else if (_vida <= 0 && _explosiva && _vivo) 
        {
            _vivo = false;
            _spriteRenderer.sprite = null;
            transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(Desaparecer());

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entro");
        if (collision.gameObject.layer == 10) { _vida--; }
        if (collision.gameObject.tag == "Explocion") { _vida = 0; }
    }

    private IEnumerator Desaparecer()
    {
        yield return new WaitForSeconds(0.5f);
        _spriteRenderer.sprite = _sprite;
        transform.GetChild (0).gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
