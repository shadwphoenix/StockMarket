using FluentAssertions;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace StockMarket.Domain.Test
{
    public class StockMarketProcessorTest
    {

        [Fact]
        public void EnqueueOrder_Should_Process_SellOrder_When_BuyOrder_Is_Already_Enqueued_Test()
        {
            //Arrange

            var sut = new StockMarketProcessor();// sut = System Under Test
            var buyOrderId = sut.EnqueueOrder(side: TradeSide.Buy, quantity: 1, price: 1500);

            //Act

            var sellOrderId = sut.EnqueueOrder(side: TradeSide.Sell, quantity: 2, price: 1400);

            //Assert

            Assert.Equal(2, sut.Orders.Count());
            sut.Orders.First().Should().BeEquivalentTo(new
            {
                Side = TradeSide.Buy,
                Quantity = 0M,
                Price = 1500M
            });
            sut.Orders.Skip(1).First().Should().BeEquivalentTo(new
            {
                Side = TradeSide.Sell,
                Quantity = 1M,
                Price = 1400M
            });

            Assert.Single(sut.Trades);
            sut.Trades.First().Should().BeEquivalentTo(new
            {
                Quantity = 1M,
                Price = 1400M,
                sellOrderId = sellOrderId,
                buyOrderId = buyOrderId
            });
        }

        [Fact]
        public void EnqueueOrder_Should_Process_BuyOrder_When_SellOrder_Is_Already_Enqueued_Test()
        {
            //Arrange

            var sut = new StockMarketProcessor();
            sut.EnqueueOrder(side: TradeSide.Sell, quantity: 1, price: 1400);

            //Act

            sut.EnqueueOrder(side: TradeSide.Buy, quantity: 1, price: 1500);

            //Assert

            Assert.Equal(2, sut.Orders.Count());
            Assert.Single(sut.Trades);
        }
        [Fact]

        public void EnqueueOrder_Should_Process_SellOrder_With_The_Highest_Price_BuyOrder()
        {
            //Arrange
            var sut = new StockMarketProcessor();
            var buyOrderId1 = sut.EnqueueOrder(side: TradeSide.Buy, quantity: 3, price: 1500);
            var buyOrderId2 = sut.EnqueueOrder(side: TradeSide.Buy, quantity: 3, price: 1600);

            //Act
            var sellOrderId = sut.EnqueueOrder(TradeSide.Sell, quantity: 2, price: 1400);

            //Assert
            Assert.Equal(3, sut.Orders.Count());
            Assert.Single(sut.Trades);
            sut.Trades.First().Should().BeEquivalentTo(new
            {

                Quantity = 2M,
                Price = 1400M,
                sellOrderId = sellOrderId,
                buyOrderId = buyOrderId2

            });
        }

        [Fact]
        public void EnqueueOrder_Should_Process_BuyOrder_With_Least_Price_SellOrder()
        {
            //Arrange
            var sut = new StockMarketProcessor();
            var sellOrderId1 = sut.EnqueueOrder(side: TradeSide.Sell, quantity: 3, price: 1500);
            var sellOrderId2 = sut.EnqueueOrder(side: TradeSide.Sell, quantity: 3, price: 1600);

            //Act
            var buyOrderId = sut.EnqueueOrder(TradeSide.Buy, quantity: 2, price: 1700);

            //Assert
            Assert.Equal(3, sut.Orders.Count());
            Assert.Single(sut.Trades);
            sut.Trades.First().Should().BeEquivalentTo(new
            {

                Quantity = 2M,
                Price = 1500M,
                sellOrderId = sellOrderId1,
                buyOrderId = buyOrderId

            });
        }


        [Fact]
        public void CancelOrder_Should_Cancel_Order()
        {
            //Arrange
            var sut = new StockMarketProcessor();
            var orderId = sut.EnqueueOrder(side: TradeSide.Buy, quantity: 1, price: 1500);

            //Act
            sut.CancelOrder(orderId);

            //Assert
            sut.Orders.First().Should().BeEquivalentTo(new
            {
                Side = TradeSide.Buy,
                Quantity = 1M,
                Price = 1500M,
                IsCanceled = true
            });
        }

        [Fact]
        public void CanceledOrder_Should_Not_Trade_With_Other_Order()
        {
            //Arrange
            var sut = new StockMarketProcessor();
            var canceledOrderId = sut.EnqueueOrder(side: TradeSide.Buy, quantity: 1, price: 1500);

            //Act
            sut.CancelOrder(canceledOrderId);
            sut.EnqueueOrder(side: TradeSide.Sell, quantity: 1, price: 1500);

            //Assert
            Assert.Empty(sut.Trades);
        }
    }
}