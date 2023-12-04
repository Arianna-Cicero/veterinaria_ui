using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic
{
    public class LoginManager
    {
        private List<string> UsernamesExistentes = new List<string>();
        public void GetLoginInfoFromUser (Login login)
        {
            int opcao;
            Console.WriteLine("Bem-vindo a clinica");
            Console.WriteLine("Login");
            Console.WriteLine("Caso pretenda efetuar o registo pressione 1, caso pretenda continuar pressione 2");
            opcao = Convert.ToInt32(Console.ReadLine());
            if (opcao == 1) 
            {
                Console.WriteLine("Insira o username que pretende utilizar");
                login.user = Console.ReadLine();
                if (GetValidUsername(login))
                {
                    //TODO
                }
                
            }
            if (opcao == 2)
            {
                Console.WriteLine("Insira o seu username");
                login.user = Console.ReadLine();
                //TODO
                //if (login.user)
                //{

                //}
                //Console.WriteLine("Insira a  sua password");
                //if (IsValidPassword(login))
                //{
                //    //mandar

                //}
            }
        }

        private bool IsValidPassword(Login login)
        {
            if (login.password.Length < 8)
            {
                Console.WriteLine("Password must be at least 8 characters long.");
                return false;
            }

            if (!Regex.IsMatch(login.password, @"][!@#$%^&*(),.?\:{ }|<>"))
            {
                Console.WriteLine("Password must contain at least one special character.");
                return false;
            }
            return true;
        }

        private bool GetValidUsername(Login login)
        {
            string newUsername;
            do
            {
                newUsername = Console.ReadLine();

                if (string.IsNullOrEmpty(newUsername))
                {
                    Console.WriteLine("Username não pode estar vazio. Insira o username novamente:");
                    login.user = Console.ReadLine();
                }
                else if (UsernamesExistentes.Contains(newUsername))
                {
                    Console.WriteLine("Username já existe. Escolha outro:");
                    login.password = Console.ReadLine();
                }
            } while (string.IsNullOrEmpty(newUsername) || UsernamesExistentes.Contains(newUsername));

            UsernamesExistentes.Add(newUsername);
            login.user = newUsername;

            return true;
        }
    }
}

