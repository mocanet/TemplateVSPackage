
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiYABiS.SeleniumTestAssist;

namespace $rootnamespace$
{
    [TestClass]
    public class $safeitemname$ : AbstractSeleniumTest
    {

        #region Declare

        private const string _PORT = "80";

        private static string _baseUrl = string.Format("http://localhost:{0}/", _PORT);

        #region Logging For Log4net
        /// <summary>Logging For Log4net</summary>
        readonly log4net.ILog _mylog = log4net.LogManager.GetLogger(String.Empty);
        #endregion
        #endregion

        #region 追加のテスト属性

        /// <summary>
        /// クラスの最初のテストを実行する前にコードを実行するには、ClassInitialize を使用
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            SeleniumInitialize(_baseUrl);
        }

        /// <summary>
        /// クラスのすべてのテストを実行した後にコードを実行するには、ClassCleanup を使用
        /// </summary>
        [ClassCleanup()]
        public static void ClassCleanup()
        {
            SeleniumCleanup();
        }

        /// <summary>
        /// 各テストを実行する前にコードを実行するには、TestInitialize を使用
        /// </summary>
        [TestInitialize()]
        public override void TestInitialize()
        {

        }

        /// <summary>
        /// 各テストを実行した後にコードを実行するには、TestCleanup を使用
        /// </summary>
        [TestCleanup()]
        public override void TestCleanup()
        {
            base.TestCleanup();
        }

        #endregion

        [TestMethod,
         Description("テスト内容"),
         TestCategory("カテゴリ")]
        public void TestMethod1()
        {
        }
    }
}
