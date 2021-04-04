using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Best_Oil_with_MVP_pattern.Models;
using Best_Oil_with_MVP_pattern.Presenters;
using Best_Oil_with_MVP_pattern.View;
using Newtonsoft.Json;

namespace Best_Oil_with_MVP_pattern
{

    #region JSON        

    public static class JsonFileHelper
    {        
        public static void JSONSerialization<T>(List<T> petrolPaymentOperations, string fileName)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                var serializer = new JsonSerializer();

                using (var sw = new StreamWriter($"{fileName}.json"))
                {
                    using (var jw = new JsonTextWriter(sw))
                    {
                        jw.Formatting = Formatting.Indented;
                        serializer.Serialize(jw, petrolPaymentOperations);
                    }
                }
            }           
        }
        public static List<T> JSONDeSerialization<T>(string fileName)
        {
            List<T> list = null;

            if (File.Exists($"{fileName}.json"))
            {
                var serializer = new JsonSerializer();

                using (var sr = new StreamReader($"{fileName}.json"))
                {
                    using (var jr = new JsonTextReader(sr))
                    {
                        list = serializer.Deserialize<List<T>>(jr);
                    }
                }
            }

            return list;

        }

    }


    #endregion

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainView mainView = new MainView();
            MainViewPresenter mainViewPresenter = new MainViewPresenter(mainView);

            Application.Run(mainView);
        }
    }
}
