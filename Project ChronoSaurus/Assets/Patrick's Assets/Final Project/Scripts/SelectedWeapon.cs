using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedWeapon : MonoBehaviour
{
    public Text text;

    public GameObject textGun;
    public void WeaponName(string name)
    {
        text.text = name;
        textGun.GetComponent<Text>().text = name;

    }
}
