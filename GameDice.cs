using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the <see cref="GameDie" />
/// </summary>
public class GameDie : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// Defines the rigid
    /// </summary>
    public Rigidbody rigid;

    /// <summary>
    /// Defines the rollDirection
    /// </summary>
    public Vector3 rollDirection;

    /// <summary>
    /// Defines the rollSpeed
    /// </summary>
    public float rollSpeed;

    /// <summary>
    /// Defines the maxRollSpeed
    /// </summary>
    public float maxRollSpeed = 30;

    /// <summary>
    /// Defines the randomlyChangeDirectionEvery
    /// </summary>
    public float randomlyChangeDirectionEvery = 0f;

    /// <summary>
    /// Defines the facings
    /// </summary>
    public List<FacingData> facings = new List<FacingData>();

    /// <summary>
    /// Defines the trueUp
    /// </summary>
    public Vector3 trueUp;

    /// <summary>
    /// Defines the state
    /// </summary>
    public DieState state;

    /// <summary>
    /// Defines the renderer
    /// </summary>
    public new Renderer renderer;

    /// <summary>
    /// Defines the dieValue
    /// </summary>
    public int dieValue;


    /// <summary>
    /// Defines the lastChange
    /// </summary>
    private float lastChange = 0f;

    #endregion

    

    #region Enums

    /// <summary>
    /// Defines the DieState
    /// </summary>
    public enum DieState { /// <summary>
        /// Defines the Idle
        /// </summary>
        Idle, 
        /// <summary>
        /// Defines the Rolling
        /// </summary>
        Rolling, 
        /// <summary>
        /// Defines the Thrown
        /// </summary>
        Thrown, 
        /// <summary>
        /// Defines the Stopped
        /// </summary>
        Stopped}

    #endregion

    #region Methods

    /// <summary>
    /// The ChangeValue
    /// </summary>
    /// <param name="val">The val<see cref="int"/></param>
    public void ChangeValue (int val)
    {
		for (int i = 0; i < facings.Count;i++)
		{
			if (facings[i].faceValue == val)
			{
				transform.up = new Vector3(-facings [i].direction.x, facings[i].direction.y, -facings[i].direction.z);
				return;
			}
		}
    }

    /// <summary>
    /// The ChangeValue
    /// </summary>
    /// <param name="val">The val<see cref="string"/></param>
    public void ChangeValue (string val)
    {
		int i = 0;
		if(int.TryParse(val, out i))
		{
			ChangeValue(i);
		}
    }

    /// <summary>
    /// The RandomizeRollDirection
    /// </summary>
    public void RandomizeRollDirection ()
    {
		for (int i = 0; i < 3; i ++)
		{
			rollDirection [i] = UnityEngine.Random.Range(0.5f, 1f);
			int x = UnityEngine.Random.Range(0, 2);
			if (x ==1)
			{
				rollDirection [i] *= -1f;
			}

		}
    }

    /// <summary>
    /// The RandomValue
    /// </summary>
    public void RandomValue ()
    {
		int index = UnityEngine.Random.Range(0, facings.Count);

		ChangeValue(facings [index].faceValue);
    }

    // Update is called once per frame
    /// <summary>
    /// The FixedUpdate
    /// </summary>
    private void FixedUpdate()
    {
		rigid.maxAngularVelocity = maxRollSpeed;
		if (state == DieState.Rolling)
		{
			if (randomlyChangeDirectionEvery != 0f && lastChange + randomlyChangeDirectionEvery <= Time.time)
			{
				RandomizeRollDirection();
			}
			rigid.angularVelocity = rollDirection * rollSpeed;
		}
		else if (state == DieState.Thrown)
		{
			rigid.useGravity = true;
			if (rigid.IsSleeping())
			{
				state = DieState.Stopped;
			}
		}
    }

    /// <summary>
    /// The OnDrawGizmos
    /// </summary>
    private void OnDrawGizmos()
    {
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + trueUp * 10f);
    }



    /// <summary>
    /// The OnValidate
    /// </summary>
    private void OnValidate()
    {
		float max = -1f;
		int match = 0;
		for (int i = 0; i < facings.Count; i++)
		{
			float dotValue = Vector3.Dot(transform.rotation * facings [i].direction, Vector3.up);
			//Debug.Log(dotValue);
			if (max < dotValue)
			{
				max = dotValue;
				//upSide = i + 1;
				match = facings [i].faceValue;
				//break;
			}
		}
		dieValue = match;
    }

    /// <summary>
    /// The Start
    /// </summary>
    private void Start()
    {
		renderer = GetComponent<Renderer>();
		RandomizeRollDirection();
    }

    /// <summary>
    /// The Update
    /// </summary>
    private void Update()
    {
		//trueUp = transform.rotation * transform.up;
			//Vector3.Lerp(  transform.InverseTransformDirection(Vector3.up),  transform.TransformDirection(Vector3.up), 0.5f).normalized;
			//transform.rotation * transform.up;
		float max = -1f;
		int match = 0;
		for (int i = 0; i < facings.Count; i++)
		{
			float dotValue = Vector3.Dot(transform.rotation * facings [i].direction, Vector3.up);
			//Debug.Log(dotValue);
			if (max < dotValue)
			{
				max = dotValue;
				//upSide = i + 1;
				match = facings[i].faceValue;
				//break;
			}
		}
		dieValue = match;
    }

    #endregion

    /// <summary>
    /// Defines the <see cref="FacingData" />
    /// </summary>
    [Serializable]
	public struct FacingData
    {
        #region Fields

        /// <summary>
        /// Defines the faceValue
        /// </summary>
        public int faceValue;

        /// <summary>
        /// Defines the direction
        /// </summary>
        public Vector3 direction;

        #endregion
    }

	public static int operator +(GameDie b, GameDie c)
	{
		return b.dieValue + c.dieValue;
	}
	public static int operator +(GameDie b, int i)
	{
		return b.dieValue + i;
	}
	public static int operator +(int i, GameDie b)
	{
		return i + b.dieValue;
	}
}

