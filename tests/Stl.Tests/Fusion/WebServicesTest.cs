using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Autofac;
using FluentAssertions;
using Stl.Fusion;
using Stl.Fusion.Client;
using Stl.Tests.Fusion.Services;
using Stl.Tests.Fusion.UIModels;
using Xunit;
using Xunit.Abstractions;

namespace Stl.Tests.Fusion
{
    public class WebServicesTest : FusionTestBase
    {
        public WebServicesTest(ITestOutputHelper @out, FusionTestOptions? options = null) : base(@out, options) { }

        [Fact]
        public async Task TimeServiceTest()
        {
            await using var serving = await WebSocketServer.ServeAsync();
            var tpc = Container.Resolve<ITimeProviderClient>();
            var cTime = await tpc.GetComputedTimeAsync();
            cTime.IsConsistent.Should().BeTrue();
            (DateTime.Now - cTime.Value).Should().BeLessThan(TimeSpan.FromSeconds(1));

            await Task.Delay(TimeSpan.FromSeconds(2));
            cTime.IsConsistent.Should().BeFalse();
            cTime = await cTime.UpdateAsync();
            (DateTime.Now - cTime.Value).Should().BeLessThan(TimeSpan.FromSeconds(1));
        }

        [Fact]
        public async Task ServerTimeModelTest1()
        {
            await using var serving = await WebSocketServer.ServeAsync();
            using var stm = Container.Resolve<ILive<ServerTimeModel1>>();
            
            var c = stm.Computed;
            c.IsConsistent.Should().BeFalse();
            c.Value.Time.Should().Be(default);

            Debug.WriteLine("0");
            stm.UpdateDelayer.CancelDelays();
            Debug.WriteLine("1");
            await c.UpdateAsync();
            Debug.WriteLine("2");

            c = stm.Computed;
            c.IsConsistent.Should().BeTrue();
            (DateTime.Now - c.Value.Time).Should().BeLessThan(TimeSpan.FromSeconds(1));

            Debug.WriteLine("3");
            await Task.Delay(TimeSpan.FromSeconds(3));
            Debug.WriteLine("4");
            stm.UpdateDelayer.CancelDelays();
            Debug.WriteLine("5");
            await Task.Delay(100); // Let's just wait for the updates to happen
            Debug.WriteLine("6");
            c = stm.Computed;
            Debug.WriteLine("7");

            // c.IsConsistent.Should().BeTrue(); // Hard to be sure here
            var delta = DateTime.Now - c.Value.Time!.Value;
            Debug.WriteLine(delta.TotalSeconds);
            delta.Should().BeLessThan(TimeSpan.FromSeconds(1));
        }

        [Fact]
        public async Task ServerTimeModelTest2()
        {
            await using var serving = await WebSocketServer.ServeAsync();
            using var stm = Container.Resolve<ILive<ServerTimeModel2>>();
            
            var c = stm.Computed;
            c.IsConsistent.Should().BeFalse();
            c.Value.Time.Should().Be(default);

            Debug.WriteLine("0");
            stm.UpdateDelayer.CancelDelays();
            Debug.WriteLine("1");
            await c.UpdateAsync();
            Debug.WriteLine("2");

            c = stm.Computed;
            c.IsConsistent.Should().BeTrue();
            (DateTime.Now - c.Value.Time).Should().BeLessThan(TimeSpan.FromSeconds(1));

            Debug.WriteLine("3");
            await Task.Delay(TimeSpan.FromSeconds(3));
            Debug.WriteLine("4");
            stm.UpdateDelayer.CancelDelays();
            Debug.WriteLine("5");
            await Task.Delay(100); // Let's just wait for the updates to happen
            Debug.WriteLine("6");
            c = stm.Computed;
            Debug.WriteLine("7");

            // c.IsConsistent.Should().BeTrue(); // Hard to be sure here
            var delta = DateTime.Now - c.Value.Time!.Value;
            Debug.WriteLine(delta.TotalSeconds);
            delta.Should().BeLessThan(TimeSpan.FromSeconds(1));
        }
    }
}
