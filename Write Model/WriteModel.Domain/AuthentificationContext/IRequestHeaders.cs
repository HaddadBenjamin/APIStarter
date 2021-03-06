﻿using System;

namespace WriteModel.Domain.AuthentificationContext
{
    /// <summary>
    /// Contient des informations sur l'appelant qui proviennent des en-têtes de la requête HTTP.
    /// </summary>
    public interface IRequestHeaders
    {
        string ImpersonatedUserEmail { get; set; }
        string UserEmail { get; set; }
        Guid CorrelationId { get; set; }
        string ClientApplication { get; set; }
        string ReadVersion { get; set; }
        string WriteVersion { get; set; }
    }
}