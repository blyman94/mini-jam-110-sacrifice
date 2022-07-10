using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class Functionalities : MonoBehaviour //inventoryTab
{
    //script to open inventory and display description.

    [Header ("Inventory Openning")] 
    public GameObject inventoryTab;
    private float starPosChangerY;
    private Vector2 finalPos;
    private Vector2 startPos;

    [Header("Item Description")]
    public Text itemTitle;
    public Text itemBody;
    public Text atributte1;
    public Image image_atributte1;
    public Text itemRarity;

    float timeUntilClose=0.5f;
    float startTime = 0;
    float currentTime;
    bool active = false;

    private InputAction Open;

    private void Start()
    {
        //clean description
        image_atributte1.enabled = false;
        itemTitle.text = "";
        itemBody.text = "";
        atributte1.text = "";
        itemRarity.text = "";

        startPos = new Vector2(1153f, -275f);
        finalPos = new Vector2(254f, -275f);
        starPosChangerY = 15f;
        inventoryTab.GetComponent<RectTransform>().anchoredPosition = startPos;
        
        Open = new InputAction(binding: "<Keyboard>/space");
        Open.performed += ctx => OpenInventory();
        Open.Enable();
    }

    // Update is called once per frame
    void OpenInventory()
    {
        //open inventory using key I, and dont let open it so fast
    
            if (active)
            {
                active = !active;
                inventoryTab.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPos.x, startPos.y);
            }
            else
            {
                active = !active;
                inventoryTab.GetComponent<RectTransform>().anchoredPosition = new Vector2(finalPos.x, finalPos.y);
            }
        }
        


    //This function is called when the mouses passes through an item in the inventory
    public void changeDescription(string title, string body, int att1 = 0, string rarity = "",Sprite icon1 = null)
    {
        itemTitle.text = title;
        itemBody.text = body;
        itemRarity.text = rarity;

        if (att1 > 0)
            atributte1.text = "+" + att1.ToString();
        else if (att1 < 0)
            atributte1.text = "-" + att1.ToString();
        else
            atributte1.text = "";

        if (icon1 != null)
        { 
            image_atributte1.enabled = true;
            image_atributte1.sprite = icon1;
        }
        else
            image_atributte1.enabled = false;

    }
}


