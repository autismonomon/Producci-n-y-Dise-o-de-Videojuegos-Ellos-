using UnityEngine;

public class Camara : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private float _maxX;
    [SerializeField] 
    private float _maxY;
    [SerializeField]
    private float _minX;
    [SerializeField]
    private float _minY;

    private float _plaX;
    private float _plaY;

    // Update is called once per frame
    void Update()
    {
        if (_player.transform.position.x > _minX && _player.transform.position.x < _maxX) { _plaX = _player.transform.position.x; }
        else if (_player.transform.position.x < _minX) { _plaX = _minX; }
        else { _plaX = _maxX; }

        if (_player.transform.position.y > _minY && _player.transform.position.y < _maxY) { _plaY = _player.transform.position.y; }
        else if (_player.transform.position.y < _minY) { _plaY = _minY; }
        else { _plaY = _maxY; }

        transform.position = new Vector3(_plaX, _plaY, transform.position.z);
    }
}
