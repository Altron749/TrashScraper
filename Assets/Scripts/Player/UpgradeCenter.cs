using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * what will be upgradable:
 * - powerup lengths
 * - ? running speed - maybe later on
 * - point multiplier powerup value
 *
 * what will be enablable:
 * - double jump
 * - head start
 * - levels
 */

namespace Player
{
    public class UpgradeCenter : IUpgradable, IEnablable
    {
        private static UpgradeCenter _instance;
        
        //constants
        public const float DefaultPowerupDuration = 15.0f;
        public const float DefaultMultiplyPointsValue = 2.0f;
        public const float DefaultShortPowerupDuration = 8.0f;
        public const float DefaultPowerupIncreaseDuration = 5.0f;
        public const float DefaultShortPowerupIncreaseDuration = 3.0f;

        public readonly int[] PricesForUpgradeLevel = new int[] {2000, 5000, 15000};

        private const bool _doubleJumpEnabledByDefault = true;
        private const bool _headStartEnabledByDefault = false;
        private const bool _firstLevelEnabledByDefault = true;
        private const bool _secondLevelEnabledByDefault = false;

        //string constants
        public const string HighJumpDuration = "highJumpDuration";
        public const string DoublePointsDuration = "doublePointsDuration";
        public const string JetpackDuration = "jetpackDuration";
        public const string FreezeDuration = "freezeDuration";
        public const string HooverDuration = "hooverDuration";
        public const string MultiplyPointsValue = "multiplyPointsValue";
        public const string DoubleJumpEnabled = "doubleJumpEnabled";
        public const string HeadStartEnabled = "headStartEnabled";

        public static string GetLevelEnabledString(int levelNumber)
        {
            return "level" + levelNumber + "Enabled";
        }
        
        private static Dictionary<string, UpgradableNumber> _upgradableProperties = new Dictionary<string, UpgradableNumber>();
        private static Dictionary<string, EnablableBoolean> _enablableProperties = new Dictionary<string, EnablableBoolean>();

        private UpgradeCenter()
        {
            InitiateState();
        }

        public static UpgradeCenter GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UpgradeCenter();
            }
            return _instance;
        }

        /*
         * Initiate starting state of all properties
         */
        private static void InitiateState()
        {
            //upgrades
            if (!_upgradableProperties.ContainsKey(HighJumpDuration))
            {
                _upgradableProperties.Add(HighJumpDuration, new UpgradableNumber(DefaultPowerupDuration));
            }
            if (!_upgradableProperties.ContainsKey(DoublePointsDuration))
            {
                _upgradableProperties.Add(DoublePointsDuration, new UpgradableNumber(DefaultPowerupDuration));
            }
            if (!_upgradableProperties.ContainsKey(JetpackDuration))
            {
                _upgradableProperties.Add(JetpackDuration, new UpgradableNumber(DefaultShortPowerupDuration));
            }
            if (!_upgradableProperties.ContainsKey(MultiplyPointsValue))
            {
                _upgradableProperties.Add(MultiplyPointsValue, new UpgradableNumber(DefaultMultiplyPointsValue));
            }
            if (!_upgradableProperties.ContainsKey(FreezeDuration))
            {
                _upgradableProperties.Add(FreezeDuration, new UpgradableNumber(DefaultShortPowerupDuration));
            }
            if (!_upgradableProperties.ContainsKey(HooverDuration))
            {
                _upgradableProperties.Add(HooverDuration, new UpgradableNumber(DefaultShortPowerupDuration));
            }
            
            //enables
            if (!_enablableProperties.ContainsKey(DoubleJumpEnabled))
            {
                _enablableProperties.Add(DoubleJumpEnabled, new EnablableBoolean(_doubleJumpEnabledByDefault, 0));
            }
            if (!_enablableProperties.ContainsKey(HeadStartEnabled))
            {
                _enablableProperties.Add(HeadStartEnabled, new EnablableBoolean(_headStartEnabledByDefault, 0));
            }

            //levels enabled
            if (!_enablableProperties.ContainsKey(GetLevelEnabledString(0)))
            {
                _enablableProperties.Add(GetLevelEnabledString(0), new EnablableBoolean(_firstLevelEnabledByDefault, 0));
            }
            if (!_enablableProperties.ContainsKey(GetLevelEnabledString(1)))
            {
                _enablableProperties.Add(GetLevelEnabledString(1), new EnablableBoolean(_secondLevelEnabledByDefault, 5000));
            }
        }

        public void Upgrade(int amount, string property)
        {
            _upgradableProperties[property].Upgrade(amount);
        }

        public float GetValue(string property)
        {
            return _upgradableProperties[property].GetValue();
        }

        public void ChangeAvailability(bool state, string property)
        {
             _enablableProperties[property].ChangeAvailability(state);   
        }

        public bool GetAvailability(string property)
        {
            return _enablableProperties[property].IsEnabled();
        }

        public int GetCostOfUpgrade(string property)
        {
            return _upgradableProperties[property].GetUpgradeCount() > PricesForUpgradeLevel.Length ? 0 : PricesForUpgradeLevel[_upgradableProperties[property].GetUpgradeCount()];
        }

        public int GetCostOfEnable(string property)
        {
            return _enablableProperties[property].GetPrice();
        }

        public static PlayerUpgradeData GetPlayerUpgradeData()
        {
            return new PlayerUpgradeData(_upgradableProperties, _enablableProperties);
        }

        public static void SetPlayerUpgradeData(PlayerUpgradeData pud)
        {
            if (pud.UpgradableProperties != null)
            {
                _upgradableProperties = pud.UpgradableProperties;
            }
            if (pud.EnablableProperties != null)
            {
                _enablableProperties = pud.EnablableProperties;
            }
            
            //check not null
            if (_upgradableProperties == null)
            {
                _upgradableProperties = new Dictionary<string, UpgradableNumber>();
            }
            if (_enablableProperties == null)
            {
                _enablableProperties = new Dictionary<string, EnablableBoolean>();
            }
        }
    }
}

[Serializable]
public class UpgradableNumber {
    private float _value;
    private int _upgradeCount = 0;

    public UpgradableNumber(float value)
    {
        this._value = value;
    }

    public void Upgrade(float amount)
    {
        _value += amount;
        _upgradeCount++;
    }

    public float GetValue()
    {
        return _value;
    }

    public int GetUpgradeCount()
    {
        return _upgradeCount;
    }
}

[Serializable]
public class EnablableBoolean
{
    private bool _enabled;
    private int _price;

    public EnablableBoolean(bool state, int price)
    {
        this._enabled = state;
        this._price = price;
    }

    public void ChangeAvailability(bool state)
    {
        this._enabled = state;
    }

    public bool IsEnabled()
    {
        return _enabled;
    }

    public int GetPrice()
    {
        return _price;
    }
}

public interface IUpgradable
{
    void Upgrade(int amount, string property);
    float GetValue(string property);
}

public interface IEnablable
{
    void ChangeAvailability(bool state, string property);
    bool GetAvailability(string property);
}

[Serializable]
public class PlayerUpgradeData
{
    public Dictionary<string, UpgradableNumber> UpgradableProperties;
    public Dictionary<string, EnablableBoolean> EnablableProperties;

    public PlayerUpgradeData(Dictionary<string, UpgradableNumber> UpgradableProperties, Dictionary<string, EnablableBoolean> EnablableProperties)
    {
        this.UpgradableProperties = UpgradableProperties;
        this.EnablableProperties = EnablableProperties;
    }
}