using Ardalis.GuardClauses;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;

namespace GuardSampleArdalis
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchmarkClass>();
            Console.ReadLine();
        }
    }
    public class SampleCtorWithNullAbleParamClassGuard
    {
        private readonly int? sampleParamInt;
        private readonly ISampleInterface sampleParamInterface;

        public SampleCtorWithNullAbleParamClassGuard(int? sampleParamInt)
        {
            if (!sampleParamInt.HasValue)
                throw new ArgumentNullException(nameof(sampleParamInt));
            this.sampleParamInt = sampleParamInt;
        }
        public SampleCtorWithNullAbleParamClassGuard(ISampleInterface sampleParamInterface)
        {
            if (sampleParamInterface == null)
                throw new ArgumentNullException(nameof(sampleParamInt));
            this.sampleParamInterface = sampleParamInterface;
        }
    }

    public class SampleCtorWithNullAbleParamClassNoGuard
    {
        private readonly int? sampleParamInt;
        private readonly ISampleInterface sampleParamInterface;

        public SampleCtorWithNullAbleParamClassNoGuard(int? sampleParamInt)
        {
            this.sampleParamInt = Guard.Against.Default(sampleParamInt, nameof(sampleParamInt));
        }
        public SampleCtorWithNullAbleParamClassNoGuard(ISampleInterface sampleParamInterface)
        {
            this.sampleParamInterface = Guard.Against.Default(sampleParamInterface, nameof(sampleParamInterface));
        }
    }

    public interface ISampleInterface
    {

    }
    public class SampleImplementation : ISampleInterface
    {

    }
    [MemoryDiagnoser]
    public class BenchmarkClass
    {
        [Benchmark]
        public void WithGuardIntNull()
        {
            try
            {
                new SampleCtorWithNullAbleParamClassGuard((int?)null);
            }
            catch { }
        }
        [Benchmark]
        public void WithGuardIntNotNull()
        {
            new SampleCtorWithNullAbleParamClassGuard(0);
        }
        [Benchmark]
        public void WithGuardServiceNull()
        {
            try
            {
                new SampleCtorWithNullAbleParamClassGuard((ISampleInterface)null);
            }
            catch { }
        }
        [Benchmark]
        public void WithGuardServiceNotNull()
        {
            new SampleCtorWithNullAbleParamClassGuard(new SampleImplementation());
        }


        [Benchmark]
        public void WithNoGuardIntNull()
        {
            try
            {
                new SampleCtorWithNullAbleParamClassNoGuard((int?)null);
            }
            catch { }
        }
        [Benchmark]
        public void WithNoGuardIntNotNull()
        {
            new SampleCtorWithNullAbleParamClassNoGuard(0);
        }
        [Benchmark]
        public void WithNoGuardServiceNull()
        {
            try
            {
                new SampleCtorWithNullAbleParamClassNoGuard((ISampleInterface)null);
            }
            catch { }
        }
        [Benchmark]
        public void WithNoGuardServiceNotNull()
        {
            new SampleCtorWithNullAbleParamClassNoGuard(new SampleImplementation());
        }
    }
}
