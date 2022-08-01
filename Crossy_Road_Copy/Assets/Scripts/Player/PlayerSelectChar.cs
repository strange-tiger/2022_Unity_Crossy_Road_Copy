using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectChar : MonoBehaviour
{
    private GameObject[] _characters;
    private int _childCount;
    private int _charIndex;
    private void Awake()
    {
        _childCount = transform.childCount;
        _characters = new GameObject[_childCount];

        for (int i = 0; i < _childCount; ++i)
        {
            _characters[i] = transform.GetChild(i).gameObject;
            _characters[i].SetActive(false);
        }

        _charIndex = PlayerPrefs.GetInt("Character", 1);
        _characters[_charIndex].SetActive(true);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            for (int i = 0; i < _childCount; ++i)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0 + i)) _charIndex = i;
            }
            PlayerPrefs.SetInt("Character", _charIndex);
        }
    }
}
