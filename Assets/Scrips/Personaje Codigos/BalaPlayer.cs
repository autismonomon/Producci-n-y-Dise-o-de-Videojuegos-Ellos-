using UnityEngine;

public class BalaPlayer : Balas
{

    [SerializeField]
    private ParticleSystem chispas;

    private void Awake()
    {
        instance = this;
    }
    public override void Desaparece()
    {
        if(_vida <= 0) { Destroy(gameObject); }
        else { _vida -= Time.deltaTime; }
    }
    public override void Daño(int dmg)
    {
        _daño = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Instantiate(chispas, transform.position, transform.rotation);
            SoundManager.instance.PlaySFX(SoundManager.instance.golpearEnemigo);
            collision.GetComponent<Entity>().DañoRecivido(_daño);
            Destroy(gameObject);
        }
        if(collision.gameObject.layer == 11) 
        {
            //Instantiate(chispas, transform.position, transform.rotation);
            SoundManager.instance.PlaySFX(SoundManager.instance.golpearObjeto);
            Destroy(gameObject); 
        }
    }

    protected override void OnBecameVisible()
    {
         
    }

}
