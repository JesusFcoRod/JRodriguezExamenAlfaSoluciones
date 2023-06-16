using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Alumno
    {

        public static ML.Result Add(ML.Alumno alumno)
        {
            ML.Result resultAdd = new ML.Result();

            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "[AlumnoAdd]";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = contex;
                        cmd.CommandText = query;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[3];

                        collection[0] = new SqlParameter("Nombre", System.Data.SqlDbType.VarChar);
                        collection[0].Value = alumno.Nombre;

                        collection[1] = new SqlParameter("Genero", System.Data.SqlDbType.Char);
                        collection[1].Value = alumno.Genero;

                        collection[2] = new SqlParameter("Edad", System.Data.SqlDbType.Int);
                        collection[2].Value = alumno.Edad;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();

                        int RowAffected = cmd.ExecuteNonQuery();

                        if (RowAffected > 0)
                        {
                            resultAdd.Correct = true;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                resultAdd.Exception = ex;
                resultAdd.ErrorMessage = ex.Message;
                resultAdd.Correct = false;
            }
            return resultAdd;
        }

        public static ML.Result Update(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "[AulmnoUpdate]";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = contex;
                        cmd.CommandText = query;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[4];

                        collection[0] = new SqlParameter("idAlumno", System.Data.SqlDbType.Int);
                        collection[0].Value = alumno.IdAlumno;

                        collection[1] = new SqlParameter("Nombre", System.Data.SqlDbType.VarChar);
                        collection[1].Value = alumno.Nombre;

                        collection[2] = new SqlParameter("Genero", System.Data.SqlDbType.Char);
                        collection[2].Value = alumno.Genero;

                        collection[3] = new SqlParameter("Edad", System.Data.SqlDbType.Int);
                        collection[3].Value = alumno.Edad;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();

                        int RowAffected = cmd.ExecuteNonQuery();

                        if (RowAffected > 0)
                        {
                            result.Correct = true;
                        }
                    }

                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Exception = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;

        }

        public static ML.Result Delete(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "[DeleteAlumno]";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = contex;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdAlumno", System.Data.SqlDbType.Int);
                        collection[0].Value = IdAlumno;


                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();

                        int RowAffected = cmd.ExecuteNonQuery();

                        if (RowAffected > 0)
                        {
                            result.Correct = true;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Exception = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                string query = "[AlumnoGetAll]";

                using (SqlConnection contex = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = contex;
                        cmd.CommandText = query;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        DataTable TablaAlumno = new DataTable();
                        SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
                        Adapter.Fill(TablaAlumno);

                        if (TablaAlumno.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow Row in TablaAlumno.Rows)
                            {
                                ML.Alumno alumno = new ML.Alumno();

                                alumno.IdAlumno = int.Parse(Row[0].ToString());
                                alumno.Nombre = Row[1].ToString();
                                alumno.Genero = char.Parse(Row[2].ToString());
                                alumno.Edad = int.Parse(Row[3].ToString());

                                result.Objects.Add(alumno);

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

        public static ML.Result GetById(int IdAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "[AlumnoGetById]";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = contex;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("IdAlumno", System.Data.SqlDbType.Int);
                        collection[0].Value = IdAlumno;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();

                        DataTable TablaAlumno = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(TablaAlumno);

                        if (TablaAlumno.Rows.Count > 0)
                        {
                            result.Object = new object();
                            DataRow row = TablaAlumno.Rows[0];

                            ML.Alumno alumno = new ML.Alumno();

                            alumno.IdAlumno = int.Parse(row[0].ToString());
                            alumno.Nombre = row[1].ToString();
                            alumno.Genero = char.Parse(row[2].ToString());
                            alumno.Edad = int.Parse(row[3].ToString());

                            result.Object = alumno;

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
                result.Correct = false;
                result.Exception = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;

        }

    }
}

