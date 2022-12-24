﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/**
 * Levelling up class 
 * 
 */
public class PlayerStats : MonoBehaviour {

    public int currentLevel;
    public int currentExp;

    public int maxExp;

    public int[] ExpNeededToLevelUp;

    public int[] HPLevels;
    public int[] attackLevels;
    public int[] defenseLevels;

    public int currentHp;
    public int currentAttack;
    public int currentDefense;

    private bool check;

    public bool dead;
    public float respawnTime;
    public float respawnTimeCounter;
    public string respawnPoint;

    private PlayerHealthManager thePlayerHealth;
    private PlayerController thePC;
    private SFXManager theSFX;
    private PlayerStartPoint thePSP;

    public Transform player;
    public GameObject levelUpText;


    // Use this for initialization
    void Start () {
        currentHp = HPLevels[1];
        currentAttack = attackLevels[1];
        currentDefense = defenseLevels[1];

        maxExp = ExpNeededToLevelUp[1];

        thePlayerHealth = FindObjectOfType<PlayerHealthManager>();
        theSFX = FindObjectOfType<SFXManager>();
        thePSP = FindObjectOfType<PlayerStartPoint>();
        thePC = FindObjectOfType<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (currentExp >= ExpNeededToLevelUp[currentLevel])
        {
            //currentLevel++;
            LevelUp();
            
        }

        // Respawning

        if (!dead)
        {
            if (thePlayerHealth.playerCurrentHealth <= 0)
            {
                //deadCheck = true;
                
                dead = true;
                respawnTimeCounter = respawnTime;

                //Destroy(gameObject);
                
            }
            check = true;
        }

        if (dead)
        {
            if (respawnTimeCounter > 0)
            {
                respawnTimeCounter -= Time.deltaTime;
            }


            if (respawnTimeCounter <= 0)
            {
                thePC.gameObject.SetActive(true);
                //thePC.transform.position = thePSP.transform.position;
                dead = false;
                thePlayerHealth.playerCurrentHealth = thePlayerHealth.playerMaxHealth;
                thePC.transform.position = Vector2.zero;
            }
            if (check)
            {
                currentExp = currentExp / 2;
                check = false;
            }
        }
       


    }

    public void AddExp(int expToAdd)
    {
        currentExp += expToAdd;
    }

    public void LevelUp()
    {
        currentLevel++;
        currentExp = 0;

        thePlayerHealth.playerMaxHealth = currentHp;
        thePlayerHealth.playerCurrentHealth += currentHp - HPLevels[currentLevel - 1];

        thePlayerHealth.playerCurrentHealth = HPLevels[currentLevel - 1]; // Make player health back to full hp

        currentHp = HPLevels[currentLevel];
        currentAttack = attackLevels[currentLevel];
        currentDefense = defenseLevels[currentLevel];

        maxExp = ExpNeededToLevelUp[currentLevel];
        theSFX.playerLevelUp.Play();

        //var clone = (GameObject)Instantiate(levelUpText, player.position, Quaternion.Euler(Vector3.zero)); // I dont get quaternion either
        //clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
        //clone.transform.position = new Vector2(thePC.transform.position.x, thePC.transform.position.y);

    }

    // For SAVE AND LOAD
    //public void Save()
    //{
    //    BinaryFormatter bf = new BinaryFormatter(); // Turn file into binary
    //    FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat"); // Create file

    //    PlayerData data = new PlayerData();
    //    data.currentHp = currentHp;
    //    data.currentAttack = currentAttack;
    //    data.currentDefense = currentDefense;

    //    bf.Serialize(file, data);
    //    file.Close();
    //}

    //public void Load()
    //{
    //    if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter(); // Turn file into binary so people can't modify it
    //        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open); // Make the file
    //        PlayerData data = (PlayerData)bf.Deserialize(file); // Uses playerData class below
    //        file.Close();

    //        currentAttack = data.currentAttack; // When loading, set currentAttack into the serializable attribute
    //        currentHp = data.currentHp;
    //        currentDefense = data.currentDefense;
    //    }
    //}
}

