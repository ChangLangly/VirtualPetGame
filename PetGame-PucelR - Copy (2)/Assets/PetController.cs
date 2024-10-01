using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetController : MonoBehaviour
{

    TheAnimal yourPet = null;  
    public TMP_Text fullness;
    public TMP_Text happiness;
    public TMP_Text energy;
    public TMP_Text petInfo;

    public InputField nameInput;
    public TMP_Text nameText;

    public Button namePet;

    string owzEFeelin;
    string whatENeedz;

    private Coroutine mealTimeCoroutine;
    private Coroutine sleepyTimeCoroutine;
    private Coroutine playTimeCoroutine;

    public Image overallHappiness;
    public Sprite[] overallHappinessPics;
    private int currentImageIndex;

    private void Start()
    {
        sleepyTimeCoroutine = null; 
        playTimeCoroutine = null;
        mealTimeCoroutine = null;
        petInfo.text = "Adopt any pet you'd like to love and give it a name you'd like to yell";
        NamingCeremony();
    }

    private void Update()
    {
        if (string.IsNullOrEmpty(nameInput.text) || nameInput.text == "Your pet's name")
        {
            namePet.interactable = false;
        }

        if (!string.IsNullOrEmpty(nameInput.text))
        {
            namePet.interactable = true;
        }

        if (yourPet != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (sleepyTimeCoroutine == null)
                {
                    sleepyTimeCoroutine = StartCoroutine("SleepyTime");
                    if (mealTimeCoroutine != null)
                    {
                        StopCoroutine(mealTimeCoroutine);
                        mealTimeCoroutine = null;
                    }
                    if (playTimeCoroutine != null)
                    {
                        StopCoroutine(playTimeCoroutine);
                        playTimeCoroutine = null;
                    }
                    
                    Debug.Log("Yer pet is sleeping");
                }
            }
            if (sleepyTimeCoroutine != null && yourPet.Energy >= 100)
            {
                StopCoroutine(sleepyTimeCoroutine);
                sleepyTimeCoroutine = null;
                Debug.Log("Sleep Stopped");
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (mealTimeCoroutine == null)
                {
                    mealTimeCoroutine = StartCoroutine("MealTime");
                    if (sleepyTimeCoroutine != null)
                    {
                        StopCoroutine(sleepyTimeCoroutine);
                        sleepyTimeCoroutine = null;
                    }
                    if (playTimeCoroutine != null)
                    {
                        StopCoroutine(playTimeCoroutine);
                        playTimeCoroutine = null;
                    }
                    
                    Debug.Log("Yer pet is eating");
                }
            }

            if (mealTimeCoroutine != null && yourPet.Fullness >= 100)
            {
                StopCoroutine(mealTimeCoroutine);
                mealTimeCoroutine = null;
                Debug.Log("Meal Stopped");
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                if (playTimeCoroutine == null)
                {
                    playTimeCoroutine = StartCoroutine("PlayTime");
                    if (mealTimeCoroutine != null)
                    {
                        StopCoroutine(mealTimeCoroutine);
                        mealTimeCoroutine = null;
                    }
                    if (sleepyTimeCoroutine != null)
                    {
                        StopCoroutine(sleepyTimeCoroutine);
                        sleepyTimeCoroutine = null;
                    }
                    
                    Debug.Log("You and yer pet are playing happily");
                }
            }

            if (playTimeCoroutine != null && yourPet.Happiness >= 100)
            {
                StopCoroutine(playTimeCoroutine);
                playTimeCoroutine = null;
                Debug.Log("Play Stopped");
            }

            FullControl();
            HappinessControl();
            EnergyControl();
            HowsEFeelin();
            YouSuckAtHavingPets();
        }
    }

    IEnumerator Fussy()
    {

        while (true)
        {
            yourPet.Fullness -= 1;
            yourPet.Energy -= 2;
            yourPet.Happiness -= 4;
            //fullness.text = "Fullness - " + yourPet.Fullness.ToString();
            //happiness.text = "Happiness - " + yourPet.Happiness.ToString();
            //energy.text = "Energy - " + yourPet.Energy.ToString();
            yield return new WaitForSeconds(4);
        }
    }

    IEnumerator MealTime()
    {
        while (true) 
        { 
            yourPet.Fullness += 33;
            yourPet.Energy += 11;
            yourPet.Happiness += 6;
            yield return new WaitForSeconds(2.5f);
        }
        
    }

    IEnumerator PlayTime()
    {
        while (true) 
        {
            yourPet.Happiness += 70;
            yourPet.Energy -= 50;
            yourPet.Fullness -= 30;
            yield return new WaitForSeconds(1.5f);
        }
        
    }

    IEnumerator SleepyTime()
    {
        int sleepQuality = Random.Range(2, 6);
        int remSleep = sleepQuality * 3;
        
        while (true) 
        {
            yourPet.Energy += remSleep;
            yield return new WaitForSeconds(3f);
        }
    }

    void FullControl()
    {
        if (yourPet.Fullness > 100)
        {
            yourPet.Fullness = 100;
            if (mealTimeCoroutine != null)
            {
                StopCoroutine(mealTimeCoroutine);
            }
            
        }

        if (yourPet.Fullness < 0)
        {
            yourPet.Fullness = 0;
            Debug.Log("Your pet is starving");
        }
        fullness.text = "Fullness - " + yourPet.Fullness.ToString();
    }

    void HappinessControl()
    {
        if (yourPet.Happiness > 100)
        {
            yourPet.Happiness = 100;
            if (playTimeCoroutine != null)
            {
                StopCoroutine(playTimeCoroutine);
            }
        }

        if (yourPet.Happiness < 0)
        {
            yourPet.Happiness = 0;
            Debug.Log("Your pet is depressed");
        }
        happiness.text = "Happiness - " + yourPet.Happiness.ToString();
    }

    void EnergyControl()
    {
        if (yourPet.Energy > 100)
        {
            yourPet.Energy = 100;
            if (sleepyTimeCoroutine != null)
            {
                StopCoroutine(sleepyTimeCoroutine);
            }
            
        }

        if (yourPet.Energy < 0)
        {
            yourPet.Energy = 0;
            Debug.Log("Your pet is neglected");
        }
        energy.text = "Energy - " + yourPet.Energy.ToString();
    }

    void HowsEFeelin()
    {
        int overallHappinessAverage = (yourPet.Fullness + yourPet.Happiness + yourPet.Energy) / 3;
        if (yourPet != null)
        {
            if (yourPet.Happiness <= 0 || yourPet.Fullness <= 0 || yourPet.Energy <= 0)
            {
                petInfo.text = "Yer pet is in a bad way. Do somethin about it now or animal control will take it away, you bad, bad person";
            }
            else
            {
                if (overallHappinessAverage >= 0 && overallHappinessAverage <= 20)
                {
                    currentImageIndex = 0;
                    owzEFeelin = "Yer pet is feelin terrible" + whatENeedz;
                }
                else if (overallHappinessAverage >= 21 && overallHappinessAverage <= 40)
                {
                    currentImageIndex = 1;
                    owzEFeelin = "Yer pet is feelin bad" + whatENeedz;
                }
                else if (overallHappinessAverage >= 41 && overallHappinessAverage <= 60)
                {
                    currentImageIndex = 2;
                    owzEFeelin = "Yer pet is feelin fine" + whatENeedz;
                }
                else if (overallHappinessAverage >= 61 && overallHappinessAverage <= 80)
                {
                    currentImageIndex = 3;
                    owzEFeelin = "Yer pet is feelin happy" + whatENeedz;
                }
                else if (overallHappinessAverage >= 81 && overallHappinessAverage <= 100)
                {
                    currentImageIndex = 4;
                    owzEFeelin = "Yer pet is feelin ecstatic" + whatENeedz;
                }

                overallHappiness.sprite = overallHappinessPics[currentImageIndex];
                petInfo.text = owzEFeelin + " with an overall Happiness of " + overallHappinessAverage;
            }
        }
    }


    IEnumerator DebugStats() 
    { 
        while (true) 
        {
            Debug.Log("Hunger " + yourPet.Fullness + " Energy " + yourPet.Energy + " Happiness " + yourPet.Happiness);
            yield return new WaitForSeconds(10);
        }
        
    }

    public void NamePet()
    {
        nameText.text = nameInput.text;
        yourPet = new TheAnimal(50, 50, 50);
        StartCoroutine("Fussy");
        StartCoroutine("DebugStats");
        NameSet();
    }

    void MostNeededAction()
    {
        int[] petStates = { yourPet.Happiness, yourPet.Energy, yourPet.Fullness };

        int minValue = petStates.Min();
        if (minValue == yourPet.Happiness)
        {
            whatENeedz = "and you should prolly play with your pet soon";
        }

        if (minValue == yourPet.Energy)
        {
            whatENeedz = "and you should prolly put your pet to bed soon";
        }

        if (minValue == yourPet.Fullness)
        {
            whatENeedz = "and you should prolly feed your pet soon";
        }
    }

    void YouSuckAtHavingPets()
    {
        
        if (yourPet.Happiness <= 0 || yourPet.Fullness <= 0 || yourPet.Energy <= 0)
        {
            Invoke("AnimalControl", 30);
        }
        else
        {
            CancelInvoke("AnimalControl");
        }
    }

    void AnimalControl()
    {
        yourPet = null;
        namePet.gameObject.SetActive(true);
        nameInput.gameObject.SetActive(true);
        petInfo.text = "Animal control has taken yer pet. Adopt a new one if you want. Just try not to hurt it this time";
        NamingCeremony();
    }

    void NamingCeremony()
    {
        nameText.text = "Your pet's name";
        fullness.text = "";
        happiness.text = "";
        energy.text = "";
    }

    void NameSet()
    {
        namePet.gameObject.SetActive(false);
        nameInput.gameObject.SetActive(false);
    }
}
