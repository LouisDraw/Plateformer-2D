using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpHeight;
    [SerializeField] float movementSpeed;

    [SerializeField] private float raycastOffset;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] Transform raycastOrigin1;
    [SerializeField] Transform raycastOrigin2;

    [SerializeField] Animator animator;
    [SerializeField] Transform characterSprite;

    private Score _score;

    private Rigidbody2D _rb;
    private bool _canJump;
    private bool _isGrounded;
    private bool _isJumping;
    private bool _resetForces;


    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _score = GameObject.FindWithTag("Score").GetComponent<Score>();
        GameManager.Instance.LifePoints = GameManager.Instance.MaxLifePoints;
    }


    void Update()
    {
        //deplacement horizontaux
        Movement(Input.GetAxisRaw("Horizontal"));
        
        _isGrounded = Physics2D.Raycast(raycastOrigin1.position, Vector3.down, raycastOffset, groundLayerMask) || Physics2D.Raycast(raycastOrigin2.position, Vector3.down, raycastOffset, groundLayerMask);
        Debug.DrawLine(raycastOrigin1.position, new Vector3(raycastOrigin1.position.x, raycastOrigin1.position.y - raycastOffset, raycastOrigin1.position.z), UnityEngine.Color.green);
        Debug.DrawLine(raycastOrigin2.position, new Vector3(raycastOrigin2.position.x, raycastOrigin2.position.y - raycastOffset, raycastOrigin2.position.z), UnityEngine.Color.green);
        //saut

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) && _isGrounded && !_isJumping)
        {
            _canJump = true;
        }
        if (_isGrounded)
        {
            //Physics.gravity = Vector3.zero;
        }
        else
        {
            Physics.gravity = new Vector3(0, -9.81f, 0);
        }

        if (GameManager.Instance.LifePoints <= 0) {GameOver(); }
    }

    private void FixedUpdate()
    {

        if (_resetForces == true)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = 0f;
            _resetForces = false;
        }

        if (_canJump)
        {
            _rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            _canJump = false;
            _isJumping = true;
        }
        if (_isGrounded && _isJumping)
        {
            _isJumping = false;
        }
    }

    private void Movement(float movement)
    {
        //animation
        animator.SetFloat("horizontalInput", movement);
        animator.SetFloat("yVelocity", _rb.velocity.y);

        //d�placement
        transform.position += new Vector3(movement * movementSpeed * Time.deltaTime, 0, 0);
        characterSprite.localScale = new Vector3(Mathf.Sign(movement) * Mathf.Abs(characterSprite.localScale.x), characterSprite.localScale.y, characterSprite.localScale.z);
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.layer);
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponentInParent<Enemies>().Kill();
            _resetForces = true;
            _canJump = true;
        }
        else if (collision.gameObject.layer == 7)
        {
            StartCoroutine(collision.gameObject.GetComponentInParent<Enemies>().DisableForSecond(2f));
            StartCoroutine(LooseLifePoint(2f, 4, .2f));
        }
    }

    private void GameOver()
    {

    }

    IEnumerator LooseLifePoint(float duration, int flickeringNumber, float newAlpha)
    {

        SpriteRenderer spriteRenderComponent = characterSprite.GetComponent<SpriteRenderer>();
        GameManager.Instance.LifePoints--;

        _score.UpdateLife();


        for (int i = 0; i < flickeringNumber; i++)
        {
            spriteRenderComponent.color = new Color(spriteRenderComponent.color.r, spriteRenderComponent.color.g, spriteRenderComponent.color.b, newAlpha);
            yield return new WaitForSeconds(duration / (flickeringNumber * 2));
            spriteRenderComponent.color = new Color(spriteRenderComponent.color.r, spriteRenderComponent.color.g, spriteRenderComponent.color.b, 1);
            yield return new WaitForSeconds(duration / (flickeringNumber * 2));
        }
    }
}
