using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{

    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] float speed;

    private float _rotation = -1;
    private Animator _animator;

    private bool _isDead;




    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDead)
        {
            Run();
        }
    }


    void Run()
    {
        float x = gameObject.transform.position.x;
        if (x < pointA.position.x || x > pointB.position.x)
        {
            _rotation *= -1;
            if (_rotation == 1)
            {
                transform.position = pointA.position;
            }
            else
            {
                transform.position = pointB.position;
            }

            transform.Rotate(Vector3.up * 180);
        }
        transform.position += new Vector3(speed * Time.deltaTime * _rotation, 0, 0);
    }


    public void Kill()
    {
        _isDead = true;
        Destroy(transform.GetChild(0).gameObject);
        Destroy(transform.GetChild(0).gameObject);
        _animator.SetBool("death", true);
        StartCoroutine(PauseAndKill(0.5f));
    }


    public IEnumerator DisableForSecond(float time)
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }


    IEnumerator PauseAndKill(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
