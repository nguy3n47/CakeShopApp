using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop
{
    public class Product : INotifyPropertyChanged
    {
        // Properties
        public string Name { get; set; }
        public string Id { get; set; }
        public long Price { get; set; }
        public System.DateTime Date { get; set; }
        public int InitialAmount { get; set; }
        public int CurrentAmount { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string ProductType { get; set; }
        public string ImagePath { get; set; }
        public string Foreground { get; set; }
        public string Background { get; set; }
        public long Total { get; set; }
        public BindingList<string> listImages { get; set; }
        public virtual ProductType ProductTypes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
