using MallContainer;

namespace MallContainerTests;

public class MallContainerTests : IDisposable
{
    public void Dispose()
    {
        TestA.ResetCount();
        TestB.ResetCount();
    }

    [Fact]
    public void AddSingletonTestOneTypeOf()
    {
        //arrange
        var mallBuilder = new ContainerBuilder();
        mallBuilder.AddSingleton(typeof(TestA));

        var mall = mallBuilder.Build();

        var expectedCountA = 1;

        //act
        var a = (TestA)mall.GetService(typeof(TestA));
        mall.GetService(typeof(TestA));
        mall.GetService(typeof(TestA));

        var actualCountA = a.GetCount();

        //assert
        Assert.Equal(expectedCountA, actualCountA);
    }

    [Fact]
    public void AddSingletonTestTwoTypeOf()
    {
        //arrange
        var mallBuilder = new ContainerBuilder();
        mallBuilder.AddSingleton(typeof(TestA), typeof(TestB));

        var mall = mallBuilder.Build();

        var expectedCountA = 1;

        //act
        var ab = (TestB)mall.GetService(typeof(TestA));
        mall.GetService(typeof(TestA));
        mall.GetService(typeof(TestA));

        var actualCountA = ab.GetCount();

        //assert
        Assert.Equal(expectedCountA, actualCountA);
    }


    [Fact]
    public void AddSingletonTestOneType()
    {
        //arrange
        var mallBuilder = new ContainerBuilder();
        mallBuilder.AddSingleton<TestA>();

        var mall = mallBuilder.Build();

        var expectedCountA = 1;

        //act
        var a = (TestA)mall.GetService(typeof(TestA));
        mall.GetService(typeof(TestA));
        mall.GetService(typeof(TestA));

        var actualCountA = a.GetCount();

        //assert
        Assert.Equal(expectedCountA, actualCountA);
    }

    [Fact]
    public void AddSingletonTestTwoTypes()
    {
        //arrange
        var mallBuilder = new ContainerBuilder();
        mallBuilder.AddSingleton<TestA, TestB>();

        var mall = mallBuilder.Build();

        var expectedCountA = 1;

        //act
        var ab = (TestB)mall.GetService(typeof(TestA));
        mall.GetService(typeof(TestA));
        mall.GetService(typeof(TestA));

        var actualCountA = ab.GetCount();

        //assert
        Assert.Equal(expectedCountA, actualCountA);
    }
}

public class TestA
{
    static int _count;

    public TestA()
    {
        _count++;
    }

    public int GetCount()
    {
        return _count;
    }

    public static void ResetCount() 
    {
        _count = 0;
    }
}

public class TestB : TestA
{
    static int _count;

    public TestB()
    {
        _count++;
    }

    public int GetCount()
    {
        return _count;
    }

    public static void ResetCount()
    {
        _count = 0;
    }
}
