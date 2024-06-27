using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateTech
{
    internal class Food
    {
        string item;
        string category;
        double price;
        int number;
        string size;
       

        public Food(string item, string category, double price, int number, string size)
        {
            this.item = item;
            this.category = category;
            this.price = price;
            this.number = number;
            this.size = size;
           
        }
        public string Getname()
        {
            return item;
        }
        public string Getcategory()
        {
            return category;

        }
        public double Getprice()
        {
            return price;
        }
        public int Getnumber()
        {
            return number;
        }
        public string Getsize()
        {
            return size;
        }
        public void Setname(string item)
        {
            this.item = item;
        }
        public void Setcategory(string category)
        {
            this.category = category;
        }
        public void Setprice(double price)
        {
            this.price = price;
        }
        public void Setnumber(int number)
        {
            this.number = number;
        }
        public void Setsize(string size)
        {
            this.size = size;
        }
       


    }
}
