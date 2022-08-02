using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public enum CharactersPrices
    {
        Chicken = 0,
        Cat = 10,
        Dog = 20,
        Penguin = 50,
        Lion = 100,
        Max
    }
    
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI CharName;
    public TextMeshProUGUI CharPriceText;
    public GameObject Characters;
    public int Coin;
    public int Index
    {
        get
        {
            return _index;
        }
        set
        {
            _index = _charIndex - 1;
        }
    }

    private Camera _camera;
    private Animator _animator;
    private GameObject[] _characters;
    private int _charCount;
    private int _charIndex;
    private int _index = 0;
    private float _charXDistance = 5f;
    private string[] _charName;
    private int[] _charPrice;
    void Awake()
    {
        Coin = PlayerPrefs.GetInt("Coin", 0);
        CoinText.text = $"{Coin}";
        
        _camera = Camera.main;

        _charCount = Characters.transform.childCount;
        _characters = new GameObject[_charCount];
        _charName = new string[_charCount];
        _charPrice = new int[_charCount];

        for (int i = 0; i < _charCount; ++i)
        {
            _characters[i] = Characters.transform.GetChild(i).gameObject;
            _characters[i].SetActive(true);
            _charName[i] = _characters[i].name;
        }

        _charPrice[0] = (int)CharactersPrices.Chicken;
        _charPrice[1] = (int)CharactersPrices.Cat;
        _charPrice[2] = (int)CharactersPrices.Dog;
        _charPrice[3] = (int)CharactersPrices.Penguin;
        _charPrice[4] = (int)CharactersPrices.Lion;

        _charIndex = PlayerPrefs.GetInt("Character", 1);
        _index = _charIndex - 1;
        _camera.transform.position += _charXDistance * (Index) * Vector3.right;

        CharName.text = _charName[Index];
        CharPriceText.text = $"{_charPrice[Index]}";
    }

    public void RightBtnPushed()
    {
        if(_charIndex > 0 && _charIndex < 5)
        {
            ++_charIndex;
            _camera.transform.position += _charXDistance * Vector3.right;
        }
        else
        {
            _charIndex = 1;
            _camera.transform.position += 4f * _charXDistance * Vector3.left;
        }
    }

    public void LeftBtnPushed()
    {
        if (_charIndex > 1 && _charIndex <= 5)
        {
            --_charIndex;
            _camera.transform.position += _charXDistance * Vector3.left;

        }
        else
        {
            _charIndex = 5;
            _camera.transform.position += 4f * _charXDistance * Vector3.right;
        }
    }

    public void Purchase()
    {
        if (Coin > _charPrice[Index])
        {
            Coin -= _charPrice[Index];
            PlayerPrefs.SetInt("Coin", Coin);
            _animator = _characters[Index].GetComponent<Animator>();
            _animator.SetTrigger("jump");
            PlayerPrefs.SetInt("Character", _charIndex);
        }
    }
}
