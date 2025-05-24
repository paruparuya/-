using Mono.Cecil;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHP = 15;
    public int currentHP { get; private set; }


   
    void Start()
    {
        if(PlayerPrefs.HasKey("PlayerHP"))
        {
            currentHP = PlayerPrefs.GetInt("PlayerHP");
            Debug.Log("HPを読み込みました" +  currentHP);
        }
        else
        {
            currentHP = maxHP;
        }
    }


    void Update()
    {
        Dead();

        if (Input.GetKeyDown(KeyCode.C))
        {
            currentHP -= 1;
            currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        }
        else  if(Input.GetKeyDown(KeyCode.Z))
        {
            currentHP += 1;
            currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        }

        if(Input.GetKeyDown(KeyCode.K))  //セーブ
        {
            PlayerPrefs.SetInt("PlayerHP", currentHP);
            PlayerPrefs.Save();
            Debug.Log("セーブしました" + currentHP);
        }
        if (Input.GetKeyDown(KeyCode.L))  //ロード
        {
            currentHP = PlayerPrefs.GetInt("PlayerHP", maxHP);
            Debug.Log("ロードしました" + (currentHP));
        }
        if (Input.GetKeyDown(KeyCode.J))  //データ削除
        {
            PlayerPrefs.DeleteKey("PlayerHP");
            Debug.Log("HPのセーブデータを削除しました");
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    private void Dead()
    {
        if(currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
