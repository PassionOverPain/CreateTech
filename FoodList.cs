using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateTech
{
    internal class FoodList
    {
       
        
        public FoodList() 
        { 
           
        }
        
        public static void ReadData(ArrayList fooddata)  // Acquiring data to be used by CreateIT
        {
            string item;
            string category;
            double price;
            int number;
            string size;
            StreamReader sr = new StreamReader("Ingredients.txt");
            string line = sr.ReadLine();
            string[] lines;
            while (line != null)
            {
               lines = line.Split(',');
               item = lines[0];
               category = lines[1];
               price = double.Parse(lines[2]);
               number = int.Parse(lines[3]);
               size = lines[4];
               Food food = new Food(item, category, price, number,size);
               fooddata.Add(food);
               line = sr.ReadLine();

            }
            sr.Close();
        }
        public void WriteData()
        {

        }
        public static Food GetFood(string item,ArrayList Foodcart)
        {
            foreach(Food food in Foodcart)
            {
                if(food.Getname() == item)
                { return food; }
            }
            return null;
        }
        public  static void Purchase( Food item, ArrayList Foodcart)
        {
            if ((MessageBox.Show($"{item.Getname()} is {item.Getprice():C2} per {item.Getsize()}.\nYou have {item.Getnumber()} in total.\nWould you like to add this to your shopping cart?", "Purchase Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes))
            {
                int quantity = int.Parse(Interaction.InputBox("Enter the amount you would like to add: ", "Purchase Details"));
                if (quantity > 0)
                {
                    double total = item.Getprice() * quantity;
                    if ((MessageBox.Show($"That will be {total:C2}, confirm purchase?", "Purchase Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes))
                    {
                        GetFood(item.Getname(),Foodcart).Setnumber(GetFood(item.Getname(),Foodcart).Getnumber()+quantity);
                        StreamWriter sw = new StreamWriter("ShoppingCart.txt",true);
                        sw.WriteLine($"{item.Getname()}\t{item.Getcategory()}\t{quantity} {item.Getsize()}\t{total}");
                        MessageBox.Show($"Item Successfully added.\nYou now have {item.Getnumber()} {item.Getsize()} of {item.Getname()} in your shopping cart✔.","Purchase Confirmation");
                        sw.Close();
                    }
                    else
                        MessageBox.Show($"Purchase successfully cancelled❌.","Purchase Information");
                }
            }
            else
            {

                MessageBox.Show($"Purchase successfully cancelled❌.","Purchase Information");
            }
        }

    }
}
