using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent( typeof(BoundsCheck) )]
public class PowerUp : MonoBehaviour {
    [Header("Inscribed")]
    [Tooltip("x holds a min value and y a max vlaue for a Random.Range() call.")]
    public Vector2 rotMinMax = new Vector2(15,90);
    [Tooltip("x holds a min value and y a max value for a Random.Range() call.")]
    public Vector2 driftMinMax = new Vector2(.25f, 2);
    public float lifeTime = 10; 
    public float fadeTime = 4;

    [Header("Dynamic")]
    public eWeaponType _type; 
    public GameObject cube; 
    public TextMeshPro letter;
    public Vector3 rotPerSecond; 
    public float birthTime; 
    private Rigidbody rigid;
    private BoundsCheck bndCheck;
    private Material cubeMat;

    void Awake() {
        cube = transform.GetChild(0).gameObject;
        letter = GetComponent<TextMeshPro>();
        rigid = GetComponent<Rigidbody>();
        bndCheck = GetComponent<BoundsCheck>();
        cubeMat = cube.GetComponent<Renderer>().material;

        Vector3 vel = Random.onUnitSphere; 
        vel.z = 0; 
        vel.Normalize(); 

        vel *= Random.Range(driftMinMax.x, driftMinMax.y);
        rigid.velocity = vel;

        transform.rotation = Quaternion.identity;

        rotPerSecond = new Vector3( Random.Range( rotMinMax[0], rotMinMax[1] ),
                                    Random.Range( rotMinMax[0], rotMinMax[1] ),
                                    Random.Range( rotMinMax[0], rotMinMax[1] ) );
        
        birthTime = Time.time;
    }

    void Update() {
        cube.transform.rotation = Quaternion.Euler( rotPerSecond*Time.time );


        float u = (Time.time - (birthTime+lifeTime)) / fadeTime;
        if (u >= 1) {
            Destroy( this.gameObject );
            return;
        }
        if (u>0) {
            Color c = cubeMat.color;
            c.a = 1f - u;  
            cubeMat.color = c;
            c = letter.color;
            c.a = 1f - (u*0.5f); 
            letter.color = c;
        }

        if (!bndCheck.isOnScreen) {
            Destroy( gameObject );
        }
    }

    public eWeaponType type { get { return _type; } set { SetType(value); } }

    public void SetType( eWeaponType wt) {
        WeaponDefinition def = Main.GET_WEAPON_DEFINITION( wt );
        cubeMat.color = def.powerUpColor; 
        letter.text = def.letter;         
        _type = wt;                        
    }

    public void AbsorbedBy( GameObject target ) {
        Destroy( this.gameObject );
    }

}