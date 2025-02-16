namespace Bdev.Net.Dns.Records
{
    /// <summary>
    ///     An CNAME Resource Record (RR) (RFC1035 3.3.1)
    /// </summary>
    public class CNameRecord : RecordBase
    {
        /// <summary>
        ///     Constructs an CNAME record by reading bytes from a return message
        /// </summary>
        /// <param name="pointer">A logical pointer to the bytes holding the record</param>
        internal CNameRecord(Pointer pointer)
        {
            Value = pointer.ReadDomain();
        }

        /// <summary>
        ///     Gets the domain value
        /// </summary>
        public string Value { get; }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
