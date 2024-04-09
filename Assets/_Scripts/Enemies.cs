using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{

    [SerializeField] List<Transform> waypoints;
    [SerializeField] float speed;

    private int _currentWaypoint;
    private int _nextWaypoint;
    //private List<Transform> _waypoints= new List<Transform>(waypointsInput);
    private bool _isPausing;
    private Animator _animator;

    private bool _isDead;
    private AudioController audioController;



    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        audioController = GameManager.Instance.GetComponent<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDead)
        {
            Run();
        }
    }


    private void Run()
    {
        if (_isPausing) { return; }

        if(Vector3.Distance(transform.position, waypoints[_currentWaypoint + 1].position) < 0.1f)
        {
            UpdateWaypoint();
        }

        transform.Translate((waypoints[_currentWaypoint + 1].position - transform.position).normalized * Time.deltaTime * speed);
    }

    private void UpdateWaypoint()
    {
        if(_currentWaypoint + 1 == waypoints.Count - 1)
        {
            waypoints.Reverse();
            _currentWaypoint = 0;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            _currentWaypoint += 1;
        }
    }


    public void Kill()
    {
        audioController.PlayExplosion();
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
