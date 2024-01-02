using Data;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veterinaria_ui.Presentation
{
    public class LoginMenu
    {
        public LoginMenu(Login sharedLogin)
        {
            LoginManager loginManager = new LoginManager(sharedLogin, null); 
            int largura = 40;

            LoopDeco.ExibirLinhaDecorativa(largura);
            LoopDeco.ExibirLinhaCentralizada("Menu Login", largura);
            LoopDeco.ExibirLinhaDecorativa(largura);
            loginManager.GetLoginInfoFromUser();
            LoopDeco.ExibirLinhaDecorativa(largura);
        }
    }
}
