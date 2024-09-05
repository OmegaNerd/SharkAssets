using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioSource waterSource;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip upSound;
    [SerializeField] private AudioClip downSound;
    [SerializeField] private AudioClip healSound;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private List<SpriteRenderer> redSprites = new List<SpriteRenderer>();
    [SerializeField] private List<SpriteRenderer> greenSprites = new List<SpriteRenderer>();
    [SerializeField] private float redMaxVisible;
    [SerializeField] private float redPlusVisible;
    [SerializeField] private float greenMaxVisible;
    [SerializeField] private float greenPlusVisible;
    [SerializeField] private bool endlessMode = false;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveSpeedBonus;
    public float jumpSpeed;
    [SerializeField] private float jumpSpeedPlus;
    [SerializeField] private float jumpSpeedMinus;
    [SerializeField] private float jumpSpeedMinusWater;
    [SerializeField] private float fallSpeedPlus;
    [SerializeField] private float fallSpeedPlusWater;
    [SerializeField] private float fallSpeedMinusWater;
    [SerializeField] private float fallSpeedMinWater;
    [SerializeField] private float maxJumpSpeedWater;
    [SerializeField] private float dnoPos;
    private Vector3 lookVector;
    [SerializeField] private float xMax;
    [SerializeField] private float xStart;
    [SerializeField] private Animator animator;
    private Camera _cam;
    private bool seeFood = false;
    [SerializeField] private GameObject headDef;
    [SerializeField] private GameObject headOpen;

    [SerializeField] private float timeWithOpenHead;
    private float timeHeadCurrent = 0;
    private bool greenFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        Application.targetFrameRate = 60;
        for (int i = 0; i < redSprites.Count; i++)
        {
            redSprites[i].color = new Color(redSprites[i].color.r, redSprites[i].color.g, redSprites[i].color.b, 0);
        }
        GameManager.sharkMoveSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        if (GameManager.state == GameState.Game) {
            if (transform.position.y > 0.1f)
            {
                transform.position += new Vector3(GameManager.sharkMoveSpeed * GameManager.difficultyMultiply, 0, 0) * Time.deltaTime;
            }
            else {
                transform.position += new Vector3(GameManager.sharkMoveSpeed * GameManager.difficultyMultiply, 0, 0) * Time.deltaTime;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.state == GameState.Game)
        {
            if (GameManager.damageSoundFlag == true) {
                GameManager.damageSoundFlag = false;
                audioSource.PlayOneShot(damageSound);
            }
            if (transform.position.y >= 0)
            {
                if (waterSource.isPlaying == true)
                {
                    
                    waterSource.volume -= 5 * Time.deltaTime;
                    if (waterSource.volume <= 0) {
                        waterSource.Stop();
                    }
                }
            }
            else
            {
                if (waterSource.isPlaying == false)
                {
                    waterSource.volume = 0;
                    waterSource.Play();
                }
                else {
                    if (waterSource.volume < 0.5f)
                    {
                        waterSource.volume += 5 * Time.deltaTime;
                    }
                    else {
                        waterSource.volume = 0.5f;
                    }

                }
            }
            GameManager.mana -= GameManager.manaMinus * GameManager.difficultyMultiply * Time.deltaTime;
            if (GameManager.mana <= 0)
            {
                GameManager.UpdateState(GameState.Lose);
            }
            if (GameManager.damageFlag == true)
            {

                for (int i = 0; i < redSprites.Count; i++)
                {
                    redSprites[i].color = new Color(redSprites[i].color.r, redSprites[i].color.g, redSprites[i].color.b, redSprites[i].color.a + redPlusVisible * Time.deltaTime);
                }
                if (redSprites[0].color.a >= redMaxVisible)
                {
                    GameManager.damageFlag = false;
                }
            }
            else
            {
                if (redSprites[0].color.a >= 0)
                {
                    for (int i = 0; i < redSprites.Count; i++)
                    {
                        redSprites[i].color = new Color(redSprites[i].color.r, redSprites[i].color.g, redSprites[i].color.b, redSprites[i].color.a - redPlusVisible * Time.deltaTime);
                    }
                }
                else
                {
                    GameManager.canDamage = true;
                }
            }
            if (greenFlag == true)
            {
                for (int i = 0; i < greenSprites.Count; i++)
                {
                    greenSprites[i].color = new Color(greenSprites[i].color.r, greenSprites[i].color.g, greenSprites[i].color.b, greenSprites[i].color.a + greenPlusVisible * Time.deltaTime);
                }
                if (greenSprites[0].color.a >= greenMaxVisible)
                {
                    greenFlag = false;
                }
            }
            else
            {
                if (greenSprites[0].color.a >= 0)
                {
                    for (int i = 0; i < greenSprites.Count; i++)
                    {
                        greenSprites[i].color = new Color(greenSprites[i].color.r, greenSprites[i].color.g, greenSprites[i].color.b, greenSprites[i].color.a - greenPlusVisible * Time.deltaTime);
                    }
                }
                else
                {
                }
            }
            if (seeFood == true)
            {
                timeHeadCurrent += 1 * Time.deltaTime;
                if (timeHeadCurrent >= timeWithOpenHead)
                {
                    seeFood = false;
                    timeHeadCurrent = 0;
                }
            }
            headDef.SetActive(!seeFood);
            headOpen.SetActive(seeFood);
            if (endlessMode == false)
            {
                if (transform.position.x >= xMax)
                {
                    transform.position = new Vector3(xStart, transform.position.y, transform.position.z);
                }
            }
            else
            {
                //_cam.transform.position = Vector3.Lerp(_cam.transform.position, new Vector3(transform.position.x + 5, _cam.transform.position.y, _cam.transform.position.z), 0.005f);
                _cam.transform.position = new Vector3(transform.position.x + 2.5f, _cam.transform.position.y, _cam.transform.position.z);
            }

            JumpControls();
            if (jumpSpeed > maxJumpSpeedWater * GameManager.difficultyMultiply && transform.position.y < 0)
            {
                transform.position += new Vector3(0, maxJumpSpeedWater * GameManager.difficultyMultiply, 0) * Time.deltaTime;
            }
            else
            {
                transform.position += new Vector3(0, jumpSpeed * GameManager.difficultyMultiply, 0) * Time.deltaTime;
            }
            if (transform.position.y < dnoPos)
            {
                transform.position = new Vector3(transform.position.x, dnoPos, transform.position.z);
            }
            if (transform.position.y <= dnoPos)
            {
                lookVector = new Vector3(5, 0, 0);
            }
            else
            {
                if (transform.position.y < 0 && jumpSpeed < 0 && jumpSpeed >= fallSpeedMinWater)
                {
                    lookVector = new Vector3(5, -1, 0);
                }
                else
                {
                    lookVector = new Vector3(5, jumpSpeed, 0);
                }
            }
            transform.right = Vector3.Lerp(transform.right, lookVector, 0.005f);
            if (transform.position.y > 0)
            {
                if (animator.GetBool("Water") == true) {
                    audioSource.PlayOneShot(upSound);
                }
                animator.SetBool("Water", false);
            }
            else
            {
                if (animator.GetBool("Water") == false)
                {
                    audioSource.PlayOneShot(downSound);
                }
                animator.SetBool("Water", true);
            }
            if (jumpSpeed > 0)
            {
                animator.SetBool("Up", true);
            }
            else
            {
                animator.SetBool("Up", false);
            }
            if (transform.position.y == dnoPos)
            {
                animator.SetBool("Dno", true);
            }
            else
            {
                animator.SetBool("Dno", false);
            }
        }
        else {
            if (waterSource.isPlaying == true)
            {
                waterSource.Stop();
            }
        }
    }

    public void JumpControls() {
        if (transform.position.y < 0)
        {
            if (Input.touchCount > 0 || Input.GetKey(KeyCode.Mouse0))
            {
                if (jumpSpeed < 0)
                {
                    //jumpSpeed = fallSpeedMinWater;
                }
                jumpSpeed += jumpSpeedPlus  * Time.deltaTime;
            }
            else {
                if (transform.position.y <= dnoPos)
                {
                    jumpSpeed = fallSpeedMinWater * GameManager.difficultyMultiply;
                }
                else {
                    if (jumpSpeed > 0)
                    {
                        if (jumpSpeed > maxJumpSpeedWater * GameManager.difficultyMultiply)
                        {
                            jumpSpeed = maxJumpSpeedWater * GameManager.difficultyMultiply;
                        }
                        jumpSpeed -= jumpSpeedMinusWater  * Time.deltaTime;
                    }
                    else
                    {
                        if (jumpSpeed < fallSpeedMinWater * GameManager.difficultyMultiply)
                        {
                            jumpSpeed += fallSpeedMinusWater  * Time.deltaTime;
                            if (jumpSpeed >= fallSpeedMinWater * GameManager.difficultyMultiply)
                            {
                                jumpSpeed = fallSpeedMinWater * GameManager.difficultyMultiply;
                            }
                        }
                        else
                        {
                            if (jumpSpeed > fallSpeedMinWater * GameManager.difficultyMultiply)
                            {
                                jumpSpeed -= fallSpeedPlusWater * GameManager.difficultyMultiply * Time.deltaTime;
                                if (jumpSpeed <= fallSpeedMinWater * GameManager.difficultyMultiply)
                                {
                                    jumpSpeed = fallSpeedMinWater * GameManager.difficultyMultiply;
                                }
                            }
                        }
                    }
                }
            }
            
        }
        else {
            if (jumpSpeed > 0)
            {
                jumpSpeed -= jumpSpeedMinus * Time.deltaTime;
            }
            else {
                jumpSpeed -= fallSpeedPlus * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.state == GameState.Game) {
            if (collision.gameObject.GetComponent<FoodObject>() == true) {
                if (collision.gameObject.GetComponent<FoodObject>().used == false) {
                    collision.gameObject.GetComponent<FoodObject>().used = true;
                    seeFood = true;
                    timeHeadCurrent = 0;
                    collision.gameObject.GetComponent<FoodObject>().Die();
                    greenFlag = true;
                    GameManager.GetMana(0.25f);
                    audioSource.PlayOneShot(healSound);
                }
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.state == GameState.Game)
        {
            if (collision.gameObject.GetComponent<FoodObject>() == true)
            {
                //seeFood = false;
            }
        }
    }
}
