using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    [Header("Variables Player")]
    public bool mori = false;

    [SerializeField]
    private float _radio;
    [SerializeField]
    private float _angulo;
    [SerializeField]
    private float _rango;
    [SerializeField]
    private float _cantBalas;
    [SerializeField]
    private float _tempRecarga;
    [SerializeField]
    private float _maxAmmo;
    [SerializeField]
    private float _fireRate;

    private float _cdDisparo = 5;

    [Header("Codigos Personajes")]
    private PlaMovimiento _plamoviento;
    private PleControls _pleControls;
    private Player _player;
    [SerializeField]
    private Animator _hijoAnimator;

    private bool _recargando = false;

    [Header("Game Objects")]
    [SerializeField]
    private GameObject _bala;
    [SerializeField]
    private Transform _rotSpownPoint;
    [SerializeField]
    private Transform _spownPoint;
    [SerializeField]
    private Transform _mouse;




    private void Start()
    {
        _player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody2D>();
        _meTrans = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _plamoviento = new PlaMovimiento(_rb, _velocidad, _rotSpownPoint, transform, _spownPoint, _angulo, _animator);
        _pleControls = new PleControls(_plamoviento, _player, _dashForce, _mouse);
        _vidaActual = _vidaMax;
    }

    private void Update()
    {
        if (GameManager.instance.pausado)
        {
            return;
        }

        if (!_animator.GetBool("Muerto"))
        {

            _pleControls.Disparo();
            _pleControls.PosPlayer(transform.position);
        }

        if (_cdDisparo >= _fireRate)
        {
            _cdDisparo = _fireRate;
        }
        _cdDisparo += 1 * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (!_animator.GetBool("Muerto"))
        {
            _pleControls.ArtificialUpdate();

        }
        else { _rb.linearVelocity = Vector2.zero; }
    }

    public override void DañoRecivido(int dañoRes)
    {
        _vidaActual -= dañoRes;
        EfectoRecibirDaño();
        UIManager.ActualizarVida(_vidaActual, _vidaMax);
        UIManager.Instance.efectoDePantalla.SetTrigger("Dañado");
        SoundManager.instance.PlaySFX(SoundManager.instance.recibirDaño);
        //Object.FindAnyObjectByType<UIManager>().salud.SetFloat("vidaActual", _vidaActual);
        if (_vidaActual <= 0) { Muerto(); }
    }

    public override void Muerto()
    {
        _rotSpownPoint.gameObject.SetActive(false);
        StartCoroutine(PersonajeMuerto());

    } 

    IEnumerator PersonajeMuerto()
    {
        _animator.SetBool("Muerto", true);
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.PlaySFX(SoundManager.instance.morir);

    }


    public void Disparo()
    {
        if (_cantBalas > 0 && _cdDisparo >= _fireRate && _recargando == false)
        {
            _cantBalas--;
            _cdDisparo = 0;
            //Object.FindAnyObjectByType<UIManager>().cargador.SetTrigger("Disparar");
            SoundManager.instance.PlaySFX(SoundManager.instance.disperoPlayer);
            StartCoroutine(AniArma());
            /*if (_cdDisparo >= _fireRate)
            {
                
            }
            else { _cdDisparo++; }*/
        }
        else if (_cantBalas == 0 && _cdDisparo >= _fireRate && _recargando == false)
        {
            _cdDisparo = 0;
            UIManager.Instance.cargador.SetTrigger("Disparar");
            SoundManager.instance.PlaySFX(SoundManager.instance.disparoSinBalas);
        }
    }

    IEnumerator AniArma()
    {
        _hijoAnimator.SetBool("Disparo", true);
        Instantiate(_bala, _spownPoint.position, _rotSpownPoint.rotation);
        BalaPlayer.instance.Daño(_daño);
        UIManager.Instance.cargador.SetTrigger("Disparar");
        yield return new WaitForSeconds(0.1f);
        _hijoAnimator.SetBool("Disparo", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(transform.position.x + _radio, transform.position.y + _radio, transform.position.z), new Vector3(transform.position.x + _radio, transform.position.y - _radio, transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x + _radio, transform.position.y - _radio, transform.position.z), new Vector3(transform.position.x - _radio, transform.position.y - _radio, transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x - _radio, transform.position.y - _radio, transform.position.z), new Vector3(transform.position.x - _radio, transform.position.y + _radio, transform.position.z));
        Gizmos.DrawLine(new Vector3(transform.position.x - _radio, transform.position.y + _radio, transform.position.z), new Vector3(transform.position.x + _radio, transform.position.y + _radio, transform.position.z));
        //if (_angulo <= 0) { return; }
        //float medioAngulo = _angulo * 0.5f;
        //Vector2 p1 = PointForAngle(medioAngulo, _rango);
        //Vector2 p2 = new Vector2(p1.x, -p1.y);
        //Vector2 p3 = new Vector2(-p1.x, p1.y);
        //Vector2 p4 = new Vector2(-p1.x, -p1.y);
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, (Vector2)transform.position + p1);
        //Gizmos.color = Color.gray;
        //Gizmos.DrawLine(transform.position, (Vector2)transform.position + p2);
        //Gizmos.DrawLine(transform.position, (Vector2)transform.position + p3);
        //Gizmos.DrawLine(transform.position, (Vector2)transform.position + p4);
        var _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Gizmos.DrawWireSphere(_mousePosition, _rango);
    }

    public void Recarga()
    {
        if(_cantBalas >= _maxAmmo)
        {
            _cantBalas = _maxAmmo;
            return;
        }
        if(!_recargando) { StartCoroutine(Recargando()); }
    }

    private IEnumerator Recargando()
    {
        _recargando = true;
        SoundManager.instance.PlaySFX(SoundManager.instance.recarga);
        UIManager.Instance.cargador.SetTrigger("Recargar");
        yield return new WaitForSeconds(_tempRecarga);
        _cantBalas = _maxAmmo;
        _recargando = false;
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if(collision.gameObject.layer == 13) 
        {
            SoundManager.instance.PlaySFX(SoundManager.instance.curarse);
            _vidaActual += 10;
            if(_vidaActual > _vidaMax) { _vidaActual = _vidaMax;}
            Destroy(collision.gameObject);
            UIManager.ActualizarVida(_vidaActual, _vidaMax);
        }

        if(collision.gameObject.layer == 12) { GameManager.instance.Victory(); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14) 
        {
            GameManager.instance.IniciarAnimacion();
            EventosManager.instance.Eventos();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == 15) 
        { 
            EventosManager.instance.Eventos();
            Destroy(collision.gameObject);
        }
    }

    Vector3 PointForAngle(float angulo, float distancia)
    {
        return new Vector2 (Mathf.Cos(_angulo * Mathf.Deg2Rad), Mathf.Sin(_angulo * Mathf.Deg2Rad)) * distancia;
    }


    public void EfectoRecibirDaño()
    {
        _animator.GetComponent<SpriteRenderer>().DOColor(Color.red, 0.2f).OnComplete(() => _animator.GetComponent<SpriteRenderer>().DOColor(Color.white, 0.2f));
    }

}
