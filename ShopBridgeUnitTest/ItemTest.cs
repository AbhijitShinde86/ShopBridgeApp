using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopBridge.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using ShopBridge.Models;
using ShopBridge.Repository;
using System.Configuration;

namespace ShopBridgeUnitTest
{
    [TestClass]
    public class ItemTest
    {
        [TestMethod]
        public void TestCreateView()
        {
            ItemController itemController = new ItemController();
            var result = itemController.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void TestGetItems()
        {
            ItemAPIController itemAPIController = new ItemAPIController(new ItemRepository());
            List<Item>  result = (List<Item >)itemAPIController.GetItems().Result.Data ;
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void TestGetItem()
        {
            ItemAPIController itemAPIController = new ItemAPIController(new ItemRepository());
            List<Item> result = (List<Item>)itemAPIController.GetItem(1).Result.Data;
            Assert.IsNotNull(result);
        }
        
    }
}
