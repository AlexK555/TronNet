using Grpc.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TronNet
{
    public class GrpcChannelClient : IGrpcChannelClient
    {
        private readonly ILogger<GrpcChannelClient> _logger;
        private readonly IOptions<TronNetOptions> _options;

        public GrpcChannelClient(ILogger<GrpcChannelClient> logger, IOptions<TronNetOptions> options)
        {
            _logger = logger;
            _options = options;
        }

        //TODO: not safe
        IList<Channel> _channels = new List<Channel>();

        public Channel GetProtocol() {
            var c = new Channel(_options.Value.Channel.Host, _options.Value.Channel.Port, ChannelCredentials.Insecure);
            _channels.Add(c);
            return c;
        }

        public Channel GetSolidityProtocol() {
            var c = new Channel(_options.Value.SolidityChannel.Host, _options.Value.SolidityChannel.Port, ChannelCredentials.Insecure);
            _channels.Add(c);
            return c;
        }

        public async ValueTask DisposeAsync() {
            var channels = _channels.ToList();
            foreach (var c in channels) {
                await c.ShutdownAsync();
                _channels.Remove(c);
            }
        }
    }

}
