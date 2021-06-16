using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Laba_4
{
    public class Chef : IXmlSerializable
    {
        public XmlSchema GetSchema()
        {
            return null;
        } //done

        public void ReadXml(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "FirstName":
                            reader.Read();
                            firstname = reader.Value;
                            break;
                        case "LastName":
                            reader.Read();
                            lastname = reader.Value;
                            break;
                    }
                }

                if (reader.Name.Equals("Chef"))
                {
                    break;
                }
            }
        } //done
        public void WriteXml(XmlWriter writer)
        {
                    writer.WriteStartElement("Chef");
                    writer.WriteElementString("FirstName", firstname);
                    writer.WriteElementString("LastName", lastname);
                    writer.WriteEndElement();
        } //done
        public static List<Chef> ReadOrdersList(string fileName)
        {
            List<Chef> chefs = new List<Chef>();
            if (File.Exists(fileName))
            {
                using (XmlReader reader = XmlReader.Create(fileName))
                {
                    reader.MoveToContent();
                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && !reader.Name.Equals("Chefs"))
                        {
                            Chef chef = new Chef();
                            chef.ReadXml(reader);
                            chefs.Add(chef);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return chefs;
        } //done

        public static void WriteChefsToFile(string fileName, List<Chef> chefs)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = false;
            settings.NewLineOnAttributes = true;
            settings.ConformanceLevel = ConformanceLevel.Auto;

            XmlWriter xmlWriter = XmlWriter.Create(fileName, settings);
            xmlWriter.WriteStartElement("Chefs");
            chefs.ForEach(chef =>
            {
                chef.WriteXml(xmlWriter);
            });
            xmlWriter.WriteEndElement();
            xmlWriter.Close();
        } //done

        private string firstname;
        private string lastname;
        public string FisrtName
        {
            get
            {
                return firstname;
            }
            set
            {
                firstname = value ;

            }
        } //done

        public string LastName
        {
            get
            {
                return lastname;
            }
            set
            {
                lastname = value ;
            }
        } //done

        public Chef ()
            {

        } //done

        public Chef(string firstname, string lastname)
        {
            this.firstname = firstname;
            this.lastname = lastname;
        } //done


        public override string ToString()
        {
            return this.firstname + " " + this.lastname;
        }
    }
 }


