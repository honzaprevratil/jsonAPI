using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace jsonAPI
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CSVhelper csvHelper = new CSVhelper();
        private List<JokeFromApi> Jokes { get; set; }
        private Random RNG = new Random();

        public MainWindow()
        {
            InitializeComponent();

            Jokes = csvHelper.ReadFile();

            if (Jokes.Count == 0)
            {
                for ( int i = 0; i <= 20; i++ )
                {
                    JokeFromApi loadedJoke = GetJoke().Result;
                    csvHelper.WriteAddOne(loadedJoke);
                    Jokes.Add(loadedJoke);
                }
            }

            JokeLabel.Content = Jokes[RNG.Next(0, 20)].ToString();

            Show();
        }

        public async Task<JokeFromApi> GetJoke()
        {
            // Vytvoření klienta
            HttpClient client = new HttpClient();
            
            // Odeslání dotazu na API + pamaretr pro výpis z kategorie dev
            var response = await client.GetAsync("https://api.icndb.com/jokes/random");
            
            // Získání odpovědi v Json    
            string json = await response.Content.ReadAsStringAsync();

            // Deserializace na JokeFromApi objekt
            dynamic Json = JsonConvert.DeserializeObject(json);
            string innerJson = JsonConvert.SerializeObject(Json.value);

            JokeFromApi joke = JsonConvert.DeserializeObject<JokeFromApi>(innerJson);
            joke.date = DateTime.Now;

            return joke;
        }

        private void Random_Joke(object sender, RoutedEventArgs e)
        {
            JokeLabel.Content = Jokes[RNG.Next(0, 20)].ToString();
        }
    }
}
