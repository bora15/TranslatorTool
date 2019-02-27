using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TranslatorTool.Helpers
{
    public static class TranslationHelper
    {
        static string path = "translations.xml";

        public static Translations TranslationList;
        static TranslationHelper()
        {
            TranslationList = new Translations();
            TranslationList.Translation = new List<Translation>();
        }

        static XmlSerializerNamespaces GetNamespaces()
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            return ns;
        }

        public static void LoadTranslations()
        {

            var fullpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, TranslationHelper.path);
            if (!File.Exists(path))
            {
                TranslationList.Translation = new List<Translation>();
                SaveTranslations();
            }
            try
            {
                string content = File.ReadAllText(path);

                XmlSerializer serializer = new XmlSerializer(typeof(Translations));
                using (TextReader reader = new StringReader(content))
                {
                    TranslationList = (Translations)serializer.Deserialize(reader);
                }

            }
            catch (Exception ex)
            {
                TranslationList.Translation = new List<Translation>();
            }
        }

        public static bool Exists(string from, out int id)
        {
            if (from == null)
            {
                id = 0;
                return false;
            }
            var item = TranslationList.Translation.FirstOrDefault(x => x.From.ToLower() == from.ToLower());
            if (item != null)
            {
                id = item.Id;
                return true;
            } else
            {
                id = 0;
                return false;
            }
        }

        public static int AddTranslation(string from, string to)
        {
            int lastId = TranslationList.Translation.OrderByDescending(x => x.Id)
                .Select(x => x.Id)
                .FirstOrDefault();

            lastId++;

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            TranslationList.Translation.Add(new Translation()
            {
                Id = lastId,
                From = from,
                To = to,
                Timestamp = timestamp
            });

            SaveTranslations();

            return lastId;
        }

        public static Translation GetTranslation(int id)
        {
            return TranslationList.Translation.FirstOrDefault(x => x.Id == id);
        }

        public static Translations GetTranslations()
        {
            return TranslationList;
        }

        public static void SaveTranslations()
        {
            try
            {
                var fullpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, TranslationHelper.path);
                XmlSerializer serializer = new XmlSerializer(typeof(Translations));

                using (var ms = new MemoryStream())
                {
                    using (XmlWriter xwriter = XmlWriter.Create(ms, new XmlWriterSettings()
                    {
                        Indent = true,
                        Encoding = Encoding.UTF8,
                        CloseOutput = false,
                        OmitXmlDeclaration = false,
                    }))
                    {
                        serializer.Serialize(xwriter, TranslationList, GetNamespaces());
                        string toWrite = Encoding.UTF8.GetString(ms.ToArray());
                        File.WriteAllText(fullpath, toWrite);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
