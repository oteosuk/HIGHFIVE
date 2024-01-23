// 델리게이트 선언
using UnityEngine;

public delegate void MyDelegate(string message);

public class MyClass
{
    // 델리게이트 인스턴스 생성
    public MyDelegate myDelegate;

    // 델리게이트를 사용하는 메서드
    public void InvokeDelegate(string msg)
    {
        if (myDelegate != null)
        {
            myDelegate(msg);
        }
    }
}

public class DelegateTest : MonoBehaviour
{
    private void Start()
    {
        // MyClass 인스턴스 생성
        MyClass myClass = new();

        // 델리게이트에 메서드 할당
        myClass.myDelegate += Method1;
        myClass.myDelegate += Method2;
        myClass.myDelegate += Method2;

        // 메서드 호출
        myClass.InvokeDelegate("Hello, World!");
        myClass.InvokeDelegate("두번도되나?");

        myClass.myDelegate -= Method1;
        myClass.myDelegate -= Method2;

        myClass.InvokeDelegate("해제하고 나서는?");
    }

    // 델리게이트와 시그니처가 일치하는 메서드
    private void Method1(string message)
    {
        Debug.Log("Method1 호출 : " + message);
    }

    private void Method2(string message)
    {
        Debug.Log("Method2 호출 : " + message + "??");
    }
}
