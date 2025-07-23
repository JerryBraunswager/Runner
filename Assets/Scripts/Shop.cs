using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Shop : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Image _characterLook;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _buyButton;
    [SerializeField] private List<Actor> _buyableCharacters;
    [SerializeField] private List<int> _prices;

    private int _characterIndex = 0;

    public event Action ScoreChanged;

    private void Start()
    {
        SetLook();

        if(YandexGame.savesData.BuyedCharacters.Count < _buyableCharacters.Count)
        {
            for(int i = 0; i < _buyableCharacters.Count; i++) 
            {
                if (i == YandexGame.savesData.BuyedCharacters.Count)
                {
                    YandexGame.savesData.BuyedCharacters.Add(false);
                    YandexGame.savesData.Price.Add(_prices[i]);
                }

                YandexGame.SaveProgress();
            }
        }
        else
        {
            for (int i = 0; i < _buyableCharacters.Count; i++)
            {
                if (YandexGame.savesData.BuyedCharacters[i] == true)
                {
                    _spawner.AddCharacterInPrefabs(_buyableCharacters[i]);
                }
            }

            CheckBuy();
        }
    }

    public void PreviousCharacter()
    {
        _characterIndex--;

        if(_characterIndex < 0 )
        {
            _characterIndex = _buyableCharacters.Count - 1;
        }

        SetLook();
        CheckBuy();
    }

    public void NextCharacter() 
    { 
        _characterIndex++;

        if(_characterIndex >= _buyableCharacters.Count ) 
        {
            _characterIndex = 0;
        }

        SetLook();
        CheckBuy();
    }

    public void BuyCharacter()
    {
        if(YandexGame.savesData.Score < _prices[_characterIndex])
        { 
            return; 
        
        }
        _spawner.AddCharacterInPrefabs(_buyableCharacters[_characterIndex]);
        YandexGame.savesData.BuyedCharacters[_characterIndex] = true;
        YandexGame.savesData.Score -= _prices[_characterIndex];
        YandexGame.SaveProgress();
        ScoreChanged?.Invoke();
        CheckBuy();
    }

    private void SetLook()
    {
        _characterLook.sprite = _buyableCharacters[_characterIndex].GetComponent<SpriteRenderer>().sprite;
        _price.text = _prices[_characterIndex].ToString();
    }

    private void CheckBuy()
    {
        if (YandexGame.savesData.BuyedCharacters[_characterIndex] == true)
        {
            _buyButton.interactable = false;
        }
        else
        {
            _buyButton.interactable = true;
        }
    }
}
