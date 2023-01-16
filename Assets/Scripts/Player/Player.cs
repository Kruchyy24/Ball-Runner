using UnityEngine;

public class Player : SingletonMonobehaviour<Player>
{
    [SerializeField] private float startSpeed;
    [SerializeField] private GameObject ball;
    
    private float acceleration;
    private float speed;

    private const float speedIncreaser = 0.0006f;
    private const float speedScaleX = 0.06f;

    protected override void Awake()
    {
        base.Awake();

        GameManager.Instance.isGamePlaying = true;
    }

    private void Start()
    {
        speed = startSpeed;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += new Vector3(speed * Time.deltaTime, 0f, -speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += new Vector3(-speed * Time.deltaTime, 0f, -speed * Time.deltaTime);
        }
        else if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                transform.localPosition += new Vector3(speedScaleX * touch.deltaPosition.x * speed * Time.deltaTime, 0f,
                    -speed * Time.deltaTime);
            }
            else if (touch.phase == TouchPhase.Began)
            {
                transform.localPosition += new Vector3(0f, 0f,
                    -speed * Time.deltaTime);
            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                transform.localPosition += new Vector3(0f, 0f,
                    -speed * Time.deltaTime);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                transform.localPosition += new Vector3(0f, 0f,
                    -speed * Time.deltaTime);
            }
        }
        else
        {
            transform.localPosition += new Vector3(0f, 0f, -speed * Time.deltaTime);
        }
        
        if (ball.transform.localPosition.y < -2f)
        {
            GameManager.Instance.GameOver();
        }

        IncreasePlayerSpeed();
    }

    private void IncreasePlayerSpeed()
    {
        speed += speedIncreaser;
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.localPosition;
    }
}
