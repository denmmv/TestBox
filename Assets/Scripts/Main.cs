using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static Main instance;

    [SerializeField] private GameObject _box;
    [SerializeField] private InputField _delayInput;
    [SerializeField] private InputField _distantionInput;
    [SerializeField] private InputField _speedInput;
    [SerializeField] private Text _time;

    [SerializeField] private Button _upButton;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Button _downButton;

    private Vector3 _spawnPoint = new Vector3(0,1,0);
    private Color _alpha;
    private Color _default;

    public int delay = 5;
    public int distantion = 8;
    public int speed = 1;
    public direction dir;

    public enum direction {down,left,up,right};

    Coroutine core;
    void Start()
    {
        instance = this;
        _default = _upButton.image.color;
        _alpha = _default;
        _alpha.a = 0;
        dir = direction.down;
        if (core == null)
        {
            core = StartCoroutine(Core());
        }
    }

    IEnumerator Core()
    {
        while (true)
        {
            int.TryParse(_delayInput.text, out int _delayResult);
            if (_delayResult < 1 || _delayResult > 100)
            {
                _delayInput.text = 3.ToString();
                _delayResult = 3;
            }
            delay = _delayResult;
            int.TryParse(_distantionInput.text, out int _distantionResult);
            if (_distantionResult < 1 || _distantionResult > 101)
            {
                _distantionInput.text = 20.ToString();
                _distantionResult = 20;
            }
            distantion = _distantionResult;
            int.TryParse(_speedInput.text, out int _speedResult);
            if (_speedResult < 1 || _speedResult > 51)
            {
                _speedInput.text = 6.ToString();
                _speedResult = 6;
            }
            speed = _speedResult;
            SpawnGoat();
            int timer = delay;
            while (timer > 0)
            {
                _time.text = timer.ToString();
                timer = timer - 1;                                
                yield return new WaitForSecondsRealtime(1f);
            }            
        }       
    }
    public void SpawnGoat()
    {
       Instantiate(_box,_spawnPoint,Quaternion.Euler(0,90*((int)dir),0));
    }
    public void up()
    {
        dir = direction.up;
        _upButton.image.color = _alpha;
        _leftButton.image.color = _default;
        _rightButton.image.color = _default;
        _downButton.image.color = _default ;
    }
    public void left()
    {
        dir = direction.left;
        _upButton.image.color = _default;
        _leftButton.image.color = _alpha;
        _rightButton.image.color = _default;
        _downButton.image.color = _default;
    }
    public void right()
    {
        dir = direction.right;
        _upButton.image.color = _default;
        _leftButton.image.color = _default;
        _rightButton.image.color = _alpha;
        _downButton.image.color = _default;

    }
    public void down()
    {
        dir = direction.down;
        _upButton.image.color = _default;
        _leftButton.image.color = _default;
        _rightButton.image.color = _default;
        _downButton.image.color = _alpha;
    }
}
