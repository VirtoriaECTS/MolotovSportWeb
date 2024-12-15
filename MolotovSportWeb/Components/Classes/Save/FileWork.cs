
using Microsoft.AspNetCore.Hosting.Server;
using MolotovSportWeb.Components.Classes.Servers;
using System.IO;
using System.Text;
using static MudBlazor.Defaults;

namespace MolotovSportWeb.Components.Classes.Save
{

    public class FileWork
    {

        public void Write(UserStateService userStateService)
        {
            StreamWriter sw = File.CreateText("user.txt");

            string text = userStateService.AuthState + "|" + userStateService.UserId + "|" + userStateService.UserRole;
            sw.WriteLine(text);
            sw.Close();
        }

        public string[] Read()
        {
            

            string text = "";

            if (File.Exists("user.txt"))
            {
                StreamReader sr1 = File.OpenText("user.txt");

                while(!sr1.EndOfStream)
                {
                    text += sr1.ReadLine();
                }

                sr1.Close();
            }

            string[] array = text.Split("|");

            return array;
        }
    }
}
