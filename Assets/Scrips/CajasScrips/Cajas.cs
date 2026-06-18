using System.Collections;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class Cajas : MonoBehaviour
{
    [SerializeField]
    private float _vida;
    [SerializeField]
    private float _vidaMaxima;
    [SerializeField] 
    private float _rango;
    [SerializeField]
    private bool _explosiva = false;
    [SerializeField]
    private bool _marron = false;


    SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Sprite _sprite;

   

    private BoxCollider2D _boxCollider;

    private bool _vivo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!_marron && !_explosiva) 
        {
            Destroy(transform.GetChild(0).gameObject);
            _vidaMaxima = 9999; 
        }
        else if (!_marron && _explosiva) { _vidaMaxima = 2; }
        else 
        {
            Destroy(transform.GetChild(0).gameObject);
            _vidaMaxima = 3;
        }


        _vida = _vidaMaxima;
        DOTween.Init();
        _boxCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _sprite;
        _vivo = true;


    }

    // Update is called once per frame
    void Update()
    {
        if (_vida <= 0 && !_explosiva && _boxCollider != null) 
        {
            SoundManager.instance.PlaySFX(SoundManager.instance.cajaMarronRota);
            
            Destroy(_boxCollider); 
        }
        else if (_vida <= 0 && _explosiva && _vivo) 
        {
            SoundManager.instance.PlaySFX(SoundManager.instance.cajaExplosiva);
            _vivo = false;
            //_spriteRenderer.sprite = null;
            transform.GetChild(0).gameObject.SetActive(true);
            //Explosion.instance.Explo();
            transform.GetChild(0).GetComponent<Explosion>().Explo();
            StartCoroutine(Desaparecer());

        }

        


        if (_vida == _vidaMaxima)
        {
            if (!_marron && !_explosiva) { _sprite = Resources.Load<Sprite>("Imagenes/Assets Remakes/Caja-Reforzada"); }
            else if (!_marron && _explosiva) { _sprite = Resources.Load<Sprite>("Imagenes/Assets Remakes/Caja-Roja"); }
            else { _sprite = Resources.Load<Sprite>("Imagenes/Assets Remakes/Caja-Marron"); }

            _spriteRenderer.sprite = _sprite;
        }
        else if (_vida < _vidaMaxima && _vida > 1)
        {
            if (!_marron && !_explosiva) { _sprite = Resources.Load<Sprite>("Imagenes/Assets Remakes/Caja-Reforzada"); }
            else if (!_marron && _explosiva) { _sprite = Resources.Load<Sprite>("Imagenes/Assets Remakes/Caja-Roja BR-Stage 3"); }
            else { _sprite = Resources.Load<Sprite>("Imagenes/Assets Remakes/Caja-Marron BR-Stage 2"); }

            _spriteRenderer.sprite = _sprite;
        }
        else if (_vida == 1)
        {
            if (!_marron && !_explosiva) { _sprite = Resources.Load<Sprite>("Imagenes/Assets Remakes/Caja-Reforzada"); }
            else if (!_marron && _explosiva) { _sprite = Resources.Load<Sprite>("Imagenes/Assets Remakes/Caja-Roja BR-Stage 3"); }
            else { _sprite = Resources.Load<Sprite>("Imagenes/Assets Remakes/Caja-Marron BR-Stage 3"); }

            _spriteRenderer.sprite = _sprite;
        }
        else if (_boxCollider != null && _vida <= 0)
        {
            if (!_marron && !_explosiva) { _sprite = Resources.Load<Sprite>("Imagenes/Assets Remakes/Caja-Reforzada"); }
            else if (!_marron && _explosiva) { _sprite = Resources.Load<Sprite>("Imagenes/Assets Remakes/Caja-Roja_BR-Stage_4"); }
            else { _sprite = Resources.Load<Sprite>("Imagenes/Assets Remakes/Caja-Marron BR-Stage 4"); }

            _spriteRenderer.sprite = _sprite;
        }

    }

    public void RecibirDaño(float daño)
    {
        _vida -= daño;
    }
    private void Temblar()
    {
        transform.DOShakePosition(0.2f, 0.05f, 20, 90, false, true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entro");
        if (collision.gameObject.layer == 10) 
        {
            SoundManager.instance.PlaySFX(SoundManager.instance.cajaMarronGolpe);
            _vida--; 
            Temblar();
        }
        if (collision.gameObject.CompareTag("BalaEspinas")) { RecibirDaño(1f); }

    }

    private IEnumerator Desaparecer()
    {
        yield return new WaitForSeconds(0.5f);
        //_spriteRenderer.sprite = _sprite;
        transform.GetChild (0).gameObject.SetActive(false);
        Destroy(_boxCollider);
    }

   

}
