using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class PrimaryButtonEvent : UnityEvent<bool> { }

public class GameController : MonoBehaviour
{
    //Levels
    public GameObject menuLevel;
    public GameObject activeLevel;
    //
    private GameObject levelToLoad;

    public PlayerController playerController;


    public GameObject virtualBulletPrefab;
    public GameObject realBulletPrefab;
    public BulletController virtualBulletController;
    public RealBulletController realBulletController;
    public Vector3 originPos;
    public GameObject pistolBulletOrigin;
    public GameObject bulletTrail;
    public float bulletDistance;//refers to virtual bullet //from 0 to 1 where 1 is fulldistance ahead and 0 is the end of the bullets available distance
    private float realBulletDistance;
    public float maxBulletDistance = 10.0f;


    public bool canShootMode = false;
    public bool canFireVirtBullet = true;
    public bool canFireRealBullet = false;

    public PrimaryButtonEvent primaryButtonPress;

    private bool lastButtonState = false;
    private List<InputDevice> devicesWithPrimaryButton;

    public GameObject rightController;
    public GameObject dotsContainer;

    public AudioSource audioSource;
    public AudioClip winClip;
    public AudioClip missClip;
    public AudioClip nobulletsClip;

    public GameObject environmentHolder;


    public PistolController pistolController;
    public int bulletsAmount;
    public int maxBulletsAmount;


    public float waitTime;
    private float wTime;
    private bool wait = false;
    private bool waitForMenuLevel = false;
    private bool waitForLevel = false;

    public bool isTriggerPressed = false;
    

    private void Awake()
    {
        if (primaryButtonPress == null)
        {
            primaryButtonPress = new PrimaryButtonEvent();
        }

        devicesWithPrimaryButton = new List<InputDevice>();

       // pistolController = GameObject.FindGameObjectWithTag("Pistol").GetComponent<PistolController>();
    }

    void OnEnable()
    {
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);
        foreach (InputDevice device in allDevices)
            InputDevices_deviceConnected(device);

        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected -= InputDevices_deviceConnected;
        InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;
        devicesWithPrimaryButton.Clear();
    }

    private void InputDevices_deviceConnected(InputDevice device)
    {
        bool discardedValue;
        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out discardedValue))
        {
            devicesWithPrimaryButton.Add(device); // Add any devices that have a primary button.
        }
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if (devicesWithPrimaryButton.Contains(device))
            devicesWithPrimaryButton.Remove(device);
    }

    // Update is called once per frame
    void Update()
    {
        if(wait)
        {
            wTime -= Time.deltaTime;
            if(wTime < 0)
            {
                wait = false;
                if (waitForMenuLevel)
                {
                    LoadLevel2();
                    waitForMenuLevel = false;
                }
                else if (waitForLevel)
                {
                    LoadMenuLevel();
                    waitForLevel = false;
                }
            }
        }
        if(canShootMode)
        {
            bool tempState = false;
            foreach (var device in devicesWithPrimaryButton)
            {
                //Debug.Log(device.name);
                if(device.name == "Oculus Touch Controller - Right")
                {
                    bool primaryButtonState = false;
                    tempState = device.TryGetFeatureValue(CommonUsages.triggerButton, out primaryButtonState) // did get a value
                                && primaryButtonState // the value we got
                                || tempState; // cumulative result from other controllers
                }
            }
            if (tempState != lastButtonState) // Button state changed since last frame
            {
                Debug.Log("changed button state");
                isTriggerPressed = !isTriggerPressed;
                primaryButtonPress.Invoke(tempState);
                lastButtonState = tempState;
                if (canFireRealBullet && !isTriggerPressed && bulletsAmount>0)
                {
                    virtualBulletController.EndTracking();
                    FireRealBullet();
                    canFireRealBullet = false;
                }
                if (canFireVirtBullet && isTriggerPressed && bulletsAmount > 0)
                {
                    PulledTrigger();
                    canFireVirtBullet = false;
                    canFireRealBullet = true;
                }
            }
        }
        if(virtualBulletController != null)
        {
            realBulletDistance = virtualBulletController.distance;
            bulletDistance = realBulletDistance / maxBulletDistance;
        }
    }

    public void GetTrackToRealBullet()
    {
        realBulletController.bulletPositions = virtualBulletController.bulletPositions;
        realBulletController.bulletRotations = virtualBulletController.bulletRotations;
    }

    public void PulledTrigger()
    {
        GameObject virtualBullet = Instantiate(virtualBulletPrefab, pistolBulletOrigin.transform.position, pistolBulletOrigin.transform.rotation);
        virtualBulletController = virtualBullet.GetComponent<BulletController>();
        virtualBulletController.distance = maxBulletDistance;
       // Debug.Log("Trigger pulled!!!");
    }

    private void FireRealBullet()
    {
        GameObject realBullet = Instantiate(realBulletPrefab, pistolBulletOrigin.transform.position, pistolBulletOrigin.transform.rotation);
        realBulletController = realBullet.GetComponent<RealBulletController>();
        GetTrackToRealBullet();
        bulletsAmount--;
        pistolController.PistolShot();
        realBulletController.startReplay = true;
    }

    public void VirtualBulletStopped()
    {
        canFireRealBullet = true;
    }
    public void clearDots()
    {
        foreach (Transform child in dotsContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void targetHit()
    {
        audioSource.clip = winClip;
        audioSource.Play();
        //tutaj odpalic transition sceny
        activeLevel.GetComponent<LevelController>().StartDisappearing();
        wait = true;
        wTime = waitTime;
        waitForLevel = true;
    }
    public void outOfBullets()
    {
        audioSource.clip = nobulletsClip;
        audioSource.Play();
        //tutaj odpalic transition sceny
        activeLevel.GetComponent<LevelController>().StartDisappearing();
        wait = true;
        wTime = waitTime;
        waitForLevel = true;
    }

    public void obstacleHit()
    {
        audioSource.clip = missClip;
        audioSource.Play();
    }

    public void LoadLevel(GameObject level)// this and loadmenulevel is to be changed
    {
        levelToLoad = level;
        wait = true;
        waitForMenuLevel = true;
        wTime = waitTime;
        menuLevel.GetComponent<LevelController>().StartDisappearing();
    }
    public void LoadLevel2()
    {
        ClearBulletTrail();
        bulletsAmount = maxBulletsAmount;
        pistolController.ResetPistol();
        canShootMode = true;
        menuLevel.SetActive(false);
        playerController.ControllersToPistol();
        activeLevel = Instantiate(levelToLoad);
        activeLevel.transform.parent = environmentHolder.transform;
    }

    public void LoadMenuLevel()
    {
        ClearBulletTrail();
        playerController.ControllersToHands();
        canShootMode = false;
        Destroy(activeLevel);
        menuLevel.SetActive(true);
        menuLevel.GetComponent<MenuLevelController>().SetupMenuLevel();
        menuLevel.GetComponent<LevelController>().StartShowing();
    }
    public void ClearBulletTrail()
    {
        //Debug.Log("czyszczenie toru");
        foreach (Transform child in bulletTrail.transform) {
            Destroy(child.gameObject);
        }
    }

    public void BulletEnd()// is invoked when real bullet ended either by hitting something or by running to the end of the trail
    {
        if(bulletsAmount <= 0)
        {
            Debug.Log("No bullets no fun");
            outOfBullets();
        }
    }
}
