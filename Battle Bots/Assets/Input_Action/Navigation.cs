//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Input_Action/Navigation.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Navigation: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Navigation()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Navigation"",
    ""maps"": [
        {
            ""name"": ""Navigation1"",
            ""id"": ""cc3b6a86-424f-4e40-874e-8fe28555d9f9"",
            ""actions"": [
                {
                    ""name"": ""OnNavigate"",
                    ""type"": ""Value"",
                    ""id"": ""64e98589-aba8-4fd4-9abe-27c562344307"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""OnSubmit"",
                    ""type"": ""Button"",
                    ""id"": ""93b0ec9c-2d7e-4dc8-bd4e-7114d53bdd69"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b860f6b9-f5d6-4ce4-875b-042f88214b05"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OnNavigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1ba80b6-0fc9-46a1-afa4-2392cae7a864"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OnSubmit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Navigation1
        m_Navigation1 = asset.FindActionMap("Navigation1", throwIfNotFound: true);
        m_Navigation1_OnNavigate = m_Navigation1.FindAction("OnNavigate", throwIfNotFound: true);
        m_Navigation1_OnSubmit = m_Navigation1.FindAction("OnSubmit", throwIfNotFound: true);
    }

    ~@Navigation()
    {
        UnityEngine.Debug.Assert(!m_Navigation1.enabled, "This will cause a leak and performance issues, Navigation.Navigation1.Disable() has not been called.");
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Navigation1
    private readonly InputActionMap m_Navigation1;
    private List<INavigation1Actions> m_Navigation1ActionsCallbackInterfaces = new List<INavigation1Actions>();
    private readonly InputAction m_Navigation1_OnNavigate;
    private readonly InputAction m_Navigation1_OnSubmit;
    public struct Navigation1Actions
    {
        private @Navigation m_Wrapper;
        public Navigation1Actions(@Navigation wrapper) { m_Wrapper = wrapper; }
        public InputAction @OnNavigate => m_Wrapper.m_Navigation1_OnNavigate;
        public InputAction @OnSubmit => m_Wrapper.m_Navigation1_OnSubmit;
        public InputActionMap Get() { return m_Wrapper.m_Navigation1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Navigation1Actions set) { return set.Get(); }
        public void AddCallbacks(INavigation1Actions instance)
        {
            if (instance == null || m_Wrapper.m_Navigation1ActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Navigation1ActionsCallbackInterfaces.Add(instance);
            @OnNavigate.started += instance.OnOnNavigate;
            @OnNavigate.performed += instance.OnOnNavigate;
            @OnNavigate.canceled += instance.OnOnNavigate;
            @OnSubmit.started += instance.OnOnSubmit;
            @OnSubmit.performed += instance.OnOnSubmit;
            @OnSubmit.canceled += instance.OnOnSubmit;
        }

        private void UnregisterCallbacks(INavigation1Actions instance)
        {
            @OnNavigate.started -= instance.OnOnNavigate;
            @OnNavigate.performed -= instance.OnOnNavigate;
            @OnNavigate.canceled -= instance.OnOnNavigate;
            @OnSubmit.started -= instance.OnOnSubmit;
            @OnSubmit.performed -= instance.OnOnSubmit;
            @OnSubmit.canceled -= instance.OnOnSubmit;
        }

        public void RemoveCallbacks(INavigation1Actions instance)
        {
            if (m_Wrapper.m_Navigation1ActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(INavigation1Actions instance)
        {
            foreach (var item in m_Wrapper.m_Navigation1ActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Navigation1ActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Navigation1Actions @Navigation1 => new Navigation1Actions(this);
    public interface INavigation1Actions
    {
        void OnOnNavigate(InputAction.CallbackContext context);
        void OnOnSubmit(InputAction.CallbackContext context);
    }
}