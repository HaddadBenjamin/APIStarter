using System;
using WriteModel.Domain.AuthentificationContext;

namespace WriteModel.Infrastructure.AuthentificationContext
{
    public class AuthentificationContext : IAuthentificationContext
    {
        public AuthentificationContext(IRequestHeaders requestHeaders, IAuthentificationContextUserProvider userProvider)
        {
            CorrelationId = requestHeaders.CorrelationId;

            if (requestHeaders.ImpersonatedUserEmail != null)
                User = ImpersonatedUser = userProvider.Get(requestHeaders.ImpersonatedUserEmail);

            if (requestHeaders.UserEmail != null)
            {
                User = userProvider.Get(requestHeaders.UserEmail);

                if (ImpersonatedUser is null)
                    ImpersonatedUser = User;
            }
        }

        public AuthentificationContextUser User { get; set; }
        public AuthentificationContextUser ImpersonatedUser { get; set; }
        public Guid CorrelationId { get; set; }

        public bool IsValid() => User != null && ImpersonatedUser != null;
    }
}