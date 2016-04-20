using System;
using System.Runtime.InteropServices;

namespace MyClassLibrary
{
    public class AspriseOCR
    {
        /* ------------------------------------------------------------------------------------------------------------------
         * 注：使用时需要将 Bin 目录里的 “AspriseOCR.dll”“DevIL.dll”“ILU.dll”拷贝至启动项目的 Bin 目录中，不用添加引用
         * 
         *     精确图像文件路径及格式，可识别字母数字和符号，该功能将以字符串形式返回图片内容，
         * 
         *     如果类型参数设置为-1，Asprise OCR将自动决定文件格式。AspriseOCR支持的图片格式较广泛
         *     
         *     如.bmp,.ico,.jpg,.jpeg,.png,.pic,.jng,.gif等多达30种图片格式。
         * ------------------------------------------------------------------------------------------------------------------
         */

        #region 导入 DLL 引用

        /*=====================================================================================================================*/

        // 注：图片的全区域实施OCR任务

        [DllImport("AspriseOCR.dll", EntryPoint = "OCR", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr OCR(String file, Int32 type);

        /*=====================================================================================================================*/

        // 注：图片的部分区域实施OCR任务，其中(startX, startY)对应图像的左上方区域，(width, height)对应区域的宽度和高度。

        [DllImport("AspriseOCR.dll", EntryPoint = "OCRpart", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr OCRpart(String file, Int32 type, Int32 startX, Int32 startY, Int32 width, Int32 height);

        /*=====================================================================================================================*/

        // 注：识别图片中的条形码，当有多个条形码时，会以换行符分割

        [DllImport("AspriseOCR.dll", EntryPoint = "OCRBarCodes", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr OCRBarCodes(String file, Int32 type);

        /*=====================================================================================================================*/

        // 注：识别图片中条形码的部分区域

        [DllImport("AspriseOCR.dll", EntryPoint = "OCRpartBarCodes", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr OCRpartBarCodes(String file, Int32 type, Int32 startX, Int32 startY, Int32 width, Int32 height);

        /*=====================================================================================================================*/

        #endregion


        /// <summary>
        /// 图片全区域识别
        /// </summary>
        /// <param name="file">图片路径</param>
        /// <returns></returns>
        public static String OCRStr(String file)
        {
            return Marshal.PtrToStringAnsi(OCR(file, -1));
        }

        /// <summary>
        /// 图片部分区域识别
        /// </summary>
        /// <param name="file">图片路径</param>
        /// <param name="startX">识别起始 X 坐标</param>
        /// <param name="startY">识别起始 Y 坐标</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <returns></returns>
        public static String OCRpartStr(String file, Int32 startX, Int32 startY, Int32 width, Int32 height)
        {
            return Marshal.PtrToStringAnsi(OCRpart(file, -1, startX, startY, width, height));
        }

        /// <summary>
        /// 条形码全区域识别
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns></returns>
        public static String OCRBarCodesStr(String file)
        {
            return Marshal.PtrToStringAnsi(OCRBarCodes(file, -1));
        }

        /// <summary>
        /// 条形码部分区域识别
        /// </summary>
        /// <param name="file">图片路径</param>
        /// <param name="startX">识别起始 X 坐标</param>
        /// <param name="startY">识别起始 Y 坐标</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <returns></returns>
        public static String OCRpartBarCodesStr(String file, Int32 startX, Int32 startY, Int32 width, Int32 height)
        {
            return Marshal.PtrToStringAnsi(OCRpart(file, -1, startX, startY, width, height));
        }
    }
}
