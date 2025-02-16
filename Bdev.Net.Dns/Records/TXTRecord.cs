using System.Text;

namespace Bdev.Net.Dns.Records
{
    /// <summary>
    ///     An TXT Resource Record (RR) (RFC1035 3.3.14)
    /// </summary>
    public class TXTRecord : RecordBase
    {
        /// <summary>
        ///     Constructs an TXT record by reading bytes from a return message
        /// </summary>
        /// <param name="pointer">A logical pointer to the bytes holding the record</param>
        /// <param name="recordLength">The length of the record in bytes</param>
        internal TXTRecord(Pointer pointer, int recordLength)
        {
            var position = pointer.Position;

            var sb = new StringBuilder(recordLength);

            // there can be multiple strings in one TXT record
            // loop until full recordLength is read
            while (pointer.Position - position < recordLength)
            {
                // read the string length
                var length = pointer.ReadByte();
                for (int i = 0; i < length; i++)
                {
                    sb.Append(pointer.ReadChar());
                }
            }

            Value = sb.ToString();
            Length = sb.Length;
        }

        /// <summary>
        ///     Gets the text value
        /// </summary>
        public string Value { get; }

        /// <summary>
        ///     Gets the text length
        /// </summary>
        public int Length { get; }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}