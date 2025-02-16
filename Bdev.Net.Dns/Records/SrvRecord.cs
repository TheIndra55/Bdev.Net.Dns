using System;
using System.Collections.Generic;

namespace Bdev.Net.Dns.Records
{
    /// <summary>
    ///     An SRV Resource Record (RR) (RFC2782)
    /// </summary>
    public class SrvRecord : RecordBase, IComparable, IEquatable<SrvRecord>
    {
        /// <summary>
        ///     Constructs an SRV record by reading bytes from a return message
        /// </summary>
        /// <param name="pointer">A logical pointer to the bytes holding the record</param>
        internal SrvRecord(Pointer pointer)
        {
            Priority = pointer.ReadShort();
            Weight = pointer.ReadShort();
            Port = pointer.ReadShort();
            Target = pointer.ReadDomain();
        }

        /// <summary>
        ///     Gets the priority of the record
        /// </summary>
        public int Priority { get; }

        /// <summary>
        ///     Gets the weight of the record
        /// </summary>
        public int Weight { get; }

        /// <summary>
        ///     Gets the service port of the record
        /// </summary>
        public int Port { get; }

        /// <summary>
        ///     Gets the target domain name of the record
        /// </summary>
        public string Target { get; }

        public override string ToString()
        {
            return $"Priority = {Priority}, Weight = {Weight}, Port = {Port}, Target = {Target}";
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            var other = obj as SrvRecord;

            if (Priority == other.Priority) return Weight.CompareTo(other.Weight);
            return Priority.CompareTo(other.Priority);
        }

        public bool Equals(SrvRecord other)
        {
            return other != null &&
                Priority == other.Priority &&
                Weight == other.Weight &&
                Port == other.Port &&
                Target == other.Target;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SrvRecord);
        }

        public override int GetHashCode()
        {
            var hashCode = 1394496566;
            hashCode = hashCode * -1521134295 + Priority.GetHashCode();
            hashCode = hashCode * -1521134295 + Weight.GetHashCode();
            hashCode = hashCode * -1521134295 + Port.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Target);
            return hashCode;
        }

        public static bool operator ==(SrvRecord record1, SrvRecord record2)
        {
            return EqualityComparer<SrvRecord>.Default.Equals(record1, record2);
        }

        public static bool operator !=(SrvRecord record1, SrvRecord record2)
        {
            return !(record1 == record2);
        }

        public static bool operator <(SrvRecord record1, SrvRecord record2)
        {
            if (record1.Priority == record2.Priority) return record1.Weight < record2.Weight;
            return record1.Priority < record2.Priority;
        }

        public static bool operator >(SrvRecord record1, SrvRecord record2)
        {
            if (record1.Priority == record2.Priority) return record1.Weight > record2.Weight;
            return record1.Priority > record2.Priority;
        }

        #endregion
    }
}
