using ShopBridge.Helper;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace ShopBridge.Repository
{
    public interface IItemRepository
    {
        Task<List<Item>> GetItemsAsync();
        Task<List<Item>> GetItemAsync(int ItemID);
        Task<Tuple<bool, Exception>> AddItemAsync(Item Item);
        Task<Tuple<bool, Exception>> EditItemAsync(Item Item);
        Task<Tuple<bool, Exception>> DeleteItemAsync(int ItemID);
    }
    public class ItemRepository : IItemRepository
    {
        readonly string CON_STRING;

        public ItemRepository()
        {            
            //Application.StartupPath + "//App_Data//ShopBridgeDB.mdf";
            CON_STRING = ConfigurationManager.AppSettings["ConnectionString"].ToString();

        }

        #region IItemRepository implementation


        public async Task<List<Item>> GetItemsAsync()
        {
            List<Item> lstData = new List<Item>();
            try
            {
                using (SqlConnection con = new SqlConnection(CON_STRING))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PROC_GET_ITEMS";

                    await con.OpenAsync();
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            lstData.Add(new Item
                            {
                                ItemID = dr.GetInt32(0),
                                ItemName = dr.GetSafeString(1),
                                Description = dr.GetSafeString(2),
                                Price = dr.GetSafeDouble(3)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lstData;
        }

        public async Task<List<Item>> GetItemAsync(int ItemID)
        {
            List<Item> lstData = new List<Item>();
            try
            {
                using (SqlConnection con = new SqlConnection(CON_STRING))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PROC_GET_ITEM";

                    cmd.Parameters.Add("ItemID", SqlDbType.Int).Value = ItemID;

                    await con.OpenAsync();
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            lstData.Add(new Item
                            {
                                ItemID = dr.GetInt32(0),
                                ItemName = dr.GetSafeString(1),
                                Description = dr.GetSafeString(2),
                                Price = dr.GetSafeDouble(3)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lstData;
        }

        public async Task<Tuple<bool, Exception>> AddItemAsync(Item Item)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CON_STRING))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PROC_ADD_ITEM";

                    cmd.Parameters.Add("ItemName", SqlDbType.NVarChar, 100).Value = Item.ItemName;
                    cmd.Parameters.Add("Description", SqlDbType.NVarChar, 300).Value = Item.Description;
                    cmd.Parameters.Add("Price", SqlDbType.Float).Value = Item.Price;

                    await con.OpenAsync();
                    await cmd.ExecuteScalarAsync();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, Exception>(false, ex);
            }

            return new Tuple<bool, Exception>(true, new Exception());
        }

        public async Task<Tuple<bool, Exception>> EditItemAsync(Item Item)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CON_STRING))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PROC_UPDATE_ITEM";

                    cmd.Parameters.Add("ItemID", SqlDbType.Int).Value = Item.ItemID;

                    cmd.Parameters.Add("ItemName", SqlDbType.NVarChar, 100).Value = Item.ItemName;
                    cmd.Parameters.Add("Description", SqlDbType.NVarChar, 300).Value = Item.Description;
                    cmd.Parameters.Add("Price", SqlDbType.Float).Value = Item.Price;

                    await con.OpenAsync();
                    await cmd.ExecuteScalarAsync();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, Exception>(false, ex);
            }

            return new Tuple<bool, Exception>(true, new Exception());
        }

        public async Task<Tuple<bool, Exception>> DeleteItemAsync(int ItemID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(CON_STRING))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PROC_REMOVE_ITEM";

                    cmd.Parameters.Add("ItemID", SqlDbType.Int).Value = ItemID;

                    await con.OpenAsync();
                    await cmd.ExecuteScalarAsync();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, Exception>(false, ex);
            }

            return new Tuple<bool, Exception>(true, new Exception());
        }

        #endregion
    }
}