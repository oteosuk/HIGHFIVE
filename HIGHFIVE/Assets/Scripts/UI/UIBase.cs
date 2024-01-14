using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIBase : MonoBehaviourPunCallbacks
{

    protected Dictionary<Type, UnityEngine.Object[]> _objects = new();

    //@@@@@@@@@@@@@@@@@@@@ Bind!!!!!!!!!!!
    protected void Bind<T>(Type type, bool recursive = false) where T : UnityEngine.Object //recursive 재귀적으로 찾을거냐 
    {
        //이넘 이름 가져오기
        string[] names = Enum.GetNames(type);
        
        //이넘 길이만큼 생성
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];

        //하이알키 상에서의 우리가 넘겨준 Enum 네임과 일치하는 오브젝트를 찾기 (현재 gameobject로 찾아주는 거기 때문에 Find보다 성능 up)
        for (int i = 0; i < names.Length; i++)
        {
            objects[i] = typeof(T) == typeof(GameObject) ? Util.FindChild(gameObject, names[i], recursive) : Util.FindChild<T>(gameObject, names[i], recursive);

            if (objects[i] == null)
                Debug.Log($"Failed to bind({names[i]})");
        }

        //_objects(딕셔너리) 자료형에 넣어줌
        //key: TMP_Text value: warriorName안에 있는 TMP_Text컴포넌트[]
        _objects.Add(typeof(T), objects);
    }

    //Bind에서 _objects(딕셔너리)에 넣어준 자료를 가져오는 작업
    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        if (!_objects.TryGetValue(typeof(T), out UnityEngine.Object[] objs)) return null;
        return objs[index] as T;
    }

    protected void AddUIEvent(GameObject go, Define.UIEvent uIEvent, Action<PointerEventData> action = null)
    {
        UIEventHandler uiEventHandler = Util.GetOrAddComponent<UIEventHandler>(go);


        switch (uIEvent)
        {
            case Define.UIEvent.Click:
                uiEventHandler.ClickAction -= action;
                uiEventHandler.ClickAction += action;
                break;
        }
    }
}
