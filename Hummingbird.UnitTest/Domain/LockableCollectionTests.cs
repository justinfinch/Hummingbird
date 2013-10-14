using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using Hummingbird.Domain;

namespace Hummingbird.UnitTest.Domain
{
    [TestFixture]
    public class LockableCollectionTests
    {
        [Test]
        public void Cant_Add_To_Locked_Collection_Without_Key()
        {
            //Arrange
            var lockableCollection = new LockableCollection<int>();
            lockableCollection.Add(1);
            var key = lockableCollection.Lock();

            //Act
            Assert.Throws<InvalidOperationException>(() =>  lockableCollection.Add(2));

            //Assert
            Assert.AreEqual(lockableCollection.Count, 1);
        }

        [Test]
        public void Can_Add_To_UnLocked_Collection_Without_Key()
        {
            //Arrange
            var lockableCollection = new LockableCollection<int>();
            lockableCollection.Add(1);

            //Act
            lockableCollection.Add(2);

            //Assert
            Assert.AreEqual(lockableCollection.Count, 2);
        }
    }
}
