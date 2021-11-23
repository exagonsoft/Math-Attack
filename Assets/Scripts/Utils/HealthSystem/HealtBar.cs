using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtBar : MonoBehaviour
{
    #region Properties
    private Transform bar;
    private HealtSystem healthSystem;
    #endregion

    #region Events


    #endregion

    #region Functions
    public void SetSize(float sizeNormaliced)
    {
    }

    public void setColor(Color newColor)
    {
        bar.Find("Healt").GetComponent<SpriteRenderer>().color = newColor;
    }

    public void SetUpBar(HealtSystem healtSystem)
    {
        this.healthSystem = healtSystem;
        bar = transform.Find("Bar");
        bar.Find("Healt").GetComponent<SpriteRenderer>().color = Color.red;
        healthSystem.onHealthChanged += HealthSystem_onHealthChanged;
    }

    private void HealthSystem_onHealthChanged(object sender, System.EventArgs e)
    {
        float healtPercent = healthSystem.GetHealthPercent();
        transform.Find("Bar").localScale = new Vector3(healtPercent, 1);
        
    }

    #endregion
}
