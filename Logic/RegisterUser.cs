﻿using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    internal class RegisterUser
    {
       //comentario
        public void GetRegisterInfoFromUser()
        {
            Login login = new Login();
            Console.WriteLine("Bem-vindo ao registo");
            Console.WriteLine("Insira o seu username: ");
            login.user = Console.ReadLine();
            while (string.IsNullOrEmpty(login.user)) // Verifica se o username é nulo ou vazio
            {
                Console.WriteLine("O username não pode ser vazio. Por favor, insira um nome de username: ");
                login.user = Console.ReadLine();
            }
            

            Console.WriteLine("Insira a sua password: ");
            login.password = Console.ReadLine();
            if (login.password == null)
            {
                Console.WriteLine("A sua password não pode ser vazia. Por favor, insira uma password: ");
                login.password = Console.ReadLine();
                if(login.password.Length != 8)
                {
                    Console.WriteLine("A password é muito fraca. Por favor insira outra password com pelo menos minimo 8 caracteres:");
                    login.password = Console.ReadLine();
                }
                //simbolos validacao
            }
            else
            {
                login.user = Console.ReadLine();
            }
        }
    }
}
