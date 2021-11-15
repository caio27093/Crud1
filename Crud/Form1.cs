using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud
{
    public partial class Form1 : Form
    {
        public static SqlConnection connection = new SqlConnection ( "Data Source=SQL5063.site4now.net;Initial Catalog=db_a7bda0_escola;User Id=db_a7bda0_escola_admin;Password=deidara337" );
        public bool fg_pessoa = false;
        public Form1 ( )
        {
            InitializeComponent ( );
        }

        private void button2_Click ( object sender, EventArgs e )
        {
            FormularioFuncionario funcRel = new FormularioFuncionario ( );
            funcRel.Show ( );
        }

        private void button1_Click ( object sender, EventArgs e )
        {

            try
            {

                if (String.IsNullOrEmpty ( txtid.Text ))
                {//cadastro

                    if (String.IsNullOrEmpty ( txtNome.Text ) || String.IsNullOrEmpty ( txtdateNasc.Text ) || String.IsNullOrEmpty ( txtSalario.Text ))
                        MessageBox.Show ( "Por Favor insira todas as informações necessárias" );
                    if (Convert.ToInt32 ( txtSalario.Text ) <= 0)
                        MessageBox.Show ( "O salario deve ser maior que 0" );


                    string insertPessoa = "INSERT INTO tb_funcionario_crud (Nome,data_nasc,salario,fg_pessoafilho) VALUES (@Nome,@data_nasc,@salario,@fg_pessoafilho)";

                    SqlCommand cm = new SqlCommand ( insertPessoa, connection );
                    cm.Parameters.Add ( new SqlParameter ( "@Nome", txtNome.Text ) );
                    cm.Parameters.Add ( new SqlParameter ( "@data_nasc", Convert.ToDateTime( txtdateNasc.Text) ) );
                    cm.Parameters.Add ( new SqlParameter ( "@salario", txtSalario.Text ) );
                    cm.Parameters.Add ( new SqlParameter ( "@fg_pessoafilho", chkFilhos.Checked ) );

                    connection.Open ( );

                    cm.ExecuteNonQuery ( );
                    connection.Close ( );

                    if (chkFilhos.Checked)
                    {

                        DataSet ds = new DataSet ( );
                        string selectsql = "SELECT Top 1 Id FROM tb_funcionario_crud ORDER BY Id DESC";
                        SqlCommand cmd = new SqlCommand ( selectsql, connection );
                        connection.Open ( );
                        SqlDataAdapter da = new SqlDataAdapter ( cmd );
                        da.Fill ( ds );
                        connection.Close ( );

                        MessageBox.Show ( "Funcionario cadastrado com sucesso!" );
                        CadFilho cf = new CadFilho ( Convert.ToInt32 ( ds.Tables[0].Rows[0]["Id"].ToString ( ) ),false );
                        cf.Show ( );

                        txtSalario.Clear ( );
                        txtNome.Clear ( );
                        txtid.Clear ( );
                        txtdateNasc.Clear ( );
                        chkFilhos.Checked = false;
                    }
                    else
                    {
                        MessageBox.Show ( "Funcionario cadastrado com sucesso!" );
                        txtSalario.Clear ( );
                        txtNome.Clear ( );
                        txtid.Clear ( );
                        txtdateNasc.Clear ( );
                        chkFilhos.Checked = false;
                    }

                }
                else
                {//consulta 


                    if (!fg_pessoa)
                    {
                        DataSet ds = new DataSet ( );
                        string selectsql = "SELECT * FROM tb_funcionario_crud where Id="+Convert.ToInt32(txtid.Text);
                        SqlCommand cmd = new SqlCommand ( selectsql, connection );
                        connection.Open ( );
                        SqlDataAdapter da = new SqlDataAdapter ( cmd );
                        da.Fill ( ds );
                        connection.Close ( );

                        if (ds.Tables[0].Rows.Count != 0)
                        {

                            txtNome.Text = ds.Tables[0].Rows[0]["Nome"].ToString ( );
                            txtSalario.Text = ds.Tables[0].Rows[0]["salario"].ToString ( );
                            txtdateNasc.Text = ds.Tables[0].Rows[0]["data_nasc"].ToString ( );
                            chkFilhos.Checked = Convert.ToBoolean ( ds.Tables[0].Rows[0]["fg_pessoafilho"] );
                        }
                        else
                        {
                            MessageBox.Show ( "Funcionario Inexistente" );
                            return;
                        }

                        fg_pessoa = true;
                    }
                    else
                    {

                        if (Convert.ToInt32 ( txtSalario.Text.Replace(",",string.Empty) ) <= 0)
                            MessageBox.Show ( "O salario deve ser maior que 0" );

                        string insertPessoa = "Update tb_funcionario_crud set Nome='"+txtNome.Text+ "',data_nasc='" + txtdateNasc.Text + "',salario='" + Convert.ToInt32(txtSalario.Text.Replace(",",".")) + "',fg_pessoafilho='" + chkFilhos.Checked + "' where Id=" + Convert.ToInt32 (txtid.Text);

                        SqlCommand cmd1 = new SqlCommand ( insertPessoa, connection );

                        connection.Open ( );

                        cmd1.ExecuteNonQuery ( );
                        connection.Close ( );
                        fg_pessoa = false;

                        if (chkFilhos.Checked)
                        {

                            txtSalario.Clear ( );
                            txtNome.Clear ( );
                            txtdateNasc.Clear ( );
                            chkFilhos.Checked = false;
                            MessageBox.Show ( "Funcionario Alterado com sucesso" );
                            CadFilho cf = new CadFilho ( Convert.ToInt32 (txtid.Text), true );
                            cf.Show ( );
                        }
                        else
                        {

                            txtSalario.Clear ( );
                            txtNome.Clear ( );
                            txtid.Clear ( );
                            txtdateNasc.Clear ( );
                            chkFilhos.Checked = false;
                            MessageBox.Show ( "Funcionario Alterado com sucesso" );
                        }


                    }


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show ( "Ocorreu um problema, por favor contate o administrador do sistema" );
                //throw;
            }
        }

        private void button3_Click ( object sender, EventArgs e )
        {
            try
            {

                if ( txtid.Text == null)
                    MessageBox.Show ( "Insira o Id do funcionario antes de excluir" );

                string insertPessoa = "delete from tb_funcionario_crud where Id=" +txtid.Text;

                SqlCommand cmd1 = new SqlCommand ( insertPessoa, connection );

                connection.Open ( );

                cmd1.ExecuteNonQuery ( );
                connection.Close ( );
                MessageBox.Show ("Funcionario excluido com sucesso" );
            }
            catch (Exception ex)
            {

                //throw;
            }
        }

        private void txtSalario_KeyPress ( object sender, KeyPressEventArgs e )
        {
            if (!Char.IsDigit(e.KeyChar)&&e.KeyChar!=(char)8)
            {
                e.Handled = true;
            }
        }
    }
}
