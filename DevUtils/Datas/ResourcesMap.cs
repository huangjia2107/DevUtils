﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevUtils.Models;
using UtilModelService;

namespace DevUtils.Datas
{
    public static class ResourcesMap
    {
        public static Dictionary<UtilType, string> UtilTypeDictionary = new Dictionary<UtilType, string>
        {
            {UtilType.Color,         "颜色处理"},
            {UtilType.File,          "文件访问"},
            {UtilType.Debug,         "程序调试"},
            {UtilType.Test,          "代码测试"},
            {UtilType.Misc,          "其他工具"}
        };

        public static Dictionary<UtilType, UtilDisplay> UtilTypeToDisplayDictionary = new Dictionary<UtilType, UtilDisplay>
        {
            {UtilType.Color,         new UtilDisplay{Name="颜色处理",Data="M17.5,12A1.5,1.5 0 0,1 16,10.5A1.5,1.5 0 0,1 17.5,9A1.5,1.5 0 0,1 19,10.5A1.5,1.5 0 0,1 17.5,12M14.5,8A1.5,1.5 0 0,1 13,6.5A1.5,1.5 0 0,1 14.5,5A1.5,1.5 0 0,1 16,6.5A1.5,1.5 0 0,1 14.5,8M9.5,8A1.5,1.5 0 0,1 8,6.5A1.5,1.5 0 0,1 9.5,5A1.5,1.5 0 0,1 11,6.5A1.5,1.5 0 0,1 9.5,8M6.5,12A1.5,1.5 0 0,1 5,10.5A1.5,1.5 0 0,1 6.5,9A1.5,1.5 0 0,1 8,10.5A1.5,1.5 0 0,1 6.5,12M12,3A9,9 0 0,0 3,12A9,9 0 0,0 12,21A1.5,1.5 0 0,0 13.5,19.5C13.5,19.11 13.35,18.76 13.11,18.5C12.88,18.23 12.73,17.88 12.73,17.5A1.5,1.5 0 0,1 14.23,16H16A5,5 0 0,0 21,11C21,6.58 16.97,3 12,3Z"}},
            {UtilType.File,          new UtilDisplay{Name="文件访问",Data="M6,2A2,2 0 0,0 4,4V20A2,2 0 0,0 6,22H18A2,2 0 0,0 20,20V8L14,2H6M6,4H13V9H18V20H6V4M8,12V14H16V12H8M8,16V18H13V16H8Z"}},
            {UtilType.Debug,         new UtilDisplay{Name="程序调试",Data="M21,16 L21,4 3,4 3,16 21,16 M21,2 A2,2,0,0,1,23,4 L23,16 A2,2,0,0,1,21,18 L3,18 C1.89,18 1,17.1 1,16 L1,4 C1,2.89 1.89,2 3,2 L21,2 M5,6 L14,6 14,11 5,11 5,6 M15,6 L19,6 19,8 15,8 15,6 M19,9 L19,14 15,14 15,9 19,9 M5,12 L9,12 9,14 5,14 5,12 M10,12 L14,12 14,14 10,14 10,12 z"}},
            {UtilType.Test,          new UtilDisplay{Name="代码测试",Data="M5,19A1,1 0 0,0 6,20H18A1,1 0 0,0 19,19C19,18.79 18.93,18.59 18.82,18.43L13,8.35V4H11V8.35L5.18,18.43C5.07,18.59 5,18.79 5,19M6,22A3,3 0 0,1 3,19C3,18.4 3.18,17.84 3.5,17.37L9,7.81V6A1,1 0 0,1 8,5V4A2,2 0 0,1 10,2H14A2,2 0 0,1 16,4V5A1,1 0 0,1 15,6V7.81L20.5,17.37C20.82,17.84 21,18.4 21,19A3,3 0 0,1 18,22H6M13,16L14.34,14.66L16.27,18H7.73L10.39,13.39L13,16M12.5,12A0.5,0.5 0 0,1 13,12.5A0.5,0.5 0 0,1 12.5,13A0.5,0.5 0 0,1 12,12.5A0.5,0.5 0 0,1 12.5,12Z"}},
            {UtilType.Misc,          new UtilDisplay{Name="其他工具",Data="M13,17L19,17 19,19 13,19z M21,16L24,16 24,21 21,21z M8,16L11,16 11,21 8,21z M3,7C2.448,7,2,7.448,2,8L2,17 3,17 6,17 6,19 4,19 4,28 28,28 28,19 26,19 26,17 29,17 30,17 30,8C30,7.448,29.552,7,29,7L25,7 7,7z M8,2L8,5 24,5 24,2z M7,0L25,0C25.552999,0,26,0.4470005,26,1L26,5 29,5C30.653999,5,32,6.3459997,32,8L32,18C32,18.553,31.552999,19,31,19L30,19 30,29C30,29.552979,29.553009,30,29,30L3,30C2.446991,30,2,29.552979,2,29L2,19 1,19C0.44700003,19,0,18.553,0,18L0,8C0,6.3459997,1.346,5,3,5L6,5 6,1C6,0.4470005,6.447,0,7,0z"}}
        };
    }
}
