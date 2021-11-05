using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wfaTrabalhoFinal
{
    public partial class Form1 : Form
    {
        Pedido model = new Pedido();
        Fila fila = new Fila();
        List<NohFila> ListFila = new List<NohFila>();

        public Form1()
        {
            
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            txtNome.Text = txtSobrenome.Text = txtPedido.Text = txtPreco.Text = txtSenha.Text = "";
            btnCadastrar.Text = "Cadastrar";
            btnDeletar.Enabled = false;
            model.id = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Clear();
            SelectPedidos();

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                model.nome = txtNome.Text.Trim();
                model.sobrenome = txtSobrenome.Text.Trim();
                model.senha = Int32.Parse(txtSenha.Text.Trim());
                model.preco = float.Parse(txtPreco.Text.Trim());
                model.pedido = txtPedido.Text.Trim();
                using (PedidosContext db = new PedidosContext())
                {
                    if (model.id == 0) //INSERT
                    {
                        db.pedidos.Add(model);

                    }
                    else  //UPDATE
                    {
                        db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.SaveChanges();
                }
                Clear();
                SelectPedidos();
                MessageBox.Show("Pedido Cadastrado!");
            }
            catch (FormatException fe)
            {
                MessageBox.Show("Digite as informações!", "Há campos vazios");
            }
        }

        void SelectPedidos()
        {
            dvgPedidos.AutoGenerateColumns = false;
            using (PedidosContext db = new PedidosContext())
            {
                dvgPedidos.DataSource = db.pedidos.ToList<Pedido>();
            }
        }

        private void dvgPedidos_DoubleClick(object sender, EventArgs e)
        {
            if(dvgPedidos.CurrentRow.Index != -1)
            {
                model.id = Convert.ToInt32(dvgPedidos.CurrentRow.Cells["id"].Value);
                using (PedidosContext db = new PedidosContext())
                {
                    model = db.pedidos.Where(x => x.id == model.id).FirstOrDefault();
                    txtNome.Text = model.nome;
                    txtSobrenome.Text = model.sobrenome;
                    txtSenha.Text = Convert.ToString(model.senha);
                    txtPreco.Text = Convert.ToString(model.preco);
                    txtPedido.Text = model.pedido;
                }
                btnCadastrar.Text = "Atualizar";
                btnDeletar.Enabled = true;
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Você tem certeza que quer deletar?","Deletando...", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (PedidosContext db = new PedidosContext())
                {
                    var entry = db.Entry(model);
                    if (entry.State == System.Data.Entity.EntityState.Detached)
                        db.pedidos.Attach(model);
                    db.pedidos.Remove(model);
                    db.SaveChanges();
                    SelectPedidos();
                    Clear();
                    MessageBox.Show("Deletado com sucesso!");
                }
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                fila.insere(Int32.Parse(txtSenha.Text.Trim()));
                Console.WriteLine(fila.imprime());
                String mostra = fila.imprime();
                txtFila.Text = mostra;
            }
            catch(FormatException fe)
            {
                MessageBox.Show("Selecione um pedido!","Pedido não selecionado");
            }
            

        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            fila.retirar();
            Console.WriteLine(fila.imprime());
            String mostra = fila.imprime();
            txtFila.Text = mostra;
        }
    }
}
