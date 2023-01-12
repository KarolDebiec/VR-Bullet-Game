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

    public GameObject environmentHolder;


    public float waitTime;
    private float wTime;
    private bool wait = false;
    private bool waitForMenuLevel = false;
    private bool waitForLevel = false;
    private void Awake()
    {
        if (primaryButtonPress == null)
        {
            primaryButtonPress = new PrimaryButtonEvent();
        }

        devicesWithPrimaryButton = new List<InputDevice>();
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
                bool primaryButtonState = false;
                tempState = device.TryGetFeatureValue(CommonUsages.triggerButton, out primaryButtonState) // did get a value
                            && primaryButtonState // the value we got
                            || tempState; // cumulative result from other controllers
            }
            if (tempState != lastButtonState) // Button state changed since last frame
            {
                primaryButtonPress.Invoke(tempState);
                lastButtonState = tempState;
                if (canFireVirtBullet)
                {
                    PulledTrigger();
                    canFireVirtBullet = false;
                }
                if (canFireRealBullet)
                {
                    FireRealBullet();
                    canFireRealBullet = false;
                }
            }
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
       // Debug.Log("Trigger pulled!!!");
    }

    private void FireRealBullet()
    {
        GameObject realBullet = Instantiate(realBulletPrefab, pistolBulletOrigin.transform.position, pistolBulletOrigin.transform.rotation);
        realBulletController = realBullet.GetComponent<RealBulletController>();
        GetTrackToRealBullet();
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
            GameObject.Destroy(child.gameObject);
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
        Debug.Log("czyszczenie toru");
        foreach (Transform child in bulletTrail.transform) {
            Destroy(child.gameObject);
        }
    }
}
