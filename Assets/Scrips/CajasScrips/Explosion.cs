using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private int _danio;
    [SerializeField]
    private float _radio;
    private Collider2D[] _hit;

    public static Explosion instance;

    private void Awake()
    {
        instance = this;
    }

    public void Explo()
    {
        _hit = Physics2D.OverlapCircleAll(transform.position, _radio);

        foreach (Collider2D col in _hit) 
        { 
           if(col.gameObject.layer == 11) { Destroy(col.gameObject);}

            if (col.gameObject.layer == 7) { col.gameObject.GetComponent<Player>().DañoRecivido(_danio); }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _radio);
    }
}
