using System.ComponentModel;

namespace StockMarket.Domain.Test
{
    public class StockMarketProcessorTest
    {
        [Fact]
        public void EnqueueOrder_Should_Process_SellOrder_When_BuyOrder_Is_Already_Enqueued_Test()
        {
            //Arrange
            var sut = new StockMarketProcessor();// sut = System Under Test

            //Act

            sut.EnqueueOrder(side, quantity, price);

            //Assert

        }
    }
}