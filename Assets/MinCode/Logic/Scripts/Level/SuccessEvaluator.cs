using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Events;

/// <summary>
/// Evaluates level money and stars
/// </summary>
public class SuccessEvaluator : MonoBehaviour
{
    public bool printDebugInfo;
    public FloatReference moneyPerStar;
    [Header("Maximum random money addition to level's money")]
    public IntegerReference moneyMaxExtra;
    public IntegerVariable levelStars;
    public IntegerVariable levelMoney;
    public IntegerVariable totalMoney;
    public UnityEvent onEvaluationDone;

    public void Evaluate()
    {
        var stars = 3;
        var money = 0;

        if (printDebugInfo)
        {
            print($"Evaluating stars");
        }

        // Evaluation logic here, delete this: "stars = 3;"

        stars = 3;
        levelStars.SetValue(Mathf.Clamp(stars, 0, 3));

        if (stars > 0)
        {
            if (printDebugInfo)
            {
                print($"levelStars = {levelStars}, calculating money. moneyPerStar: {moneyPerStar}");
            }

            money = Mathf.CeilToInt(levelStars * moneyPerStar) + Random.Range(0, moneyMaxExtra);
            if (printDebugInfo)
            {
                print($"money = {money}");
            }
        }

        levelMoney.SetValue(money);
        totalMoney.SetValue(totalMoney + money);
        onEvaluationDone.Invoke();
    }
}
