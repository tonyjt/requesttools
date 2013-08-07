using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using requesttools;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string url = "locahost:2013";
            List<KeyValuePair<string,string>> paras = new  List<KeyValuePair<string,string>>();
            paras.Add(new KeyValuePair<string,string>("test","2245 0525+1"));

            string str = HttpRequestManager.SendRequest(url, paras,false);

            Assert.AreEqual("", str);
        }
    }
}
