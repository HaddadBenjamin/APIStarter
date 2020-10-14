using System;
using APIStarter.Domain.AuthentificationContext;

namespace APIStarter.Infrastructure.AuthentificationContext
{
    public class AuthentificationContext : IAuthentificationContext
    {
        public AuthentificationContext(IRequestContext requestContext, IAuthentificationContextUserProvider userProvider)
        {
            CorrelationId = requestContext.CorrelationId;

            if (requestContext.ImpersonatedUserEmail != null)
                User = ImpersonatedUser = userProvider.Get(requestContext.ImpersonatedUserEmail);

            if (requestContext.UserEmail != null)
            {
                User = userProvider.Get(requestContext.UserEmail);

                if (ImpersonatedUser is null)
                    ImpersonatedUser = User;
            }
        }

        public AuthentificationContextUser User { get; set; }
        public AuthentificationContextUser ImpersonatedUser { get; set; }
        public Guid CorrelationId { get; set; }
    }
}