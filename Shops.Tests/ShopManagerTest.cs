using System.Collections.Generic;
using NUnit.Framework;
using Shops.Entities;
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
        public void DeliveryOfProducts_DeliverNotRegisteredProductsToTheShop_ThrowException()
        {
            Shop shopToTest = _shopManager.FindShop("Diksi");
            
            Assert.Catch<NotInBaseException>(() =>
            {
                shopToTest.DeliverProducts(new List<Box>()
                {
                    new (_shopManager.GetProduct("Lay's"), 100, 10),
                    new (_shopManager.GetProduct("Ginger"), 15, 10),
                });
            });
        }
        
        [Test]
        public void DeliveryOfProducts_DeliverProductsToTheShop_ShopContainsBoxesWithRegisteredProducts()
        {
            Shop shopToTest = _shopManager.FindShop("Diksi");
            shopToTest.DeliverProducts(new List<Box>
            {
                new (_shopManager.GetProduct("Lay's"), 100, 10),
                new (_shopManager.GetProduct("Coca-Cola"), 50, 5),
                new (_shopManager.GetProduct("Bread"), 30, 15)
            });
            Assert.True(shopToTest.Boxes[0].Product.Id == 1 && 
                        shopToTest.Boxes[0].ProductPrice == 100 &&
                        shopToTest.Boxes[0].Quantity == 10);
            Assert.True(shopToTest.Boxes[1].Product.Id == 2 && 
                        shopToTest.Boxes[1].ProductPrice == 50 &&
                        shopToTest.Boxes[1].Quantity == 5);
            Assert.True(shopToTest.Boxes[2].Product.Id == 3 && 
                        shopToTest.Boxes[2].ProductPrice == 30 &&
                        shopToTest.Boxes[2].Quantity == 15);
        }

        [Test]
        public void ChangeProductPrice_SetAndChangeProductPrice_PriceChanged()
        {
            Shop shopToTest = _shopManager.FindShop("Diksi");
            shopToTest.DeliverProducts(new List<Box>()
            {
                new (_shopManager.GetProduct("Lay's"), 100, 10),
                new (_shopManager.GetProduct("Coca-Cola"), 50, 5)
            });
            Assert.IsTrue(shopToTest.Boxes[0].ProductPrice == 100);
            Assert.IsTrue(shopToTest.Boxes[1].ProductPrice == 50);
            shopToTest.ChangeProductPrice(_shopManager.GetProduct("Lay's"), 150);
            shopToTest.ChangeProductPrice(_shopManager.GetProduct("Coca-Cola"), 45);
            Assert.IsTrue(shopToTest.Boxes[0].ProductPrice == 150);
            Assert.IsTrue(shopToTest.Boxes[1].ProductPrice == 45);
        }

        [Test]
        public void ChangeProductPrice_ChangePriceOfNotRegisteredProduct_ThrowException()
        {
            Shop shopToTest = _shopManager.FindShop("Diksi");
            Assert.Catch<NotInBaseException>(() =>
                {
                    shopToTest.ChangeProductPrice(_shopManager.GetProduct("Ginger"), 45);
                }
            );
        }
        
        [Test]
        public void Buy_PersonBuysUnavailableQuantityOfProduct_ThrowException()
        {
            Shop shopToTest = _shopManager.FindShop("Diksi");
            Person tester = new ("Ivan", 1000);
            shopToTest.DeliverProducts(new List<Box>
            {
                new (_shopManager.GetProduct("Lay's"), 100, 7)
            });
            Assert.Catch<NotEnoughProductException>(() =>
                {
                    _shopManager.Buy(tester, new List<ProductOrder>
                    {
                        new (_shopManager.GetProduct("Lay's"), 10)
                    });
                }
            );
        }
        
        [Test]
        public void Buy_ThePersonHasLessMoneyThenNeeded_ThrowException()
        {
            Shop shopToTest = _shopManager.FindShop("Diksi");
            Person tester = new ("Ivan", 1000);
            shopToTest.DeliverProducts(new List<Box>
            {
                new (_shopManager.GetProduct("Lay's"), 110, 15)
            });
            Assert.Catch<NotEnoughMoneyException>(() =>
                {
                    _shopManager.Buy(tester, new List<ProductOrder>
                    {
                        new (_shopManager.GetProduct("Lay's"), 10)
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
            firstShop.DeliverProducts(new List<Box>
            {
                new (_shopManager.GetProduct("Lay's"), 100, 3),
                new (_shopManager.GetProduct("Coca-Cola"), 40, 1)
            });
            secondShop.DeliverProducts(new List<Box>
            {
                new (_shopManager.GetProduct("Lay's"), 150, 2),
                new (_shopManager.GetProduct("Coca-Cola"), 50, 5)
            });
            thirdShop.DeliverProducts(new List<Box>
            {
                new (_shopManager.GetProduct("Lay's"), 90, 1),
                new (_shopManager.GetProduct("Coca-Cola"), 100, 3),
            });
            _shopManager.Buy(tester, new List<ProductOrder>
            {
                new (_shopManager.GetProduct("Lay's"), 5),
                new (_shopManager.GetProduct("Coca-Cola"), 2)
            });
            Assert.IsTrue(firstShop.Boxes[0].Quantity == 0 &&
                          firstShop.Boxes[1].Quantity == 0 &&
                          firstShop.Boxes[0].Product.Id == 1 &&
                          firstShop.Boxes[1].Product.Id == 2 &&
                          firstShop.Proceeds == 340);
            Assert.IsTrue(secondShop.Boxes[0].Quantity == 1 &&
                          secondShop.Boxes[1].Quantity == 4 &&
                          secondShop.Boxes[0].Product.Id == 1 &&
                          secondShop.Boxes[1].Product.Id == 2 &&
                          secondShop.Proceeds == 200);
            Assert.IsTrue(thirdShop.Boxes[0].Quantity == 0 &&
                          thirdShop.Boxes[1].Quantity == 3 &&
                          thirdShop.Boxes[0].Product.Id == 1 &&
                          thirdShop.Boxes[1].Product.Id == 2 &&
                          thirdShop.Proceeds == 90);
            Assert.IsTrue(tester.Money == 370);
        }
    }
}