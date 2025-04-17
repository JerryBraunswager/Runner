using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] private Police _police;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _actorsLocation;
    [SerializeField] private Transform _menu;
    [SerializeField] private Transform _deadScreen;
    [SerializeField] private PlayerChooseTrigger _chooseTrigger;

    private bool _firstStart = true;

    private void OnDisable()
    {
        _playerController.AliveChanged -= ShowDeadScreen;
    }

    public void NewGame()
    {
        _deadScreen.gameObject.SetActive(false);
        _chooseTrigger.gameObject.SetActive(true);
        _police.SetStartPoint();
        _playerController.InitNewGame();

        for(int i = 0; i < _actorsLocation.transform.childCount; i++) 
        { 
            Destroy(_actorsLocation.transform.GetChild(i).gameObject);
        }
    }

    public void ShowMenu()
    {
        _deadScreen.gameObject.SetActive(false);
        _menu.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        if (_firstStart == true)
        {
            _menu.gameObject.SetActive(false);
            _spawner.gameObject.SetActive(true);
            _playerController.gameObject.SetActive(true);
            _playerController.AliveChanged += ShowDeadScreen;
            _firstStart = false;
        }
        else
        {
            _menu.gameObject.SetActive(false);
            NewGame();
        }
    }

    private void ShowDeadScreen(bool alive)
    {
        if(alive == false)
        {
            _deadScreen.gameObject.SetActive(true);
        }
        else
        {
            _deadScreen.gameObject.SetActive(false);
        }
    }
}
