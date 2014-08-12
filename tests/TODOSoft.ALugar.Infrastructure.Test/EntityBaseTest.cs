using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TODOSoft.ALugar.Infrastructure.DomainBase;

namespace TODOSoft.ALugar.Infrastructure.Test
{
    [TestClass]
    public class EntityBaseTest
    {
        [TestMethod]
        public void EqualsValidate_CompareIfTwoEntitiesAreEquals_MustReturnTrue()
        {
            // Arrange
            var key = Guid.NewGuid();
            EntityBase entity1 = new EntityTest(key);
            EntityBase entity2 = new EntityTest(key); 
            
            // Act
            var result = entity1.Equals(entity2);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EqualsValidate_CompareIfTwoEntitiesAreEquals_MustReturnFalse()
        {
            // Arrange            
            EntityBase entity1 = new EntityTest();
            EntityBase entity2 = new EntityTest();

            // Act
            var result = entity1.Equals(entity2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NotEqualsOperatorValidate_CompareIfTwoEntitiesAreNotEquals_MustReturnTrue()
        {
            // Arrange            
            EntityBase entity1 = new EntityTest();
            EntityBase entity2 = new EntityTest();

            // Act
            var result = entity1 != entity2;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NotEqualsOperatorValidate_CompareIfTwoEntitiesAreNotEquals_MustReturnFalse()
        {
            // Arrange            
            var key = new Guid();
            EntityBase entity1 = new EntityTest(key);
            EntityBase entity2 = new EntityTest(key);

            // Act
            var result = entity1 != entity2;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NewKeyValidate_GenerateANewKeyForEntity_MustReturnANewKeyOfGuidType()
        {
            // Arrange                                   
            Guid result;

            // Act
            result = (Guid)EntityBase.NewKey();

            // Assert
            Assert.IsTrue(result != Guid.Empty);
        }
    }

    class EntityTest : EntityBase
    {
        public EntityTest(): base(null)
        {

        }

        public EntityTest(object key) : base(key)
        {
            
        }

        public string Name { get; set; }
    }
}
