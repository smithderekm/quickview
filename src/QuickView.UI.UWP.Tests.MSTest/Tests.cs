﻿using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using QuickView.UI.UWP.ViewModels;

namespace QuickView.UI.UWP.Tests.MSTest
{
    // TODO WTS: Add appropriate tests
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        // TODO WTS: Add tests for functionality you add to ContentGridViewModel.
        [TestMethod]
        public void TestContentGridViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            var vm = new ContentGridViewModel();
            Assert.IsNotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to MainViewModel.
        [TestMethod]
        public void TestMainViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            var vm = new MainViewModel();
            Assert.IsNotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to SelectSubjectsViewModel.
        [TestMethod]
        public void TestSelectSubjectsViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            var vm = new SelectSubjectsViewModel();
            Assert.IsNotNull(vm);
        }

        // TODO WTS: Add tests for functionality you add to SettingsViewModel.
        [TestMethod]
        public void TestSettingsViewModelCreation()
        {
            // This test is trivial. Add your own tests for the logic you add to the ViewModel.
            var vm = new SettingsViewModel();
            Assert.IsNotNull(vm);
        }
    }
}
