﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public interface IColumnCharacteristics : ITableModeler
    {
        Type DataType { get; }

        IColumnDefinitionResult AsPrimaryKey();

        IColumnDefinitionResult WithIndex();

        IColumnDefinitionResult WithIndex(string name);

        IColumnDefinitionResult NotNull();

        IColumnDefinitionResult IsUnique();

        IColumnDefinitionResult WithDefaultValue(object value);

        IColumnDefinitionResult WithDefaultValue<TDataType>(TDataType value);

        //IColumnDefinitionResult WithTrigger(string name);
    }
}
