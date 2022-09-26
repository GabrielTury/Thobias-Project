using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

   public Slider slider;

    public static LevelManager instance;    
    public float life = 1;
    public GameObject respawn;
    public GameObject playerprefab;
    GameObject playerinstance;
    public MyCamera mycamera;

    public GameObject pauseMenu;
    private bool paused;

    // Use this for initialization
    void Start () {
        instance = this;
       
    }
	
	// Update is called once per frame
	void Update () {       
        slider.value = life;
        if (!playerinstance)
        {
            CreatePlayer();
            //Stationary_Enemy.instance.GetPlayerTransform(); //-- Descobrir pq isso só funciona com 1 ao mesmo tempo
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            paused = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);          
        }else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            paused = false;
            Time.timeScale = 2.0f;
            pauseMenu.SetActive(false);
        }

        if(Time.timeScale != 0)
        {
            pauseMenu.SetActive(false);
            paused = false;
        }
    }
    /// <summary>
    /// Spawna o jogador
    /// </summary>
    void CreatePlayer()
    {
        playerinstance = Instantiate(playerprefab, respawn.transform.position, Quaternion.identity);
        mycamera.SetPlayer(playerinstance);
        life = 1;
    }
    /// <summary>
    /// Aplica pouco dano
    /// </summary>
    public void LowDamage()
    {
        life -= 0.1f;
        NewControls.instance.GotHit();
        life = Mathf.Clamp01(life);
        if (life <= 0.01f)
        {
            Destroy(playerinstance);
        }
    }
    /// <summary>
    /// Dano que leva quando enconsta iEnemy
    /// </summary>
    public void TouchDamage()
    {
        life -= iEnemyScript.instance.touchDamageValue;
        NewControls.instance.GotHit();
        life = Mathf.Clamp01(life);
        if (life <= 0.01f)
        {
            Destroy(playerinstance);
        }
    }
    /// <summary>
    /// Dano de ataque do iEnemy
    /// </summary>
    public void AttackDamage()
    {
        life -= iEnemyScript.instance.atackDamageValue;
        NewControls.instance.GotHit();
        life = Mathf.Clamp01(life);
        if (life <= 0.01f)
        {
            Destroy(playerinstance);
        }
    }
}
