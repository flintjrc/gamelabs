using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveComponents : MonoBehaviour {

    private int PV;
    private float TorchFuel;
    private Inventory inventory;
    private playerBehaviour Behaviour;
    private List<IInventoryItem> menuItems;
    private string currentScene;

    // Use this for initialization
    void Start () {
        currentScene = SceneManager.GetActiveScene().name.ToString();
        Behaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehaviour>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        PV = Behaviour.getHP();
        TorchFuel = Behaviour.getFuel();
        menuItems = inventory.GetItems();
    }
	
	// Update is called once per frame
	void Update () {
        PV = Behaviour.getHP();
        TorchFuel = Behaviour.getFuel();
        menuItems = inventory.GetItems();

        currentScene = SceneManager.GetActiveScene().name.ToString();

        if(currentScene == "Level_2")
        {
           Behaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<playerBehaviour>();
           inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        }


    }

    public List<IInventoryItem> getItemsSave()
    {
        return menuItems;
    }

    public int getHPSave()
    {
        return PV;
    }

    public float getFuelSave()
    {
        return TorchFuel;
    }
}
