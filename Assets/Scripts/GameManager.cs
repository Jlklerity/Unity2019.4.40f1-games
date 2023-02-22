using UnityEngine;
using UnityEngine.UI;
 
public class GameManager : MonoBehaviour
{
    public Text healthText;
    public int health;
    public Text diedText;
    public Text roundText;
    public Text roundTitle;
    public int round;
    public Player player;
    public GameObject Round1;
    public GameObject Round2;
    public GameObject Round3;
    public GameObject Round4;
    public GameObject Round5;
    public GameObject Round6;
    public GameObject Round7;
    public GameObject Round8;
    public Vector2 startPos;
    public GameObject ShadeWin;
    public Text WonText;
    public Text EText;
   
    private void Awake()
    {
        health = 10;
        diedText.enabled = false;
        player.enabled = true;
        round = 1;
        Round1.SetActive(true);
        Round2.SetActive(false);
        Round3.SetActive(false);
        Round4.SetActive(false);
        Round5.SetActive(false);
        Round6.SetActive(false);
        Round7.SetActive(false);
        Round8.SetActive(false);
        ShadeWin.SetActive(false);
        EText.enabled = true;
        WonText.enabled = false;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        
    }
    public void DecreaseHealth()
    {
        health--;
        healthText.text = health.ToString();
        
        
    }
    public void RoundSys()
    {
        EText.enabled=false;
        health = 10;
        healthText.text = health.ToString();
        round++;
        roundText.text = round.ToString();
        
        
        switch(round)
        {
            case 2: 
            Round1.SetActive(false);
            Round2.SetActive(true);
            break;

            case 3: 
            Round2.SetActive(false);
            Round3.SetActive(true);
            break;

            case 4: 
            Round3.SetActive(false);
            Round4.SetActive(true);
            break;

            case 5: 
            Round4.SetActive(false);
            Round5.SetActive(true);
            break;

            case 6: 
            Round5.SetActive(false);
            Round6.SetActive(true);
            break;

            case 7: 
            Round6.SetActive(false);
            Round7.SetActive(true);
            break;

            case 8: 
            roundTitle.text = "Final Round";
            roundText.text = "";
            Round7.SetActive(false);
            Round8.SetActive(true);
            break;

            case 9:
            roundTitle.text = "";
            roundText.text = "";
            ShadeWin.SetActive(true);
            WonText.enabled = true;
            player.enabled = false;
            break;

            default:
            break;
        }
       
    }
    public void Died()
    {
        if(health == 0)
        {
            Pause();
            diedText.enabled = true;
        }
        
    }
   
    
}
