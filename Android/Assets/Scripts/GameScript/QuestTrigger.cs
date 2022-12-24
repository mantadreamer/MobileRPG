﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour {

    private QuestManager theQuestManager;

    public int questNumber;

    public bool startQuest;
    public bool endQuest;

    public bool triggerQuestAfterCompletion;
	// Use this for initialization
	void Start () {
        theQuestManager = FindObjectOfType<QuestManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (triggerQuestAfterCompletion)
        {
            if (!theQuestManager.questCompleted[questNumber]) // If the current quest is not completed, so quest DOES NOT REPEAT
            {
                if (startQuest == true && !theQuestManager.quests[questNumber].gameObject.activeSelf) // If quest has already started and NOT ACTIVATED
                {
                    if (theQuestManager.questCompleted[questNumber - 1])
                    {

                        theQuestManager.quests[questNumber].gameObject.SetActive(true); // Quest started!
                        theQuestManager.quests[questNumber].StartQuest();
                    }
                }
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") // If the box collider touches "Player1"
        {
            if (!theQuestManager.questCompleted[questNumber]) // If the current quest is not completed, so quest DOES NOT REPEAT
            {
                if (startQuest == true && !theQuestManager.quests[questNumber].gameObject.activeSelf) // If quest has already started and NOT ACTIVATED
                {
                    
                    theQuestManager.quests[questNumber].gameObject.SetActive(true); // Quest started!
                    theQuestManager.quests[questNumber].StartQuest();
                }

                if (endQuest == true && theQuestManager.quests[questNumber].gameObject.activeSelf)
                {
                    theQuestManager.quests[questNumber].EndQuest(); // Don't need setActive(false) because it is already in questObject
                    theQuestManager.quests[questNumber].gameObject.SetActive(false);
                }
            }
 
        }
    }
}
