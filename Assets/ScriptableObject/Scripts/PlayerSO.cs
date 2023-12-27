using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("Player"), menuName = ("Characters/Player"))]
public class PlayerSO : ScriptableObject
{
    [field :SerializeField] public PlayerGroundSO GroundSO {  get; private set; }
    [field :SerializeField] public PlayerAirSO AirSO { get; private set; }
}
