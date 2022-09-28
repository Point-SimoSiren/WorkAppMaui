using Newtonsoft.Json;
using System.Collections.ObjectModel;
using WorkAppMaui.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WorkAppMaui;

public partial class EmployeePage : ContentPage
{

    public EmployeePage()
	{
	
        // Muuttujan alustaminen
        ObservableCollection<Employee> dataa = new ObservableCollection<Employee>();

            InitializeComponent();

            // Kutsutaan alempana määriteltyä funktiota kun ohjelma käynnistyy
            LoadDataFromRestAPI();


            async void LoadDataFromRestAPI()
            {

                // Annetaan latausilmoitus
                emp_lataus.Text = "Ladataan työntekijöitä...";

                try
                {
                   
                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("https://workappbackendssi.azurewebsites.net/");
                    string json = await client.GetStringAsync("api/employees");

                    IEnumerable<Employee> employees = JsonConvert.DeserializeObject<Employee[]>(json);
                    // dataa -niminen observableCollection on alustettukin jo ylhäällä päätasolla että hakutoiminto,
                    // pääsee siihen käsiksi.
                    // asetetaan sen sisältö ensi kerran tässä.
                    dataa = new ObservableCollection<Employee>(employees);
                   
                    // Asetetaan datat näkyviin xaml tiedostossa olevalle listalle
                    employeeList.ItemsSource = dataa;

                    // Tyhjennetään latausilmoitus label
                    emp_lataus.Text = "";

                }

                catch (Exception e)
                {
                    await DisplayAlert("Virhe", e.Message.ToString(), "SELVÄ!");

                }
            }
        }
    }




