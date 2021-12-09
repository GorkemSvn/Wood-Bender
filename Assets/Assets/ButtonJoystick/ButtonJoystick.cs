using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[CreateAssetMenu(fileName ="Joystick",menuName ="UI/Joystick")]
public class ButtonJoystick : MonoBehaviour
{
    //read
    public Vector2 swipe { get; private set; }
    public Vector2 Direction { get; private set; }
    public Vector2 deltaPos { get; private set; }
    public float sqrm { get; private set; }
    public bool swiping { get; private set; }
    public Vector2 posMemory { get; private set; }

    [SerializeField] bool dontAppear;
    [SerializeField] bool multipleJoysticks;

    public event JoystickEvent OnPress;

    public static ButtonJoystick singleTon;

    private float radius = 400;
    private RectTransform rectTransform;
    private Transform circle,joystick;
    private Vector2 startpos;
    private int fingerID;

    public delegate void JoystickEvent(Vector2 tapPosition);

    public void Awake()
    {
        if (multipleJoysticks) SelfSetUp();
        else if (singleTon) Destroy(gameObject);
        else SelfSetUp();
        
    }
    void SelfSetUp()
    {
        singleTon = this;
        circle = transform.GetChild(0);
        joystick = transform.GetChild(0).GetChild(0);
        radius = circle.GetComponent<RectTransform>().sizeDelta.x / 2;
        startpos = circle.position;
        rectTransform = transform.GetComponent<RectTransform>();
        Disappear();
    }

    void Update()
    {
        if(swiping)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).fingerId == fingerID)
                {
                    Prosedure(Input.GetTouch(i).position);
                    return;
                }
            }

            Prosedure(Input.mousePosition);
        }

    }
    void Prosedure(Vector2 position)
    {
        swipe = position - startpos;

        deltaPos = swipe - posMemory;
        posMemory = swipe;

        Direction = (position - startpos) / radius;
        sqrm = Direction.sqrMagnitude;

        if (sqrm > 1)
        {
            Direction = Direction.normalized;
            sqrm = 1;
        }
        
        joystick.position = startpos + Direction * radius;
    }

    public void onclick()
    {
        posMemory = Vector2.zero;
        //register finger id
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 localpos= rectTransform.InverseTransformPoint(Input.GetTouch(i).position);
            if (rectTransform.rect.Contains(localpos))
            {
                fingerID = Input.GetTouch(i).fingerId;
                startpos = Input.GetTouch(i).position;
                circle.position = startpos;
                swiping = true;
                if (!dontAppear)
                    Appear();
                break ;
            }
        }

        if (Input.GetMouseButton(0))
        {
            swiping = true;
            if (!dontAppear)
                Appear();
            circle.position = Input.mousePosition;
            startpos = Input.mousePosition;
        }
        OnPress?.Invoke(startpos);
    }
    public void onrelease()
    {
        Disappear();
       // joystick.position = startpos;
        Direction = Vector2.zero;
        swiping = false;
        sqrm = 0;
    }

    void Appear()
    {
        circle.DOKill();
        joystick.DOKill();
        circle.GetComponent<Image>().DOFade(1f, 0.2f);
        joystick.DOScale(Vector3.one, 0.2f);
    }
    void Disappear()
    {
        circle.DOKill();
        joystick.DOKill();
        circle.GetComponent<Image>().DOFade(0f, 0.2f);
        joystick.DOScale(Vector3.zero, 0.2f);
    }
}
