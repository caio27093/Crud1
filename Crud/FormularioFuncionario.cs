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
    public partial class FormularioFuncionario : Form
    {
        public FormularioFuncionario ( )
        {
            InitializeComponent ( );
        }

        public static SqlConnection connection = new SqlConnection ( "Data Source=SQL5063.site4now.net;Initial Catalog=db_a7bda0_escola;User Id=db_a7bda0_escola_admin;Password=deidara337" );
        private void FormularioFuncionario_Load ( object sender, EventArgs e )
        {
            try
            {
                DataSet ds = new DataSet ( );
                string selectsql = "select tb_funcionario_crud.Id,tb_funcionario_crud.Nome,tb_funcionario_crud.salario,tb_funcionario_dependente_crud.Nome AS 'Nome Filho',tb_funcionario_dependente_crud.data_nasc as 'Nascimento Filho' from tb_funcionario_crud left JOIN tb_funcionario_dependente_crud ON tb_funcionario_crud.Id = tb_funcionario_dependente_crud.id_funcionario";
                SqlCommand cmd = new SqlCommand ( selectsql, connection );
                connection.Open ( );
                SqlDataAdapter da = new SqlDataAdapter ( cmd );
                da.Fill ( ds );


                dataGridView1.DataSource = ds.Tables[0];
                connection.Close ( );
            }
            catch (Exception ex)
            {

                //throw;
            }
        }

        private void button1_Click ( object sender, EventArgs e )
        {
            
        }
    }
}
