using System.Xml;
using System.Data;
using System;
using System.Collections.Generic;

namespace MyClassLibrary
{
    /// <summary>
    /// Xml的操作公共类
    /// </summary>    
    public class XmlHelper<T> where T : new()
    {
        #region 字段定义
        /// <summary>
        /// XML文件的物理路径
        /// </summary>
        private string _filePath = string.Empty;
        /// <summary>
        /// Xml文档
        /// </summary>
        private XmlDocument _xml;
        /// <summary>
        /// XML的根节点
        /// </summary>
        private XmlElement _element;
        #endregion

        #region 构造方法
        /// <summary>
        /// 实例化XmlHelper对象
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的相对路径</param>
        public XmlHelper(string xmlFilePath)
        {
            //获取XML文件的绝对路径
            _filePath = SysHelper.GetPath(xmlFilePath);
        }
        #endregion

        #region 创建XML的根节点
        /// <summary>
        /// 创建XML的根节点
        /// </summary>
        private void CreateXMLElement()
        {

            //创建一个XML对象
            _xml = new XmlDocument();

            if (DirFile.IsExistFile(_filePath))
            {
                //加载XML文件
                _xml.Load(this._filePath);
            }

            //为XML的根节点赋值
            _element = _xml.DocumentElement;
        }
        #endregion

        #region 获取指定XPath表达式的节点对象
        /// <summary>
        /// 获取指定XPath表达式的节点对象
        /// </summary>        
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public XmlNode GetNode(string xPath)
        {
            //创建XML的根节点
            CreateXMLElement();

            //返回XPath节点
            return _element.SelectSingleNode(xPath);
        }
        #endregion

        #region 获取指定XPath表达式节点的值
        /// <summary>
        /// 获取指定XPath表达式节点的值
        /// </summary>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public string GetValue(string xPath)
        {
            //创建XML的根节点
            CreateXMLElement();

            //返回XPath节点的值
            return _element.SelectSingleNode(xPath).InnerText;
        }
        #endregion

        #region 获取指定XPath表达式节点的属性值
        /// <summary>
        /// 获取指定XPath表达式节点的属性值
        /// </summary>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        /// <param name="attributeName">属性名</param>
        public string GetAttributeValue(string xPath, string attributeName)
        {
            //创建XML的根节点
            CreateXMLElement();

            //返回XPath节点的属性值
            return _element.SelectSingleNode(xPath).Attributes[attributeName].Value;
        }
        #endregion

        #region 新增节点
        /// <summary>
        /// 1. 功能：新增节点。
        /// 2. 使用条件：将任意节点插入到当前Xml文件中。
        /// </summary>        
        /// <param name="xmlNode">要插入的Xml节点</param>
        public void AppendNode(XmlNode xmlNode)
        {
            //创建XML的根节点
            CreateXMLElement();

            //导入节点
            XmlNode node = _xml.ImportNode(xmlNode, true);

            //将节点插入到根节点下
            _element.AppendChild(node);
        }

        /// <summary>
        /// 1. 功能：新增节点。
        /// 2. 使用条件：将DataSet中的第一条记录插入Xml文件中。
        /// </summary>        
        /// <param name="ds">DataSet的实例，该DataSet中应该只有一条记录</param>
        public void AppendNode(DataSet ds)
        {
            //创建XmlDataDocument对象
            XmlDataDocument xmlDataDocument = new XmlDataDocument(ds);

            //导入节点
            XmlNode node = xmlDataDocument.DocumentElement.FirstChild;

            //将节点插入到根节点下
            AppendNode(node);
        }
        #endregion

        #region 删除节点
        /// <summary>
        /// 删除指定XPath表达式的节点
        /// </summary>        
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public void RemoveNode(string xPath)
        {
            //创建XML的根节点
            CreateXMLElement();

            //获取要删除的节点
            XmlNode node = _xml.SelectSingleNode(xPath);

            //删除节点
            _element.RemoveChild(node);
        }
        #endregion //删除节点

        #region 保存XML文件
        /// <summary>
        /// 保存XML文件
        /// </summary>        
        public void Save()
        {
            //创建XML的根节点
            CreateXMLElement();

            //保存XML文件
            _xml.Save(this._filePath);
        }
        #endregion //保存XML文件

        #region 静态方法

        #region 创建根节点对象
        /// <summary>
        /// 创建根节点对象
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的相对路径</param>        
        private static XmlElement CreateRootElement(string xmlFilePath)
        {
            //定义变量，表示XML文件的绝对路径
            string filePath = "";

            //获取XML文件的绝对路径
            filePath = SysHelper.GetPath(xmlFilePath);

            //创建XmlDocument对象
            XmlDocument xmlDocument = new XmlDocument();
            //加载XML文件
            xmlDocument.Load(filePath);

            //返回根节点
            return xmlDocument.DocumentElement;
        }
        #endregion

        #region 获取指定XPath表达式节点的值
        /// <summary>
        /// 获取指定XPath表达式节点的值
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的相对路径</param>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        public static string GetValue(string xmlFilePath, string xPath)
        {
            //创建根对象
            XmlElement rootElement = CreateRootElement(xmlFilePath);

            //返回XPath节点的值
            return rootElement.SelectSingleNode(xPath).InnerText;
        }
        #endregion

        #region 获取指定XPath表达式节点的属性值
        /// <summary>
        /// 获取指定XPath表达式节点的属性值
        /// </summary>
        /// <param name="xmlFilePath">Xml文件的相对路径</param>
        /// <param name="xPath">XPath表达式,
        /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
        /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
        /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
        /// </param>
        /// <param name="attributeName">属性名</param>
        public static string GetAttributeValue(string xmlFilePath, string xPath, string attributeName)
        {
            //创建根对象
            XmlElement rootElement = CreateRootElement(xmlFilePath);

            //返回XPath节点的属性值
            return rootElement.SelectSingleNode(xPath).Attributes[attributeName].Value;
        }
        #endregion

        #endregion

        #region 实体类转成Xml
        /// <summary>
        /// 对象实例转成xml
        /// </summary>
        /// <param name="item">对象实例</param>
        /// <returns></returns>
        public static string EntityToXml(T item)
        {
            IList<T> items = new List<T>();
            items.Add(item);
            return EntityToXml(items);
        }

        /// <summary>
        /// 对象实例集转成xml
        /// </summary>
        /// <param name="items">对象实例集</param>
        /// <returns></returns>
        public static string EntityToXml(IList<T> items)
        {
            //创建XmlDocument文档
            XmlDocument doc = new XmlDocument();
            //创建根元素
            XmlElement root = doc.CreateElement(typeof(T).Name + "s");
            //添加根元素的子元素集
            foreach (T item in items)
            {
                EntityToXml(doc, root, item);
            }
            //向XmlDocument文档添加根元素
            doc.AppendChild(root);

            return doc.InnerXml;
        }

        private static void EntityToXml(XmlDocument doc, XmlElement root, T item)
        {
            //创建元素
            XmlElement xmlItem = doc.CreateElement(typeof(T).Name);
            //对象的属性集

            System.Reflection.PropertyInfo[] propertyInfo =
            typeof(T).GetProperties(System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Instance);



            foreach (System.Reflection.PropertyInfo pinfo in propertyInfo)
            {
                if (pinfo != null)
                {
                    //对象属性名称
                    string name = pinfo.Name;
                    //对象属性值
                    string value = String.Empty;

                    if (pinfo.GetValue(item, null) != null)
                        value = pinfo.GetValue(item, null).ToString();//获取对象属性值
                    //设置元素的属性值
                    xmlItem.SetAttribute(name, value);
                }
            }
            //向根添加子元素
            root.AppendChild(xmlItem);
        }

        #endregion

        #region Xml转成实体类
        /// <summary>
        /// Xml转成对象实例
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns></returns>
        public static T XmlToEntity(string xml)
        {
            IList<T> items = XmlToEntityList(xml);
            if (items != null && items.Count > 0)
                return items[0];
            else return default(T);
        }

        /// <summary>
        /// Xml转成对象实例集
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns></returns>
        public static IList<T> XmlToEntityList(string xml)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xml);
            }
            catch
            {
                return null;
            }
            if (doc.ChildNodes.Count != 2)
                return null;
            if (doc.ChildNodes[1].Name.ToLower() != typeof(T).Name.ToLower() + "s")
                return null;

            XmlNode node = doc.ChildNodes[1];

            IList<T> items = new List<T>();

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name.ToLower() == typeof(T).Name.ToLower())
                    items.Add(XmlNodeToEntity(child));
            }

            return items;
        }

        private static T XmlNodeToEntity(XmlNode node)
        {
            T item = new T();

            if (node.NodeType == XmlNodeType.Element)
            {
                XmlElement element = (XmlElement)node;

                System.Reflection.PropertyInfo[] propertyInfo = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                foreach (XmlAttribute attr in element.Attributes)
                {
                    string attrName = attr.Name.ToLower();
                    string attrValue = attr.Value.ToString();
                    foreach (System.Reflection.PropertyInfo pinfo in propertyInfo)
                    {
                        if (pinfo != null)
                        {
                            string name = pinfo.Name.ToLower();
                            Type dbType = pinfo.PropertyType;
                            if (name == attrName)
                            {
                                if (String.IsNullOrEmpty(attrValue))
                                    continue;
                                switch (dbType.ToString())
                                {
                                    case "System.Int32":
                                        pinfo.SetValue(item, Convert.ToInt32(attrValue), null);
                                        break;
                                    case "System.Boolean":
                                        pinfo.SetValue(item, Convert.ToBoolean(attrValue), null);
                                        break;
                                    case "System.DateTime":
                                        pinfo.SetValue(item, Convert.ToDateTime(attrValue), null);
                                        break;
                                    case "System.Decimal":
                                        pinfo.SetValue(item, Convert.ToDecimal(attrValue), null);
                                        break;
                                    case "System.Double":
                                        pinfo.SetValue(item, Convert.ToDouble(attrValue), null);
                                        break;
                                    default:
                                        pinfo.SetValue(item, attrValue, null);
                                        break;
                                }
                                continue;
                            }
                        }
                    }
                }
            }
            return item;
        }
        #endregion
    }
}