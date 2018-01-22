using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private readonly float Force = 0.03f;
    private readonly float BaseSpeed = 0.03f;
    private playerController _playerController;
    private GameObject _player;
    private CircleCollider2D _collider;

    private readonly float MaximumDistanceToMove = 15f;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _playerController = _player.GetComponent<playerController>();
        _collider = gameObject.GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        CheckIfShouldMoveToPlayer();
    }

    private void CheckIfShouldMoveToPlayer()
    {
        if (_playerController.HooverEnabled)
        {
            float playerX = _player.transform.position.x;
            float playerY = _player.transform.position.y;
            float objectX = gameObject.transform.position.x;
            float objectY = gameObject.transform.position.y;

            float greaterX = playerX > objectX ? playerX : objectX;
            float lowerX = playerX > objectX ? objectX : playerX;
            float greaterY = playerY > objectY ? playerY : objectY;
            float lowerY = playerY > objectY ? objectY : playerY;

            if ((greaterX - lowerX < MaximumDistanceToMove) &&
                (greaterY - lowerY < MaximumDistanceToMove))
            {
                if (_collider.radius < 4)
                {
                    _collider.radius = 4;
                }
                MoveToPlayer();
            }
            
        }
    }

    private void MoveToPlayer()
    {
        float playerX = _player.transform.position.x;
        float playerY = _player.transform.position.y;
        float objectX = gameObject.transform.position.x;
        float objectY = gameObject.transform.position.y;

        float greaterX = playerX > objectX ? playerX : objectX;
        float lowerX = playerX > objectX ? objectX : playerX;
        float greaterY = playerY > objectY ? playerY : objectY;
        float lowerY = playerY > objectY ? objectY : playerY;

        //if difference between player X coordinate and object X coordinate is greater then between Y coordinates
        bool isXDiffGreater = (greaterX - lowerX > greaterY - lowerY);

        float x = (playerX - objectX) * BaseSpeed;
        float y = (playerY - objectY) * BaseSpeed;
        
        //powerup floating underneath the player - speed it up "a bit"
        if (x < 0.2f && x > -0.2f)
        {
            y = (y < 0) ? y : y * 2;
        }
        
        /*float x = (playerX > objectX) ? BaseSpeed : -BaseSpeed;
        float y = (playerY > objectY) ? BaseSpeed*3 : -BaseSpeed;*/
            
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + x, gameObject.transform.position.y + y);
    }
}