using System;
using System.ComponentModel;
using System.Globalization;
using Stl.ImmutableModel.Indexing;

namespace Stl.ImmutableModel.Internal 
{
    public class NodeLinkTypeConverter : TypeConverter 
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) 
            => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {  
            if (destinationType == typeof(string))
                return ((NodeLink) value).Format();
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) 
        {
            if (value is string s)
                // ReSharper disable once HeapView.BoxingAllocation
                return NodeLink.Parse(s);
            return base.ConvertFrom(context, culture, value);
        }
    }
}
