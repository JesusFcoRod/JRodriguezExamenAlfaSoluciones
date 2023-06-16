using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoBeca
    {

        public static ML.Result BecasAsignadasGetByIdAlumno(int IdAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                string query = "[BecasAsignadasGetByAlumno]";

                using (SqlConnection contex = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = contex;
                        cmd.CommandText = query;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("IdAlumno", System.Data.SqlDbType.Int);
                        collection[0].Value = IdAlumno;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();

                        DataTable TableBecas = new DataTable();
                        SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
                        Adapter.Fill(TableBecas);

                        if (TableBecas.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow Row in TableBecas.Rows)
                            {
                                ML.AlumnoBeca alumnobeca = new ML.AlumnoBeca();

                                alumnobeca.IdAlumnoBeca = int.Parse(Row[0].ToString());

                                alumnobeca.Beca = new ML.Beca();
                                alumnobeca.Beca.IdBeca = int.Parse(Row[1].ToString());
                                alumnobeca.Beca.NombreBeca = Row[2].ToString();

                                alumnobeca.Alumno = new ML.Alumno();
                                alumnobeca.Alumno.IdAlumno = int.Parse(Row[3].ToString());
                                alumnobeca.Alumno.Nombre = Row[4].ToString();

                                result.Objects.Add(alumnobeca);
                            }
                            result.Correct = true;

                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;
            }
            return result;
        }
    }
}
