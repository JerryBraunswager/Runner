using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private GameObject _downPoint;
    [SerializeField] private GameObject _upPoint;

    public void Move(int speed)
    {
        gameObject.transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
    }

    public void Activate(Vector3 position)
    {
        gameObject.transform.position = position;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Check(float coordinate)
    {
        if(_upPoint.transform.position.y < coordinate) 
        {
            Deactivate();
        }
    }

    public bool CheckYCoordinate(float yCoordinate)
    {
        if (yCoordinate < _upPoint.transform.position.y & yCoordinate > _downPoint.transform.position.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
