﻿using System.Collections.Generic;
using App.Metrics.Apdex;
using App.Metrics.Core.Extensions;
using App.Metrics.Reporting;
using FluentAssertions;
using Xunit;

namespace App.Metrics.Facts.Core.Extensions
{
    // ReSharper disable InconsistentNaming
    public class Apdex_MetricValueExtensionsTests
        // ReSharper restore InconsistentNaming
    {
        private static readonly MetricValueDataKeys DataKeys = new MetricValueDataKeys();

        [Fact]
        public void apdex_can_use_custom_data_keys_and_should_provide_corresponding_values()
        {
            // Arrange
            var value = new ApdexValue(1, 2, 3, 4, 5);
            var data = new Dictionary<string, object>();
            var dataKeys = new MetricValueDataKeys(
                apdex: new Dictionary<ApdexValueDataKeys, string>
                       {
                           { ApdexValueDataKeys.Samples, "size_of_sample" }
                       });

            // Act
            value.AddApdexValues(data, dataKeys.Apdex);

            // Assert
            data.ContainsKey(DataKeys.Apdex[ApdexValueDataKeys.Samples]).Should().BeFalse();
            data["size_of_sample"].Should().Be(5);
        }

        [Fact]
        public void apdex_default_data_keys_should_provide_corresponding_values()
        {
            // Arrange
            var value = new ApdexValue(1, 2, 3, 4, 5);
            var data = new Dictionary<string, object>();

            // Act
            value.AddApdexValues(data, DataKeys.Apdex);

            // Assert
            data[DataKeys.Apdex[ApdexValueDataKeys.Score]].Should().Be(1.0);
            data[DataKeys.Apdex[ApdexValueDataKeys.Satisfied]].Should().Be(2);
            data[DataKeys.Apdex[ApdexValueDataKeys.Tolerating]].Should().Be(3);
            data[DataKeys.Apdex[ApdexValueDataKeys.Frustrating]].Should().Be(4);
            data[DataKeys.Apdex[ApdexValueDataKeys.Samples]].Should().Be(5);
        }
    }
}