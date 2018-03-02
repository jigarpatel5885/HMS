using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
namespace HMS.Custom_Classes.Service_Classes
{

   
    class ClsDataAccess
    {
         #region Global Variable

          private string msrtConnectionString = string.Empty;
         
         #endregion  

          public string GetConnectionString()
          {
              string connectionXmlPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, @"Connection.xml");
                
              var dataSet = new DataSet();
              string server = string.Empty, database = string.Empty, userId = string.Empty, password = string.Empty, connectionString = string.Empty;
              dataSet.ReadXml(connectionXmlPath);

              if (dataSet.Tables["Connection"].Rows.Count > 0)
              {
                  foreach (DataRow row in dataSet.Tables["Connection"].Rows)
                  {
                      server = row["Server"].ToString();
                      database = row["Database"].ToString();
                      userId = row["UserId"].ToString();
                      password = row["Password"].ToString();
                  }
              }

              connectionString = "Server=" + server + ";" + "Database=" + database + ";" + "User Id=" + userId + ";" +
                                "Password=" + password + ";" + "Connection Timeout=30;Pooling = true";

              return connectionString;
          }


          public DataSet ExecuteStoredProcedure(string ProcedureName,List<SqlParameter>parameters,out Dictionary<string,string> outputParams)
          {

           
              var dataSet = new DataSet();
              outputParams = new Dictionary<string, string>();
              var storeProcedureName = "NOPROCEDURE";

              if(!string.IsNullOrWhiteSpace(ProcedureName))
              {
                  storeProcedureName = ProcedureName;
              }

              using (SqlConnection sqlConn = new SqlConnection(GetConnectionString()))
              {
                 
                  using (SqlCommand sqlCommand = new SqlCommand())
                  {
                      try
                      {
                          sqlCommand.Connection = sqlConn;
                          sqlCommand.CommandTimeout = 300000;
                          sqlCommand.CommandType = CommandType.StoredProcedure;
                          sqlCommand.CommandText = storeProcedureName;

                          if (parameters != null)
                          {
                              sqlCommand.Parameters.AddRange(parameters.ToArray());
                          }
                          using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                          {
                              sqlDataAdapter.Fill(dataSet);
                          }

                          //fetch output parameter Values
                          foreach (var output in parameters)
                          {
                              if (output.Direction == ParameterDirection.Output || output.Direction == ParameterDirection.InputOutput)
                              {
                                  if (!outputParams.ContainsKey(output.ParameterName.ToString()))
                                  {
                                      outputParams.Add(output.ParameterName.ToString(), output.Value.ToString());
                                  }
                              }
                          }
                      }
                      catch (Exception ex)
                      {
                          dataSet = null;
                          MessageBox.Show(ex.Message.ToString());
                      }
                     

                  }
              }
              return dataSet;
          }

    }
}
