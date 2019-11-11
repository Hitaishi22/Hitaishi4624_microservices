using System;
using AutoMapper;

namespace TransactionAPI.Mappers
{
    using Models;
    using Services;
    using TransactionFramework.Domain;
    using TransactionFramework.Extensions;
    using TransactionFramework.Types;

    public class SetIdentityAction : IMappingAction<TransactionModel, AccountTransaction>
    {
        private readonly IIdentityService _identityService;

        public SetIdentityAction(IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public void Process(TransactionModel source, AccountTransaction destination)
        {
            var identity = _identityService.GetIdentity();

            destination.AccountNumber = identity.AccountNumber;
            destination.Amount = new Money(source.Amount, identity.Currency.TryParseEnum<Currency>());
        }

        public void Process(TransactionModel source, AccountTransaction destination, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
