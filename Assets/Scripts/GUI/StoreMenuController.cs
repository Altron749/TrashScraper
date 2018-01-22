using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GUI
{
    public class StoreMenuController : MonoBehaviour
    {
        public UpgradeCenter UpgradeCenter;

        public int indexOfFirst = 0;
        public const int upgradeCount = 5;

        private const int HighScoreIndex = 0;
        private const int JetpackIndex = 1;
        private const int HooverIndex = 2;
        private const int HighJumpIndex = 3;
        private const int FreezeIndex = 4;
        
        private const int yCoordOfFirst = 463;

        private GameObject HS;
        private GameObject JP;
        private GameObject HV;
        private GameObject HJ;
        private GameObject FZ;

        public Button SecondLevelUnlockButton;
        public GameObject SecondLevelUnlocker;
        public GameObject SecondLevelDollar;
        private GameObject World2Price;
        private GameObject World1Selected;
        private GameObject World2Selected;
        private GameObject World1Selector;
        private GameObject World2Selector;
        public Button World1;
        public Button World2;

        private List<GameObject> HighScoreValues;
        private List<GameObject> HighScoreImages;
        private List<GameObject> HighScoreLevels;
        private List<GameObject> HighScorePrices;
        private GameObject HighScoreDollar;
        public Button HSButton;
        
        private List<GameObject> JetpackLevels;
        private List<GameObject> JetpackPrices;
        private GameObject JetpackDollar;
        public Button JPButton;
        
        private List<GameObject> HooverLevels;
        private List<GameObject> HooverPrices;
        private GameObject HooverDollar;
        public Button HVButton;

        private List<GameObject> HighJumpLevels;
        private List<GameObject> HighJumpPrices;
        private GameObject HighJumpDollar;
        public Button HJButton;

        private List<GameObject> FreezeLevels;
        private List<GameObject> FreezePrices;
        private GameObject FreezeDollar;
        public Button FZButton;

        public Button Back;
        public Button Up;
        public Button Down;

        public Text Money;
        
        private void Start()
        {
            InitiateLists();

            SelectCorrectImages();
            
            //set onclick
            SecondLevelUnlockButton.onClick.AddListener(UnlockSecondLevel);
            
            HSButton.onClick.AddListener(HighScoreUpgrade);
            JPButton.onClick.AddListener(JetpackUpgrade);
            HVButton.onClick.AddListener(HooverUpgrade);
            HJButton.onClick.AddListener(HighJumpUpgrade);
            FZButton.onClick.AddListener(FreezeUpgrade);
            World1.onClick.AddListener(SelectWorld1);
            World2.onClick.AddListener(SelectWorld2);
            
            Back.onClick.AddListener(GoBack);
            Up.onClick.AddListener(GoUp);
            Down.onClick.AddListener(GoDown);
        }

        private void InitiateLists()
        {
            //Levels
            SecondLevelUnlocker = GameObject.Find("World2LockedEffector");
            World1Selected = GameObject.Find("World1Selected");
            World2Selected = GameObject.Find("World2Selected");
            World1Selector = GameObject.Find("World1SelectedEffector");
            World2Selector = GameObject.Find("World2SelectedEffector");

            SecondLevelUnlocker = GameObject.Find("World2Price");
            SecondLevelDollar = GameObject.Find("World2$");
            
            //High score
            HighScoreValues = new List<GameObject>();
            HighScoreValues.Add(GameObject.Find("HS2to3"));
            HighScoreValues.Add(GameObject.Find("HS3to4"));
            HighScoreValues.Add(GameObject.Find("HS4to5"));

            HighScoreImages = new List<GameObject>();
            HighScoreImages.Add(GameObject.Find("HSPicture2"));
            HighScoreImages.Add(GameObject.Find("HSPicture3"));
            HighScoreImages.Add(GameObject.Find("HSPicture4"));
            HighScoreImages.Add(GameObject.Find("HSPicture5"));
            
            HighScoreLevels = new List<GameObject>();
            HighScoreLevels.Add(GameObject.Find("HSLevels2"));
            HighScoreLevels.Add(GameObject.Find("HSLevels3"));
            HighScoreLevels.Add(GameObject.Find("HSLevels4"));
            HighScoreLevels.Add(GameObject.Find("HSLevels5"));
            
            HighScorePrices = new List<GameObject>();
            HighScorePrices.Add(GameObject.Find("HS2kCost"));
            HighScorePrices.Add(GameObject.Find("HS5kCost"));
            HighScorePrices.Add(GameObject.Find("HS15kCost"));
            HighScorePrices.Add(GameObject.Find("HSFullyUpgraded"));

            HighScoreDollar = GameObject.Find("HS$");
            
            //Jetpack
            JetpackLevels = new List<GameObject>();
            JetpackLevels.Add(GameObject.Find("JPLevels"));
            JetpackLevels.Add(GameObject.Find("JPLevels2"));
            JetpackLevels.Add(GameObject.Find("JPLevels3"));
            JetpackLevels.Add(GameObject.Find("JPLevels4"));
            
            JetpackPrices = new List<GameObject>();
            JetpackPrices.Add(GameObject.Find("JP2kCost"));
            JetpackPrices.Add(GameObject.Find("JP5kCost"));
            JetpackPrices.Add(GameObject.Find("JP15kCost"));
            JetpackPrices.Add(GameObject.Find("JPFullyUpgraded"));

            JetpackDollar = GameObject.Find("JP$");
            
            //Hoover
            HooverLevels = new List<GameObject>();
            HooverLevels.Add(GameObject.Find("HVLevels"));
            HooverLevels.Add(GameObject.Find("HVLevels2"));
            HooverLevels.Add(GameObject.Find("HVLevels3"));
            HooverLevels.Add(GameObject.Find("HVLevels4"));
            
            HooverPrices = new List<GameObject>();
            HooverPrices.Add(GameObject.Find("HV2kCost"));
            HooverPrices.Add(GameObject.Find("HV5kCost"));
            HooverPrices.Add(GameObject.Find("HV15kCost"));
            HooverPrices.Add(GameObject.Find("HVFullyUpgraded"));

            HooverDollar = GameObject.Find("HV$");
            
            //HighJump
            HighJumpLevels = new List<GameObject>();
            HighJumpLevels.Add(GameObject.Find("HJLevels"));
            HighJumpLevels.Add(GameObject.Find("HJLevels2"));
            HighJumpLevels.Add(GameObject.Find("HJLevels3"));
            HighJumpLevels.Add(GameObject.Find("HJLevels4"));
            
            HighJumpPrices = new List<GameObject>();
            HighJumpPrices.Add(GameObject.Find("HJ2kCost"));
            HighJumpPrices.Add(GameObject.Find("HJ5kCost"));
            HighJumpPrices.Add(GameObject.Find("HJ15kCost"));
            HighJumpPrices.Add(GameObject.Find("HJFullyUpgraded"));

            HighJumpDollar = GameObject.Find("HJ$");
            
            //Freeze
            FreezeLevels = new List<GameObject>();
            FreezeLevels.Add(GameObject.Find("FZLevels"));
            FreezeLevels.Add(GameObject.Find("FZLevels2"));
            FreezeLevels.Add(GameObject.Find("FZLevels3"));
            FreezeLevels.Add(GameObject.Find("FZLevels4"));
            
            FreezePrices = new List<GameObject>();
            FreezePrices.Add(GameObject.Find("FZ2kCost"));
            FreezePrices.Add(GameObject.Find("FZ5kCost"));
            FreezePrices.Add(GameObject.Find("FZ15kCost"));
            FreezePrices.Add(GameObject.Find("FZFullyUpgraded"));

            FreezeDollar = GameObject.Find("FZ$");

            HS = GameObject.Find("HS");
            JP = GameObject.Find("JP");
            HV = GameObject.Find("HV");
            HJ = GameObject.Find("HJ");
            FZ = GameObject.Find("FZ");
        }

        private void SelectCorrectImages()
        {
            SetLevels();
            SetNavigation();
            SetContent();
        }

        private void UpdateMoney()
        {
            Money.text = DataManager.instance.GetMoney().ToString();
        }

        private void SetContent()
        {
            if (HighScoreIndex >= indexOfFirst && HighScoreIndex < indexOfFirst + 3)
            {
                SetMultiplyPoints(HighScoreIndex-indexOfFirst, UpgradeCenter.GetInstance().GetValue(UpgradeCenter.MultiplyPointsValue));
                HS.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                HS.transform.localScale = new Vector3(0, 0, 0);
            }
            if (JetpackIndex >= indexOfFirst && JetpackIndex < indexOfFirst + 3)
            {
                SetJetpack(JetpackIndex-indexOfFirst, UpgradeCenter.GetInstance().GetValue(UpgradeCenter.JetpackDuration));
                JP.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                JP.transform.localScale = new Vector3(0, 0, 0);
            }
            if (HooverIndex >= indexOfFirst && HooverIndex < indexOfFirst + 3)
            {
                SetHoover(HooverIndex-indexOfFirst, UpgradeCenter.GetInstance().GetValue(UpgradeCenter.HooverDuration));
                HV.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                HV.transform.localScale = new Vector3(0, 0, 0);
            }
            if (HighJumpIndex >= indexOfFirst && HighJumpIndex < indexOfFirst + 3)
            {
                SetHighJump(HighJumpIndex-indexOfFirst, UpgradeCenter.GetInstance().GetValue(UpgradeCenter.HighJumpDuration));
                HJ.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                HJ.transform.localScale = new Vector3(0, 0, 0);
            }
            if (FreezeIndex >= indexOfFirst && FreezeIndex < indexOfFirst + 3)
            {
                SetFreeze(FreezeIndex - indexOfFirst, UpgradeCenter.GetInstance().GetValue(UpgradeCenter.FreezeDuration));
                FZ.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                FZ.transform.localScale = new Vector3(0, 0, 0);
            }
            UpdateMoney();
        }

        private void SetLevels()
        {
            if (DataManager.instance.selectedLevel == 0)
            {
                World1Selected.transform.localScale = new Vector3(1,1,1);
                World2Selected.transform.localScale = new Vector3(0,0,0);
                World1Selector.transform.localScale = new Vector3(1,1,1);
                World2Selector.transform.localScale = new Vector3(0,0,0);
            } else if (DataManager.instance.selectedLevel == 1)
            {
                World1Selected.transform.localScale = new Vector3(0,0,0);
                World2Selected.transform.localScale = new Vector3(1,1,1);
                World1Selector.transform.localScale = new Vector3(0,0,0);
                World2Selector.transform.localScale = new Vector3(1,1,1);
            }

            if (UpgradeCenter.GetInstance().GetAvailability(UpgradeCenter.GetLevelEnabledString(1)))
            {
                SecondLevelUnlocker.transform.localScale = new Vector3(0, 0, 0);
                SecondLevelDollar.transform.localScale = new Vector3(0, 0, 0);
                SecondLevelUnlockButton.transform.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                SecondLevelUnlocker.transform.localScale = new Vector3(1, 1, 1);
                SecondLevelDollar.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        private void SetNavigation()
        {
            Up.transform.localScale = new Vector3(1, 1, 1);
            Down.transform.localScale = new Vector3(1, 1, 1);
            
            if (indexOfFirst == 0)
            {
                Up.transform.localScale = new Vector3(0, 0, 0);
            }
            if (indexOfFirst == upgradeCount - 3)
            {
                Down.transform.localScale = new Vector3(0, 0, 0);
            }
        }        
        
        private void SetMultiplyPoints(int relativeIndex, float value)
        {
            HS.transform.position = new Vector3(HS.transform.position.x, yCoordOfFirst - 150 * relativeIndex, HS.transform.position.z);
           
            for (int i = 0; i < 3; i++)
            {
                HighScoreValues[i].transform.localScale = new Vector3(0,0,0);
            }
            
            for (int i = 0; i < 4; i++)
            {
                HighScoreImages[i].transform.localScale = new Vector3(0,0,0);
                HighScoreLevels[i].transform.localScale = new Vector3(0,0,0);
                HighScorePrices[i].transform.localScale = new Vector3(0,0,0);
            }
            
            HighScoreDollar.transform.localScale = new Vector3(0,0,0);
            
            Vector3 originalVector = new Vector3(1,1,1);
            if (Mathf.Approximately(2.0f, value))
            {
                HighScoreValues[0].transform.localScale = originalVector;
                HighScoreImages[0].transform.localScale = originalVector;
                HighScoreLevels[0].transform.localScale = originalVector;
                HighScorePrices[0].transform.localScale = originalVector;
                HighScoreDollar.transform.localScale = originalVector;
            } else if (Mathf.Approximately(3.0f, value))
            {
                HighScoreValues[1].transform.localScale = originalVector;
                HighScoreImages[1].transform.localScale = originalVector;
                HighScoreLevels[1].transform.localScale = originalVector;
                HighScorePrices[1].transform.localScale = originalVector;
                HighScoreDollar.transform.localScale = originalVector;
            } else if (Mathf.Approximately(4.0f, value))
            {
                HighScoreValues[2].transform.localScale = originalVector;
                HighScoreImages[2].transform.localScale = originalVector;
                HighScoreLevels[2].transform.localScale = originalVector;
                HighScorePrices[2].transform.localScale = originalVector;
                HighScoreDollar.transform.localScale = originalVector;
            } else if (Mathf.Approximately(5.0f, value))
            {
                HighScoreImages[3].transform.localScale = originalVector;
                HighScoreLevels[3].transform.localScale = originalVector;
                HighScorePrices[3].transform.localScale = originalVector;
            }
        }
    
        void HighScoreUpgrade()
        {
            Debug.Log("Player money: " + DataManager.instance.GetMoney());
            float value = UpgradeCenter.GetInstance().GetValue(UpgradeCenter.MultiplyPointsValue);
            if (value < 5.0f)
            {
                if (DataManager.instance.Purcase(UpgradeCenter.GetInstance().GetCostOfUpgrade(UpgradeCenter.MultiplyPointsValue)))
                {
                    UpgradeCenter.GetInstance().Upgrade(1, UpgradeCenter.MultiplyPointsValue);
                    SetContent();
                }
            }
        }
        
        private void SetJetpack(int relativeIndex, float value)
        {
            //set index of the whole section based on relativeIndex
            JP.transform.position = new Vector3(JP.transform.position.x, yCoordOfFirst - 150 * relativeIndex, JP.transform.position.z);
            
            for (int i = 0; i < 4; i++)
            {
                JetpackLevels[i].transform.localScale = new Vector3(0,0,0);
                JetpackPrices[i].transform.localScale = new Vector3(0,0,0);
            }
            
            JetpackDollar.transform.localScale = new Vector3(0,0,0);
            
            Vector3 originalVector = new Vector3(1,1,1);
            
            if (Mathf.Approximately(UpgradeCenter.DefaultShortPowerupDuration, value))
            {
                JetpackLevels[0].transform.localScale = originalVector;
                JetpackPrices[0].transform.localScale = originalVector;
                JetpackDollar.transform.localScale = originalVector;
            } else if (Mathf.Approximately(UpgradeCenter.DefaultShortPowerupDuration + UpgradeCenter.DefaultShortPowerupIncreaseDuration, value))
            {
                JetpackLevels[1].transform.localScale = originalVector;
                JetpackPrices[1].transform.localScale = originalVector;
                JetpackDollar.transform.localScale = originalVector;
            } else if (Mathf.Approximately(UpgradeCenter.DefaultShortPowerupDuration + 2*UpgradeCenter.DefaultShortPowerupIncreaseDuration, value))
            {
                JetpackLevels[2].transform.localScale = originalVector;
                JetpackPrices[2].transform.localScale = originalVector;
                JetpackDollar.transform.localScale = originalVector;
            } else if (Mathf.Approximately(UpgradeCenter.DefaultShortPowerupDuration + 3*UpgradeCenter.DefaultShortPowerupIncreaseDuration, value))
            {
                JetpackLevels[3].transform.localScale = originalVector;
                JetpackPrices[3].transform.localScale = originalVector;
            }
        }
    
        void JetpackUpgrade()
        {
            float value = UpgradeCenter.GetInstance().GetValue(UpgradeCenter.JetpackDuration);
            if (value < UpgradeCenter.DefaultShortPowerupDuration + 3*UpgradeCenter.DefaultShortPowerupIncreaseDuration)
            {
                if (DataManager.instance.Purcase(UpgradeCenter.GetInstance().GetCostOfUpgrade(UpgradeCenter.JetpackDuration)))
                {
                    UpgradeCenter.GetInstance().Upgrade((int)UpgradeCenter.DefaultShortPowerupIncreaseDuration, UpgradeCenter.JetpackDuration);
                    SetContent();
                }
            }
        }
        
        private void SetHoover(int relativeIndex, float value)
        {
            //set index of the whole section based on relativeIndex
            HV.transform.position = new Vector3(HV.transform.position.x, yCoordOfFirst - 150 * relativeIndex, HV.transform.position.z);
            
            for (int i = 0; i < 4; i++)
            {
                HooverLevels[i].transform.localScale = new Vector3(0,0,0);
                HooverPrices[i].transform.localScale = new Vector3(0,0,0);
            }
            
            HooverDollar.transform.localScale = new Vector3(0,0,0);
            
            Vector3 originalVector = new Vector3(1,1,1);
            
            if (Mathf.Approximately(UpgradeCenter.DefaultShortPowerupDuration, value))
            {
                HooverLevels[0].transform.localScale = originalVector;
                HooverPrices[0].transform.localScale = originalVector;
                HooverDollar.transform.localScale = originalVector;
            } else if (Mathf.Approximately(UpgradeCenter.DefaultShortPowerupDuration + UpgradeCenter.DefaultShortPowerupIncreaseDuration, value))
            {
                HooverLevels[1].transform.localScale = originalVector;
                HooverPrices[1].transform.localScale = originalVector;
                HooverDollar.transform.localScale = originalVector;
            } else if (Mathf.Approximately(UpgradeCenter.DefaultShortPowerupDuration + 2*UpgradeCenter.DefaultShortPowerupIncreaseDuration, value))
            {
                HooverLevels[2].transform.localScale = originalVector;
                HooverPrices[2].transform.localScale = originalVector;
                HooverDollar.transform.localScale = originalVector;
            } else if (Mathf.Approximately(UpgradeCenter.DefaultShortPowerupDuration + 3*UpgradeCenter.DefaultShortPowerupIncreaseDuration, value))
            {
                HooverLevels[3].transform.localScale = originalVector;
                HooverPrices[3].transform.localScale = originalVector;
            }
        }
    
        void HooverUpgrade()
        {
            float value = UpgradeCenter.GetInstance().GetValue(UpgradeCenter.HooverDuration);
            if (value < UpgradeCenter.DefaultShortPowerupDuration + 3*UpgradeCenter.DefaultShortPowerupIncreaseDuration)
            {
                if (DataManager.instance.Purcase(UpgradeCenter.GetInstance().GetCostOfUpgrade(UpgradeCenter.HooverDuration)))
                {
                    UpgradeCenter.GetInstance().Upgrade((int) UpgradeCenter.DefaultShortPowerupIncreaseDuration, UpgradeCenter.HooverDuration);
                    SetContent();
                }
            }
        }
        
        private void SetHighJump(int relativeIndex, float value)
        {
            //set index of the whole section based on relativeIndex
            HJ.transform.position = new Vector3(HJ.transform.position.x, yCoordOfFirst - 150 * relativeIndex, HJ.transform.position.z);
            
            for (int i = 0; i < 4; i++)
            {
                HighJumpLevels[i].transform.localScale = new Vector3(0,0,0);
                HighJumpPrices[i].transform.localScale = new Vector3(0,0,0);
            }
            
            HighJumpDollar.transform.localScale = new Vector3(0,0,0);
            
            Vector3 originalVector = new Vector3(1,1,1);
            
            if (Mathf.Approximately(UpgradeCenter.DefaultPowerupDuration, value))
            {
                HighJumpLevels[0].transform.localScale = originalVector;
                HighJumpPrices[0].transform.localScale = originalVector;
                HighJumpDollar.transform.localScale = originalVector;
            } else if (Mathf.Approximately(UpgradeCenter.DefaultPowerupDuration + UpgradeCenter.DefaultPowerupIncreaseDuration, value))
            {
                HighJumpLevels[1].transform.localScale = originalVector;
                HighJumpPrices[1].transform.localScale = originalVector;
                HighJumpDollar.transform.localScale = originalVector;
            } else if (Mathf.Approximately(UpgradeCenter.DefaultPowerupDuration + 2*UpgradeCenter.DefaultPowerupIncreaseDuration, value))
            {
                HighJumpLevels[2].transform.localScale = originalVector;
                HighJumpPrices[2].transform.localScale = originalVector;
                HighJumpDollar.transform.localScale = originalVector;
            } else if (Mathf.Approximately(UpgradeCenter.DefaultPowerupDuration + 3*UpgradeCenter.DefaultPowerupIncreaseDuration, value))
            {
                HighJumpLevels[3].transform.localScale = originalVector;
                HighJumpPrices[3].transform.localScale = originalVector;
            }
        }
    
        void HighJumpUpgrade()
        {
            float value = UpgradeCenter.GetInstance().GetValue(UpgradeCenter.HighJumpDuration);
            if (value < UpgradeCenter.DefaultPowerupDuration + 3*UpgradeCenter.DefaultPowerupIncreaseDuration)
            {
                if (DataManager.instance.Purcase(UpgradeCenter.GetInstance()
                    .GetCostOfUpgrade(UpgradeCenter.HighJumpDuration)))
                {
                    UpgradeCenter.GetInstance().Upgrade((int) UpgradeCenter.DefaultPowerupIncreaseDuration,
                        UpgradeCenter.HighJumpDuration);
                    SetContent();
                }
            }
        }
        
        private void SetFreeze(int relativeIndex, float value)
        {
            //set index of the whole section based on relativeIndex
            FZ.transform.position = new Vector3(FZ.transform.position.x, yCoordOfFirst - 150 * relativeIndex, FZ.transform.position.z);
            
            for (int i = 0; i < 4; i++)
            {
                FreezeLevels[i].transform.localScale = new Vector3(0,0,0);
                FreezePrices[i].transform.localScale = new Vector3(0,0,0);
            }
            
            FreezeDollar.transform.localScale = new Vector3(0,0,0);
            
            Vector3 originalVector = new Vector3(1,1,1);
            
            if (Mathf.Approximately(UpgradeCenter.DefaultShortPowerupDuration, value))
            {
                FreezeLevels[0].transform.localScale = originalVector;
                FreezePrices[0].transform.localScale = originalVector;
                FreezeDollar.transform.localScale = originalVector;
            } else if (Mathf.Approximately(UpgradeCenter.DefaultShortPowerupDuration + UpgradeCenter.DefaultShortPowerupIncreaseDuration, value))
            {
                FreezeLevels[1].transform.localScale = originalVector;
                FreezePrices[1].transform.localScale = originalVector;
                FreezeDollar.transform.localScale = originalVector;
            } else if (Mathf.Approximately(UpgradeCenter.DefaultShortPowerupDuration + 2*UpgradeCenter.DefaultShortPowerupIncreaseDuration, value))
            {
                FreezeLevels[2].transform.localScale = originalVector;
                FreezePrices[2].transform.localScale = originalVector;
                FreezeDollar.transform.localScale = originalVector;
            } else if (Mathf.Approximately(UpgradeCenter.DefaultShortPowerupDuration + 3*UpgradeCenter.DefaultShortPowerupIncreaseDuration, value))
            {
                FreezeLevels[3].transform.localScale = originalVector;
                FreezePrices[3].transform.localScale = originalVector;
            }
        }
    
        void FreezeUpgrade()
        {
            float value = UpgradeCenter.GetInstance().GetValue(UpgradeCenter.FreezeDuration);
            if (value < UpgradeCenter.DefaultShortPowerupDuration + 3*UpgradeCenter.DefaultShortPowerupIncreaseDuration)
            {
                if (DataManager.instance.Purcase(UpgradeCenter.GetInstance()
                    .GetCostOfUpgrade(UpgradeCenter.FreezeDuration)))
                {
                    UpgradeCenter.GetInstance().Upgrade((int) UpgradeCenter.DefaultShortPowerupIncreaseDuration,
                        UpgradeCenter.FreezeDuration);
                    SetContent();
                }
            }
                
        }

        void UnlockSecondLevel()
        {
            if (DataManager.instance.Purcase(
                UpgradeCenter.GetInstance().GetCostOfEnable(UpgradeCenter.GetLevelEnabledString(1))))
            {
                SecondLevelUnlockButton.transform.localScale = new Vector3(0, 0, 0);
                UpgradeCenter.GetInstance().ChangeAvailability(true, UpgradeCenter.GetLevelEnabledString(1));
                SetLevels();
                UpdateMoney();
            }
        }

        void GoBack()
        {
            SceneManager.LoadSceneAsync(DataManager.instance.menuSceneIndex);
        }

        void GoUp()
        {
            indexOfFirst--;
            SetNavigation();
            SetContent();
        }

        void GoDown()
        {
            indexOfFirst++;
            SetNavigation();
            SetContent();
        }

        void SelectWorld1()
        {
            if (UpgradeCenter.GetInstance().GetAvailability(UpgradeCenter.GetLevelEnabledString(0)))
            {
                Debug.Log("Selected level setting to 0");
                DataManager.instance.selectedLevel = 0;
                SetLevels();
            }
        }

        void SelectWorld2()
        {
            if (UpgradeCenter.GetInstance().GetAvailability(UpgradeCenter.GetLevelEnabledString(1)))
            {
                Debug.Log("Selected level setting to 1");
                DataManager.instance.selectedLevel = 1;
                SetLevels();
            }
        }
    }
}