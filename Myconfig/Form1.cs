using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Myconfig
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            createXml();
        }
        #region 创建xml文件
        public void createXml()
        {
            //xml保存的路径，这里放在Assets路径 注意路径。
            string filepath = Application.LocalUserAppDataPath + @"/my.xml";
            //继续判断当前路径下是否有该文件
            if (!File.Exists(filepath))
            {
                //创建XML文档实例
                XmlDocument xmlDoc = new XmlDocument();
                //创建root节点，也就是最上一层节点
                XmlElement root = xmlDoc.CreateElement("transforms");
                //继续创建下一层节点
                XmlElement elmNew = xmlDoc.CreateElement("rotation");
                //设置节点的两个属性 ID 和 NAME
                elmNew.SetAttribute("id", "0");
                elmNew.SetAttribute("name", "momo");
                //继续创建下一层节点
                XmlElement rotation_X = xmlDoc.CreateElement("x");
                //设置节点中的数值
                rotation_X.InnerText = "0";
                XmlElement rotation_Y = xmlDoc.CreateElement("y");
                rotation_Y.InnerText = "1";
                XmlElement rotation_Z = xmlDoc.CreateElement("z");
                rotation_Z.InnerText = "2";
                //这里在添加一个节点属性，用来区分。。
                rotation_Z.SetAttribute("id", "1");

                //把节点一层一层的添加至XMLDoc中 ，请仔细看它们之间的先后顺序，这将是生成XML文件的顺序
                elmNew.AppendChild(rotation_X);
                elmNew.AppendChild(rotation_Y);
                elmNew.AppendChild(rotation_Z);
                root.AppendChild(elmNew);
                xmlDoc.AppendChild(root);
                //把XML文件保存至本地
                xmlDoc.Save(filepath);
               MessageBox.Show("createXml OK!");
            }
        }
        #endregion
        #region 修改xml文件
        public void UpdateXml()
        {
            string filepath = Application.LocalUserAppDataPath + @"/my.xml";
            if (File.Exists(filepath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                //根据路径将XML读取出来
                xmlDoc.Load(filepath);
                //得到transforms下的所有子节点
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("transforms").ChildNodes;
                //遍历所有子节点
                foreach (XmlElement xe in nodeList)
                {
                    //拿到节点中属性ID =0的节点
                    if (xe.GetAttribute("id") == "0")
                    {
                        //更新节点属性
                        xe.SetAttribute("id", "1000");
                        //继续遍历
                        foreach (XmlElement x1 in xe.ChildNodes)
                        {
                            if (x1.Name == "z")
                            {
                                //这里是修改节点名称对应的数值，而上面的拿到节点连带的属性。。。
                                x1.InnerText = "update00000";
                            }

                        }
                        break;
                    }
                }
                xmlDoc.Save(filepath);
                Console.WriteLine("UpdateXml OK!");
            }

        }
        #endregion
        #region 添加xml文件
        public void AddXml()
        {
            string filepath = Application.LocalUserAppDataPath + @"/my.xml";
            if (File.Exists(filepath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filepath);
                XmlNode root = xmlDoc.SelectSingleNode("transforms");
                XmlElement elmNew = xmlDoc.CreateElement("rotation");
                elmNew.SetAttribute("id", "1");
                elmNew.SetAttribute("name", "yusong");

                XmlElement rotation_X = xmlDoc.CreateElement("x");
                rotation_X.InnerText = "0";
                rotation_X.SetAttribute("id", "1");
                XmlElement rotation_Y = xmlDoc.CreateElement("y");
                rotation_Y.InnerText = "1";
                XmlElement rotation_Z = xmlDoc.CreateElement("z");
                rotation_Z.InnerText = "2";

                elmNew.AppendChild(rotation_X);
                elmNew.AppendChild(rotation_Y);
                elmNew.AppendChild(rotation_Z);
                root.AppendChild(elmNew);
                xmlDoc.AppendChild(root);
                xmlDoc.Save(filepath);
                Console.WriteLine("AddXml OK!");
            }
        }

        #endregion
        #region 删除xml文件
        public void deleteXml()
        {
            string filepath = Application.LocalUserAppDataPath + @"/my.xml";
            if (File.Exists(filepath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filepath);
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("transforms").ChildNodes;
                foreach (XmlElement xe in nodeList)
                {
                    if (xe.GetAttribute("id") == "1")
                    {
                        xe.RemoveAttribute("id");
                    }

                    foreach (XmlElement x1 in xe.ChildNodes)
                    {
                        if (x1.Name == "z")
                        {
                            x1.RemoveAll();

                        }
                    }
                }
                xmlDoc.Save(filepath);
                Console.WriteLine("deleteXml OK!");
            }

        }

        #endregion
        #region 解析与输出xml文件
        public void showXml()
        {
            string filepath = Application.LocalUserAppDataPath + @"/my.xml";
            if (File.Exists(filepath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filepath);
                XmlNodeList nodeList = xmlDoc.SelectSingleNode("transforms").ChildNodes;
                //遍历每一个节点，拿节点的属性以及节点的内容
                foreach (XmlElement xe in nodeList)
                {
                    Console.WriteLine("Attribute :" + xe.GetAttribute("name"));
                    Console.WriteLine("NAME :" + xe.Name);
                    foreach (XmlElement x1 in xe.ChildNodes)
                    {
                        if (x1.Name == "y")
                        {
                            Console.WriteLine("VALUE :" + x1.InnerText);

                        }
                    }
                }
                Console.WriteLine("all = " + xmlDoc.OuterXml);

            }
        }
        #endregion
    }
}
