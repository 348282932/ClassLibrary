using System;
using System.Data;
using System.Data.OleDb;

namespace MyClassLibrary
{
    public class ImportExcel
    {
        /// <summary>
        /// 从Excel导入帐户(新建oleDb连接,Excel整表读取,适于无合并单元格时)
        /// </summary>
        /// <param name="fileName">完整路径名</param>
        /// <returns></returns>
        public static DataTable ImpExcelDt(String fileName)
        {
            /* 创建、打开.xlsx文件(不带宏操作)兼容Excel 2007、2010
             * String strCon = " Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + fileName + "; Extended Properties=’Excel 12.0 Xml; HDR=YES'";
             * 使用此语法可以打开.xlsx文件，如果此文件不存在则创建一个。
             * HDR=YES表示第一行不是数据，而是相当于数据库中字段。
             */

            /* 创建、打开.xlsx文件(不带宏操作)兼容Excel 2007、2010
             * 当你想把所有的数据都当做文本对待时，覆盖Excel通常的猜测这列的数据类型。(文件存在的情况下)
             * String strCon = " Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + fileName + "; Extended Properties=’Excel 12.0 Xml; HDR=YES;IMEX=1'";
             * 使用此语法可以打开.xlsx文件，如果此文件不存在则创建一个。
             * 如果想把列名也读出来可以设置HDR=NO。IMEX=1把混合型数据作为文本型读取。
             */

            /* 创建、打开.xlsb文件(不带宏操作)兼容Excel 2007、2010
             * .xlsb是保存二进制格式，如果数据量很大时，可以大大提高性能
             * String strCon = " Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + fileName + "; Extended Properties=’Excel 12.0; HDR=YES'";
             * 使用此语法可以打开.xlsb文件，如果此文件不存在则创建一个。
             * 如果想把列名也读出来可以设置HDR=NO。IMEX=1把混合型数据作为文本型读取。
             */

            /* 创建、打开.xlsm文件(带宏操作)兼容Excel 2007、2010
             * String strCon = " Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + fileName + "; Extended Properties=’Excel 12.0 Macro; HDR=YES'";
             * 使用此语法可以打开.xlsb文件，如果此文件不存在则创建一个。
             * 如果想把列名也读出来可以设置HDR=NO。IMEX=1把混合型数据作为文本型读取。
             */

            // 创建和 Excel 2003兼容格式 Excel 链接字符串（自动忽略第一行,常用）

            String strCon = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + fileName + ";Extended Properties=Excel 8.0; HDR=Yes;IMEX=1";

            // 创建链接对象

            OleDbConnection myConn = new OleDbConnection(strCon);

            // 获取非隐藏的 Excel 工作表

            DataTable dtDATA = myConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            // 获取第一个工作表名称（不适用中文工作表名，只有一个工作表时例外）

            String tabName = dtDATA.Rows[0]["Table_Name"].ToString().Trim();

            // 创建查询 SQL 语句（如果查询指定列则修改如下格式 " SELECT * FROM [" + tabName + "A1:D100]" ）

            String strCom = " SELECT * FROM [" + tabName + "] ";

            // 打开连接对象

            myConn.Open();

            OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn);

            DataSet myDataSet = new DataSet();

            myCommand.Fill(myDataSet, "[" + tabName + "]");

            myConn.Close();

            DataTable dtUsers = myDataSet.Tables[0];

            return dtUsers;
        }
    }
}
