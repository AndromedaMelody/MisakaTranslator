﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCRLibrary
{
    public class OCRCommon
    {
        public static List<string> lstOCR = new List<string>()
        {
            "BaiduOCR",
            "BaiduFanyiOCR",
            "Tesseract5",
            "WinRtOCR"
        };

        public static List<string> GetOCRList()
        {
            return lstOCR;
        }

        public static OCREngine OCRAuto(string ocr) {
            switch (ocr)
            {
                case "BaiduOCR":
                    return new BaiduGeneralOCR();
                case "BaiduFanyiOCR":
                    return new BaiduFanyiOCR();
                case "Tesseract5":
                    return new Tesseract5OCR();
                case "WinRtOCR":
                    return new WinRtOCR();
                default:
                    return null;
            }
        }

        public static System.Text.Json.JsonSerializerOptions JsonOP = new System.Text.Json.JsonSerializerOptions()
        {
            IncludeFields = true
        };
    }
}
