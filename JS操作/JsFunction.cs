using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;

namespace MyClassLibrary
{
    public class JsFunction
    {
        /// <summary>
        /// 在客户端注册Javascript脚本,以便调用
        /// </summary>
        /// <param name="name">脚本块标识。</param>
        /// <param name="content">脚本代码</param>
        public static void RegJs(string name, string content)
        {
            string js = content;
            if (js.IndexOf("<script") < 0)
            {
                js = "<script language=\"JavaScript\">" + js + "</script>";
            }
            Page page = (Page)HttpContext.Current.Handler;
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), name, js);
        }


        /// <summary>
        /// 在客户端执行一段脚本
        /// </summary>
        /// <param name="name">脚本块标识。</param>
        /// <param name="js">要执行的脚本</param>
        public static void ExeJs(string name, string js)
        {
            if (js.IndexOf("<script") < 0)
            {
                js = string.Format("<script language=\"JavaScript\">{0}</script>", js);
            }
            
            Page page = (Page)HttpContext.Current.Handler;
            
            page.ClientScript.RegisterStartupScript(page.GetType(), name, js);
        }

        /// <summary>
        /// 回车转Tab键
        /// </summary>
        public static void EnterToTab()
        {
            string EnterToTab = "<script language=\"javascript\" event=\"onkeydown\" for=\"document\">if(event.keyCode==13 && event.srcElement.type!='button' && event.srcElement.type!='submit' && event.srcElement.type!='reset' && event.srcElement.type!='textarea' && event.srcElement.type!='' && event.srcElement.type!='imagebutton')event.keyCode=9;</script>";
            
            RegJs("enterToTab", EnterToTab);
        }

        /// <summary>
        /// 输出信息,并延迟跳转指定页面。
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="toURL">跳转地址</param>
        /// <param name="time">延迟时间（毫秒）</param>
        public static void WriteAndRedirect(string message, string toURL, string time)
        {
            HttpContext.Current.Response.Write(message);

            HttpContext.Current.Response.Write("<script language=JavaScript>setTimeout(\"location.href='" + toURL + "'\"," + time + ")</script>");
        }

        /// <summary>
        /// 弹出提示对话框
        /// </summary>
        /// <param name="strMessage">消息字符串</param>
        public static void Alert(string strMessage)
        {
            Alert("alert", strMessage);
        }


        /// <summary>
        /// 弹出提示对话框
        /// </summary>
        /// <param name="name">脚本块标识。</param>
        /// <param name="strMessage">消息字符串</param>
        public static void Alert(string name, string strMessage)
        {
            RegJs(name, string.Format("<script language=\"javascript\">alert('{0}');</script>", strMessage));
        }

        /// <summary>
        /// 弹出提示对话框并转向
        /// </summary>
        /// <param name="message">消息字符串</param>
        /// <param name="toURL">转向地址</param>
        public static void AlertAndRedirect(string message, string toURL)
        {
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            js = string.Format(js, message, toURL);
            RegJs("AlertAndRedirect", js);
        }

        /// <summary>
        /// 服务器端弹出alert对话框，并使控件获得焦点
        /// </summary>
        /// <param name="name">脚本块标识。</param>
        /// <param name="str_Message">提示信息</param>
        /// <param name="focusControl">需要获得焦点的控件名称</param>
        public static void Alert(string name, string str_Message, string focusControl)
        {
            ExeJs(name, string.Format("<script language=\"javascript\">alert('{0}');document.forms(0).{1}.focus(); document.forms(0).{2}.select();</script>", str_Message, focusControl, focusControl));
        }

        /// <summary>
        /// 弹出确认对话框
        /// </summary>
        /// <param name="name">脚本块标识.</param>
        /// <param name="strMessage">消息字符串</param>
        public static void Confirm(string name, string strMessage)
        {
            RegJs(name, "<script language=\"javascript\"> confirm('" + strMessage + "')</script>");
        }


        /// <summary>
        /// 使控件获得焦点
        /// </summary>
        /// <param name="ctlId">获得焦点控件Id值,比如：txt_Name</param>
        public static void GetFocus(string ctlId)
        {
            ExeJs("GetFocus", string.Format("<script language=\"javascript\">document.getElementById('{0}').focus(); document.getElementById('{1}').select();</script>", ctlId, ctlId));
        }


        /// <summary>
        /// 关闭网页，生成关闭网页的脚本代码
        /// </summary>
        /// <returns>关闭网页的脚本代码</returns>
        public static void ClosePage()
        {
            StringBuilder js = new StringBuilder();
            js.Append("<script language=\"JavaScript\">window.close();</script>");
            RegJs("ClosePage", js.ToString());
        }

        /// <summary>
        /// 生成调用浏览器打印的脚本代码
        /// </summary>
        /// <returns>调用浏览器打印的脚本代码</returns>
        public static void PrintPage()
        {
            StringBuilder js = new StringBuilder();
            js.Append("<script language=\"JavaScript\">window.print();</script>");
            RegJs("PrintPage", js.ToString());
        }

        /// <summary>
        /// 生成打开窗口的脚本代码
        /// </summary>
        /// <param name="url">要打开的联接</param>
        /// <param name="width">窗口宽度</param>
        /// <param name="height">窗口高度</param>
        /// <returns>打开窗口的脚本代码</returns>
        public static void OpenPage(string url, int width, int height)
        {

            StringBuilder js = new StringBuilder();
            js.Append("<script language=\"JavaScript\">");
            js.Append("window.open('");
            js.Append(url);
            js.AppendFormat("', '_blank', 'left=20,top=20,height={0},width={1},toolbar=no, menubar=no,scrollbars=yes, resizable=yes, location=no,status=no')", height, width);
            js.Append("</script>");
            RegJs("OpenPage", js.ToString());
        }
        /// <summary>
        /// 转向Url制定的页面
        /// </summary>
        /// <param name="url"></param>
        public static void LocationHref(string url)
        {
            string js = "<script language=\"JavaScript\">window.location.replace('{0}');</Script>";
            js = string.Format(js, url);
            RegJs("LocationHref", js.ToString());
        }

        /// <summary>
        /// 后退/前进
        /// </summary>
        /// <param name="value">-1/1</param>
        public static void GoHistory(int value)
        {
            string js = "<script language=\"JavaScript\">history.go({0});</Script>";
            js = string.Format(js, value);
            RegJs("GoHistory", js.ToString());
        }
        /// <summary>
        /// 弹出消息并且返回
        /// </summary>
        public static void Alertandback(string message)
        {
            RegJs("alert", string.Format("<script language=\"javascript\">alert('{0}');history.back();</script>", message));
        }
        public static string replacestr(string str)
        {
            return Regex.Replace(str, "[^\x00-\xff]", "**");
            //return str.replace(/[^\x00-\xff]/g, "**");
        }
        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面指针，一般为"this"</param>
        /// <param name="msg">提示信息</param>
        public static void ShowAlert(System.Web.UI.Page page, string msg)
        {
            System.Text.StringBuilder Builder = new System.Text.StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
        }
        /// <summary>
        /// 打开大小不可变模式窗口
        /// </summary>
        /// <param name="page">当前页面指针，一般为"this"</param>
        /// <param name="PageUrl">打开的模式窗口显示的网页地址</param>
        /// <param name="Width">打开的模式窗口的宽度</param>
        /// <param name="Height">打开的模式窗口的高度</param>
        public static void OpenFixModalDialog(System.Web.UI.Page page, String PageUrl, int Width, int Height)
        {
            System.Text.StringBuilder Builder = new System.Text.StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("window.showModalDialog('{0}',null,'dialogWidth:{1}px;dialogHeight:{2}px;help:no;unadorned:no;resizable:no;status:no');", PageUrl, Width, Height);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
        }
        /// <summary>
        /// 打开大小可变模式窗口
        /// </summary>
        /// <param name="page">当前页面指针，一般为"this"</param>
        /// <param name="PageUrl">打开的模式窗口显示的网页地址</param>
        /// <param name="Width">打开的模式窗口的宽度</param>
        /// <param name="Height">打开的模式窗口的高度</param>
        public static void OpenSizeableModalDialog(System.Web.UI.Page page, String PageUrl, int Width, int Height)
        {
            System.Text.StringBuilder Builder = new System.Text.StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("window.showModalDialog('{0}',null,'dialogWidth:{1}px;dialogHeight:{2}px;help:no;unadorned:no;resizable:yes;status:no');", PageUrl, Width, Height);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
        }
        /// <summary>
        /// 打开悬浮提示窗口
        /// </summary>
        /// <param name="page">页面指针 一般输入"this"</param>
        /// <param name="message">显示的消息</param>
        /// <param name="Width">窗口宽度</param>
        /// <param name="height">窗口高度</param>
        public static void OpenFloatDialog(System.Web.UI.Page page, string message, int Width, int height)
        {
            System.Text.StringBuilder Builder = new System.Text.StringBuilder();
            Builder.Append("<script type='text/javascript' language='javascript' defer>");
            //   Builder.Append("var msgw,msgh,bordercolor; ");
            Builder.AppendLine("function ShowBDDialog(){ ");
            Builder.AppendLine("bordercolor='#66ccff';titlecolor='#99CCFF';");
            Builder.AppendLine("var sWidth,sHeight; sWidth=document.body.offsetWidth; sHeight=document.body.offsetHeight;");
            Builder.AppendLine("var bgObj=document.createElement('div'); ");
            Builder.AppendLine(" bgObj.setAttribute('id','bgDiv'); ");
            Builder.AppendLine("bgObj.style.position='absolute'; ");
            Builder.AppendLine("bgObj.style.top='0'; bgObj.style.background='#dcdcdc';");
            Builder.AppendLine("bgObj.style.filter='progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75';");
            Builder.AppendLine("bgObj.style.opacity='0.6'; ");
            Builder.AppendLine("bgObj.style.left='0';");
            Builder.AppendLine("bgObj.style.width=sWidth + 'px'; ");
            Builder.AppendLine("bgObj.style.height=sHeight + 'px';");
            Builder.AppendLine("document.body.appendChild(bgObj); ");
            Builder.AppendLine("var msgObj=document.createElement('div')");
            Builder.AppendLine("msgObj.setAttribute('id','msgDiv');");
            Builder.AppendLine("msgObj.setAttribute('align','center');");
            Builder.AppendLine("msgObj.style.position='absolute';msgObj.style.background='white'; ");
            Builder.AppendLine("msgObj.style.font='12px/1.6em Verdana, Geneva, Arial, Helvetica, sans-serif';");
            Builder.AppendLine("msgObj.style.border='1px solid ' + bordercolor;");
            Builder.AppendFormat("msgObj.style.width='{0} '+ 'px'; ", Width);
            Builder.AppendFormat("msgObj.style.height='{0}' + 'px';", height);
            Builder.AppendFormat("msgObj.style.top=(document.documentElement.scrollTop + (sHeight-'{0}')/2) + 'px';", height);
            Builder.AppendFormat("msgObj.style.left=(sWidth-'{0}')/2 + 'px';", Width);
            Builder.AppendLine("var title=document.createElement('h4');");
            Builder.AppendLine("title.setAttribute('id','msgTitle');");
            Builder.AppendLine("title.setAttribute('align','right');");
            Builder.AppendLine("title.style.margin='0'; ");
            Builder.AppendLine("title.style.padding='3px'; title.style.background=bordercolor; ");
            Builder.AppendLine("title.style.filter='progid:DXImageTransform.Microsoft.Alpha(startX=20, startY=20, finishX=100, finishY=100,style=1,opacity=75,finishOpacity=100);'; ");
            Builder.AppendLine("title.style.opacity='0.75'; ");
            Builder.AppendLine("title.style.border='1px solid ' + bordercolor;title.innerHTML='<a style=font-size:small href=#>关闭</a>'; ");
            Builder.AppendLine("title.onclick=function(){ document.body.removeChild(bgObj);document.getElementById('msgDiv').removeChild(title); document.body.removeChild(msgObj);} ");
            Builder.AppendLine("document.body.appendChild(msgObj); ");
            Builder.AppendLine("document.getElementById('msgDiv').appendChild(title);");
            Builder.AppendLine("var txt=document.createElement('p');");
            Builder.AppendFormat("txt.style.height='{0}';", height);
            Builder.AppendFormat("txt.style.width='{0}';", Width);
            Builder.AppendLine(" txt.style.margin='1em 0' ");
            Builder.AppendLine("txt.setAttribute('id','msgTxt');");
            Builder.AppendFormat("txt.innerHTML='{0}'; ", message);
            Builder.AppendLine("document.getElementById('msgDiv').appendChild(txt);return false;}");
            Builder.AppendLine(" ShowBDDialog(); </script>");
            page.Response.Write(Builder.ToString());
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javscript'>ShowBDDialog();</" + "script>");
        }
        /// <summary>
        /// 打开悬浮弹出窗口
        /// </summary>
        /// <param name="page">页面指针 一般输入"this"</param>
        /// <param name="url">打开的页面的url</param>
        /// <param name="Width">窗口宽度</param>
        /// <param name="height">窗口高度</param>
        public static void OpenFloatModalWindow(System.Web.UI.Page page, string url, int Width, int height)
        {
            System.Text.StringBuilder Builder = new System.Text.StringBuilder();
            Builder.Append("<script type='text/javascript' language='javascript' defer>");
            //   Builder.Append("var msgw,msgh,bordercolor; ");

            Builder.AppendLine("function ShowBDDialog(){ ");
            Builder.AppendLine("bordercolor='#66ccff';titlecolor='#99CCFF';");
            Builder.AppendLine("var sWidth,sHeight; sWidth=document.body.offsetWidth; sHeight=document.body.offsetHeight;");
            Builder.AppendLine("var bgObj=document.createElement('div'); ");
            Builder.AppendLine(" bgObj.setAttribute('id','bgDiv'); ");
            Builder.AppendLine("bgObj.style.position='absolute'; ");
            Builder.AppendLine("bgObj.style.top='0'; bgObj.style.background='#dcdcdc';");
            Builder.AppendLine("bgObj.style.filter='progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75';");
            Builder.AppendLine("bgObj.style.opacity='0.6'; ");
            Builder.AppendLine("bgObj.style.left='0';");
            Builder.AppendLine("bgObj.style.width=sWidth + 'px'; ");
            Builder.AppendLine("bgObj.style.height=sHeight + 'px';");
            Builder.AppendLine("document.body.appendChild(bgObj); ");
            Builder.AppendLine("var msgObj=document.createElement('div')");
            Builder.AppendLine("msgObj.setAttribute('id','msgDiv');");
            Builder.AppendLine("msgObj.setAttribute('align','center');");
            Builder.AppendLine("msgObj.style.position='absolute';msgObj.style.background='white'; ");
            Builder.AppendLine("msgObj.style.font='12px/1.6em Verdana, Geneva, Arial, Helvetica, sans-serif';");
            Builder.AppendLine("msgObj.style.border='1px solid ' + bordercolor;");
            Builder.AppendFormat("msgObj.style.width='{0} '+ 'px'; ", Width);
            Builder.AppendFormat("msgObj.style.height='{0}' + 'px';", height);
            Builder.AppendFormat("msgObj.style.top=(document.documentElement.scrollTop + (sHeight-'{0}')/2) + 'px';", height);
            Builder.AppendFormat("msgObj.style.left=(sWidth-'{0}')/2 + 'px';", Width);
            Builder.AppendLine("var title=document.createElement('h4');");
            Builder.AppendLine("title.setAttribute('id','msgTitle');");
            Builder.AppendLine("title.setAttribute('align','right');");
            Builder.AppendLine("title.style.margin='0'; ");
            Builder.AppendLine("title.style.padding='3px'; title.style.background=bordercolor; ");
            Builder.AppendLine("title.style.filter='progid:DXImageTransform.Microsoft.Alpha(startX=20, startY=20, finishX=100, finishY=100,style=1,opacity=75,finishOpacity=100);'; ");
            Builder.AppendLine("title.style.opacity='0.75'; ");
            Builder.AppendLine("title.style.border='1px solid ' + bordercolor;title.innerHTML='<a style=font-size:small href=#>关闭</a>'; ");
            Builder.AppendLine("title.onclick=function(){ document.body.removeChild(bgObj);document.getElementById('msgDiv').removeChild(title); document.body.removeChild(msgObj);} ");
            Builder.AppendLine("document.body.appendChild(msgObj); ");
            Builder.AppendLine("document.getElementById('msgDiv').appendChild(title);");
            Builder.AppendLine("var txt=document.createElement('iframe');");
            Builder.AppendFormat("txt.style.height='{0}';", height);
            Builder.AppendFormat("txt.style.width='{0}';", Width);
            Builder.AppendLine(" txt.style.margin='1em 0' ");
            Builder.AppendLine("txt.setAttribute('id','msgTxt');");
            Builder.AppendFormat("txt.src='{0}'; ", url);
            Builder.AppendLine("document.getElementById('msgDiv').appendChild(txt);return false;}");
            Builder.AppendLine(" ShowBDDialog(); </script>");
            page.Response.Write(Builder.ToString());
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javscript'>ShowBDDialog();</" + "script>");
        }

        /// <summary>
        /// 弹出信息,并返回历史页面
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="value">历史记录索引</param>
        public static void AlertAndGoHistory(string message, int value)
        {
            string js = @"<Script language='JavaScript'>alert('{0}');history.go({1});</Script>";
            HttpContext.Current.Response.Write(string.Format(js, message, value));
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 直接跳转到指定的页面
        /// </summary>
        /// <param name="toURL">跳转地址</param>
        public static void Redirect(string toUrl)
        {
            string js = @"<script language=javascript>window.location.replace('{0}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, toUrl));
        }

        /// <summary>
        /// 弹出信息 并指定到父窗口
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="toURL">跳转地址</param>
        public static void AlertAndParentUrl(string message, string toURL)
        {
            string js = "<script language=javascript>alert('{0}');window.top.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
        }

        /// <summary>
        /// 返回到父窗口
        /// </summary>
        /// <param name="toURL">跳转地址</param>
        public static void ParentRedirect(string toUrl)
        {
            string js = "<script language=javascript>window.top.location.replace('{0}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, toUrl));
        }

        /// <summary>
        /// 返回历史页面
        /// </summary>
        public static void BackHistory(int value)
        {
            string js = @"<Script language='JavaScript'>history.go({0});</Script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
            HttpContext.Current.Response.End();
        }
    }
}
