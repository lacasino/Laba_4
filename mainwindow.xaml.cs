using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laba_4
{
    /// <summary>
    /// Логика взаимодействия для mainWindowWPF.xaml
    /// </summary>
    public partial class mainWindowWPF : Window
    {
        public List<Order> orders;
        public mainWindowWPF()
        {
            InitializeComponent();
            orders = Order.ReadOrderList("Orders");

            orders.ForEach(order => 
            {
                OrdersList.Items.Add(order.ToShortString());
            });
        }

        private void OrderListWPF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Order.WriteOrderToFile("Orders", orders);
        } //done

        private void Create_Click(object sender, RoutedEventArgs e) 
        {
            Order chef;

            if (string.IsNullOrEmpty(CafeName.Text) )
            {
                MessageBox.Show("Заповніть поля");
            }
            else
            {
                chef = new Order();
                chef.CafeName = CafeName.Text;
                chef.DishsList = Dish.ReadDishsList("Dishes");

                orders.Add(chef);
            }


        } //done

        private void Edit_Click(object sender, RoutedEventArgs e) 
        {
          
        }  

        private void Show_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = OrdersList.SelectedIndex;
            if (selectedIndex < 0 || selectedIndex >= orders.Count)
            {
                MessageBox.Show("Оберіть замовлення");
            }
            else
            {
                MessageBox.Show(orders[selectedIndex].ToString());
            }
        }  //done

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = OrdersList.SelectedIndex;
            if (selectedIndex < 0 || selectedIndex >= orders.Count)
            {
                MessageBox.Show("Оберіть замовлення");
                return;
            }
            orders.RemoveAt(selectedIndex);
            OrdersList.Items.RemoveAt(selectedIndex);
        }   //done

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void createDish_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = OrdersList.SelectedIndex;
            if (selectedIndex < 0 || selectedIndex >= orders.Count)
            {
                MessageBox.Show("Оберіть страву");
                return;
            }
            else
            {
                addOrder ordersWindow = new addOrder(orders[selectedIndex]);
                orders.Add(ordersWindow.order);

                ordersWindow.Show();
            }
        }
    }
}
