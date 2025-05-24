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
            Debug.Log("HP��ǂݍ��݂܂���" +  currentHP);
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

        if(Input.GetKeyDown(KeyCode.K))  //�Z�[�u
        {
            PlayerPrefs.SetInt("PlayerHP", currentHP);
            PlayerPrefs.Save();
            Debug.Log("�Z�[�u���܂���" + currentHP);
        }
        if (Input.GetKeyDown(KeyCode.L))  //���[�h
        {
            currentHP = PlayerPrefs.GetInt("PlayerHP", maxHP);
            Debug.Log("���[�h���܂���" + (currentHP));
        }
        if (Input.GetKeyDown(KeyCode.J))  //�f�[�^�폜
        {
            PlayerPrefs.DeleteKey("PlayerHP");
            Debug.Log("HP�̃Z�[�u�f�[�^���폜���܂���");
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
