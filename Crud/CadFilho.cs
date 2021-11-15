using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Crud
{
    public partial class CadFilho : Form
    {

        public static SqlConnection connection = new SqlConnection ( "Data Source=SQL5063.site4now.net;Initial Catalog=db_a7bda0_escola;User Id=db_a7bda0_escola_admin;Password=deidara337" );
        private int id_funcionario;
        private bool alteracao;
        public CadFilho ( int idfuncionario,bool altera)
        {
            id_funcionario = idfuncionario;
            alteracao = altera;

            InitializeComponent ( );
        }

        private void button1_Click ( object sender, EventArgs e )
        {
            try
            {
                if (alteracao)
                {

                    if (String.IsNullOrEmpty ( txtNome.Text ) || String.IsNullOrEmpty ( txtdateNasc.Text ))
                        MessageBox.Show ( "Por favor preencha todos os campos!" );


                    string insertPessoa = "Update tb_funcionario_dependente_crud set Nome='" + txtNome.Text + "',data_nasc='" + txtdateNasc.Text + "' where id_funcionario=" + Convert.ToInt32 ( id_funcionario );

                    SqlCommand cmd1 = new SqlCommand ( insertPessoa, connection );

                    connection.Open ( );

                    cmd1.ExecuteNonQuery ( );
                    connection.Close ( );
                    MessageBox.Show ( "Filho alterado com sucesso!" );
                    this.Close ( );

                }
                else
                {
                    if (String.IsNullOrEmpty ( txtNome.Text ) || String.IsNullOrEmpty ( txtdateNasc.Text ))
                        MessageBox.Show ( "Por favor preencha todos os campos!" );


                    string insertPessoa = "INSERT INTO tb_funcionario_dependente_crud(Nome,data_nasc,id_funcionario) VALUES('" + txtNome.Text+"','"+(txtdateNasc.Text)+"',"+id_funcionario+")";

                    SqlCommand cmd1 = new SqlCommand ( insertPessoa, connection );

                    connection.Open ( );

                    cmd1.ExecuteNonQuery ( );
                    connection.Close ( );

                    txtNome.Clear ( );
                    txtdateNasc.Clear ( );
                    MessageBox.Show ( "Filho cadastrado com sucesso!" );

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show ( "Por favor, contate um administrador do sistema pois obtivemos um erro!" );
                //throw;
            }
        }

        private void CadFilho_Load ( object sender, EventArgs e )
        {
            if (alteracao)
            {
                try
                {
                    DataSet ds = new DataSet ( );
                    string selectsql = "SELECT * FROM tb_funcionario_dependente_crud where id_funcionario=" + Convert.ToInt32 ( id_funcionario );
                    SqlCommand cmd = new SqlCommand ( selectsql, connection );
                    connection.Open ( );
                    SqlDataAdapter da = new SqlDataAdapter ( cmd );
                    da.Fill ( ds );
                    connection.Close ( );

                    if (ds.Tables[0].Rows.Count != 0)
                    {

                        txtNome.Text = ds.Tables[0].Rows[0]["Nome"].ToString ( );
                        txtdateNasc.Text = ds.Tables[0].Rows[0]["data_nasc"].ToString ( );
                    }
                }
                catch (Exception ex)
                {

                    //throw;
                }
            }
        }
    }
}
