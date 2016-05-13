using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;

namespace MyClassLibrary
{
    public class ExportExcel : Page
    {
        /// <summary>
        /// 将 DataTable 转换为 ArrayList
        /// </summary>
        /// <param name="data"> DataTable </param>
        /// <returns> ArrayList </returns>
        private static ArrayList DataTableToArrayList(DataTable data)
        {
            ArrayList array = new ArrayList();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow row = data.Rows[i];

                Hashtable record = new Hashtable();

                for (int j = 0; j < data.Columns.Count; j++)
                {
                    object cellValue = row[j];

                    if (cellValue.GetType() == typeof(DBNull))
                    {
                        cellValue = null;
                    }

                    record[data.Columns[j].ColumnName] = cellValue;
                }
                array.Add(record);
            }
            return array;
        }

        /// <summary>
        /// 导出 Excel
        /// </summary>
        /// <param name="columns"> 列名 </param>
        /// <param name="data"> 表数据 </param>
        public void ExportToExcel<T>(List<T> data, ArrayList columns)
        {

            HttpContext.Current.Response.Clear();

            HttpContext.Current.Response.Buffer = true;

            HttpContext.Current.Response.Charset = "GB2312";

            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls");

            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");//设置输出流为简体中文

            HttpContext.Current.Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。

            EnableViewState = false;

            HttpContext.Current.Response.Write(ExportTable(data, columns));

            HttpContext.Current.Response.End();

        }

        /// <summary>
        /// 将数据写入 Excel 表
        /// </summary>
        /// <param name="columns"> 列名 </param>
        /// <param name="data"> 表数据 </param>
        /// <returns> Excel 表 </returns>
        public static string ExportTable<T>(List<T> data, ArrayList columns)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">");

            sb.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");

            // 写出列名

            sb.AppendLine("<tr style=\"font-weight: bold; white-space: nowrap;\">");

            foreach (Hashtable column in columns)
            {
                sb.AppendLine("<td>" + column["header"] + "</td>");
            }
            sb.AppendLine("</tr>");

            // 写出数据

            foreach (T row in data)
            {
                sb.Append("<tr>");

                foreach (Hashtable column in columns)
                {
                    if (column["field"] == null) continue;

                    Object value = row.GetType().GetField(column["field"].ToString()).GetValue(row);

                    sb.AppendLine("<td>" + value + "</td>");
                }

                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");

            return sb.ToString();
        }
    }
}
