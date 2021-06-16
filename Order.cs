using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;

namespace Laba_4
{
    public class Order
    {
        private string cafeName;
        private DateTime date = DateTime.Now;
        List<Dish> dishsList;


        public string CafeName
        {
            get
            {
                return cafeName;
            }
            set
            {
                cafeName = value ?? throw new ArgumentNullException("Заповніть поле");
            }
        } //done

        public DateTime Date  
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        } //done

        public List <Dish> DishsList
        {
            get
            {
                return dishsList;
            }
            set
            {
                dishsList = value;
            }
        } //done

        public void AddDish(Dish dish)
        {
            dishsList.Add(dish);
        }

        public void RemoveDish(Dish dish)
        {
           dishsList.Remove(dish);
        }



        public override string ToString()
        {
            return "Назва кафе: " + cafeName + "\nКількість страв: " + dishsList.Count + "шт." + "Назва страви: " + "\nДата та час замовлення: " + date + "\nОчікувати: " + waitTime() + " хвилин";
        } //done

        public int waitTime()
        {
            int wait = 0;
           if(dishsList != null)
            {
                dishsList.ForEach(dw => { wait += dw.TimeToCook; });
            }
            return wait;
        } //done

        public string ToShortString()
        {
            return "Назва кафе: " + cafeName + "\nКількість страв: " + dishsList.Count + "шт." + "\nОчікувати: " + waitTime() + " хвилин";
        } //done

        public void ReadXml(XmlReader reader)  
        {
            reader.MoveToContent();
            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "CafeName":
                            reader.Read();
                            cafeName = reader.Value;
                            break;
                        case "Date":
                            reader.Read();
                            date = DateTime.Parse(reader.Value);
                            break;
                        case "DishsList":
                            dishsList = Dish.ReadDishsList("Dishes"); 
                            break;                              
                    }
                }

                if (reader.Name.Equals("Order"))
                {
                    break;
                }
            }
        } //done

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Order");
            writer.WriteElementString("CafeName", cafeName);
            writer.WriteElementString("Date", date.ToString());
            writer.WriteStartElement("DishsList");
            if (DishsList != null)
            {
                dishsList.ForEach(dishs =>  // code changed exhibit -- dishs
                {
                    dishs.WriteXml(writer);
                });
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
        } //done

        public static List<Order> ReadOrderList(string fileName)
        {
            List<Order> orders = new List<Order>();

            if (File.Exists(fileName))
            {
                using (XmlReader reader = XmlReader.Create(fileName))
                {
                    reader.MoveToContent();
                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && !reader.Name.Equals("Orders"))
                        {
                            Order order = new Order();
                            order.ReadXml(reader);
                            orders.Add(order);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            return orders;
        } //done

        public XmlSchema GetSchema()
        {
            return null;
        } //done

        public static void WriteOrderToFile(string fileName, List<Order> orders)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = false;
            settings.NewLineOnAttributes = true;
            settings.ConformanceLevel = ConformanceLevel.Auto;

            XmlWriter xmlWriter = XmlWriter.Create(fileName, settings);
            xmlWriter.WriteStartElement("Orders");
            orders.ForEach(order =>
            {
                order.WriteXml(xmlWriter);
            });
            xmlWriter.WriteEndElement();
            xmlWriter.Close();
        } //done
    }
}
