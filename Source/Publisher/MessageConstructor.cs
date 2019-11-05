using PublishSubscribeContract;
using System.Security.Cryptography;
using System.Text;

namespace Publisher
{
    public interface IMessageConstructor
    {
        Message Construct(string input);
    }

    public class MessageConstructor : IMessageConstructor
    {
        public Message Construct(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            return new Message
            {
                Value = input,
                SHA256 = ComputeHashSHA256(input)
            };
        }

        private static string ComputeHashSHA256(string input)
        {
            using (var hash = SHA256.Create())
            {
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
