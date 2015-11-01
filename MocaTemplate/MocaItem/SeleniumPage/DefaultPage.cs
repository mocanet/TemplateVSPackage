using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using MiYABiS.SeleniumTestAssist;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace $rootnamespace$
{
    class $safeitemname$ : SeleniumAction
    {

        public $safeitemname$(IWebDriver driver, string baseUrl) : base(driver, baseUrl)
        {
            
        }

        public override string MyPageName
        {
            get
            {
                return "Default.aspx";
            }
        }

    }
}
