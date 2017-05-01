﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tyrrrz.Settings.Tests.Mocks;

namespace Tyrrrz.Settings.Tests
{
    [TestClass]
    public class StagerTests
    {
        [TestMethod]
        public void InstantiateTest()
        {
            var stager = new Stager<MockSettingsManager>();

            // Make sure both instances exist and are not the same
            Assert.IsNotNull(stager.Current);
            Assert.IsNotNull(stager.Staging);
            Assert.IsFalse(ReferenceEquals(stager.Current, stager.Staging));
        }

        [TestMethod]
        public void InstantiateWithFactoryTest()
        {
            var stager = new Stager<MockSettingsManager>(
                new MockSettingsManager {Int = 1337},
                new MockSettingsManager {Int = 6969});

            // Make sure both instances exist and are not the same
            Assert.IsNotNull(stager.Current);
            Assert.IsNotNull(stager.Staging);
            Assert.IsFalse(ReferenceEquals(stager.Current, stager.Staging));

            // Check if factory worked
            Assert.AreEqual(1337, stager.Current.Int);
            Assert.AreEqual(6969, stager.Staging.Int);
        }

        [TestMethod]
        public void SaveSyncTest()
        {
            var stager = new Stager<MockSettingsManager>();

            // Should be in sync
            Assert.AreEqual(stager.Staging.Str, stager.Current.Str);

            // Change value
            stager.Staging.Str = "553322";

            // Should not be in sync
            Assert.AreNotEqual(stager.Staging.Str, stager.Current.Str);

            // Save
            stager.Save();

            // Should be in sync
            Assert.AreEqual(stager.Staging.Str, stager.Current.Str);
        }

        [TestMethod]
        public void LoadSyncTest()
        {
            var stager = new Stager<MockSettingsManager>();

            // Save
            stager.Save();

            // Should be in sync
            Assert.AreEqual(stager.Staging.Str, stager.Current.Str);

            // Change value
            stager.Staging.Str = "553322";

            // Should not be in sync
            Assert.AreNotEqual(stager.Staging.Str, stager.Current.Str);

            // Load
            stager.Load();

            // Should be in sync
            Assert.AreEqual(stager.Staging.Str, stager.Current.Str);
        }

        [TestMethod]
        public void RevertStagingSyncTest()
        {
            var stager = new Stager<MockSettingsManager>();

            // Should be in sync
            Assert.AreEqual(stager.Staging.Str, stager.Current.Str);

            // Change value
            stager.Staging.Str = "553322";

            // Should not be in sync
            Assert.AreNotEqual(stager.Staging.Str, stager.Current.Str);

            // Save
            stager.Save();

            // Should be in sync
            Assert.AreEqual(stager.Staging.Str, stager.Current.Str);
        }
    }
}