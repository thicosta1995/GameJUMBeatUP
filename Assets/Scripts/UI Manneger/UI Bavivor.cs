using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIBavivor : MonoBehaviour
{

    public Slider healtUI;
    public Image playerImage;
    public TMP_Text playerName;
    public TMP_Text liveText;

    public GameObject enemyUI;
    public Slider enemySlider;
    public TMP_Text enemyName;
    public Image enemyImage;
    private PersonMovement player;

    public float enemyUITime = 4f;
    public float enemyTimer;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PersonMovement>();
        healtUI.maxValue = player.maxHelth;
        healtUI.value = healtUI.maxValue;

        //playerName.text = player.playerName;
    }

    // Update is called once per frame
    void Update()
    {
        enemyTimer += Time.deltaTime;
        if(enemyTimer > enemyUITime) 
        {
            enemyUI.SetActive(true);

            enemyTimer = 0;
        }
    }
    public void UpdateHealt(int amout)
    {
        healtUI.value = amout;
    }
    public void UpdateEnemy(int maxHealth, int currentHealt) 
    {
        enemySlider.maxValue = maxHealth;
        enemySlider.value = currentHealt;
       
        
        enemyTimer = 0;
        enemyUI.SetActive(true);

    }

}
