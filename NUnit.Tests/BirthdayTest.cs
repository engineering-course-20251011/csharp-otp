using NUnit.Framework;
using csharp_otp;
using Moq;
using System;

namespace unit_test
{
    [TestFixture]
    class BirthdayTest
    {

        [Test]
        public void is_birthday()
        {
            Birthday birthday = new Birthday();

            Assert.IsTrue(birthday.IsBirthday());
        }

    }
}
