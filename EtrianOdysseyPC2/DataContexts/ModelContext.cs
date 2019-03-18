using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace EtrianOdysseyPC2.DataContexts
{
    class ModelContext
    {
        public Grid MainGrid { get; set; }

        public Grid Grid { get; set; }
#if DEBUG
        public TextBox TextBox { get; set; }
#endif

        public ModelContext()
        {
            Grid = new Grid();
            MainGrid = new Grid();

#if DEBUG
            MainGrid.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(6, System.Windows.GridUnitType.Star) });
            MainGrid.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star) });

            Grid.Background = Brushes.Wheat;

            Grid.SetRow(Grid, 0);
            MainGrid.Children.Add(Grid);

            TextBox = new TextBox();
            TextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            Grid.SetRow(TextBox, 1);
            MainGrid.Children.Add(TextBox);
#else
            MainGrid.RowDefinitions.Add(new RowDefinition { Height = new System.Windows.GridLength(1, System.Windows.GridUnitType.Star) });
            Grid.SetRow(Grid, 0);
            MainGrid.Children.Add(Grid);
#endif
        }
    }
}
