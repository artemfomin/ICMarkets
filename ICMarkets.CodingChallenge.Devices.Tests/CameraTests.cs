using ICMarkets.CodingChallenge.Devices.Connections;
using Xunit;

namespace ICMarkets.CodingChallenge.Devices.Tests
{
    public class CameraTests
    {
        [Fact]
        public void CanConnectToCamera()
        {
            var cam = new Camera(ConnectionTypes.TCP, new object(), "Back yard cam 1");
            var result = cam.Connect();

            Assert.True(result);
            cam.Disconnect();
        }

        [Fact]
        public void CannotConnectToCameraTwice()
        {
            var cam = new Camera(ConnectionTypes.TCP, new object(), "Back yard cam 1");
            cam.Connect();
            var result = cam.Connect();

            Assert.True(!result);
            cam.Disconnect();
        }

        [Fact]
        public void DisconnectsFromCamera()
        {
            var cam = new Camera(ConnectionTypes.TCP, new object(), "Back yard cam 1");
            cam.Connect();
            var result = cam.Disconnect();

            Assert.True(result);
        }

        [Fact]
        public void CannotDisconnectFromCameraTwice()
        {
            var cam = new Camera(ConnectionTypes.TCP, new object(), "Back yard cam 1");
            cam.Connect();
            cam.Disconnect();
            var result = cam.Disconnect();

            Assert.True(!result);
        }

        [Fact]
        public void ZoomsProperly()
        {
            using(var cam = new Camera(ConnectionTypes.TCP, new object(), "Back yard cam 1"))
            {
                cam.Connect();
                var previousZoom = cam.Zoom;
                cam.SwitchZoom(cam.Zoom + 4);

                Assert.True(cam.Zoom != previousZoom);
                Assert.Equal(cam.Zoom, previousZoom + 4);
            }
        }

        [Fact]
        public void ChangesPositionProperly()
        {
            using (var cam = new Camera(ConnectionTypes.TCP, new object(), "Back yard cam 1"))
            {
                cam.Connect();
                var previousX = cam.PosX;
                var previousY = cam.PosY;
                cam.ChangePosition(previousX + 4, previousY + 7);

                Assert.True(cam.PosX != previousX && cam.PosY != previousY);
                Assert.Equal(cam.PosX, previousX + 4);
                Assert.Equal(cam.PosY, previousY + 7);
            }
        }
    }
}
