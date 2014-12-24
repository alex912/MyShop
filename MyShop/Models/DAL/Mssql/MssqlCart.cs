using MyShop.Models.DAL.Interfaces;
using MyShop.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyShop.Models.DAL.Mssql
{
    public class MssqlCart : ICart
    {
        public IEnumerable<CartItem> GetAllCartItems()
        {
            using (var connection = new SqlConnection(MyShopConfig.DataConnection))
            {
                SqlCommand comm = new SqlCommand("SELECT * FROM [Cart]", connection);

                connection.Open();
                var reader = comm.ExecuteReader();


                while (reader.Read())
                {
                    yield return new CartItem()
                    {
                        Id = (int)reader["Id"],
                        Account = new Account() {UserId = (int)reader["UserId"]},
                        Item = new Item() {Id = (int)reader["GoodId"]},
                        Date = (DateTime)reader["Date"],
                        Count = (int)reader["Count"],
                        Region = new Region() { Name = (string)reader["Region"] }
                    };
                }
            }
        }

        public void InsertCartItem(CartItem item)
        {
            using (var connection = new SqlConnection(MyShopConfig.DataConnection))
            {
                SqlCommand comm = new SqlCommand("INSERT INTO [Cart]([UserId], [GoodId], [Date], [Count], [Region]) VALUES " +
                                                 "(@UserId, @GoodId, @Date, @Count, @Region)", connection);

                comm.Parameters.Add(new SqlParameter("UserId", item.Account.UserId));
                comm.Parameters.Add(new SqlParameter("GoodId", item.Item.Id));
                comm.Parameters.Add(new SqlParameter("Date", item.Date));
                comm.Parameters.Add(new SqlParameter("Count", item.Count));
                comm.Parameters.Add(new SqlParameter("Region", item.Region.Name));

                connection.Open();
                comm.ExecuteNonQuery();
                connection.Close();
            }
        }
        
        public void DeleteCartItemById(int id)
        {
            using (var connection = new SqlConnection(MyShopConfig.DataConnection))
            {
                SqlCommand comm = new SqlCommand("DELETE FROM [Cart] WHERE [Id]=@Id", connection);

                comm.Parameters.Add(new SqlParameter("Id", id));

                connection.Open();
                comm.ExecuteNonQuery();
                connection.Close();
            }
        }



        public IEnumerable<CartItem> GetCartItemsByUserId(int userid, Region region)
        {
            using (var connection = new SqlConnection(MyShopConfig.DataConnection))
            {
                SqlCommand comm = new SqlCommand("SELECT * FROM [Cart] WHERE [UserId]=@UserId AND [Region]=@Region", connection);
                comm.Parameters.Add(new SqlParameter("UserId", userid));
                comm.Parameters.Add(new SqlParameter("Region", region.Name));

                connection.Open();
                var reader = comm.ExecuteReader();


                while (reader.Read())
                {
                    yield return new CartItem()
                    {
                        Id = (int)reader["Id"],
                        Account = new Account() { UserId = (int)reader["UserId"] },
                        Item = new Item() { Id = (int)reader["GoodId"] },
                        Date = (DateTime)reader["Date"],
                        Count = (int)reader["Count"],
                        Region = new Region() { Name = (string)reader["Region"] }
                    };
                }
            }
        }
    }
}