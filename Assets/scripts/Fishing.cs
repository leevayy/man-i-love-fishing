using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Fishing : MonoBehaviour
{
    #region Singleton
    public static Fishing instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("more than one instance of Fishing");
            return;
        }

        instance = this;
    }
    #endregion

    public delegate void onFloatAction(string Animation);
    public onFloatAction onFloatActionCallBack;

    [SerializeField] private Transform[] points;
    [SerializeField] private LineController lineController;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private GameObject fishingFloat;
    [SerializeField] private float speed;
    [SerializeField] private Animator floatAnimator;
    [SerializeField] private DropManager dropManager;

    [SerializeField] private Animator FishSuckingAnimator;
    [SerializeField] private Image FishSucking;

    //timers
    [SerializeField] private float minTimeBeforeBite = 3f;
    [SerializeField] private float maxTimeBeforeBite = 10f;
    [SerializeField] private float minTimeBiting = 1f;
    [SerializeField] private float maxTimeBiting = 3f;
    //

    public bool canFish = true;

    private Collection collection;
    private Animator playerAnimator;
    private string currentState;
    private Vector3 destination;
    private float timer = 0f;
    private float timeBeforeBite;
    private float timeBiting;

    private bool isFishing = false;
    private bool timePassed = false;
    private bool timerFromThrow = false;
    private bool timerFromBite = false;

    //
    private const string man_idle = "man_idle";
    private const string man_throwingRod = "man_throwingRod";
    //
    private const string float_idle = "float_idle";
    private const string float_fishIsBiting = "float_fishIsBiting";
    private const string float_throwingAHook = "float_throwingAHook";
    //

    private void Start()
    {
        collection = Collection.instance;
        playerAnimator = GetComponent<Animator>();
        SetFishRodActive(false);
        lineController.SetUpLine(points);
        destination = fishingFloat.transform.position;
    }

    private void Update()
    {
        if(collection.items.Count >= collection.space)
        {
            canFish = false;
        }
        if (!canFish)
        {
            return;
        }
        


        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
            if ((touchPosition.y < 0) && Input.touches[i].phase == TouchPhase.Began && !isFishing)
            {
                destination = Camera.main.ScreenToWorldPoint(Input.touches[i].position);
                ThrowARod();
            }
            
            if(timePassed && Input.touches[i].phase == TouchPhase.Began && (timer < timeBiting)) // after bite before time passed
            {
                timerFromBite = false;
                timer = 0f;
                PullOutARod(true);
            }

            if(!timePassed && Input.touches[i].phase == TouchPhase.Began && (timer < timeBeforeBite && timer > 0)) // before bite 
            {
                timerFromThrow = false;
                timer = 0f;
                dropManager.OnFailure("TooEarly");
                PullOutARod(false);
            }
        }

        if (timerFromThrow) timer += Time.deltaTime;
        if (timerFromBite) timer += Time.deltaTime;

        if (!timePassed && (timer > timeBeforeBite)) // biting
        {
            FishIsBiting();
            timer = 0f;
            timerFromBite = true;
        }

        if (timePassed && (timer > timeBiting)) // after bite after time passed
        {
            timerFromBite = false;
            timer = 0f;
            dropManager.OnFailure("TooLate");
            PullOutARod(false);
        }

        if (Vector2.Distance(transform.position, destination + new Vector3(0, 0, 10)) > 0.1f)
        {
            fishingFloat.transform.position = Vector2.Lerp(fishingFloat.transform.position, destination, speed * Time.deltaTime);
        }
    }

    private void SetRandomTime()
    {
        timeBeforeBite = Random.Range(minTimeBeforeBite, maxTimeBeforeBite);
        timeBiting = Random.Range(minTimeBiting, maxTimeBiting);
    }

    private void ThrowARod()
    {
        SetRandomTime();

        ChangeAnimationState(man_throwingRod);
        isFishing = true;
        SetFishRodActive(true);
        timerFromThrow = true;

        onFloatActionCallBack.Invoke(float_throwingAHook);
    }

    private void PullOutARod(bool success)
    {
        if (success) 
        {
            dropManager.OnSuccess();
        }

        timePassed = false;
        isFishing = false;
        ChangeAnimationState(man_idle);
        SetFishRodActive(false);
    }

    private void FishIsBiting()
    {
        timerFromThrow = false;
        timerFromBite = true;
        timePassed = true;
        destination -= new Vector3(0, .5f);

        onFloatActionCallBack.Invoke(float_fishIsBiting);

    }

    private void SetFishRodActive(bool state)
    {
        fishingFloat.SetActive(state);
        lr.forceRenderingOff = !state;
        if (state == false)
        {
            fishingFloat.transform.position = new Vector3(0, 5);
            destination = fishingFloat.transform.position;
        }
    }
    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        playerAnimator.Play(newState);

        currentState = newState;
    }
}
