using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Beca
    {
        public static ML.Result Add(ML.Beca beca)
        {
            ML.Result resultAdd = new ML.Result();

            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "[BecaAdd]";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = contex;
                        cmd.CommandText = query;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("NombreBeca", System.Data.SqlDbType.VarChar);
                        collection[0].Value = beca.NombreBeca;


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

        public static ML.Result Update(ML.Beca beca)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "[BecaUpdate]";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = contex;
                        cmd.CommandText = query;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[2];

                        collection[0] = new SqlParameter("IdBeca", System.Data.SqlDbType.Int);
                        collection[0].Value = beca.IdBeca;

                        collection[1] = new SqlParameter("NombreBeca", System.Data.SqlDbType.VarChar);
                        collection[1].Value = beca.NombreBeca;

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

        public static ML.Result Delete(int IdBeca)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection contex = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    string query = "[BecaDelete]";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = contex;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdBeca", System.Data.SqlDbType.Int);
                        collection[0].Value = IdBeca;


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
                string query = "[BecaGetAll]";

                using (SqlConnection contex = new SqlConnection(DL.Conexion.GetConnection()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = contex;
                        cmd.CommandText = query;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        DataTable TablaBeca = new DataTable();
                        SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
                        Adapter.Fill(TablaBeca);

                        if (TablaBeca.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow Row in TablaBeca.Rows)
                            {
                                ML.Beca beca = new ML.Beca();

                                beca.IdBeca = int.Parse(Row[0].ToString());
                                beca.NombreBeca = Row[1].ToString();

                                result.Objects.Add(beca);

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

        public static ML.Result GetById(int IdBeca)
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
                        collection[0] = new SqlParameter("IdBeca", System.Data.SqlDbType.Int);
                        collection[0].Value = IdBeca;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();

                        DataTable TablaBeca = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(TablaBeca);

                        if (TablaBeca.Rows.Count > 0)
                        {
                            result.Object = new object();
                            DataRow row = TablaBeca.Rows[0];

                            ML.Beca beca = new ML.Beca();

                            beca.IdBeca = int.Parse(row[0].ToString());
                            beca.NombreBeca = row[1].ToString();


                            result.Object = beca;
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
