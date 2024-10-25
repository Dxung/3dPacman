using UnityEngine;
using System.Collections;

public class FloatingPellets : MonoBehaviour
{

    private Vector3 _startPoint;
    private float _value;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _amplitude = 0.5f;


    void Start()
    {
        /*--- Get starting pos and get a random value => Each pellets will move different from each other*/
        _startPoint = transform.position;
        _value = Random.Range(0, 10.5f);
    }

    // Update is called once per frame
    void Update()
    {
        _value += Time.deltaTime;
        transform.position = _startPoint + Vector3.up * Mathf.Sin(_value * _maxSpeed) * _amplitude;
    }
}
