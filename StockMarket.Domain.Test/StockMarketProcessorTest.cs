using FluentAssertions;
using System.ComponentModel;
using System.Diagnostics;

namespace StockMarket.Domain.Test
{
    public class StockMarketProcessorTest
    {

        [Fact]
        public void EnqueueOrder_Should_Process_SellOrder_When_BuyOrder_Is_Already_Enqueued_Test()
        {
            //Arrange

            var sut = new StockMarketProcessor();// sut = System Under Test
            var BuyOrderId = sut.EnqueueOrder(side: TradeSide.Buy, quantity: 1, price: 1500);

            //Act

            var SellOrderId = sut.EnqueueOrder(side: TradeSide.Sell, quantity: 2, price: 1400);

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
                Quantity = 1,
                Price = 1400,
                sellOrderId = SellOrderId,
                buyOrderId = BuyOrderId
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

        public void EnqueueOrder_Should_Process_SellOrder_With_Best_BuyOrder()
        {
            //Arrange
            var sut = new StockMarketProcessor();
            sut.EnqueueOrder(side: TradeSide.Buy, quantity: 3, price: 1500);
            sut.EnqueueOrder(side: TradeSide.Buy, quantity: 3, price: 1600);


            //Act
            sut.EnqueueOrder(TradeSide.Sell, quantity: 2, price: 1400);
            sut.EnqueueOrder(TradeSide.Sell, quantity: 1, price: 1300);
            //Assert
            Assert.Equal(4, sut.Orders.Count());
            Assert.Equal(2, sut.Trades.Count());
            Assert.Equal(1400, sut.Trades.First().Price);
            Assert.Equal(2, sut.Trades.First().IdBuy);
            Assert.Equal(1300, sut.Trades.Last().Price);
            Assert.Equal(2, sut.Trades.First().IdBuy);
        }


        //[Fact]

        //public void CancelOrder_Should_Cancel_Order()
    }
}