using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Laba_4
{
    public class Dish : IXmlSerializable
    {
        private string nameOfDish;
        private int price;
        private int timeToCook;

        Chef chefOfDish;
        Category category;

        public string NameOfDish
        {
            get
            {
                return nameOfDish;
            }
            set
            {
                 nameOfDish = value ?? throw new ArgumentNullException("Поле не может быть пустым");
            }
        } //done

        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        } //done

        public int TimeToCook
        {
            get
            {
                return timeToCook;
            }
            set
            {
                timeToCook = value;
            }
        } //done

        public Chef Chef
        {
            get
            {
                return chefOfDish;
            }
            set
            {
                chefOfDish = value ?? throw new ArgumentNullException("Поле не может быть пустым");
            }
        } //done

        public Category Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        } //done

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
                        case "NameOfDish":
                            reader.Read();
                            nameOfDish = reader.Value;
                            break;
                        case "Price":
                            reader.Read();
                            price = Int32.Parse(reader.Value);
                            break;
                        case "TimeToCook":
                            reader.Read();
                            timeToCook = Int32.Parse(reader.Value);
                            break;
                        case "ChefOfDish":
                            reader.Read();
                            chefOfDish = new Chef();
                            chefOfDish.ReadXml(reader);
                            break;
                        case "Category":
                            reader.Read();
                            category = (Category)Enum.Parse(typeof(Category), reader.Value);
                            break;
                    }
                }

                if (reader.Name.Equals("Dish"))
                {
                    break;
                }
            }
        } //done

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Dish");
            writer.WriteElementString("NameOfDish", nameOfDish);
            writer.WriteElementString("Price", price.ToString());
            writer.WriteElementString("TimeToCook", timeToCook.ToString());
            chefOfDish.WriteXml(writer);
            writer.WriteElementString("Category", category.ToString());

            writer.WriteEndElement();
        } //done

        public static List<Dish> ReadDishsList(string fileName)
        {
            List<Dish> dishs = new List<Dish>();
            if (File.Exists(fileName))
            {
                using (XmlReader reader = XmlReader.Create(fileName))
                {
                    reader.MoveToContent();
                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && !reader.Name.Equals("Dishs"))
                        {
                            Dish fund = new Dish();
                            fund.ReadXml(reader);
                            dishs.Add(fund);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return dishs;
        } //done


        public static void WriteDishsToFile(string fileName, List<Dish> dishs)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = false;
            settings.NewLineOnAttributes = true;
            settings.ConformanceLevel = ConformanceLevel.Auto;

            XmlWriter xmlWriter = XmlWriter.Create(fileName, settings);
            xmlWriter.WriteStartElement("Dishs");
            dishs.ForEach(dish =>
            {
                dish.WriteXml(xmlWriter);
            });
            xmlWriter.WriteEndElement();
            xmlWriter.Close();
        } //done
    }
     
}
