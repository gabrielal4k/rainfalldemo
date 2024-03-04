using Rainfall.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Test
{
    public class GuardTest : IClassFixture<Guard>
    {
        //testing for checking stationdID
        Guard _guardTest;
        public GuardTest(Guard src)
        {
            _guardTest = src;
        }

        [Fact]
        public void Create_ReturnOKStatus_WhenInRangeValue()
        {
            try
            {
                //Correct Station ID / min 1, max 100
                int id = 1;
                var _response = _guardTest.CheckStationID(id);

                var state = _response.ResponseState;

                Assert.Equal(200, state);
            }
            catch (Exception) { throw; }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(101)]
        public void Create_ReturnBadRequestStatus_WhenOutofValueMinMaxRange(int stationID)
        {
            try
            {
                //No Station ID
                //Wrong Station ID
                var _response = _guardTest.CheckStationID(stationID);

                var state = _response.ResponseState;

                Assert.Equal(400, state);
            }
            catch (Exception) { throw; }
        }
    }
}
