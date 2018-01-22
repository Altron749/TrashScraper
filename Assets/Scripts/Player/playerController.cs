using Player;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor.Animations;


public class playerController : MonoBehaviour {

    [SerializeField]
    private readonly float _jumpForce = 1000f;
    [SerializeField]
    private readonly float _highJumpJumpForce = 1400f;
    [SerializeField]
    private readonly float _maxSpeed = 10f;
    [SerializeField]
    private readonly float _groundRadius = 0.2f;

    private readonly float JetpackSpeed = 15f;

    private bool disabled = false;



    //public AnimatorController withoutVacuum;
    //public AnimatorController withVacuum;
    public AnimatorOverrideController vacuumAnimator;
    public AnimatorOverrideController withoutVacuumAnimator;

    public AudioSource jumping;
    public InGameUI GUI;
    public GameObject HighJumpParticleEffectPrefab;
    public GameObject MultiplierParticleEffectPrefab;
    public GameObject JetPackParticleEffectPrefab;
    public GameObject VacuumParticleEffectPrefab;

    private GameObject _currentJetPackEffect;
    private GameObject _currentVacuumEffect;
    private GameObject _currentParticleEffectMult;
    private GameObject _currentParticleEffectHJ;
    public GameManagerEndless GM;
    private Rigidbody2D _rigidBody;
    public Transform GroundCheck;
    public LayerMask WhatIsGround;
    public Animator Anim;
    //public PlayerSoundController soundCenter;
    public Text AccelerationDebug;
    private UpgradeCenter _upgradeCenter;
    public WaterController WaterController;
    
    //player status
    private bool _facingRight = false;
    private bool _grounded = false;
    private bool _doubleJumpUsed = false;
    
    //powerups
    [SerializeField]
    private bool _highJumpEnabled = false;
    private float _highJumpEnds = 0.0f;

    private static float _pointMultiplier = 1;
    private float _doublePointsEnds = 0.0f;

    private float _freezeEnds = 0.0f;

    private bool _jetpackEnabled = false;
    private float _jetpackEnds = 0.0f;

    public bool HooverEnabled = false;
    private float _hooverEnds = 0.0f;

    private void Start()
    {
        jumping = GetComponent<AudioSource>();
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManagerEndless>();
        _rigidBody = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        //soundCenter = GetComponent<PlayerSoundController>();
        _upgradeCenter = UpgradeCenter.GetInstance();
    }

    private void FixedUpdate()
    {
        if (!disabled)
        {

        ControlGrounded();
        ControlMovement();
        ControlPowerUps();

        }
    }

    private void ControlGrounded()
    {
        _grounded = Physics2D.OverlapCircle(GroundCheck.position, _groundRadius, WhatIsGround);
        Anim.SetBool("Ground", _grounded);

        if(_grounded&&_rigidBody.velocity.y<0.001&& _rigidBody.velocity.y>-0.001)
        {
            _doubleJumpUsed = false;
        }
    }

    private void ControlMovement()
    {
        var move = getMove();
        if (_jetpackEnabled)
        {
            _rigidBody.velocity = new Vector2(move * _maxSpeed, JetpackSpeed);
        }
        else
        {
            _rigidBody.velocity = new Vector2(move * _maxSpeed, _rigidBody.velocity.y);
        }
        
        Anim.SetFloat("vSpeed", _rigidBody.velocity.y);
        Anim.SetFloat("Speed", Mathf.Abs(move));

        if(move > 0 && !_facingRight)
        {
            Flip();
        } else if(move < 0 && _facingRight)
        {
            Flip();
        }
    }

    private void ControlJumping()
    {
        //if the high jump power-up runs out, disable high jump
        if(_highJumpEnabled && Time.time > _highJumpEnds)
        {
            _highJumpEnabled = false;
            Destroy(_currentParticleEffectHJ);
        }
        Anim.SetBool("Ground", false);
        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
        _rigidBody.AddForce(_highJumpEnabled ? new Vector2(0, _highJumpJumpForce) : new Vector2(0, _jumpForce));
        //soundCenter.playJump();

        if(!_doubleJumpUsed && !(_grounded && _rigidBody.velocity.y < 0.001 && _rigidBody.velocity.y > -0.001)) {
            _doubleJumpUsed = true;
        }
    }

    private void ControlPowerUps()
    {
        //if the high jump power-up runs out, disable high jump
        if(_highJumpEnabled && Time.time > _highJumpEnds)
        {
            _highJumpEnabled = false;
            Destroy(_currentParticleEffectHJ);
        }
        
        //if the double points power-up runs out, set point multiplier to 1
        if (_pointMultiplier > 1 && Time.time > _doublePointsEnds)
        {
            Destroy(_currentParticleEffectMult);
            _pointMultiplier = 1;
        }
        
        //if the freeze power-up runs out, set water to unfrozen
        if (WaterController.isWaterFrozen() && Time.time > _freezeEnds)
        {
            WaterController.Unfreeze();
        }
        
        //if the jetpack power-up runs out, disable jetpack
        if (_jetpackEnabled && Time.time > _jetpackEnds)
        {
            Destroy(_currentJetPackEffect);
            _jetpackEnabled = false;
            Anim.SetBool("JetPack", false);
        }
        
        //if the hoover power-up runs out, disable hoover
        if (HooverEnabled && Time.time > _hooverEnds)
        {
            Destroy(_currentVacuumEffect);
            Anim.runtimeAnimatorController = withoutVacuumAnimator;
            HooverEnabled = false;
            Debug.Log("Hoover disabled");
       //     Anim.runtimeAnimatorController = withoutVacuum;
            Anim.SetBool("JetPack", _jetpackEnabled);

        }
    }

    private void Update()
    {
        if (!disabled)
        {
            var canDoubleJump = _upgradeCenter.GetAvailability(UpgradeCenter.DoubleJumpEnabled) && !_doubleJumpUsed;
            if (((_grounded && _rigidBody.velocity.y < 0.001 && _rigidBody.velocity.y > -0.001) || canDoubleJump) && IsJumpingKeyPressed())
            {
                ControlJumping();
                jumping.Play();    
            }
        }
    }

    private static bool IsJumpingKeyPressed()
    {
        #if UNITY_STANDALONE
            return Input.GetKeyDown(KeyCode.Space);
        #endif

        #if UNITY_ANDROID
            int fingerCount = 0;
            foreach (Touch touch in Input.touches) {
                if (touch.phase == TouchPhase.Began && touch.phase != TouchPhase.Canceled)
                {            
                fingerCount++;
                }
            }    
            return fingerCount>0;
        #endif
    }

    private float getMove()
    {
#if UNITY_ANDROID
        float acc = Input.acceleration.x;
        if(Mathf.Abs(acc)>0.05)
        {
                return 4 * acc;
        }
        return 0;
#else
        return Input.GetAxis("Horizontal");
#endif
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        var theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collided with water
        if(collision.CompareTag("Water"))
        {
            Drown();
        }
        
        //--powerup section--//
        
        //collided with a high jump powerup
        if (collision.CompareTag("HighJumpPowerUp"))
        {
            HighJumpPowerupCollected();
        }
        
        //collided with a double points powerup
        if (collision.CompareTag("PointMultiplyPowerUp"))
        {
            PointMultiplyPowerupCollected();
        }
        
        //collided with a freeze powerup
        if (collision.CompareTag("FreezePowerUp"))
        {
            FreezePowerupCollected();
        }
        
        //collided with a jetpack powerup
        if (collision.CompareTag("JetpackPowerUp"))
        {
            JetpackPowerupCollected();
        }
        
        //collided with a hoover powerup
        if (collision.CompareTag("HooverPowerUp"))
        {
            HooverPowerupCollected();
        }
        

        //--end of powerup section--//
    }
    
    private void HighJumpPowerupCollected()
    {
        _highJumpEnabled = true;
        _highJumpEnds = Time.time + _upgradeCenter.GetValue(UpgradeCenter.HighJumpDuration);
        if (_currentParticleEffectHJ == null)
        {
            _currentParticleEffectHJ=Instantiate(HighJumpParticleEffectPrefab,transform);
        }
        GUI.AddTime(_upgradeCenter.GetValue(UpgradeCenter.HighJumpDuration), 2);

    }

    private void PointMultiplyPowerupCollected()
    {
        _pointMultiplier = _upgradeCenter.GetValue(UpgradeCenter.MultiplyPointsValue);
        _doublePointsEnds = Time.time + _upgradeCenter.GetValue(UpgradeCenter.DoublePointsDuration);
        if (_currentParticleEffectMult == null)
        {
            _currentParticleEffectMult = Instantiate(MultiplierParticleEffectPrefab, transform);
        }
        GUI.AddTime(_upgradeCenter.GetValue(UpgradeCenter.DoublePointsDuration), 5);
    }

    private void FreezePowerupCollected()
    {
        GUI.AddTime(_upgradeCenter.GetValue(UpgradeCenter.FreezeDuration), 4);
        WaterController.Freeze();
        _freezeEnds = Time.time + _upgradeCenter.GetValue(UpgradeCenter.FreezeDuration);
        Debug.Log("Water frozen");
    }

    private void JetpackPowerupCollected()
    {
        if (_currentJetPackEffect == null)
        {
            _currentJetPackEffect = Instantiate(JetPackParticleEffectPrefab, transform);
        }
        Anim.SetBool("JetPack", true);
        _jetpackEnabled = true;
        _jetpackEnds = Time.time + _upgradeCenter.GetValue(UpgradeCenter.JetpackDuration);
        GUI.AddTime(_upgradeCenter.GetValue(UpgradeCenter.JetpackDuration), 3);

    }

    private void HooverPowerupCollected()
    {
        // Anim.runtimeAnimatorController = withVacuum;
        if (_currentVacuumEffect==null)
        {
            _currentVacuumEffect = Instantiate(VacuumParticleEffectPrefab, transform);
        }
        Anim.runtimeAnimatorController = vacuumAnimator;
        Anim.SetBool("JetPack", _jetpackEnabled);
        HooverEnabled = true;
        _hooverEnds = Time.time + _upgradeCenter.GetValue(UpgradeCenter.HooverDuration);
        Debug.Log("Hoover enabled");
        GUI.AddTime(_upgradeCenter.GetValue(UpgradeCenter.HooverDuration), 1);
    }

    private void Drown()
    {
        if (!disabled)
        {
        Anim.SetTrigger("Drowned");
            GM.StartDrowning();
        }
        disabled = true;
        Destroy(_rigidBody);
        Anim.SetBool("Ground", true);
    }

    public void PickCollectible()
    {
        //soundCenter.playPickFlower();
    }

    public void uPlayerDoneDrowning()
    {
        //GM.LoseLevel();
    }

    public float GetPointMultiplier()
    {
        return _pointMultiplier;
    }

    public float GetXVelocity()
    {
        return _rigidBody.velocity.x;
    }

    public float GetYVelocity()
    {
        return _rigidBody.velocity.y;
    }
}
