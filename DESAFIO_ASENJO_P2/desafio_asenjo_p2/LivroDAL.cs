using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace desafio_asenjo_p2
{
    class LivroDAL
    {
        //Alterar conforme necessario, arquivo do banco de dados está na pasta <banco_de_dados> do projeto. E senha do firebird é criada na instalação do produto
        private static String caminhoArquivoBD = @"";
        private static String senhaContaFirebird = "";

        private static String strConexao = $@"DataSource=localhost; Database={caminhoArquivoBD}\LIVRARIABD.FDB;
        username= SYSDBA; password = {senhaContaFirebird}";

        private static FbConnection conn = new FbConnection(strConexao);
        private static FbCommand strSQL;
        private static FbDataReader result;

        public static void conecta()
        {
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                Erro.setMensagem("Problemas ao se conectar ao Banco de Dados");
            }
        }

        public static void desconecta()
        {
            conn.Close();
        }

        public static bool codigoExiste(Livro livro)
        {
            String codigoLivro = livro.getCodigo();
            String aux = "SELECT CD_CODIGO FROM LIVRO";

            strSQL = new FbCommand(aux, conn);

            result = strSQL.ExecuteReader();

            while (result.Read())
            {
                if (result[0].ToString() == codigoLivro)
                {
                    return true;
                }
            }
            return false;
        }

        public static void CREATE_livro(Livro livro)
        {
            String aux = "INSERT INTO LIVRO(CD_CODIGO,NM_TITULO,NM_AUTOR,NM_EDITORA,AA_ANO) values (@codigo,@titulo,@autor,@editora,@ano)";

            strSQL = new FbCommand(aux, conn);
            strSQL.Parameters.Add("@codigo", OleDbType.VarChar).Value = livro.getCodigo();
            strSQL.Parameters.Add("@titulo", OleDbType.VarChar).Value = livro.getTitulo();
            strSQL.Parameters.Add("@autor", OleDbType.VarChar).Value = livro.getAutor();
            strSQL.Parameters.Add("@editora", OleDbType.VarChar).Value = livro.getEditora();
            strSQL.Parameters.Add("@ano", OleDbType.VarChar).Value = livro.getAno();

            strSQL.ExecuteNonQuery();
        }

        public static void READ_livro(Livro livro)
        {
            String aux = "SELECT * FROM LIVRO WHERE CD_CODIGO = @codigo";

            strSQL = new FbCommand(aux, conn);
            strSQL.Parameters.Add("@codigo", OleDbType.VarChar).Value = livro.getCodigo();

            result = strSQL.ExecuteReader();

            Erro.setErro(false);

            if (result.Read())
            {
                livro.setTitulo(result.GetString(1));
                livro.setAutor(result.GetString(2));
                livro.setEditora(result.GetString(3));
                livro.setAno(result.GetString(4));
            }
            else
            {
                Erro.setMensagem("Livro não cadastrado.");
            }
        }

        public static void UPDATE_livro(Livro livro)
        {
            String aux = "UPDATE LIVRO SET NM_TITULO = @titulo, NM_AUTOR = @autor, NM_EDITORA = @editora, AA_ANO = @ano " +
                "WHERE CD_CODIGO = @codigo";

            strSQL = new FbCommand(aux, conn);
            strSQL.Parameters.Add("@titulo", OleDbType.VarChar).Value = livro.getTitulo();
            strSQL.Parameters.Add("@autor", OleDbType.VarChar).Value = livro.getAutor();
            strSQL.Parameters.Add("@editora", OleDbType.VarChar).Value = livro.getEditora();
            strSQL.Parameters.Add("@ano", OleDbType.VarChar).Value = livro.getAno();
            strSQL.Parameters.Add("@codigo", OleDbType.VarChar).Value = livro.getCodigo();

            Erro.setErro(false);

            try
            {
                strSQL.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Erro.setMensagem("Não foi possivel modificar livro. Codigo não encontrado no banco de dados.");
            }
        }

        public static void DELETE_livro(Livro livro)
        {
            String aux = "DELETE FROM LIVRO WHERE CD_CODIGO = @codigo";

            strSQL = new FbCommand(aux, conn);
            strSQL.Parameters.Add("@codigo", FbDbType.VarChar).Value = livro.getCodigo();

            Erro.setErro(false);

            try
            {
                int registrosAfetados = strSQL.ExecuteNonQuery();

                if (registrosAfetados == 0)
                {
                    Erro.setMensagem("Não foi possível deletar livro. Código não encontrado no banco de dados.");
                }
            }
            catch (Exception ex)
            {
                Erro.setMensagem("Ocorreu um erro ao tentar deletar o livro: " + ex.Message);
            }
        }

    }
}
