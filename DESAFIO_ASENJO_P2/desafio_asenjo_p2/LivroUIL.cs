using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace desafio_asenjo_p2
{
    public partial class LivroUIL : Form
    {
        Livro livro = new Livro();

        public LivroUIL()
        {
            InitializeComponent();

            Load += conecta;
            FormClosing += desconecta;
        }

        private void conecta(object sender, EventArgs e)
        {
            LivroBLL.conecta();

            if (Erro.getErro())
            {
                MessageBox.Show(Erro.getMensagem());
            }
        }

        private void desconecta(object sender, EventArgs e)
        {
            LivroBLL.desconecta();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            livro.setCodigo(tbCodigo.Text);
            livro.setTitulo(tbTitulo.Text);
            livro.setAutor(tbAutor.Text);
            livro.setEditora(tbEditora.Text);
            livro.setAno(tbAno.Text);

            if (LivroBLL.codigoExiste(livro))
            {
                String header = "Livro já existe";
                String mensagem = "O código digitado já pertence a um livro. Deseja alterar livro?";

                DialogResult dr = MessageBox.Show(mensagem, header, MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    LivroBLL.validaDados(livro, "UPDATE");

                    if (Erro.getErro())
                    {
                        MessageBox.Show(Erro.getMensagem());
                    }
                    else
                    {
                        MessageBox.Show("Livro alterado com sucesso.");
                    }
                }
            }
            else
            {
                LivroBLL.validaDados(livro, "CREATE");

                if (Erro.getErro())
                {
                    MessageBox.Show(Erro.getMensagem());
                }
                else
                {
                    MessageBox.Show("Livro inserido com sucesso.");
                }
            }
        }

        private void btnLer_Click(object sender, EventArgs e)
        {
            livro.setCodigo(tbCodigo.Text);

            LivroBLL.validaCodigo(livro, "READ");

            if (Erro.getErro())
            {
                MessageBox.Show(Erro.getMensagem());
            }
            else
            {
                tbTitulo.Text = livro.getTitulo();
                tbAutor.Text = livro.getAutor();
                tbEditora.Text = livro.getEditora();
                tbAno.Text = livro.getAno();
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            tbCodigo.Clear();
            tbTitulo.Clear();
            tbAutor.Clear();
            tbEditora.Clear();
            tbAno.Clear();
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            livro.setCodigo(tbCodigo.Text);

            String header = "Deletar livro";
            String mensagem = "Tem certeza que deseja deletar esse livro?";

            DialogResult dr = MessageBox.Show(mensagem, header, MessageBoxButtons.YesNo);
            
            if (dr == DialogResult.Yes)
            {
                LivroBLL.validaCodigo(livro, "DELETE");

                if (Erro.getErro())
                {
                    MessageBox.Show(Erro.getMensagem());
                }
                else
                {
                    MessageBox.Show("Livro deletado com sucesso.");
                }
            }
        }
    }
}
