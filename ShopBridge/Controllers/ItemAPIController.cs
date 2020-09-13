using ShopBridge.Helper;
using ShopBridge.Models;
using ShopBridge.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShopBridge.Controllers
{
    public class ItemAPIController : ApiController
    {
        private readonly IItemRepository itemRepository;

        public ItemAPIController(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<ResultModel> GetItems()
        {
            try
            {
                List<Item> data = await itemRepository.GetItemsAsync();
                return LibFuncs.getResponse(data);
            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                return LibFuncs.getExceptionResponse(ex, st.GetFrame(0).GetMethod().DeclaringType.FullName);
            }
        }

        [HttpGet]
        public async Task<ResultModel> GetItem(int ItemID)
        {
            try
            {
                List<Item> data = await itemRepository.GetItemAsync(ItemID);
                return LibFuncs.getResponse(data);
            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                return LibFuncs.getExceptionResponse(ex, st.GetFrame(0).GetMethod().DeclaringType.FullName);
            }
        }

        [HttpPost]
        public async Task<ResultModel> AddItem(Item Item)
        {
            try
            {
                Tuple<bool, Exception> resultData = await itemRepository.AddItemAsync(Item);

                if (resultData.Item1)
                    return LibFuncs.getSavedResponse(null, resultData.Item1, 0);
                else
                    return LibFuncs.getExceptionResponse(resultData.Item2, "AddItem");

            }
            catch (Exception ex)
            {

                var st = new StackTrace();
                return LibFuncs.getExceptionResponse(ex, st.GetFrame(0).GetMethod().DeclaringType.FullName);
            }
        }

        [HttpPut]
        public async Task<ResultModel> EditItem(Item Item)
        {
            try
            {
                Tuple<bool, Exception> resultData = await itemRepository.EditItemAsync(Item);

                if (resultData.Item1)
                    return LibFuncs.getSavedResponse(null, resultData.Item1, 0);
                else
                    return LibFuncs.getExceptionResponse(resultData.Item2, "EditItem");
            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                return LibFuncs.getExceptionResponse(ex, st.GetFrame(0).GetMethod().DeclaringType.FullName);
            }

        }


        [HttpDelete]
        public async Task<ResultModel> DeleteItem(int ItemID)
        {
            try
            {
                Tuple<bool, Exception> resultData = await itemRepository.DeleteItemAsync(ItemID);

                if (resultData.Item1)
                    return LibFuncs.getSavedResponse(null, resultData.Item1, 0);
                else
                    return LibFuncs.getExceptionResponse(resultData.Item2, "DeleteItem");
            }
            catch (Exception ex)
            {
                var st = new StackTrace();
                return LibFuncs.getExceptionResponse(ex, st.GetFrame(0).GetMethod().DeclaringType.FullName);
            }

        }

    }
}
