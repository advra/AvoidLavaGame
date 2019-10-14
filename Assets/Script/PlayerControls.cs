using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public enum lineRenderColor
    {
        WHITE,
        GREEN,
        YELLOW,
        RED
    }

    public GameObject playerCharacter;
    public Rigidbody2D rigidbody2D;
    public TimeManager timeManager;
    public LineRenderer lineRenderer;
    public ParticleSystem SmokeParticleSystem;
    public FollowCam followCam;
    public DisplayScoreScreen displayScore;

    Color damagedColor = Color.red;

    public LayerMask GroundLayer;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private Vector2 normalizedDirection;
    [SerializeField]
    private Vector2 velocity;
    public float DRAG_MIN_THRESHOLD = 3.0f;
    public float speed = 100.0f;
    public bool isSlingShotReady = false;

    public GameObject TimerUI;
    TimerText _timerUIScript;

    private Vector2 mousePosition;
    private Vector2 startMousePosition;

    public Vector3 prevMousePosition;
    public Vector3 currMousePosition;

    private Vector3 lastFrameVelocity;
    [SerializeField]
    float _spriteWidth;

    [SerializeField]
    private bool isAlive;
    [SerializeField]
    private bool isPlaying = false;
    [SerializeField]
    private bool isBurning = false;

    ChangeColor _changeLineRendererScript;
    BoxCollider2D _boxCollider;
    CircleCollider2D _circleCollider;
    SpriteRenderer _spriteRenderer;
    int combo;
    int maxCombo;
    float meters;
    Vector2 _startPos;      // used for measuring distance traveled
    [SerializeField]
    Vector2 _trueVelocity;  // velocity of the rigid body, not same as Velocity calculated from a flick = direction * speed
    [SerializeField]
    bool _isAiming;
    bool _isStanding;

    public bool Alive
    {
        get{ return isAlive; }
    }

    public bool Playing
    {
        get { return isPlaying; }
    }

    public int Combo
    {
        get { return combo; }
    }

    public int MaxCombo
    {
        get { return maxCombo; }
    }

    public float Meters
    {
        get { return meters; }
    }

    public Vector2 Velocity
    {
        get { return velocity; }
    }

    public Vector2 TrueVelocity
    {
        get { return _trueVelocity; }
    }

    public bool IsAiming
    {
        get { return _isAiming; }
    }

    public bool IsStanding
    {
        get { return _isStanding; }
    }


    // Use this for initialization
    void Start()
    {
        maxCombo = 0;
        meters = 0;
        lineRenderer.positionCount = 2;     // 2 points
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteWidth = _spriteRenderer.sprite.rect.width;
        isAlive = true;
        _timerUIScript = TimerUI.GetComponent<TimerText>();
        _changeLineRendererScript = lineRenderer.GetComponent<ChangeColor>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.enabled = false;
        SmokeParticleSystem.Stop();
        followCam.GetComponent<FollowCam>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            meters = transform.position.y - _startPos.y;
            _trueVelocity = rigidbody2D.velocity;
        }

        if(_trueVelocity == Vector2.zero)
        {
            _isStanding = true;
        }

        //lastFrameVelocity = rigidbody2D.velocity;
        isGrounded();

        if (isBurning)
        {
            SmokeParticleSystem.transform.position = transform.position;
        }
    }

    private void OnMouseDown()
    {
        if (!isSlingShotReady || !isAlive)
        {
            return;
        }

        _isAiming = true;

        timeManager.EnableSlowMotion();
        // set line render start to character
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, new Vector3(this.transform.position.x, this.transform.position.y, 0.0f));
        lineRenderer.SetPosition(1, new Vector3(this.transform.position.x, this.transform.position.y, 0.0f));
        prevMousePosition = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        if (!isSlingShotReady || !isAlive)
        {
            return;
        }


        // update start to character
        lineRenderer.SetPosition(0, new Vector3(this.transform.position.x, this.transform.position.y, 0.0f));
        // update end to mouse
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(1, new Vector3(mousePosition.x, mousePosition.y, 0.0f));

        currMousePosition = Input.mousePosition;

        direction = prevMousePosition - currMousePosition;
        renderLineColorChange(direction);
    }

    private void OnMouseUp()
    {
        if (!isSlingShotReady || !isAlive)
        {
            return;
        }

        _isAiming = false;
        clearRigidBodyForces();
        timeManager.RestoreTime();
        lineRenderer.enabled = false;
        // apply force to character
        //direction = prevMousePosition - currMousePosition;

        // make sure we apply minmum threshold force
        if(Mathf.Abs(direction.x) < DRAG_MIN_THRESHOLD || Mathf.Abs(direction.y) < DRAG_MIN_THRESHOLD)
        {
            return;
        }

        normalizedDirection = Vector2.ClampMagnitude(direction, 100f);
        velocity = normalizedDirection * speed;
        rigidbody2D.AddForce(velocity);

        isSlingShotReady = false;

        // first inits (applies when we put enough force to move)
        if (!isPlaying)
        {
            _timerUIScript.StartTimer();
            _startPos = transform.position;
        }

        isPlaying = true;           // this is the variable we check whether to start lava

        //_boxCollider.enabled = false;
        //_circleCollider.enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isPlaying)
            {
                combo++;
            }
           

            isSlingShotReady = true;

            // if traveling fast enough then shake camera
            if(rigidbody2D.velocity.x > 3500 || velocity.y > 3500)
            {
                followCam.Shake();
            }
            

            //if (Mathf.Abs(rigidbody2D.velocity.x) < 500.0f && Mathf.Abs(rigidbody2D.velocity.y) < 500.0f)
            //{
            //    _boxCollider.enabled = true;
            //    _circleCollider.enabled = false;
            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            Debug.Log("GAME OVER");
            if (combo > maxCombo)
            {
                maxCombo = combo;
            }
            displayScore.show();
            isAlive = false;
            StartCoroutine(BurnPlayer());
            timeManager.EnableSlowMotion(0.1f);
            clearRigidBodyForces();
            StartCoroutine(slowPlayer());
            _timerUIScript.StopTimer();
        }

        if (collision.gameObject.CompareTag("LeftLavaWall"))
        {
            Debug.Log("WALL HIT");
            //isAlive = false;
            clearRigidBodyForces();
            StartCoroutine(BurnPlayer());
            rigidbody2D.AddForce(new Vector2(Random.Range(0.25f, 5.0f), Random.Range(-1f, 1f)), ForceMode2D.Impulse);
            if(combo > maxCombo)
            {
                maxCombo = combo;
            }
            combo = 0;
        }

        if (collision.gameObject.CompareTag("RightLavaWall"))
        {
            Debug.Log("WALL HIT");
            //isAlive = false;
            clearRigidBodyForces();
            StartCoroutine(BurnPlayer());
            rigidbody2D.AddForce(new Vector2(Random.Range(-0.25f, -5.0f), Random.Range(-1f, 1f)), ForceMode2D.Impulse);
            if (combo > maxCombo)
            {
                maxCombo = combo;
            }
            combo = 0;
        }
    }

    void Bounce(Vector3 collisionNormal)
    {
        var direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);
        rigidbody2D.velocity = direction;
    }

    bool isGrounded()
    {
        float distance = 1.0f;

        RaycastHit2D[] hits = new RaycastHit2D[5];

        for (int i = 0; i < hits.Length; i++)
        {
            Debug.DrawRay(new Vector2((transform.position.x * i/5 + transform.position.x) , transform.position.y), Vector2.down, Color.green);
            hits[i] = Physics2D.Raycast(transform.position, direction, distance, GroundLayer);
            if (hits[i].collider != null)
            {
                _isStanding = true;
                return true;
            }
        }

        _isStanding = false;
        return false;
    }

    void clearRigidBodyForces()
    {
        rigidbody2D.AddForce(Vector2.zero, ForceMode2D.Impulse);
        rigidbody2D.angularVelocity = 0;
        rigidbody2D.velocity = Vector3.zero;
    }

    void renderLineColorChange(Vector3 distanceFromCharacter)
    {
        distanceFromCharacter = new Vector3(Mathf.Abs(distanceFromCharacter.x), Mathf.Abs(distanceFromCharacter.y), distanceFromCharacter.z);
        followCam.convertUserDragToCameraSize(distanceFromCharacter);
        if (distanceFromCharacter.x > 225 || distanceFromCharacter.y > 225)
        {
            // red
            _changeLineRendererScript.RenderGradient(ChangeColor.Color.RED);
        }
        else if (distanceFromCharacter.x > 150 || distanceFromCharacter.y > 150)
        {
            //yellow
            _changeLineRendererScript.RenderGradient(ChangeColor.Color.YELLOW);
        }
        else if (distanceFromCharacter.x > 75 || distanceFromCharacter.y > 75)
        {
            //green
            _changeLineRendererScript.RenderGradient(ChangeColor.Color.GREEN);
        }
        else
        {
            //white
            _changeLineRendererScript.RenderGradient(ChangeColor.Color.WHITE);
        }
    }

    IEnumerator slowPlayer()
    {
        rigidbody2D.gravityScale = 0.15f;
        yield return new WaitForSeconds(1f);
        rigidbody2D.gravityScale = 0;
        clearRigidBodyForces();
        isPlaying = false;                    // allow lava to keep flowing
    }

    IEnumerator BurnPlayer()
    {
        isBurning = true;
        SmokeParticleSystem.transform.position = transform.position;
        SmokeParticleSystem.Play();
        _spriteRenderer.color = damagedColor;
        yield return new WaitForSeconds(0.125f);
        _spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.125f);
        _spriteRenderer.color = damagedColor;
        yield return new WaitForSeconds(0.125f);
        _spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.125f);
        _spriteRenderer.color = damagedColor;
        yield return new WaitForSeconds(0.125f);
        _spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.125f);
        _spriteRenderer.color = damagedColor;
        yield return new WaitForSeconds(0.125f);
        _spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.125f);
        yield return new WaitForSeconds(1f);
        SmokeParticleSystem.Stop();
        isBurning = false;
    }


}
