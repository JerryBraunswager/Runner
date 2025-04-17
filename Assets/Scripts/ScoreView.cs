using TMPro;
using UnityEngine;
using YG;

[RequireComponent(typeof(TMP_Text))]
public class ScoreView : MonoBehaviour
{
    [SerializeField] private Map _map;
    [SerializeField] private PlayerController _player;
    [SerializeField] private TMP_Text _highscore;

    private int _score = 0;
    private TMP_Text _currentScore;

    private void Awake()
    {
        _currentScore = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        _highscore.text = YandexGame.savesData.Highscore.ToString();
    }

    private void OnEnable()
    {
        _map.ScoreReset += ResetScore;
        _map.ScoreAdded += AddScore;
        _player.ScoreSaved += SaveScore;
    }

    private void OnDisable()
    {
        _map.ScoreReset -= ResetScore;
        _map.ScoreAdded -= AddScore;
        _player.ScoreSaved -= SaveScore;
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
            YandexGame.SaveProgress();
        }

        _highscore.text = YandexGame.savesData.Highscore.ToString();
    }
}
