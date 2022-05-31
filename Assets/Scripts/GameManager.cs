using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Allow an instance of GameManager to be accesible from anywhere
    public static GameManager instance;

    // Once the game starts up, define the instance of GameManager as this
    private void Awake() {
        // If there is already a GameManager in the loaded scene, destroy the new one and keep the old one
        if (GameManager.instance != null) {
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

    private void Start()
    {
        Invoke("getValues",0);
    }

    private void getValues()
    {
        spawnPoint = GameObject.Find("SpawnPoint");
        player = spawnPoint.transform.GetChild(0).gameObject.GetComponent<Player>();
        ui = GameObject.Find("UI");
        hitpointBar = ui.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<RectTransform>();
        faithBar = ui.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<RectTransform>();
        floatingTextManager = GameObject.Find("FloatingTextManager").gameObject.GetComponent<FloatingTextManager>();
    }

    // Game resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    //public List<int> weaponPrices;
    //public List<int> xpTable;

    // References
    public GameObject spawnPoint;
    public Player player;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public GameObject ui;

    public RectTransform faithBar;


    // Inventory
    public int gold;
    public int experience;

    public int faith;

    // A function to change the HP bar once HP changes
    public void OnHitpointChange() {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }

    // A functoin to change the Faith bar once Faith changes
    public void OnFaithChange() {
        float ratio = (float)player.faith / (float)player.maxFaith;
        faithBar.localScale = new Vector3(1, ratio, 1);
    }

    // Function to call the Show form FloatinftextManager
    //  This is included in Game Manager so it can be called from anywhere
    public void ShowText(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) {
        floatingTextManager.Show(message, fontSize, color, position, motion, duration);
    }

    public void SaveState() {
        string s = "";

        // Save the player skin
        s += "0" + "|";

        // Save the amount of gold
        s += gold.ToString() + "|";

        // Save the amount of experience
        s += experience.ToString() + "|";

        // Save the weapon level
        s += "0";

        // Save the preferences under the id "SaveState"
        PlayerPrefs.SetString("SaveState", s);

        Debug.Log("Saved");
    }

    public void LoadState(Scene s, LoadSceneMode mode) {        
        // If the player does not have a save yet, skip the loading 
        if(!PlayerPrefs.HasKey("SaveState"))
            return;

        // Get the player preferences from the id "SaveState"
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');   

        // Change player skin

        // Change amount of gold
        gold = int.Parse(data[1]);

        // Change amount of experience
        experience = int.Parse(data[2]);

        // Change the weapon level
        
        // If the scene is not a menu scene, load the player at the spawn point

        if(s.name != "Death" && s.name != "Win" && s.name != "Menu" && s.name != "Editor")
        {
            // Every time a scene is loaded, teleport the player to Spawnpoint
            player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        }        

        Debug.Log("Loaded");
    }
}
