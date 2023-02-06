using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPad : MonoBehaviour
{
    //touchPad 오브젝트 연결 
    private RectTransform _touchPad;
    
    //터치 입력중 방향 컨트롤러의 영역안에 있는 입력 구분
    private int _touchId = -1;

    //입력 시작 좌표
    private Vector3 _startPos = Vector3.zero;
    
    //방향 컨트롤러가 원으로 움직이는 반지름
    public float _dragRedius = 60f;

    //플레이어의 움직임을 관리하는 PlayerMovement 스크립트와 연결
    //방향키가 변경되면 플레이어에게 신호를 보내야하기 때문
    public PlayerMovement _player;

    //버튼이 눌렸는지 체크
    private bool _buttonPressed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _touchPad = GetComponent<RectTransform>();
        _startPos = _touchPad.position;
    }

    //버튼눌림
    public void ButtonDown()
    {
        _buttonPressed = true;
    }

    //버튼 뗌, 터치패드 좌표 초기화
    public void ButtonUp()
    {
        _buttonPressed = false;
        HandleInput(_startPos);
    }

    private void FixedUpdate()
    {
        //모바일에선 터치패드 방식으로 터치 입력 받음
        HandleTouchInput();
        
        //모바일이 아닌건 마우스로 입력 받음
    #if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER

        HandleInput(Input.mousePosition);

    #endif
    }

    void HandleTouchInput()
    {
        //터치 아이디를 매기기위한 번호
        int i = 0;
        
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                i++;
                Vector3 touchPos = new Vector3(touch.position.x, touch.position.y);
                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x <= (_startPos.x+_dragRedius))
                    {
                        _touchId = i;
                    }
                }

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    if (_touchId == i)
                    {
                        HandleInput(touchPos);
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    if (_touchId == i)
                    {
                        _touchId = -1;
                    }
                }
            }   
        } 
    }
    
    void HandleInput(Vector3 input)
    {
        if (_buttonPressed)
        {
            Vector3 diffVector = (input - _startPos);
            if (diffVector.sqrMagnitude > _dragRedius * _dragRedius)
            {
                diffVector.Normalize();
                _touchPad.position = _startPos + diffVector * _dragRedius;
            }
            else
            {
                _touchPad.position = input;
            }
        }
        else
        {
            _touchPad.position = _startPos;
        }

        Vector3 diff = _touchPad.position - _startPos;
        Vector2 normDiff = new Vector2(diff.x / _dragRedius, diff.y / _dragRedius);

        if (!ReferenceEquals(_player,null))
        {
            _player.OnStickChanged(normDiff);
        }

    }
    


    // Update is called once per frame
    void Update()
    {
        
    }
}
