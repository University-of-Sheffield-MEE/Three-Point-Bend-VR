using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BendProfile
{
    public string name;
    public float bendConstant;
    public Animator model;
    public GameObject profileModel;
    public BendProfile(string _name, float _bendConstant)
    {
        name = _name;
        bendConstant = _bendConstant;
    }
}

public class ThreePointBendController : MonoBehaviour
{
    public Profile profile = new Profile();
    [Range(0, 700)]
    public float force = 0;
    public float deltaForce = 0; 
    public bool on = false;
    [SerializeField]
    private InputField forceDisplay;
    [SerializeField]
    private InputField deflectionDisplay;
    [SerializeField]
    private Transform rigLocation;
    [SerializeField]
    private List<BendProfile> profiles = new List<BendProfile>(){
        new BendProfile("Empty", 0.1f),
        new BendProfile("Square Load", 0.0007512857143f),
        new BendProfile("Square Tube", 0.0009918571429f),
        new BendProfile("Tall Wood", 0.001815857143f),
        new BendProfile("IBeam", 0.0007225714286f),
        new BendProfile("Flat Rectangle", 0.003783428571f),
        new BendProfile("Bar", 0.001018571429f),
        new BendProfile("Tall Rectangle", 0.001172857143f),
        new BendProfile("Tube", 0.001543428571f),
        new BendProfile("Steel Square", 0.0004834285714f),
        new BendProfile("Flat Wood", 0.002869857143f),
    };

    [SerializeField]
    private float maxDeflection = 2.7f;
    [SerializeField]
    private string animationName = "Bendybizzle";
    void Start()
    {

    }

    void Update()
    {
        IncreaseForce(deltaForce);
        
        BendProfile bendProfile = profiles[(int)profile];

        float bendConstant = bendProfile.bendConstant;
        float actualForce = on ? force : 0.0f;
        float deflection = bendConstant * actualForce;
        float animationProgress = Mathf.Clamp(deflection / maxDeflection, 0, 1) * 0.7f + 0.3f;

        Animator animator = bendProfile.model;
        if (animator != null)
        {
            animator.speed = 0;
            animator.Play(animationName, 0, animationProgress);
            animator.gameObject.SetActive(true);
            animator.gameObject.transform.position = rigLocation.position;
        }

        foreach (BendProfile p in profiles)
        {
            if (p.model != null && p.model != animator)
            {
                p.model.gameObject.SetActive(false);
            }
        }

        forceDisplay.text = String.Format("{0}", actualForce);
        deflectionDisplay.text = String.Format("{0:0.0000}", deflection);
    }

    public void SetForce(System.Single f)
    {
        force = f;
    }

    public void IncreaseForce(System.Single f)
    {
        force += f;
        if (force > 700)
        {
            force = 700;
        }
        if (force < 0)
        {
            force = 0;
        }
    }

    public void DecreaseForce(System.Single f)
    {
        IncreaseForce(-f);
    }

    public void SetDeltaForce(System.Single f)
    {
        deltaForce = f;
    }

    public void SetProfile(int p)
    {
        profile = (Profile)p;
    }

    public void SetProfile(GameObject g)
    {
        // Find the profile with that game object
        for (int i=0; i<profiles.Count; i++)
        {
            if (profiles[i].profileModel != null && profiles[i].profileModel.GetInstanceID() == g.GetInstanceID()) {
                profile = (Profile)i;
            }
        }
        // Set the profile number
    }
}

public enum Profile
{
    Empty = 0,
    SquareLoad = 1,
    SquareTube = 2,
    TallWood = 3,
    IBeam = 4,
    FlatRectangle = 5,
    Bar = 6,
    TallRectangle = 7,
    Tube = 8,
    SteelSquare = 9,
    FlatWood = 10,
}
