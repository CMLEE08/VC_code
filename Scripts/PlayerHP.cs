using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public List<GameObject> PlayerHps = new List<GameObject>();
    public int HpIndex = 2;
    public GameSystem gameSystem;
    public Result result;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Virus") || other.CompareTag("Backteria") || other.CompareTag("Fungus"))
        {
            if (HpIndex == 0){
                gameSystem.isOver = true;
                result.EndResult();
            }

            Destroy(other.gameObject);   
            GameObject CurrentHP = PlayerHps[HpIndex];   
            PlayerHps.RemoveAt(HpIndex);
            Destroy(CurrentHP);

            HpIndex = HpIndex - 1;
            

        }
    }
}
