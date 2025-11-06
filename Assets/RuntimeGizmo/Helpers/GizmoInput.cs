using System;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
#endif

namespace RuntimeGizmos
{
        static class GizmoInput
        {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
                static KeyControl GetKeyControl(Keyboard keyboard, KeyCode keyCode)
                {
                        if(keyboard == null || keyCode == KeyCode.None) return null;

                        Key? key = ConvertKeyCode(keyCode);
                        return key.HasValue ? keyboard[key.Value] : null;
                }

                static Key? ConvertKeyCode(KeyCode keyCode)
                {
                        if(Enum.TryParse(keyCode.ToString(), true, out Key parsedKey))
                        {
                                return parsedKey;
                        }

                        switch(keyCode)
                        {
                                case KeyCode.LeftControl: return Key.LeftCtrl;
                                case KeyCode.RightControl: return Key.RightCtrl;
                                case KeyCode.Return: return Key.Enter;
                                case KeyCode.KeypadEnter: return Key.NumpadEnter;
                                case KeyCode.Numlock: return Key.NumLock;
                        }

                        return null;
                }

                static ButtonControl GetMouseButtonControl(Mouse mouse, int button)
                {
                        if(mouse == null) return null;

                        switch(button)
                        {
                                case 0: return mouse.leftButton;
                                case 1: return mouse.rightButton;
                                case 2: return mouse.middleButton;
                                default: return null;
                        }
                }
#endif

                public static bool GetKey(KeyCode keyCode)
                {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
                        var control = GetKeyControl(Keyboard.current, keyCode);
                        return control != null && control.isPressed;
#else
                        return Input.GetKey(keyCode);
#endif
                }

                public static bool GetKeyDown(KeyCode keyCode)
                {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
                        var control = GetKeyControl(Keyboard.current, keyCode);
                        return control != null && control.wasPressedThisFrame;
#else
                        return Input.GetKeyDown(keyCode);
#endif
                }

                public static bool GetKeyUp(KeyCode keyCode)
                {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
                        var control = GetKeyControl(Keyboard.current, keyCode);
                        return control != null && control.wasReleasedThisFrame;
#else
                        return Input.GetKeyUp(keyCode);
#endif
                }

                public static bool GetMouseButton(int button)
                {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
                        var control = GetMouseButtonControl(Mouse.current, button);
                        return control != null && control.isPressed;
#else
                        return Input.GetMouseButton(button);
#endif
                }

                public static bool GetMouseButtonDown(int button)
                {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
                        var control = GetMouseButtonControl(Mouse.current, button);
                        return control != null && control.wasPressedThisFrame;
#else
                        return Input.GetMouseButtonDown(button);
#endif
                }

                public static bool GetMouseButtonUp(int button)
                {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
                        var control = GetMouseButtonControl(Mouse.current, button);
                        return control != null && control.wasReleasedThisFrame;
#else
                        return Input.GetMouseButtonUp(button);
#endif
                }

                public static Vector3 MousePosition
                {
                        get
                        {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
                                var mouse = Mouse.current;
                                if(mouse == null) return Vector3.zero;

                                Vector2 position = mouse.position.ReadValue();
                                return new Vector3(position.x, position.y, 0f);
#else
                                return Input.mousePosition;
#endif
                        }
                }

                public static float GetMouseAxis(string axisName)
                {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
                        var mouse = Mouse.current;
                        if(mouse == null) return 0f;

                        Vector2 delta = mouse.delta.ReadValue();

                        if(string.Equals(axisName, "Mouse X", StringComparison.OrdinalIgnoreCase)) return delta.x;
                        if(string.Equals(axisName, "Mouse Y", StringComparison.OrdinalIgnoreCase)) return delta.y;

                        return 0f;
#else
                        return Input.GetAxis(axisName);
#endif
                }
        }
}
