using TMPro;
using UnityEngine;
using YG;

[RequireComponent(typeof(TMP_Text))]
public class ScoreView : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private PlayerController _player;
    [SerializeField] private Shop _shop;
    [SerializeField] private TMP_Text _highscore;
    [SerializeField] private TMP_Text _earnedScore;

    private int _score = 0;
    private TMP_Text _currentScore;

    private void Awake()
    {
        _currentScore = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        SetView();
    }

    private void OnEnable()
    {
        _map.ScoreReset += ResetScore;
        _map.ScoreAdded += AddScore;
        _player.ScoreSaved += SaveScore;
        _shop.ScoreChanged += SetView;
    }

    private void OnDisable()
    {
        _map.ScoreReset -= ResetScore;
        _map.ScoreAdded -= AddScore;
        _player.ScoreSaved -= SaveScore;
        _shop.ScoreChanged -= SetView;
    }

    private void ResetScore()
    {
        _score = 0;
        _currentScore.text = _score.ToString();
    }

    private void AddScore(int score)
    {
        _score += score;
        _currentScore.text = _score.ToString();
    }

    private void SaveScore()
    {
        if(_score > YandexGame.savesData.Highscore)
        {
            YandexGame.savesData.Highscore = _score;
        }

        YandexGame.savesData.Score += _score;
        YandexGame.SaveProgress();
        SetView();
    }

    private void SetView()
    {
        _highscore.text = YandexGame.savesData.Highscore.ToString();
        _earnedScore.text = YandexGame.savesData.Score.ToString();
    }
}
