using UnityEngine;
using YG;

public class RewardButton : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Police _police;

    private void OnEnable() 
    { 
        YandexGame.RewardVideoEvent += Rewarded; 
    }

    private void OnDisable() 
    { 
        YandexGame.RewardVideoEvent -= Rewarded; 
    }

    public void ClickButton()
    {
        YandexGame.RewVideoShow(1);
    }

    private void Rewarded(int id)
    {
        if (id == 1)
        {
            _playerController.Resurrect();
            _police.SetStartPoint();
        }
    }
}
