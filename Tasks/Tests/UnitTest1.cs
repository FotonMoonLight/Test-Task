using Task_Compression;
using Task_Compression.Classes;
namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Compression()
        {
          
            string check = "aaabbcccdde";

            
            string result = check.Compression();

           
            Assert.Equal("a3b2c3d2e", result);
        }


        [Fact]
        public void Decompression_Test() {

            string check_two = "a3b2c3d2efd4";


            string result = check_two.Decompressions();

            
            Assert.Equal("aaabbcccddefdddd", result);
        }
    }
}
