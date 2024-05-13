using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio_asenjo_p2
{
    class LivroBLL
    {
        public static void conecta()
        {
            LivroDAL.conecta();
        }

        public static void desconecta()
        {
            LivroDAL.desconecta();
        }

        public static bool codigoExiste(Livro livro)
        {
            return LivroDAL.codigoExiste(livro);
        }

        public static void validaCodigo(Livro livro, String operacao)
        {
            Erro.setErro(false);

            if (livro.getCodigo().Equals(""))
            {
                Erro.setMensagem("O código é de preenchimento obrigatório!");
                return;
            }

            if (operacao == "READ")
            {
                LivroDAL.READ_livro(livro);
            }
            else if (operacao == "DELETE")
            {
                LivroDAL.DELETE_livro(livro);
            }
        }

        public static void validaDados(Livro livro, String operacao)
        {
            Erro.setErro(false);

            if (livro.getCodigo().Equals(""))
            {
                Erro.setMensagem("O código é de preenchimento obrigatório!");
                return;
            }
            if (livro.getTitulo().Equals(""))
            {
                Erro.setMensagem("O título é de preenchimento obrigatório!");
                return;
            }
            if (livro.getAutor().Equals(""))
            {
                Erro.setMensagem("O autor é de preenchimento obrigatório!");
                return;
            }
            if (livro.getEditora().Equals(""))
            {
                Erro.setMensagem("A Editora é de preenchimento obrigatório!");
                return;
            }
            if (livro.getAno().Equals(""))
            {
                Erro.setMensagem("O ano é de preenchimento obrigatório!");
                return;
            }

            try
            {
                int.Parse(livro.getAno());
            }
            catch (Exception)
            {
                Erro.setMensagem("O valor do ano deve ser numérico!");
                return;
            }

            if (int.Parse(livro.getAno()) <= 0)
            {
                Erro.setMensagem("O valor do ano deve ser numérico e positivo!");
                return;
            }

            if (operacao == "CREATE")
            {
                LivroDAL.CREATE_livro(livro);
            }
            else if (operacao == "UPDATE")
            {
                LivroDAL.UPDATE_livro(livro);
            }
        }
    }
}
