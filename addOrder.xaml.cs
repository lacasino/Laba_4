using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Laba_4
{
    /// <summary>
    /// Логика взаимодействия для DopOkno.xaml
    /// </summary>
    public partial class addOrder : Window
    {
        public List<Chef> chefs = new List<Chef>();
        public List<Dish> dishes = new List<Dish>();
        public Category[] categories = (Category[])Enum.GetValues(typeof(Category));

        public Order order;
        public addOrder()
        {
           

        }

        public addOrder(Order order)
        {
            this.order = order;
            InitializeComponent();

            foreach (Category per in categories)
            {
                comBoxOfCategory.Items.Add(per.ToString());
            }

            chefs = Chef.ReadOrdersList("Chefs");
            chefs.ForEach(fund =>
            {
                comboboxOfChef.Items.Add(fund.ToString());
            });

            order.DishsList = Dish.ReadDishsList("Dishes");
        }


        private void NameOfChef_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void createChef_Click(object sender, RoutedEventArgs e)
        {
            Chef chef;

            if(string.IsNullOrEmpty(NameOfChef.Text) || string.IsNullOrEmpty(SurNameOfChef.Text))
            {
                MessageBox.Show("Заповніть поля");
            }
            else
            {
                chef = new Chef();
                chef.FisrtName = NameOfChef.Text;
                chef.LastName = SurNameOfChef.Text;
                chefs.Add(chef);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Chef.WriteChefsToFile("Chefs", chefs);
            Dish.WriteDishsToFile("Dishes", order.DishsList);

        }

        private void CreateFood_Click(object sender, RoutedEventArgs e)
        {
            Dish dis;

            if (string.IsNullOrEmpty(CookTime.Text) || string.IsNullOrEmpty(Price.Text) || string.IsNullOrEmpty(NameOfdish.Text))
            {
                MessageBox.Show("Заповніть поля");
            }
            else
            {
                
                dis = new Dish();

                dis.Chef = chefs[comboboxOfChef.SelectedIndex];
                dis.Category = (Category)categories[comBoxOfCategory.SelectedIndex];
                dis.Price = int.Parse(Price.Text);
                dis.NameOfDish = NameOfdish.Text;
                dis.TimeToCook = int.Parse(CookTime.Text);

                order.DishsList.Add(dis);
            }
        }
    }
}
