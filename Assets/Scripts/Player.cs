using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    private float moveSpeed;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;
    private float jumpForce;
    
    
    
    public bool iFrame;
    public float iFrameSec;
    public Vector2 startPos;

    private void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        
    }
    private void Start()
    {
        moveSpeed = 3f;
        jumpForce = 60f;
        isJumping = false;
        iFrame = false;
        iFrameSec = 1.5f;
        
        startPos = transform.position;
      
    } 
    private void Update()
    {
         
        moveHorizontal = Input.GetAxisRaw("Horizontal"); // default in unity settings is A(Left = -1) and D(Right = 1) or left and right arrow (No keypress = 0)
        moveVertical = Input.GetAxisRaw("Vertical"); //default is W S Up Down
        
         
    }

    private void FixedUpdate() //similar to Update but used when working with physics
    {
        if(moveHorizontal > 0.1f || moveHorizontal < -0.1f) // not 0 since keypress is 0 which may cause confusion
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse); //Time.deltaTime is not used since AddForce has it applied as default in ForceMode
        }

        if(!isJumping && moveVertical > 0.1f) // not 0 since keypress is 0 which may cause confusion
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Complete")
        {
            Debug.Log("Player Entered");
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isJumping = false;
            
        }
     
        
        if(collision.gameObject.tag == "Complete" && Input.GetKeyDown(KeyCode.E))
        {
            
            Debug.Log("Level Completed!");
            FindObjectOfType<GameManager>().RoundSys();
            transform.position = startPos;
            
            
        }
       
        if(!iFrame && collision.gameObject.tag == "Obstacle")
            {
            FindObjectOfType<GameManager>().DecreaseHealth();   
            
            StartCoroutine(IFrames());
            StartCoroutine(IFrameBlink());
            FindObjectOfType<GameManager>().Died();
            }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground") 
        {
            isJumping = true;
        }
       
        if(collision.gameObject.tag == "Complete")
        {
            Debug.Log("Player Exited");
        }
    }
    public IEnumerator IFrames()
    {
         
        Debug.Log("Player Invinsible!");
        iFrame = true;
        
        yield return new WaitForSeconds(iFrameSec);
        
        
        iFrame = false;
        Debug.Log("Player Vulnerable!");
         
    }
    public IEnumerator IFrameBlink()
    {
        while(iFrame==true && FindObjectOfType<GameManager>().health > 0)
        {
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.enabled=false;
            yield return new WaitForSeconds(0.05f);
            spriteRenderer.enabled=true;
            yield return null;
        }
         
    }
     
}
