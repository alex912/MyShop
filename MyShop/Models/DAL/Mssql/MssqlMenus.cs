using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using MyShop.Models.Entities;
using MyShop.Models.DAL.Interfaces;

namespace MyShop.Models.DAL.Mssql
{
    public class MssqlMenus : IMenus
    {
        public IEnumerable<Menu> GetAllMenusByParentId(int parentid)
        {
            using (var connection = new SqlConnection(MyShopConfig.DataConnection))
            {
                SqlCommand comm = new SqlCommand("SELECT [MenuId], [ParentId], [Priority], [Name], [Side], [Type], [Region], [ImageName], [IsNew], [IsHot], [Url], [IsActive], [Path]  FROM dbo.[Menus] " +
                                                 "WHERE [ParentId]=@Param1 ORDER BY [Priority]", connection);

                comm.Parameters.Add(new SqlParameter("Param1", parentid));

                connection.Open();
                var reader = comm.ExecuteReader();


                while (reader.Read())
                {
                    yield return new Menu()
                    {
                        MenuId = (int)reader["MenuId"],
                        Parent = new Menu() { MenuId = (int)reader["ParentId"] },
                        Priority = (int)reader["Priority"],
                        Name = (string)reader["Name"],
                        Side = (int)reader["Side"],
                        Type = (MenuType)(reader["Type"]),
                        Region = new Region() { Name = (string)reader["Region"] }, 
                        ImageName = (string)reader["ImageName"],
                        IsNew = ((int)(reader["IsNew"])) != 0 ? true : false,
                        IsHot = ((int)(reader["IsHot"])) != 0 ? true : false,
                        IsActive = ((int)(reader["IsActive"])) != 0 ? true : false,
                        Url = (string)reader["Url"],
                        Path = (string)reader["Path"],
                    };
                }
            }
        }
    }
}