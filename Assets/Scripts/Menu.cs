using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject[] menus;
    InstructionManager im;

    private void Awake()
    {
        im = FindObjectOfType<InstructionManager>();
    }

    void Start()
    {
        ChangeMenu(0);
    }

    public void OnFirstBuildPressed()
    {
        ChangeMenu(1);
        im.SpawnInstruction(0);
    }

    public void ChangeMenu(int n)
    {
        if (n < 0 || n > menus.Length)
        {
            Debug.Log("Invalid menu index.");
        }

        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }

        menus[n].SetActive(true);

        im.DestroyInstruction();
    }
}
