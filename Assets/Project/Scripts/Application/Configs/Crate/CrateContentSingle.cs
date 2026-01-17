using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CrateContentSingle", menuName = "Configs/Crate/CrateContentSingle")]
public class CrateContentSingle : CompositeCrateContent
{
    [SerializeField] float chance = 1;
    [SerializeField] GameObject item;
    public override float Chance => chance;

    public override GameObject GetItem() => item;


}
