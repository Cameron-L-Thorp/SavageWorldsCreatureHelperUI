using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavageWorldsHelperUWPCode
{
    public class CreatureRetriever : INotifyPropertyChanged
    {
        public string CreatureName { get; set; }
        public int Size { get; set; }
        public string Notes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyCanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class TestCreature
    {
        public string CreatureName { get; set; }
        public int Size { get; set; }
        public string Notes { get; set; }

        public TestCreature()
        {
            CreatureName = "Ricky";
            Size = 0;
            Notes = "This guy knows how to code.";
        }
    }
    public class TestCreatures : ObservableCollection<TestCreature>
    {
        public TestCreatures()
        {
            Add(new TestCreature());
        }
    }
}
