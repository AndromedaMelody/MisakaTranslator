﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Win32;
using Windows.Win32.Foundation;

namespace KeyboardMouseHookLibrary
{
    public class FindWindowInfo
    {
        [DllImport("user32.dll", EntryPoint = "GetWindowText")]
        public static extern int GetWindowText(
            IntPtr hWnd,
            StringBuilder lpString,
            int nMaxCount
        );

        [DllImport("user32.dll", EntryPoint = "GetClassName")]
        public static extern int GetClassName(
            IntPtr hWnd,
            StringBuilder lpString,
            int nMaxCont
        );

        /// <summary>
        /// 获取指定坐标处窗口的句柄
        /// </summary>
        public static IntPtr GetWindowHWND(Point point)
        {
            return PInvoke.WindowFromPoint(point);
        }

        /// <summary>
        /// 根据HWND获得窗口标题
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static string GetWindowName(IntPtr hwnd)
        {
            StringBuilder name = new StringBuilder(256);
            GetWindowText(hwnd, name, 256);
            return name.ToString();
        }

        /// <summary>
        /// 根据HWND获得类名
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static string GetWindowClassName(IntPtr hwnd)
        {
            StringBuilder name = new StringBuilder(256);
            GetClassName(hwnd, name, 256);
            return name.ToString();
        }

        /// <summary>
        /// 根据HWND获得进程ID
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static unsafe uint GetProcessIDByHWND(IntPtr hWnd)
        {
            uint result;
            PInvoke.GetWindowThreadProcessId((HWND)hWnd, &result);
            return result;
        }
    }
}
