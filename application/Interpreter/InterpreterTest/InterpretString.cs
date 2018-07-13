using FluentAssertions;
using InterpreterDll;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace InterpreterTest
{
    [TestClass]
    public class InterpretString
    {
        public static string RandomString(int length)
        {
            var random = new Random(DateTime.Now.Second);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        [TestMethod]
        public void InterpretEnv()
        {
            #region arrange
            string textToInterpret = "";
            string textInterpreted = "";
            var var = Environment.GetEnvironmentVariables();
            foreach (var item in var.Keys)
            {
                var randomString = RandomString(10);
                textToInterpret += randomString + "#env:" + item + "#" + Environment.NewLine;
                textInterpreted += randomString + var[item] + Environment.NewLine;
                continue;
            }
            #endregion
            #region act
            var i = new Interpret();
            i.TwoSlashes = false;
            var str = i.InterpretText(textToInterpret);
            #endregion
            #region assert
            Assert.AreEqual(textInterpreted, str);

            #endregion


        }
        [TestMethod]
        public void InterpretDateTime()
        {
            #region arrange
            string textToInterpret = "this is from #now:yyyyMMddHHmmss#";
            #endregion
            #region act

            var i = new Interpret();
            i.TwoSlashes = false;
            var str = i.InterpretText(textToInterpret);
            #endregion
            #region assert
            Console.WriteLine("interpreted: " + str);
            str.Should().Contain($"this is from {DateTime.Now.ToString("yyyyMMdd")}");
            #endregion
        }
        [TestMethod]
        public void InterpretDateTimeUtcNow()
        {
            #region arrange
            string textToInterpret = "this is from #utcnow:yyyyMMddHHmmss#";
            #endregion
            #region act

            var i = new Interpret();
            i.TwoSlashes = false;
            var str = i.InterpretText(textToInterpret);
            #endregion
            #region assert
            Console.WriteLine("interpreted: " + str);
            str.Should().Contain($"this is from {DateTime.UtcNow.ToString("yyyyMMdd")}");
            #endregion
        }
        [TestMethod]
        public void InterpretGuid()
        {
            #region arrange
            string textToInterpret = "#guid:n#";
            #endregion
            #region act

            var i = new Interpret();
            i.TwoSlashes = false;
            var str = i.InterpretText(textToInterpret);
            #endregion
            #region assert
            Console.WriteLine("interpreted: " + str);
            str.Should().HaveLength(32);
            var g = new Guid(str);
            //g should exists without error            
            #endregion
        }
        [TestMethod]
        public void InterpretStaticOneParameter()
        {
            #region arrange
            string textToInterpret = "this is from #static:DateTime.Today#";
            textToInterpret += " and #static:Directory.GetCurrentDirectory()#";

            #endregion
            #region act

            var i = new Interpret();
            i.TwoSlashes = false;
            var str = i.InterpretText(textToInterpret);
            #endregion
            #region assert
            Console.WriteLine("interpreted: " + str);
            str.Should().Be($"this is from {DateTime.Today.ToString()} and {Directory.GetCurrentDirectory()}");
            #endregion
        }
        [TestMethod]
        public void InterpretStaticParameterString()
        {
            #region arrange
            string textToInterpret = "this is @static:System.IO.Path.GetFileNameWithoutExtension(#env:solutionPath#)@";
            Environment.SetEnvironmentVariable("solutionPath", "a.sln");

            #endregion
            #region act

            var i = new Interpret();
            i.TwoSlashes = false;
            var str = i.InterpretText(textToInterpret);
            #endregion
            #region assert

            str.Should().Be($"this is a");
            #endregion
        }

        [TestMethod]
        public void InterpretStaticTwoParameterString()
        {
            #region arrange

            string textToInterpret = "this is @static:Path.GetPathRoot(#static:Directory.GetCurrentDirectory()#)@";


            #endregion
            #region act

            var i = new Interpret();
            i.TwoSlashes = false;
            var str = i.InterpretText(textToInterpret);
            #endregion
            #region assert

            string s = Path.GetPathRoot(Directory.GetCurrentDirectory());
            str.Should().Be($"this is {s}");
            #endregion
        }
        [TestMethod]
        public void InterpretSettingsFile()
        {
            #region arrange
            string textToInterpret = "this is from #file:SqlServerConnectionString#";
            #endregion
            #region act

            var i = new Interpret();
            var str = i.InterpretText(textToInterpret);
            #endregion
            #region assert
            Console.WriteLine("interpreted: " + str);
            Assert.IsFalse(str.Contains("#"), "should be interpreted");
            Assert.IsTrue(str.Contains("this is from"), "should contain first chars");
            //Assert.IsTrue(str.Contains("atabase"),"should contain database");
            Assert.IsTrue(str.Contains("rusted"), "should containt trusted connection");
            #endregion


        }
    }
}
