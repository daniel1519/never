using System;

namespace Never.Serialization.Json
{
    /// <summary>
    /// json���Ի��ֶ�
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class DataMemberAttribute : Attribute
    {
        /// <summary>
        /// ��Ա����
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataMemberAttribute"/> class.
        /// </summary>
        public DataMemberAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataMemberAttribute"/> class with the specified name.
        /// </summary>
        /// <param name="propertyName">���Ի��ֶ�����</param>
        public DataMemberAttribute(string propertyName)
        {
            Name = propertyName;
        }
    }
}