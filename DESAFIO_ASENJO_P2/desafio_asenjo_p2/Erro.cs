using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio_asenjo_p2
{
    class Erro
    {
        private static String mensagem;
        private static bool erro;

        public static void setMensagem(String _mensagem)
        {
            erro = true;
            mensagem = _mensagem;
        }
        public static void setErro(bool _erro) { erro = _erro; }
        public static String getMensagem() { return mensagem; }
        public static bool getErro() { return erro; }
    }
}
