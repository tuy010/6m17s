using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePasswordManager : MonoBehaviour
{
    public enum State
    {
        NotWorking,
        ChangePassword_723,
        ChangePassword_724
    }
    public State nowState = State.NotWorking;
    public int password723 = 1217;
    public int password724 = 1113; //비밀번호 때려맞추기 방지, 비밀번호 변경시 0000으로 초기화
    [SerializeField] GameObject clock723;
    [SerializeField] GameObject clock724;
    [SerializeField] Material[] matsRedNum;
    [SerializeField] Material[] matsWhiteNum;
    private int password723_old;
    private int password724_old;
    public bool is723Changed = false;
    public bool is724Changed = false;
    public bool is724Open = false;
    private bool isColorChanged = false;

    void Start()
    {
        nowState = State.NotWorking;
        password723 = 1217;
        password724 = 1113;
        password723_old = password723;
        password724_old = password724;
    }

    // Update is called once per frame
    void Update()
    {
        if(nowState == State.ChangePassword_723&& !isColorChanged)
        {
            clock723.GetComponent<MeshRenderer>().materials = matsRedNum;
            isColorChanged = true;
        }
        else if(nowState == State.ChangePassword_724&&!isColorChanged)
        {
            clock724.GetComponent<MeshRenderer>().materials = matsRedNum;
            isColorChanged = true;
        }

        if(nowState == State.NotWorking && isColorChanged)
        {
            clock723.GetComponent<MeshRenderer>().materials = matsWhiteNum;
            clock724.GetComponent<MeshRenderer>().materials = matsWhiteNum;
            isColorChanged=false;
        }

        if(nowState == State.ChangePassword_723 && (password723 != password723_old) && !is723Changed)
        {
            is723Changed = true;
        }
        else if(nowState == State.ChangePassword_724 &&(password724 != password724_old) && !is724Changed)
        {
            is724Changed = true;
        }
        else if(nowState == State.NotWorking && (is723Changed||is724Changed))
        {
            is723Changed = false;
            is724Changed = false;  
            password723_old = password723;
            password724_old = password724;
            isColorChanged = false;
        }
    }
}
