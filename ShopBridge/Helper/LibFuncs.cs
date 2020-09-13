using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBridge.Helper
{
    static class LibFuncs
    {
        //Common get response function.
        public static ResultModel getResponse(Object data, string msg = "")
        {
            try
            {
                using (ResultModel result = new ResultModel())
                {
                    result.Id = 0;
                    result.Status = "Success";
                    result.StatusCode = 200;
                    result.Msg = msg == "" ? "Record has been fetched successfully..!!" : msg;
                    result.Data = data;
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public static ResultModel getSavedResponse(Object data, bool blnResultFlag, int savedId)
        {
            try
            {
                using (ResultModel result = new ResultModel())
                {
                    result.Id = blnResultFlag == true ? savedId : -1;
                    result.StatusCode = blnResultFlag == true ? 200 : 1003;
                    result.Status = blnResultFlag == true ? "Success" : "Error";
                    result.Msg = blnResultFlag == true ? "Record has been saved..!!" : "Record can not be saved..!!";
                    result.Data = blnResultFlag == true ? data : null;
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static ResultModel getUpdatedResponse(Object data, bool blnResultFlag, int updatedId)
        {
            try
            {
                using (ResultModel result = new ResultModel())
                {
                    result.Id = blnResultFlag == true ? updatedId : -1;
                    result.StatusCode = blnResultFlag == true ? 200 : 1003;
                    result.Status = blnResultFlag == true ? "Success" : "Error";
                    result.Msg = blnResultFlag == true ? "Record has been updated..!!" : "Record can not be updated..!!";
                    result.Data = blnResultFlag == true ? data : null;
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static ResultModel getRemovedResponse(Object data, bool blnResultFlag, int deletedId)
        {
            try
            {
                using (ResultModel result = new ResultModel())
                {
                    result.Id = blnResultFlag == true ? deletedId : -1;
                    result.StatusCode = blnResultFlag == true ? 200 : 1003;
                    result.Status = blnResultFlag == true ? "Success" : "Error";
                    result.Msg = blnResultFlag == true ? "Record has been deleted..!!" : "Record can not be deleted..!!";
                    result.Data = blnResultFlag == true ? data : null;
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static ResultModel getRestoredResponse(Object data, bool blnResultFlag, int restoredId)
        {
            try
            {
                using (ResultModel result = new ResultModel())
                {
                    result.Id = blnResultFlag == true ? restoredId : -1;
                    result.StatusCode = blnResultFlag == true ? 200 : 1003;
                    result.Status = blnResultFlag == true ? "Success" : "Error";
                    result.Msg = blnResultFlag == true ? "Record has been restored..!!" : "Record can not be restored..!!";
                    result.Data = blnResultFlag == true ? data : null;
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static ResultModel getExceptionResponse(Exception ex, string strMethodName)
        {

            AppLog.WriteLog(ex.Message + " : " + Convert.ToString(ex.InnerException), strMethodName);
            using (ResultModel result = new ResultModel())
            {
                result.Id = -1;
                result.Status = "Error";
                result.StatusCode = ex.Source == "DBAPI" ? 1002 : 1001;
                result.Msg = ex.Message; //((ex.Source == "DBAPI" && ex.HResult == -2146233088) || (((System.Data.SqlClient.SqlException)ex).Number == 50000)) ? char.ToUpper(ex.Message[0]) + ex.Message.Substring(1) + ".!!" : "Something went wrong..!!";
                result.ErrorMsg = ex.Message;
                return result;
            }
        }
    }
}