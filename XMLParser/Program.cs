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
            Console.BufferHeight = Int16.MaxValue - 1;
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
                                var tmp = XmlReader.ReadString();
                                if (tmp == "CodeIgniter" || tmp == "CSS3" || tmp == "Sinatra")
                                {
                                    userinfo.skills.Add(tmp);
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
                    Console.Write("Skills : ");
                    foreach (var skill in item.skills)
                    {
                        Console.Write("{0}  ", skill);
                    }
                    Console.Write("\n");
                    Console.WriteLine();
                }

            }
            catch
            {
                Console.WriteLine("Error");
            }
            Console.ReadLine();
        }
    }
}
