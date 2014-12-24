using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MyShop.Models.DAL.Interfaces;
using System.Data.SqlClient;
using MyShop.Models.Entities;

namespace MyShop.Models.DAL.Mssql
{
    public class MssqlAccounts : IAccounts
    {
        public IEnumerable<Account> GetAllUsers()
        {
            using (var connection = new SqlConnection(MyShopConfig.DataConnection))
            {
                SqlCommand comm = new SqlCommand("SELECT [UserId], [UserName], [PassHash], [Salt], [RegDate], [Role], [IpAddr], [BaseDiscount], [DefaultPaymentMethod], [Skype], [BattleTag], [DefaultServer], [DefaultCharacter], [DefaultFraction] FROM [Accounts] INNER JOIN [WowInfo] ON [Accounts].[UserId] = [WowInfo].[AccountId]", connection);

                connection.Open();
                var reader = comm.ExecuteReader();


                while (reader.Read())
                {
                    var acc = new Account();
                    acc.UserId = (int)reader["UserId"];
                    acc.UserName = (string)reader["UserName"];
                    acc.PassHash = (string)reader["PassHash"];
                    acc.Salt = (string)reader["Salt"];
                    acc.Role = (string)reader["Role"];
                    acc.IpAddr = (string)reader["IpAddr"];
                    acc.BaseDiscount = (int)reader["BaseDiscount"];
                    acc.RegDate = (DateTime)reader["RegDate"];
                    acc.DefaultPaymentMethod = (PaymentMethod)reader["DefaultPaymentMethod"];
                    acc.Skype = (string)reader["Skype"];
                    acc.WowInfo = new WowInfo() { BattleTag = (string)reader["BattleTag"], DefaultServer = (string)reader["DefaultServer"], DefaultCharacter = (string)reader["DefaultCharacter"], DefaultFraction = (string)reader["DefaultFraction"] };
                    yield return acc;

                }
            }
        }

        public Account GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAccount(Account acc)
        {
            using (var connection = new SqlConnection(MyShopConfig.DataConnection))
            {
                SqlCommand comm = new SqlCommand("UPDATE [Accounts] SET " +
                                                 "[UserName]=@UserName, [PassHash]=@PassHash, [Salt]=@Salt, [RegDate]=@RegDate, [Role]=@Role, [IpAddr]=@IpAddr, [BaseDiscount]=@BaseDiscount, [DefaultPaymentMethod]=@DefaultPaymentMethod, [Skype]=@Skype WHERE [UserId]=@UserId", connection);

                comm.Parameters.Add(new SqlParameter("UserId", acc.UserId));
                comm.Parameters.Add(new SqlParameter("UserName", acc.UserName));
                comm.Parameters.Add(new SqlParameter("PassHash", acc.PassHash));
                comm.Parameters.Add(new SqlParameter("Salt", acc.Salt));
                comm.Parameters.Add(new SqlParameter("RegDate", acc.RegDate));
                comm.Parameters.Add(new SqlParameter("IpAddr", acc.IpAddr));
                comm.Parameters.Add(new SqlParameter("Role", acc.Role));
                comm.Parameters.Add(new SqlParameter("BaseDiscount", acc.BaseDiscount));
                comm.Parameters.Add(new SqlParameter("DefaultPaymentMethod", acc.DefaultPaymentMethod));
                comm.Parameters.Add(new SqlParameter("Skype", acc.Skype));

                connection.Open();
                comm.ExecuteNonQuery();
                connection.Close();

                comm = new SqlCommand("UPDATE [WowInfo] SET " +
                                      "[BattleTag]=@BattleTag, [DefaultCharacter]=@DefaultCharacter, [DefaultFraction]=@DefaultFraction, [DefaultServer]=@DefaultServer " +
                                      "WHERE [AccountId]=@AccountId", connection);

                comm.Parameters.Add(new SqlParameter("AccountId", acc.UserId));
                comm.Parameters.Add(new SqlParameter("BattleTag", acc.WowInfo.BattleTag));
                comm.Parameters.Add(new SqlParameter("DefaultCharacter", acc.WowInfo.DefaultCharacter));
                comm.Parameters.Add(new SqlParameter("DefaultFraction", acc.WowInfo.DefaultFraction));
                comm.Parameters.Add(new SqlParameter("DefaultServer", acc.WowInfo.DefaultServer));

                connection.Open();
                comm.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteAccount(Account acc)
        {
            throw new NotImplementedException();
        }


        public void InsertAccount(Account acc)
        {
            using (var connection = new SqlConnection(MyShopConfig.DataConnection))
            {
                SqlCommand comm = new SqlCommand("INSERT INTO [Accounts]([UserName], [PassHash], [Salt], [RegDate], [Role], [IpAddr], [BaseDiscount], [DefaultPaymentMethod], [Skype]) VALUES " +
                                                 "(@UserName, @PassHash, @Salt, @RegDate, @Role, @IpAddr, @BaseDiscount, @DefaultPaymentMethod, @Skype); SELECT CAST(scope_identity() AS int)", connection);

                comm.Parameters.Add(new SqlParameter("UserName", acc.UserName));
                comm.Parameters.Add(new SqlParameter("PassHash", acc.PassHash));
                comm.Parameters.Add(new SqlParameter("Salt", acc.Salt));
                comm.Parameters.Add(new SqlParameter("RegDate", acc.RegDate));
                comm.Parameters.Add(new SqlParameter("IpAddr", acc.IpAddr));
                comm.Parameters.Add(new SqlParameter("Role", acc.Role));
                comm.Parameters.Add(new SqlParameter("BaseDiscount", acc.BaseDiscount));
                comm.Parameters.Add(new SqlParameter("DefaultPaymentMethod", acc.DefaultPaymentMethod));
                comm.Parameters.Add(new SqlParameter("Skype", acc.Skype));
                
                connection.Open();
                int id = (int)comm.ExecuteScalar();
                connection.Close();

                comm = new SqlCommand("INSERT INTO [WowInfo]([AccountId], [BattleTag], [DefaultCharacter], [DefaultFraction], [DefaultServer]) VALUES " +
                                      "(@AccountId, @BattleTag, @DefaultCharacter, @DefaultFraction, @DefaultServer)", connection);

                comm.Parameters.Add(new SqlParameter("AccountId", id));
                comm.Parameters.Add(new SqlParameter("BattleTag", acc.WowInfo.BattleTag));
                comm.Parameters.Add(new SqlParameter("DefaultCharacter", acc.WowInfo.DefaultCharacter));
                comm.Parameters.Add(new SqlParameter("DefaultFraction", acc.WowInfo.DefaultFraction));
                comm.Parameters.Add(new SqlParameter("DefaultServer", acc.WowInfo.DefaultServer));

                connection.Open();
                comm.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}