using System;
using System.Collections.Generic;
using explore_.net.Models;
using MediatR;

namespace explore_.net.Core
{
    public class Places
    {
        public class Query : IRequest<List<Place>>
        {
        }
    }
}
