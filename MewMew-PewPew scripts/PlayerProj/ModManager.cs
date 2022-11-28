using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModManager : MonoBehaviour
{
    [field: SerializeField] public bool EnableExplosivMod { get; private set; }
    [field: SerializeField] public bool EnablePenetrateMod { get; private set; }
    [field: SerializeField] public bool EnableSplitMod { get; private set; }

    public void EnableExplosiveMod()
    {
        EnableExplosivMod = true;
    }

    public void EnablePenetratingMod()
    {
        EnablePenetrateMod = true;
    }

    public void EnableSplittingMod()
    {
        EnableSplitMod = true;
    }

    public void DisableExplosiveMod()
    {
        EnableExplosivMod = false;
    }

    public void DisablePenetratingMod()
    {
        EnablePenetrateMod = false;
    }

    public void DisableSplittingMod()
    {
        EnableSplitMod = false;
    }
}
