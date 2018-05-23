using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLParser
{
    class Program
    {


        public class UserInfo
        {
            public string UserName;
            public string UserId;
            public HashSet<string> skills = new HashSet<string>();
        }



        static void Main(string[] args)
        {

            Console.Write("Enter xml file path: ");
            string FilePath = Console.ReadLine();
            UserInfo userinfo = new UserInfo();
            List<UserInfo> userlist = new List<UserInfo>();

            try
            {
                XmlTextReader XmlReader = new XmlTextReader(FilePath);
                XmlReader.WhitespaceHandling = WhitespaceHandling.None;
                XmlReader.MoveToContent();

                while (XmlReader.Read())
                {
                    if (XmlReader.NodeType == XmlNodeType.Element)
                    {
                        switch (XmlReader.Name)
                        {
                            case "user":
                                userinfo = new UserInfo();
                                break;
                            case "uid":
                                if (userinfo != null)
                                    userinfo.UserId = XmlReader.ReadString();
                                break;
                            case "name":
                                if (userinfo != null)
                                    userinfo.UserName = XmlReader.ReadString();
                                break;
                            case "skill":
                                var tmpstr = XmlReader.ReadString();
                                if (tmpstr == "CodeIgniter" || tmpstr == "CSS3" || tmpstr == "Sinatra")
                                {
                                    userinfo.skills.Add(tmpstr);
                                }
                                break;
                        }
                    }
                    else
                    {
                        if (XmlReader.NodeType == XmlNodeType.EndElement && XmlReader.Name == "user" && userinfo.skills.Count != 0)
                        {
                            userlist.Add(userinfo);
                            userinfo = null;
                        }
                    }
                }

                foreach (var item in userlist)
                {
                    Console.WriteLine("Name = {0} \nid = {1}", item.UserName, item.UserId);
                    foreach (var skill in item.skills)
                    {
                        Console.WriteLine("skill = {0}", skill);
                    }
                    Console.WriteLine();
                }

            }
            catch
            {
                Console.WriteLine("Error");
            }

        }
    }
}









//using System;
//using System.Collections.Generic;
//using System.IO;

//using System.Xml;

//namespace XmlUserParser
//{
//    class Program
//    {
//        public class UserInfo
//        {
//            public string UserName = string.Empty;
//            public string UserId = string.Empty;
//            public List<string> skills = new List<string>();
//        }



//        static void Main(string[] args)
//        {

//            Console.Write("Enter xml file path: ");
//            string FilePath = "..\\..\\feed-test.xml";//Console.ReadLine();
//            UserInfo userinfo = null;// = new UserInfo();
//            List<UserInfo> userlist = new List<UserInfo>();

//            try
//            {
//                XmlTextReader XmlReader = new XmlTextReader(FilePath);
//                XmlReader.WhitespaceHandling = WhitespaceHandling.None;
//                var isUserOpen = false;
//                var isSkillsOpen = false;

//                // Moves the reader to the 'document' node.
//                XmlReader.MoveToContent();
//                while (XmlReader.Read())
//                {
//                    switch (XmlReader.NodeType)
//                    {
//                        case XmlNodeType.Element:
//                            switch (XmlReader.Name)
//                            {
//                                case "user":
//                                    isUserOpen = true;
//                                    userinfo = new UserInfo();
//                                    break;
//                                case "uid":
//                                    if (userinfo != null)
//                                        userinfo.UserId = XmlReader.ReadString();
//                                    break;
//                                case "name":
//                                    if (userinfo != null)
//                                        userinfo.UserName = XmlReader.ReadString();
//                                    break;
//                                case "email":
//                                    break;
//                                case "city":
//                                    break;
//                                case "address":
//                                    break;
//                                case "phone":
//                                    break;
//                                case "gender":
//                                    break;
//                                case "skills":
//                                    if (!isUserOpen)
//                                        throw new InvalidDataException("Xml file format not well formated!");
//                                    isSkillsOpen = true;
//                                    break;
//                                case "skill":
//                                    var value = XmlReader.ReadString();
//                                    if (userinfo != null)
//                                        switch (value)
//                                        {
//                                            case "CodeIgniter":
//                                            case "CSS3":
//                                            case "Sinatra":
//                                                userinfo.skills.Add(XmlReader.Value);
//                                                break;
//                                        }
//                                    break;
//                            }
//                            break;
//                        case XmlNodeType.EndElement:
//                            switch (XmlReader.Name)
//                            {
//                                case "user":
//                                    if (isSkillsOpen)
//                                        isSkillsOpen = false;//throw new InvalidDataException("Xml file format not well formated!");
//                                    isUserOpen = false;
//                                    userlist.Add(userinfo);
//                                    userinfo = null;
//                                    break;
//                                case "skills":
//                                    if (!isUserOpen)
//                                        throw new InvalidDataException("Xml file format not well formated!");
//                                    isSkillsOpen = false;
//                                    break;
//                            }
//                            break;
//                    }

//                }

//                foreach (var item in userlist)
//                {
//                    Console.WriteLine("id = {0} name = {1}", item.UserId, item.UserName);
//                    foreach (var skill in item.skills)
//                    {
//                        Console.WriteLine("skil = {0}", skill);
//                    }
//                }
//                userlist.Clear();
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine("Error" + e.Message);
//            }
//            Console.Write("Press Enter key");
//            Console.ReadLine();
//        }
//    }
//}