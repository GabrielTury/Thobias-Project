using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;
    public SpriteRenderer lifebar;
    public float life = 1;
    public GameObject respawn;
    public GameObject playerprefab;
    GameObject playerinstance;
    public MyCamera mycamera;

    // Use this for initialization
    void Start () {
        instance = this;
       
    }
	
	// Update is called once per frame
	void Update () {
        lifebar.size = new Vector2(life * 2.17f, 0.8f);
        if (!playerinstance)
        {
            CreatePlayer();
        }
    }
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
        life = Mathf.Clamp01(life);
        if (life <= 0.01f)
        {
            Destroy(playerinstance);
        }
    }
}
