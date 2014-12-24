using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using MyShop.Models.Entities;
using MyShop.Models.DAL.Interfaces;

namespace MyShop.Models.DAL.Mssql
{
    public class MssqlSlides : ISlides
    {

        public IEnumerable<Slide> GetAllSlides()
        {
            using (var connection = new SqlConnection(MyShopConfig.DataConnection))
            {
                SqlCommand comm = new SqlCommand("SELECT [SlideId], [ImageName], [Region], [Priority], [Caption], [Text], [Classes] FROM dbo.[Slides]", connection);

                connection.Open();
                var reader = comm.ExecuteReader();
                

                while(reader.Read())
                {
                    yield return new Slide()
                    {
                        SlideId = (int)reader["SlideId"],
                        ImageName = (string)reader["ImageName"],
                        Region = new Region() { Name = (string)reader["Region"] },
                        Priority = (int)reader["Priority"],
                        Caption = (string)reader["Caption"],
                        Text = (string)reader["Text"],
                        Classes = (string)reader["Classes"],
                    };
                }
            }
        }
    }
}