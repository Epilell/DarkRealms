using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMenu : MonoBehaviour
{
    public GameObject optionMenu;

    protected bool activeOptionMenu = false;

    private void Start()
    {
        optionMenu.SetActive(activeOptionMenu);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            activeOptionMenu = !activeOptionMenu;
            optionMenu.SetActive(activeOptionMenu);
            if (activeOptionMenu == true) { Time.timeScale = 0; }
            else { Time.timeScale = 1; }
        }
    }
}