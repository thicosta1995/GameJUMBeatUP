using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
  
    [SerializeField]private string Jogo;
    
    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void JogarPlayer1()
    {
        SceneManager.LoadScene(Jogo);
       
    }
    public void JogarPlayer2()
    {
        SceneManager.LoadScene(Jogo);
       
    }
}
