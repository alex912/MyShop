using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MyShop.Models.DAL.Interfaces;
using System.Data.SqlClient;
using MyShop.Models.Entities;

namespace MyShop.Models.DAL.Mssql
{
    public class MssqlItems : IItems
    {
        public IEnumerable<Item> GetAllItems()
        {
            using (var connection = new SqlConnection(MyShopConfig.DataConnection))
            {
                SqlCommand comm = new SqlCommand("SELECT * FROM dbo.[Items] " +
                                                 " ORDER BY [Priority]", connection);

                connection.Open();
                var reader = comm.ExecuteReader();


                while (reader.Read())
                {
                    yield return new Item()
                    {
                        Id = (int)reader["Id"],
                        Path = (string)reader["Path"],
                        Region = new Region() { Name = (string)reader["Region"] },
                        Name = (string)reader["Name"],
                        Description = (string)reader["Description"],
                        Price = (double)reader["Price"],
                        PriceDesc = (string)reader["PriceDesc"],
                        Type = (ItemType)reader["Type"],
                        Priority = (int)reader["Priority"],
                        Count = (int)reader["Count"],
                        Catalog = new Menu() { MenuId = (int)reader["CatalogId"] },
                        Tags = (string)reader["Tags"],
                        ImageName = (string)reader["ImageName"],
                        AdditionalInfo = new string[] { (string)reader["AdditionalInfo1"], (string)reader["AdditionalInfo2"], (string)reader["AdditionalInfo3"], (string)reader["AdditionalInfo4"], (string)reader["AdditionalInfo5"], },
                    };
                }
            }
        }

        public IEnumerable<Item> GetAllItemsByCatalogId(int catalogid)
        {
            using (var connection = new SqlConnection(MyShopConfig.DataConnection))
            {
                SqlCommand comm = new SqlCommand("SELECT * FROM dbo.[Items] " +
                                                 "WHERE [CatalogId]=@Param1 ORDER BY [Priority]", connection);

                comm.Parameters.Add(new SqlParameter("Param1", catalogid));

                connection.Open();
                var reader = comm.ExecuteReader();


                while (reader.Read())
                {
                    yield return new Item()
                    {
                        Id = (int)reader["Id"],
                        Path = (string)reader["Path"],
                        Region = new Region() { Name = (string)reader["Region"]},
                        Name = (string)reader["Name"],
                        Description = (string)reader["Description"],
                        Price = (double)reader["Price"],
                        PriceDesc = (string)reader["PriceDesc"],
                        Type = (ItemType)reader["Type"],
                        Priority = (int)reader["Priority"],
                        Count = (int)reader["Count"],
                        Catalog = new Menu() { MenuId = (int)reader["CatalogId"] },
                        Tags = (string)reader["Tags"],
                        ImageName = (string)reader["ImageName"],
                        AdditionalInfo = new string[] { (string)reader["AdditionalInfo1"], (string)reader["AdditionalInfo2"], (string)reader["AdditionalInfo3"], (string)reader["AdditionalInfo4"], (string)reader["AdditionalInfo5"], },
                    };
                }
            }            
        }
    }
}