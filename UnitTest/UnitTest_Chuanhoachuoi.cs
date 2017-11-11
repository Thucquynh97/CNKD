using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chiecnonkidieu;
namespace UnitTest
{
    [TestClass]
    public class UnitTest_Chuanhoachuoi
    {
        
        private Chuanhoachuoi chuanhoa = null;
        string name;
        [TestInitialize]
        public void Setup()
        {
            name = "dINH vAN pHU";
            chuanhoa = new Chuanhoachuoi();
        }
        [TestMethod]
        public void Kiemtra_chucnangdinhdangchuoi()
        {
            string kq = "Dinh Van Phu";
            Assert.AreEqual(chuanhoa.btchuanhoa(name), kq);
        }
    }
}
