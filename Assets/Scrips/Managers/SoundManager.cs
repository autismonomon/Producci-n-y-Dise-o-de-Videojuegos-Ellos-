using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("AudioSource")]
    [SerializeField]
    private AudioSource _soundEffectSource;

    [Header("AudioClips")]
    public AudioClip disperoPlayer;
    public AudioClip recarga;
    public AudioClip pocaVida;
    public AudioClip golpearEnemigo;
    public AudioClip golpearObjeto;
    public AudioClip recibirDaño;
    public AudioClip morir;
    public AudioClip disparoSinBalas;
    public AudioClip cajaExplosiva;
    public AudioClip cajaMarronRota;
    public AudioClip cajaMarronGolpe;
    public AudioClip golpearEspinasBlancas;
    public AudioClip espinasBlancasMuerto;
    public AudioClip curarse;
    public AudioClip disparoEnemigo;

    public static SoundManager instance;

    private void Awake()
    {
        instance = this; 
    }

    public void PlaySFX(AudioClip clip)
    {
        _soundEffectSource.PlayOneShot(clip);
    }

}
