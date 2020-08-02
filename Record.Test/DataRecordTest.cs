using System;
using Xunit;

namespace Record.Test
{
    public class DataRecordTest
    {
        [Fact]
        public void With_Returns_NewInstance()
        {
            var record = new TestRecord("Pippo");

            var output = record.With(new 
            {
                Name = "Pluto",
            });

            Assert.NotSame(record, output);
        }

        [Fact]
        public void With_SetPrivateSetterProperties()
        {
            var name = "Pluto";
            var utcNow = DateTime.UtcNow;

            var record = new TestRecord("Pippo");

            var output = record.With(new 
            {
                Name = name,
                CreatedAt = utcNow,
            });

            Assert.Equal(name, output.Name);
            Assert.Equal(utcNow, output.CreatedAt);
        }

        [Fact]
        public void With_IgnoreUnsetProperties()
        {
            var record = new TestRecord("Pippo");

            var output = record.With(new 
            {
                Name = "Pluto",
            });

            Assert.Equal(record.Id, output.Id);
            Assert.Equal(record.CreatedAt, output.CreatedAt);
            Assert.NotEqual(record.Name, output.Name);
        }

        [Fact]
        public void With_DeepClone()
        {
            var utcNow = DateTime.UtcNow;

            var record = new TestRecord("Pippo");

            var complexRecord = new TestComplexRecord(
                name: "Pippo", 
                record: record);

            var output = complexRecord.With(new 
            {
                ModifiedAt = DateTime.UtcNow,
            });

            Assert.NotSame(complexRecord.Record, output.Record);
        }

        private class TestRecord
        {
            public TestRecord(string name)
            {
                Id = Guid.NewGuid();
                Name = name;
                CreatedAt = DateTime.UtcNow;
            }

            public Guid Id { get; }
            public string Name { get; }
            public DateTime CreatedAt { get; }
        }

        private class TestComplexRecord
        {
            public TestComplexRecord(string name, bool isEnabled = true,
                TestRecord record = null)
            {
                Id = Guid.NewGuid();
                Name = name;
                Record = record ?? new TestRecord(string.Empty);
                IsEnabled = isEnabled;
                CreatedAt = DateTime.UtcNow;
                ModifiedAt = DateTime.UtcNow;
            }

            public Guid Id { get; }
            public string Name {get;}
            public TestRecord Record {get;}
            public bool IsEnabled {get;}
            public DateTime CreatedAt {get;}
            public DateTime ModifiedAt {get;}
        }
    }
}
