using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemDispara : Enemigos
{
    [Header("Variables")]
    [SerializeField]
    private float _velDisparo;
    [SerializeField]
    private float _rangoFrenado;
    [SerializeField]
    private float _contador;
    private bool _muerto;
    private bool _buscar;

    [Header("GameObject de disparo")]
    [SerializeField]
    private GameObject _bala;
    [SerializeField]
    private GameObject _spownBala;
    [SerializeField]
    private LayerMask _personajeLayer;
    private Vector2 _dirBala;
    private Vector2 _posPlayer;
    private RaycastHit2D _hit;

    public static EnemDispara instance;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _meTrans = GetComponent<Transform>();
        _animator = GetComponentInChildren<Animator>();
        _vidaActual = _vidaMax;
        _muerto = false;
        _buscar = false;
    }

    private void Update()
    {
        if (GameManager.instance.pausado)
        {
            return;
        }


        if (_player.transform.position.x < transform.position.x) { transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); }
        else { transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); }
        _dirBala = _player.transform.position - _spownBala.transform.position;
        _spownBala.transform.right = _dirBala;
        _hit = Physics2D.Raycast(_spownBala.transform.position, _dirBala, _rangoVision, _personajeLayer);
        if (_hit.collider != null && _hit.transform.gameObject.layer == 7)
        {
            Comportamiento();
        }

        if (_buscar) { Buscar(_posPlayer); }


        if (_contador >= _velDisparo)
        {
            _contador = _velDisparo;
        }
        _contador += 1 * Time.deltaTime;


    }

    private void Comportamiento() //Pasar a una FMS para mejorar el comportamiento
    {
        Debug.Log(_hit.transform.name);
        if (!_player.GetComponent<Animator>().GetBool("Muerto"))
        {
            _animator.SetBool("Atacar", false);
            _animator.SetBool("Correr", false);
            if (_dirBala.magnitude <= _rangoAccion && _muerto == false) { Correr(); }
            else if (_dirBala.magnitude <= _rangoVision) { Disparo(); }
        }
        else
        {
            _animator.SetBool("Atacar", false);
            _animator.SetBool("Correr", false);
        }
    }

    private void Correr()
    {
        _animator.SetBool("Atacar", false);
        _animator.SetBool("Correr", true);
        var dri = _player.transform.position - transform.position;
        dri = dri.normalized;
        _rb.linearVelocity= -dri * _velocidad;
    }

    private void Buscar(Vector2 posPla)
    {
        if (_hit.collider != null && _hit.transform.gameObject.layer == 7)
        {
            _rb.linearVelocity = Vector2.zero;
            _buscar = false;
            Comportamiento();
        }
        _animator.SetBool("Atacar", false);
        _animator.SetBool("Correr", true);
        var dri = posPla - (Vector2)transform.position;
        dri = dri.normalized;
        Debug.Log(posPla);
        _rb.linearVelocity = dri * _velocidad;
        if (Vector2.Distance((Vector2)transform.position, posPla) < _rangoFrenado)
        {
            _animator.SetBool("Atacar", false);
            _animator.SetBool("Correr", false);
            _buscar = false;
            _rb.linearVelocity = Vector2.zero;
        }
        
    }

    private void Disparo()
    {
        _rb.linearVelocity = Vector2.zero;
        if (_contador >= _velDisparo && _muerto == false)
        {
            _animator.SetBool("Atacar", true);
            _animator.SetBool("Correr", false);
            Instantiate(_bala, _spownBala.transform.position, _spownBala.transform.rotation);
            BalaEnem.instance.Daño(_daño);
            _contador = 0;
        }
        //else { _contador++; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _rangoVision);
        Gizmos.DrawWireSphere(transform.position, _rangoAccion);
        if (_dirBala.magnitude <= _rangoVision) { Gizmos.color = Color.yellow; }
        else {  Gizmos.color = Color.green; }
        Gizmos.DrawLine(transform.position, _player.transform.position);
    }
    public override void DañoRecivido(int dañoRes)
    {
        _vidaActual -= dañoRes;
        
        if (_vidaActual <= 0)
        {
            _muerto = true;
            Muerto();
        }
        _buscar = true;
    }

    public void PosPlayer(Vector2 posPlayer) { _posPlayer = posPlayer; }

    public override void Muerto()
    {
        _animator.SetBool("Muerto", true);
        StartCoroutine(Destruir());
    }

    IEnumerator Destruir()
    {
        if (EventosManager.instance.enemigosVivos.Contains(gameObject)) 
        { 
            EventosManager.instance.enemigosVivos.Remove(gameObject);
            EventosManager.instance.DesaparecenParedes();
        }
        yield return new WaitForSeconds(1);
        _animator.GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() => Destroy(gameObject));

        DOTween.Kill(gameObject);
        //Destroy(gameObject);
        //gameObject.SetActive(false);
    }
}
