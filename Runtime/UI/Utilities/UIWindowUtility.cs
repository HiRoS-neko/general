using System.Linq;
using UnityEngine;

namespace Devdog.General.UI
{
    public static class UIWindowUtility
    {
        public static UIWindow FindByName(string name)
        {
            return FindByName<UIWindow>(name);
        }

        public static T FindByName<T>(string name) where T : UIWindow
        {
            return Object.FindObjectsOfType<T>().FirstOrDefault(o => o.windowName == name);
        }

        public static UIWindow[] GetAllWindowsWithInput()
        {
            return GetAllWindowsWithInput<UIWindow>();
        }

        public static T[] GetAllWindowsWithInput<T>() where T : UIWindow
        {
            return Object.FindObjectsOfType<T>().Where(o => o.GetComponent<IUIWindowInputHandler>() != null).ToArray();
        }
    }
}