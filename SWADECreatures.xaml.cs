using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SavageWorldsHelperUWPCode
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SWADECreatures : Page
    {
        public SWADECreatures()
        {
            this.InitializeComponent();
            SWADECreatureList.ItemsSource = GetCreatureSWADE("Data Source = (MSSQLSERVER)CAMERONDESKTOP; Initial Catalog = SavageWorldCreatures; Integrated Security = SSPI");
            //Dictionary<string, TestCreature> rMan = new Dictionary<string, TestCreature>();
            //rMan.Add("ricky", new TestCreature());
            //this.SWADECreatureList
        }
        private void ReturnSWADE(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdventureEditionPage));
        }
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
        public ObservableCollection<CreatureRetriever> GetCreatureSWADE(string connectionString)
        {
            const string GetCreatureQuery = "SELECT d.CreatureName, d.Size, d.Notes" /*, a.Agility, a.Smarts, a.Spirit, a.Strength, a.Vigor," +
                "s.Academics, s.Athletics, s.Battle, s.Boating, s.CommonKnowledge, s.Driving, s.Electronics, s.Faith," +
                "s.Fighting, s.Focus, s.Gambling, s.Hacking, s.Healing, s.Intimidation, s.Language, s.Notice, s.Occult," +
                "s.Performance, s.Persuasion, s.Piloting, s.Psionics, s.Repair, s.Research, s.Riding, s.Science, s.Shooting," +
                "s.Spellcasting, s.Stealth, s.Survival, s.Taunt, s.Thievery, s.WeirdScience, ds.Pace, ds.Parry, ds.Toughness"*/ +
                "FROM dbo.CreatureDescriptions AS d LEFT JOIN dbo.CreatureAttributes AS a ON d.CreatureName = a.CreatureName" +
                "LEFT JOIN dbo.CreatureSkills AS s ON d.CreatureName = s.CreatureName" +
                "LEFT JOIN dbo.CreatureDerivedStats AS ds ON d.CreatureName = ds.CreatureName";
            var creaturesSWADE = new ObservableCollection<CreatureRetriever>();
            try
            {
                SqlConnection context = new SqlConnection();
                context.ConnectionString = connectionString;
                context.Open();
                
                    if (context.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = context.CreateCommand())
                        {
                            cmd.CommandText = GetCreatureQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var creatureSWADE = new CreatureRetriever();
                                    creatureSWADE.CreatureName = reader.GetString(0);
                                    creatureSWADE.Size = reader.GetInt32(1);
                                    creatureSWADE.Notes = reader.GetString(2);
                                    creaturesSWADE.Add(creatureSWADE);
                                }
                            }
                        }
                    }
                
                return creaturesSWADE;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }

        
    }
}
