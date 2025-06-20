using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChoiceHandler : MonoBehaviour
{
    [SerializeField] private Button chargeButton;
    [SerializeField] private Button bypassButton;
    [SerializeField] private TMP_Text bypassVoteCounter;
    [SerializeField] private TMP_Text chargeVoteCounter;
    [SerializeField] private TMP_Text timerText;

    private int bypassVotes = 0;
    private int chargeVotes = 0;
    public UnityEvent OnChoiceEnd = new UnityEvent();

    private CountdownTimer timer;
    private bool hasTimerEnded = false; 

    void Start()
    {
        timer = new CountdownTimer(4f, timerText);

        chargeButton.onClick.AddListener(OnChargeButtonClicked);
        bypassButton.onClick.AddListener(OnBypassButtonClicked);
    }

    void Update()
    {
        if (timer != null)
        {
            timer.UpdateTimer();

            // Если таймер закончился и событие ещё не вызывалось
            if (!hasTimerEnded&&timerText.ToString()=="0")
            {
                OnChoiceEnd.Invoke();
                hasTimerEnded = true; // Защита от повторного вызова
            }
        }

        DisplayVotes();
    }

    private void DisplayVotes()
    {
        bypassVoteCounter.text = bypassVotes.ToString();
        chargeVoteCounter.text = chargeVotes.ToString();
    }

    private void OnChargeButtonClicked()
    {
        chargeVotes++;
    }

    private void OnBypassButtonClicked()
    {
        bypassVotes++;
    }
}

public class CountdownTimer
{
    private float timeRemaining;
    private bool timerIsRunning=true;
    private TMP_Text timeText;

    public CountdownTimer(float initialTime, TMP_Text displayText)
    {
        timeRemaining = initialTime;
        timeText = displayText;
        DisplayTime(timeRemaining);
    }

    public void UpdateTimer()
    {
        if (timerIsRunning) 
        { 
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            Debug.Log("Time has run out!");
            timeRemaining = 0;
            timerIsRunning = false;
            
        }
    }
        
    }

    // Показываем только целые секунды (округление вверх)
    private void DisplayTime(float timeToDisplay)
    {
        timeText.text = Mathf.CeilToInt(timeToDisplay).ToString();
    }



}