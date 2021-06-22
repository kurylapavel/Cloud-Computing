using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Company.Function{

    public class DatabaseContext
    {
        private readonly string connectionString;
        private string login;
        private string password;
        private string id;
        private string latitude;
        private string longitude;

        public DatabaseContext(string connectionString, string login, string password)
        {
            this.connectionString = connectionString;
            this.login = login;
            this.password = password;
        }
        public DatabaseContext(string connectionString, string id)
        {
            this.connectionString = connectionString;
            this.id = id;
        }

        public DatabaseContext(string connectionString, string id, string latitude, string longitude)
        {
            this.connectionString = connectionString;
            this.id = id;
            this.latitude = latitude.Substring(0,latitude.IndexOf('.')+4);
            this.longitude = longitude.Substring(0,longitude.IndexOf('.')+4);
        }

        public int GetUserId()
        {
            string Query = $"SELECT Id FROM Users WHERE Login = '{this.login}' AND Password = '{this.password}'";
            int Id = 0;
            try{

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(Query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.Read()){
                        Id = Convert.ToInt32(reader["Id"]);
                    }else{
                        Id = -1;
                    }

                    reader.Close();
                }
            }catch(Exception e){
                return -1;
            }
            
            return Id;

        }

        public int CreateNewUser()
        {
            int response = 0;
            string Query = $"SELECT Id FROM Users WHERE Login = '{this.login}'";
            try{

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(Query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    
                    if(!reader.Read()){
                        reader.Close();
                        Query = $"INSERT INTO Users VALUES('{this.login}','{this.password}')";

                        command = new SqlCommand(Query, connection);
                        
                        reader = command.ExecuteReader();

                        if(!reader.Read()){
                            response = 1;
                        }else{
                            response = -1;
                        }

                    }else{
                        response = 0;
                    }

                    reader.Close();
                }
            }catch(Exception e){
                response = -1;
            }
            
            return response;
        }

        public int SetTrustGeoLocation(){

            string Query = $"SELECT Id FROM Geolocation WHERE UserId ='{this.id}'";
            int response = -1;
            try{

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(Query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.Read()){
                        
                        reader.Close();
                        Query = $"UPDATE Geolocation SET Latitude = '{this.latitude}', Longitude = '{this.longitude}' WHERE UserId = {this.id}";
                        
                        command = new SqlCommand(Query, connection);
                        
                        reader = command.ExecuteReader();

                        if(reader.Read()){
                            response = -1;
                        }else{
                            response = 1;
                        }

                    }else{
                        reader.Close();

                        Query = $"INSERT INTO Geolocation VALUES('{this.id}','{this.latitude}','{this.longitude}')";

                        command = new SqlCommand(Query, connection);
                        
                        reader = command.ExecuteReader();

                        if(reader.Read()){
                            response = -1;
                        }else{
                            response = 1;
                        }


                    }

                    reader.Close();
                }
            }catch(Exception e){
                response = -1;
            }
            
            return response;
        }

        public int CheckTrustedGeoLocation(){

            string Query = $"SELECT * FROM Geolocation WHERE UserId = '{this.id}'";
            int response = 0;
            try{

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(Query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if(reader.Read()){
                        
                        string[] trustLatitude = reader["Latitude"].ToString().Split('.');
                        string[] trustLongitude = reader["Longitude"].ToString().Split('.');
                        string[] toSplitLatitude  =  this.latitude.Split('.');
                        string[] toSplitLongitude  = this.longitude .Split('.');
                        
                        int trustLatitudeInt = Convert.ToInt32(trustLatitude[1]);
                        int trustLongitudeInt = Convert.ToInt32(trustLongitude[1]);
                        int latitudeInt = Convert.ToInt32(toSplitLatitude[1]);
                        int longitudeInt = Convert.ToInt32(toSplitLongitude[1]);

                        if( (latitudeInt > trustLatitudeInt + 1 || latitudeInt < trustLatitudeInt - 1) || (longitudeInt > trustLongitudeInt + 1 || longitudeInt < trustLongitudeInt - 1) ){
                            response = 0;
                        }else{
                            response = 1;
                        }

                    }else{
                        response = -1;
                    }

                    reader.Close();
                }
            }catch(Exception e){
                response = -1;
            }
            
            return response;

        }

    }

}