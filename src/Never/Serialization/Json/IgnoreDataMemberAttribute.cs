using System;

namespace Never.Serialization.Json
{
    /// <summary>
    /// ��ʾ���������л���
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class IgnoreDataMemberAttribute : Attribute
    {
    }
}