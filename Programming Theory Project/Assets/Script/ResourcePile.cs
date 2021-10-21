using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// A subclass of Building that produce resource at a constant rate.
/// </summary>
/// INHERITANCE
public class ResourcePile : Building
{
    public ResourceItem Item;

    private float m_ProductionSpeed = 0.5f;
    public float ProductionSpeed
    {
        get { return m_ProductionSpeed; } // getter returns backing field
        set
        {
            if (value < 0.0f)
            {
                Debug.LogError("You can't set a negative production speed!");
            }
            else
            {
                m_ProductionSpeed = value; // original setter now in if/else statement
            }
        } // setter uses backing field
    }
    [System.Serializable]
    public class SaveObj
    {
        public InventoryEntry ResourceEntry;
    }
    private float m_CurrentProduction = 0.0f;
    private void Awake()
    {
        SaveObj data;
        // ABSTRACTION
        data = LoadData();
        
        if (data.ResourceEntry != null)
        {
            // ABSTRACTION
            int SecondsElapsed = Epoch.SecondsElapsed(data.ResourceEntry.LastUpdate);
            float amountProducedOffLine = Mathf.RoundToInt((float)SecondsElapsed / m_ProductionSpeed);
            // ABSTRACTION
            AddItem(Item.Id, (int)amountProducedOffLine + data.ResourceEntry.Count);
        }        
    }
    private void Update()
    {
        if (m_CurrentProduction > 1.0f)
        {
            int amountToAdd = Mathf.FloorToInt(m_CurrentProduction);
            int leftOver = AddItem(Item.Id, amountToAdd);
            SaveData();

            m_CurrentProduction = m_CurrentProduction - amountToAdd + leftOver;
        }

        if (m_CurrentProduction < 1.0f)
        {
            m_CurrentProduction += m_ProductionSpeed * Time.deltaTime;
        }
    }

    public override string GetData()
    {
        return $"Producing at the speed of {m_ProductionSpeed}/s";

    }
    // POLYMORPHISM - OVERRIDE
    public override void SaveData()
    {
        int found = m_Inventory.FindIndex(item => item.ResourceId == Item.Id);
        //Debug.Log(found);
        if (found != -1)
        {
            SaveObj data = new SaveObj();
            data.ResourceEntry = m_Inventory[found];

            string json = JsonUtility.ToJson(data);
            //Debug.Log(Application.persistentDataPath);
            File.WriteAllText(Application.persistentDataPath + "/resources/"+ Item.Id + ".json", json);
        }
    }

    public SaveObj LoadData()
    {
        string path = Application.persistentDataPath + "/resources/" + Item.Id + ".json";
        SaveObj data;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveObj>(json);
        }
        else
        {
            data = new SaveObj();
        }
        return data;
    }
}
