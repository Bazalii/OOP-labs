using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.Common.Interfaces;
using NUnit.Framework;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    [TestFixture]
    public class Tests
    {
        private ShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
            _shopManager.AddNewShop("Diksi", "Spb, Severniy pr., 40");
            _shopManager.AddNewShop("Pyatorochka", "Spb, Nauki pr., 20");
            _shopManager.AddNewShop("Okey", "Spb, Nauki pr., 30");
            _shopManager.RegisterNewProduct("Lay's");
            _shopManager.RegisterNewProduct("Coca-Cola");
            _shopManager.RegisterNewProduct("Bread");
        }

        
        
        [Test]
        public void DeliveryOfProducts_DeliverNotRegisteredProductsToTheShop_ReceiveMessage()
        {
            Shop shopToTest = _shopManager.FindShop("Diksi");
            string deliveryOutput = shopToTest.DeliveryOfProducts(_shopManager, new List<Tuple<string, int, int>>()
            {
                new ("Lay's", 100, 10),
                /*
                 * Next two products are not in base and corresponding exception
                 * is processed, check Console output for message
                 */
                new ("Ginger", 15, 10),
                new ("IceCream", 50, 150)
            });
            Assert.IsTrue(deliveryOutput == "These products weren't added: Ginger, IceCream, because the product that you want to buy doesn't exist");
        }    
            
        
        [Test]
        public void DeliveryOfProducts_DeliverProductsToTheShop_ShopContainsBoxesWithRegisteredProducts()
        {
            Shop shopToTest = _shopManager.FindShop("Diksi");
            shopToTest.DeliveryOfProducts(_shopManager, new List<Tuple<string, int, int>>
            {
                new ("Lay's", 100, 10),
                new ("Coca-Cola", 50, 5),
                new ("Bread", 30, 15),
            });
            Assert.True(shopToTest.Boxes[0].ProductId == 1 && 
                        shopToTest.Boxes[0].ProductPrice == 100 &&
                        shopToTest.Boxes[0].Quantity == 10);
            Assert.True(shopToTest.Boxes[1].ProductId == 2 && 
                        shopToTest.Boxes[1].ProductPrice == 50 &&
                        shopToTest.Boxes[1].Quantity == 5);
            Assert.True(shopToTest.Boxes[2].ProductId == 3 && 
                        shopToTest.Boxes[2].ProductPrice == 30 &&
                        shopToTest.Boxes[2].Quantity == 15);
        }

        [Test]
        public void ChangeProductPrice_SetAndChangeProductPrice_PriceChanged()
        {
            Shop shopToTest = _shopManager.FindShop("Diksi");
            shopToTest.DeliveryOfProducts(_shopManager, new List<Tuple<string, int, int>>()
            {
                new ("Lay's", 100, 10),
                new ("Coca-Cola", 50, 5)
            });
            Assert.IsTrue(shopToTest.Boxes[0].ProductPrice == 100);
            Assert.IsTrue(shopToTest.Boxes[1].ProductPrice == 50);
            shopToTest.ChangeProductPrice(_shopManager, "Lay's", 150);
            shopToTest.ChangeProductPrice(_shopManager, "Coca-Cola", 45);
            Assert.IsTrue(shopToTest.Boxes[0].ProductPrice == 150);
            Assert.IsTrue(shopToTest.Boxes[1].ProductPrice == 45);
        }

        [Test]
        public void ChangeProductPrice_ChangePriceOfNotRegisteredProduct_ThrowException()
        {
            Shop shopToTest = _shopManager.FindShop("Diksi");
            Assert.Catch<NotInBaseException>(() =>
                {
                    shopToTest.ChangeProductPrice(_shopManager, "Ginger", 45);
                }
            );
        }
        
        [Test]
        public void Buy_PersonBuysUnavailableQuantityOfProduct_ThrowException()
        {
            Shop shopToTest = _shopManager.FindShop("Diksi");
            Person tester = new ("Ivan", 1000);
            shopToTest.DeliveryOfProducts(_shopManager, new List<Tuple<string, int, int>>
            {
                new ("Lay's", 100, 7)
            });
            Assert.Catch<NotEnoughProductException>(() =>
                {
                    _shopManager.Buy(tester, new List<Tuple<string, int>>
                    {
                        new ("Lay's", 10)
                    });
                }
            );
        }
        
        [Test]
        public void Buy_ThePersonHasLessMoneyThenNeeded_ThrowException()
        {
            Shop shopToTest = _shopManager.FindShop("Diksi");
            Person tester = new ("Ivan", 1000);
            shopToTest.DeliveryOfProducts(_shopManager, new List<Tuple<string, int, int>>
            {
                new ("Lay's", 110, 15)
            });
            Assert.Catch<NotEnoughMoneyException>(() =>
                {
                    _shopManager.Buy(tester, new List<Tuple<string, int>>
                    {
                        new ("Lay's", 10)
                    });
                }
            );
        }

        [Test]
        public void Buy_PersonBuysSeveralProducts_TakeMoneyRemoveProductsGiveProceeds()
        {
            Person tester = new ("Ivan", 1000);
            Shop firstShop = _shopManager.FindShop("Diksi");
            Shop secondShop = _shopManager.FindShop("Pyatorochka");
            Shop thirdShop = _shopManager.FindShop("Okey");
            firstShop.DeliveryOfProducts(_shopManager, new List<Tuple<string, int, int>>
            {
                new ("Lay's", 100, 3),
                new ("Coca-Cola", 40, 1),
            });
            secondShop.DeliveryOfProducts(_shopManager, new List<Tuple<string, int, int>>
            {
                new ("Lay's", 150, 2),
                new ("Coca-Cola", 50, 5),
            });
            thirdShop.DeliveryOfProducts(_shopManager, new List<Tuple<string, int, int>>
            {
                new ("Lay's", 90, 1),
                new ("Coca-Cola", 100, 3),
            });
            _shopManager.Buy(tester, new List<Tuple<string, int>>
            {
                new ("Lay's", 5),
                new ("Coca-Cola", 2)
            });
            Assert.IsTrue(firstShop.Boxes[0].Quantity == 0 &&
                          firstShop.Boxes[1].Quantity == 0 &&
                          firstShop.Boxes[0].ProductId == 1 &&
                          firstShop.Boxes[1].ProductId == 2 &&
                          firstShop.Proceeds == 340);
            Assert.IsTrue(secondShop.Boxes[0].Quantity == 1 &&
                          secondShop.Boxes[1].Quantity == 4 &&
                          secondShop.Boxes[0].ProductId == 1 &&
                          secondShop.Boxes[1].ProductId == 2 &&
                          secondShop.Proceeds == 200);
            Assert.IsTrue(thirdShop.Boxes[0].Quantity == 0 &&
                          thirdShop.Boxes[1].Quantity == 3 &&
                          thirdShop.Boxes[0].ProductId == 1 &&
                          thirdShop.Boxes[1].ProductId == 2 &&
                          thirdShop.Proceeds == 90);
            Assert.IsTrue(tester.Money == 370);
        }
    }
}