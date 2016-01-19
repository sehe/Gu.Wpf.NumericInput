using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gu.Wpf.NumericInput.Tests.SandBox
{
    using System.Windows;
    using System.Windows.Controls;
    using NUnit.Framework;

    class EventBox
    {
        [Test]
        public void TestName()
        {
            var routedEvent = Control.MouseDoubleClickEvent;
            var gotKeyboardFocusEvent = UIElement.GotKeyboardFocusEvent;
            var handlerType = routedEvent.HandlerType;
        }
    }
}
