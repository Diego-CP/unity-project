using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Allow an instance of GameManager to be accesible from anywhere
    public static GameManager instance;
    public Level_Manager lvlManager;
    public CameraMotor camMotor;
    public SpellInventory si;
    public LevelStats lStats;

    // Once the game starts up, define the instance of GameManager as this
    private void Awake() 
    {
        // If there is already a GameManager in the loaded scene, destroy the new one and keep the old one
        if (GameManager.instance != null) 
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(ui);
            return;
        }
        
        // Delete all PlayerPrefs once game is started up so xp and gold is lost 
        //  when starting again
        PlayerPrefs.DeleteAll();


        instance = this;
        // Once the scene is loaded, SceneManager will go through all functions and execute them,
        //  so we add the LoadState function at the end
        SceneManager.sceneLoaded += LoadState;
    }

    private void Update()
    {
        getValues();
    }

    private void Start()    
    {
        Invoke("getValues",0);
    }

    public void getValues()
    {     
       
        if (spawnPoint == null && GameObject.Find("SpawnPoint") != null)
            spawnPoint = GameObject.Find("SpawnPoint");

        if (player == null && spawnPoint != null)
            if(GameObject.Find("Player") == null)
            {

            }
            else
            {
                player = spawnPoint.transform.GetChild(0).gameObject.GetComponent<Player>();
            }

        if (weapon == null && player != null && spawnPoint != null)
            weapon = GameObject.Find("Weapon").gameObject.GetComponent<Weapon>();
            
        if (si == null && GameObject.Find("Player") != null)
            si = GameObject.Find("Player").GetComponent<SpellInventory>();

        if (player != null && ui != null && (si.ui == null || si.uiSlots[0] == null))
        {
            si.ui = GameObject.Find("UI");
            si.uiSlots[0] = GameObject.Find("Slot1");
            si.uiSlots[1] = GameObject.Find("Slot2");
            si.slots[0] = GameObject.Find("Slot1");
            si.slots[1] = GameObject.Find("Slot2");
        }

        if (lStats == null && GameObject.Find("lvlStats") != null && GameObject.Find("Player") != null)
        {
            lStats = GameObject.Find("lvlStats").gameObject.GetComponent<LevelStats>();
            GameObject play = GameObject.Find("Player");
            play.gameObject.GetComponent<Player>().lvlStats = lStats;
            play.gameObject.GetComponent<VictoryCheck>().lvlStats = lStats;
        }

        if (camMotor == null && GameObject.FindGameObjectWithTag("MainCamera") != null)
            camMotor = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMotor>();
        

        if (camMotor.lookAt == null && GameObject.Find("Player") != null)
            camMotor.lookAt = GameObject.Find("Player").transform;

        if (ui == null && GameObject.Find("UI") != null)
            ui = GameObject.Find("UI");
      
        if (hitpointBar == null && ui != null)
            hitpointBar = ui.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<RectTransform>();

        if (faithBar == null && ui != null)
            faithBar = ui.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<RectTransform>();

        if (floatingTextManager == null && GameObject.Find("FloatingTextManager") != null)
            floatingTextManager = GameObject.Find("FloatingTextManager").gameObject.GetComponent<FloatingTextManager>();

        if (goldText == null && GameObject.Find("GoldAmount") != null)
            goldText = GameObject.Find("GoldAmount").GetComponent<GoldText>();
    }

    // References
    public GameObject spawnPoint;
    public Player player;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public RectTransform faithBar;
    public GoldText goldText;
    public LevelText levelText;
    public GameObject ui;
    public Weapon weapon;

    // Inventory
    public int gold;
    public int experience;
    public int level;
    public int faith;

    // Dictionary of spell GameObjects with their corresponding images
    public Dictionary<GameObject, GameObject> objectImages = new Dictionary<GameObject, GameObject>();

    // A function to change the HP bar once HP changes
    public void OnHitpointChange() 
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }

    // A functoin to change the Faith bar once Faith changes
    public void OnFaithChange() 
    {
        float ratio = (float)player.faith / (float)player.maxFaith;
        faithBar.localScale = new Vector3(1, ratio, 1);
    }

    public void OnGoldChange() 
    {
        goldText.AddGold();
    }
 
    public void OnExperienceChange() 
    {
        // Increase level if the player gains more than 20 xp
        if(experience >= 20) 
        {
            level ++;
            experience -= 20;
            weapon.UpgradeWeapon();

            // Find the audio manager and play the levelup sound with it
            FindObjectOfType<AudioManager>().Play("LevelupSound");
        }

        levelText.AddExperience();
    }

    // Function to call the Show form FloatinftextManager
    //  This is included in Game Manager so it can be called from anywhere
    public void ShowText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) 
    {
        floatingTextManager.Show(message, fontSize, color, position, motion, duration);
    }

    public void SaveState() 
    {
        string s = "";

        // Save the player skin
        s += "0" + "|";

        // Save the amount of gold
        s += gold.ToString() + "|";

        // Save the amount of experience
        s += experience.ToString() + "|";

        // Save the level
        s += level.ToString() + "|";

        // Save the weapon level
        s += "0";

        // Save the preferences under the id "SaveState"
        PlayerPrefs.SetString("SaveState", s);

        Debug.Log("Saved");
    }

    public void LoadState(Scene s, LoadSceneMode mode) 
    {        
        // If the player does not have a save yet, skip the loading 
        if(!PlayerPrefs.HasKey("SaveState"))
            return;

        // Get the player preferences from the id "SaveState"
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');   

        // Change amount of gold
        gold = int.Parse(data[1]);

        // Change amount of experience
        experience = int.Parse(data[2]);

        // Change level
        level = int.Parse(data[3]);
        
        // If the scene is not a menu scene, load the player at the spawn point

        if(s.name != "Death" && s.name != "Win" && s.name != "Menu" && s.name != "Editor")
        {
            // Every time a scene is loaded, teleport the player to Spawnpoint
            player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        }        

        Debug.Log("Loaded");
    }
}
